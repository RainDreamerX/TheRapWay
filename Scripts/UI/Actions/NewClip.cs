using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Enums;
using Assets.Scripts.Extentions;
using Assets.Scripts.Managers;
using Assets.Scripts.Models;
using Assets.Scripts.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Actions {
    /// <summary>
    /// Съемка клипа на трэк
    /// </summary>
    public class NewClip : BaseAction {
        public Text TrackTheme;
        public Text TrackStyle;
        public Text SuccessGradeText;
        public Text NoTrack;
        public Text Duration;
        public Text Price;
        public GameObject FeatInfo;
        public Toggle Screenwritter;
        public Toggle Operator;
        public Toggle Producer;
        public Toggle SoundProducer;
        public Button StartButton;

        private NewClipModel clip;
        private int lastTrackId = -1;
        private int duration;
        private int price;

        /// <summary>
        /// Инициализация дочернего компонента
        /// </summary>
        public override void ChildAwake() {
            Screenwritter.onValueChanged.AddListener(e => OnSettingChange());
            Operator.onValueChanged.AddListener(e => OnSettingChange());
            Producer.onValueChanged.AddListener(e => OnSettingChange());
            SoundProducer.onValueChanged.AddListener(e => OnSettingChange());
            StartButton.onClick.AddListener(CreateNewClip);
        }

        /// <summary>
        /// Открывает страницу
        /// </summary>
        public override void OpenPage() {
            var lastTrack = PlayerManager.GetInfo().LastTrack;
            if (lastTrack != null) {
                TriggerInfoVisible(true);
                ShowTrackInfo(lastTrack);
            }
            else {
                TriggerInfoVisible(false);
            }
            CalculateDurationAndPrice();
            gameObject.SetActive(true);
        }

        /// <summary>
        /// Выводит информацию о последнем треке
        /// </summary>
        private void ShowTrackInfo(NewTrackModel lastTrack) {
            TrackTheme.text = $"Тематика: {lastTrack.Theme.GetDescription()}";
            TrackStyle.text = $"Стиль: {lastTrack.Style.GetDescription()}";
            SuccessGradeText.text = $"Успешность: {lastTrack.Grade.GetDescription()}";
            if (lastTrack.Feat != null) {
                FeatInfo.gameObject.SetActive(true);
                FeatInfo.GetComponentInChildren<Text>().text = $"Совместно с {lastTrack.Feat.Name}";
            }
            else {
                FeatInfo.gameObject.SetActive(false);
            }
        }

        /// <summary>
        /// Отображает или скрывает блок информации о треке
        /// </summary>
        private void TriggerInfoVisible(bool hasTrack) {
            TrackTheme.gameObject.SetActive(hasTrack);
            TrackStyle.gameObject.SetActive(hasTrack);
            SuccessGradeText.gameObject.SetActive(hasTrack);
            FeatInfo.gameObject.SetActive(hasTrack);
            NoTrack.gameObject.SetActive(!hasTrack);
        }

        /// <summary>
        /// Собирает модель клипа
        /// </summary>
        protected override void ParseActionModel() {
            if (clip == null) clip = new NewClipModel();
            clip.Track = PlayerManager.GetInfo().LastTrack;
            clip.HasScreenwritter = Screenwritter.isOn;
            clip.HasOperator = Operator.isOn;
            clip.HasProducer = Producer.isOn;
            clip.HasSoundProducer = SoundProducer.isOn;
        }

        /// <summary>
        /// Рассчитывает длительность и стоимость
        /// </summary>
        protected override void CalculateDurationAndPrice() {
            var settingCosts = new List<SettingCost> {GetCost(Screenwritter.isOn), GetCost(Producer.isOn), GetCost(Operator.isOn), GetCost(SoundProducer.isOn)};
            duration = settingCosts.Sum(e => e.Duration);
            price = PlayerManager.GetFansPercentValue() / 5 * settingCosts.Sum(e => e.PricePercent);
            Duration.text = $"Кол-во дней: {duration}д";
            Price.text = price > 0 ? $"Стоимость: {NumberFormatter.FormatValue(price)}" : string.Empty;
        }

        /// <summary>
        /// Возвращает стоимость одной настройки
        /// </summary>
        private SettingCost GetCost(bool isSelected) {
            return new SettingCost {
                Duration = isSelected ? 1 : 5,
                PricePercent = isSelected ? 2 : 0
            };
        }

        /// <summary>
        /// Запускает съемку клипа
        /// </summary>
        private void CreateNewClip() {
            if (!ConditionsCorrect()) return;
            PlayerManager.SpendMoney(price);
            StatsManager.UpdateStats();
            gameObject.SetActive(false);
            ActionProgressManager.StartAction(duration, FinishClip);
            gameObject.GetComponentInParent<ActionsMenu>().TriggerChildVisible();
        }

        /// <summary>
        /// Проверяет корректность условия создания клипа
        /// </summary>
        private bool ConditionsCorrect() {
            if (PlayerManager.GetInfo().LastTrack == null) {
                AlertManager.ShowMessage("Вы еще не записали ни одного трэка");
                return false;
            }
            if (clip != null && clip.Track.Id == lastTrackId) {
                AlertManager.ShowMessage("Вы уже сняли клип на этот трэк");
                return false;
            }
            if (!PlayerManager.EnoughMoney(price)) {
                AlertManager.ShowMessage("У вас недостаточно денег");
                return false;
            }
            return true;
        }

        /// <summary>
        /// Обработчик завершения съемок клипа
        /// </summary>
        private void FinishClip() {
            ParseActionModel();
            var grade = ClipSuccessAnalyzer.AnalyzeClip(clip);
            var result = GetClipResult(grade);
            lastTrackId = clip.Track.Id;
            var info = PlayerManager.GetInfo();
            info.Fans += result.FansIncrease;
            info.Money += result.Income;
            ActionResult.Show(result);
        }

        /// <summary>
        /// Возвращает результат съемок
        /// </summary>
        private ActionResultModel GetClipResult(SuccessGrade grade) {
            var gradePercent = GradePercentManager.GetRewardPercent(grade, ActionType.NewClip);
            var fansPercentValue = PlayerManager.GetFansPercentValue();
            var featBonus = clip.Track.Feat?.Fans / 150 ?? 0;
            return new ActionResultModel {
                Action = ActionType.NewClip,
                FansIncrease = CalculateStatIncrease(gradePercent, fansPercentValue, 0.1f) + featBonus,
                Income = CalculateStatIncrease(gradePercent, fansPercentValue, 0.2f) * Random.Range(4, 7),
                Popularity = CalculateStatIncrease(gradePercent, fansPercentValue, 0.1f) * Random.Range(2, 5),
                Grade = grade
            };
        }
    }
}
