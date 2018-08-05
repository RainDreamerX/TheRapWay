using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Enums;
using Assets.Scripts.Extentions;
using Assets.Scripts.Managers;
using Assets.Scripts.Models;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Actions {
    /// <summary>
    /// Участие в версусах
    /// </summary>
    public class VersusBattle : BaseAction {
        private const int DEFAULT_DURATION = 5;
        private const int BATTLES_DELAY = 30;

        public Dropdown RivalDropdown;
        public Dropdown TextSourceDropdown;
        public Dropdown StrategyDropdown;
        public Toggle CheckSocialToggle;
        public Text Duration;
        public Text Price;
        public Button StartButton;

        [Header("Победный экран")]
        public Image BattleWinImage;
        public Button BattleWinButton;

        private VersusBattleModel battleModel;
        private int duration;
        private int price;
        private int lastBattleDay;
        private int lastOfferDay;

        /// <summary>
        /// Инициализация дочернего компонента
        /// </summary>
        public override void ChildAwake() {
            RivalDropdown.onValueChanged.AddListener(e => OnSettingChange());
            TextSourceDropdown.onValueChanged.AddListener(e => OnSettingChange());
            StrategyDropdown.onValueChanged.AddListener(e => OnSettingChange());
            CheckSocialToggle.onValueChanged.AddListener(e => OnSettingChange());
            StartButton.onClick.AddListener(StartVersus);
            BattleWinButton.onClick.AddListener(() => BattleWinImage.gameObject.SetActive(true));
            BattleWinImage.gameObject.SetActive(false);
        }

        /// <summary>
        /// Открывает страницу
        /// </summary>
        public override void OpenPage() {
            var playerSkills = PlayerManager.GetSkills();
            FillDrowdown(RivalDropdown, RappersManager.GetRappers().Select(e => e.Name).ToList());
            FillDrowdown(TextSourceDropdown, playerSkills.TextSourses.Select(e => e.GetDescription()).ToList());
            FillDrowdown(StrategyDropdown, playerSkills.BattleStrategies.Select(e => e.GetDescription()).ToList());
            CheckSocialToggle.gameObject.SetActive(playerSkills.CanCheckSocials);
            OnSettingChange();
            gameObject.SetActive(true);
        }

        /// <summary>
        /// Заполняет выпадающий список
        /// </summary>
        private static void FillDrowdown(Dropdown dropdown, List<string> values) {
            dropdown.ClearOptions();
            dropdown.AddOptions(values);
        }

        /// <summary>
        /// Обновляет данные баттла
        /// </summary>
        protected override void ParseActionModel() {
            if (battleModel == null) battleModel = new VersusBattleModel();
            battleModel.Rival = RappersManager.GetByName(RivalDropdown.captionText.text);
            battleModel.TextSourse = EnumExt.GetFromDescription<TextSourse>(TextSourceDropdown.captionText.text);
            battleModel.Strategy = EnumExt.GetFromDescription<BattleStrategy>(StrategyDropdown.captionText.text);
            battleModel.CheckSocials = CheckSocialToggle.isOn;
        }

        /// <summary>
        /// Рассчитывает длительность и стоимость
        /// </summary>
        protected override void CalculateDurationAndPrice() {
            var textCost = NewTrack.TextCosts[battleModel.TextSourse];
            duration = DEFAULT_DURATION;
            duration += textCost.Duration;
            duration += battleModel.CheckSocials ? 5 : 0;
            price = PlayerManager.GetFansPercentValue() / 4 * textCost.PricePercent;
            Duration.text = $"Кол-во дней: {duration}";
            Price.text = $"Стоимость: {price}";
        }

        /// <summary>
        /// Запускает подготовку к версусу
        /// </summary>
        private void StartVersus() {
            ParseActionModel();
            if (lastBattleDay != 0 && DaysManager.CurrentDay - lastBattleDay < BATTLES_DELAY) {
                AlertManager.ShowMessage($"Нельзя участвовать в батлах чаще, чем раз в {BATTLES_DELAY} дней");
                return;
            }
            if (!BattleConditionsCorrect()) return;
            if (!PlayerManager.EnoughMoney(price)) {
                AlertManager.ShowMessage("У вас недостаточно денег");
                return;
            }
            PlayerManager.SpendMoney(price);
            StatsManager.UpdateStats();
            gameObject.SetActive(false);
            ActionProgressManager.StartAction(duration, ActionType.Battle, FinishVersus);
            gameObject.GetComponentInParent<ActionsMenu>().TriggerChildVisible();
        }

        /// <summary>
        /// Проверяет, можно ли запустить батл
        /// </summary>
        private bool BattleConditionsCorrect() {
            if (lastOfferDay == DaysManager.CurrentDay) return false;
            if (RappersManager.IsAgree(battleModel.Rival, PlayerManager.GetInfo().Fans)) {
                AlertManager.ShowMessage($"{battleModel.Rival.Name} согласился на батл");
                return true;
            }
            AlertManager.ShowMessage($"{battleModel.Rival.Name} отказался от батла");
            lastOfferDay = DaysManager.CurrentDay;
            return false;
        }

        /// <summary>
        /// Обработчик завершения батла
        /// </summary>
        private void FinishVersus() {
            lastBattleDay = DaysManager.CurrentDay;
            var grade = BattleSuccessAnalyzer.AnalyzeBattle(battleModel);
            ProcessWinScreen(grade);
            var result = GetBattleResult(grade);
            var playerInfo = PlayerManager.GetInfo();
            playerInfo.Money += result.Income;
            if (result.FansIncrease < 0 && playerInfo.Fans < Math.Abs(result.FansIncrease)) {
                result.FansIncrease = 0;
            }
            playerInfo.Fans += result.FansIncrease;
            ActionResult.Show(result);
        }

        /// <summary>
        /// Обработчик победной заставки на батле
        /// </summary>
        private void ProcessWinScreen(SuccessGrade grade) {
            var playerInfo = PlayerManager.GetInfo();
            if (!playerInfo.BattleScreenWin && battleModel.Rival.Name == "Oxxxymiron" && grade > SuccessGrade.Middle) {
                BattleWinImage.GetComponentsInChildren<Text>().First(e => e.name == "Content").text = 
                    "Это историческая х@#%я! Вы одержали победу над одним из лучших батл МС и присоединились к касте элиты батл рэпа. " +
                    "Многие до сих пор не могут поверить, что это произошло!";
                BattleWinImage.gameObject.SetActive(true);
                playerInfo.BattleScreenWin = true;
            }
        }

        /// <summary>
        /// Возвращает результат съемок
        /// </summary>
        private ActionResultModel GetBattleResult(SuccessGrade grade) {
            var gradePercent = GradePercentManager.GetRewardPercent(grade, ActionType.Battle);
            var fansPercentValue = PlayerManager.GetFansPercentValue();
            var income = CalculateStatIncrease(gradePercent, fansPercentValue, 0.1f);
            var rivalFansBonus = battleModel.Rival.Fans * 5;
            return new ActionResultModel {
                Action = ActionType.Battle,
                FansIncrease = CalculateStatIncrease(gradePercent, fansPercentValue, 0.2f) / 3,
                Income = income > 0 ? income : 0, 
                Popularity = Math.Abs(CalculateStatIncrease(gradePercent, fansPercentValue, 0.1f) * 5 + rivalFansBonus),
                Grade = grade
            };
        }
    }
}
