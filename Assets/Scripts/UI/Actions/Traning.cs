using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Enums;
using Assets.Scripts.Extentions;
using Assets.Scripts.Managers;
using Assets.Scripts.Models;
using UnityEngine;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button;

namespace Assets.Scripts.UI.Actions {
    /// <summary>
    /// Улучшение навыков
    /// </summary>
    public class Traning : BaseAction {
        private const int TRANING_DURATION = 20;
        private const int MAX_SKILL_LEVEL = 10;

        public GameObject TrackThemes;
        public GameObject TrackStyles;
        public GameObject BattleStrategies;
        public GameObject Flow;
        public GameObject Vocabulary;
        public GameObject BitMaking;

        /// <summary>
        /// Инициализация дочернего компонента
        /// </summary>
        public override void ChildAwake() {
            TrackThemes.GetComponentInChildren<Button>().onClick.AddListener(() => OnDropdownLearn<TrackTheme>(TrackThemes, NewThemeFinish));
            TrackStyles.GetComponentInChildren<Button>().onClick.AddListener(() => OnDropdownLearn<TrackStyle>(TrackStyles, NewStyleFinish));
            BattleStrategies.GetComponentInChildren<Button>().onClick.AddListener(() => OnDropdownLearn<BattleStrategy>(BattleStrategies, NewBattleStrategyFinish));
            Flow.GetComponentInChildren<Button>().onClick.AddListener(() => OnSkillLearn<object>(e => OnFlowUpdate()));
            Vocabulary.GetComponentInChildren<Button>().onClick.AddListener(() => OnSkillLearn<object>(e => OnVocabularyUpdate()));
            BitMaking.GetComponentInChildren<Button>().onClick.AddListener(() => OnSkillLearn<object>(e => OnBitMakingUpdate()));
        }

        /// <summary>
        /// Открывает страницу
        /// </summary>
        public override void OpenPage() {
            var playerSkills = PlayerManager.GetSkills();
            FillDropDown(TrackThemes, playerSkills.TrackThemes);
            FillDropDown(TrackStyles, playerSkills.TrackStyles);
            FillDropDown(BattleStrategies, playerSkills.BattleStrategies);
            ShowSkill(Flow, playerSkills.Flow, $"Флоу (тек. ур. {playerSkills.Flow}):");
            ShowSkill(Vocabulary, playerSkills.Vocabulary, $"Словарный запас (тек. ур. {playerSkills.Vocabulary}):");
            ShowSkill(BitMaking, playerSkills.BitMaking, $"Битмейкинг (тек. ур. {playerSkills.BitMaking}):");

            gameObject.SetActive(true);
        }

        /// <summary>
        /// Отобращает информацию о скилле
        /// </summary>
        private static void ShowSkill(GameObject component, int skillLevel, string title) {
            var labels = component.GetComponentsInChildren<Text>(true);
            if (skillLevel == MAX_SKILL_LEVEL) {
                component.GetComponentInChildren<Button>(true).gameObject.SetActive(false);
                labels.First(e => e.name == "Done").gameObject.SetActive(true);
            }
            labels.First(e => e.name == "Title").text = title;
        }

        /// <summary>
        /// Заполнить выпадающий список значениями
        /// </summary>
        private static void FillDropDown<T>(GameObject component, ICollection<T> playerSkills) {
            var dropdown = component.GetComponentInChildren<Dropdown>();
            var toLearn = new List<string>();
            var values = Enum.GetValues(typeof(T)).OfType<T>().ToList();
            foreach (var value in values) {
                if (!playerSkills.Contains(value)) {
                    toLearn.Add((value as Enum).GetDescription());
                }
            }
            if (toLearn.Count == 0) {
                toLearn.Add("Все изучено");
                component.GetComponentInChildren<Button>(true).gameObject.SetActive(false);
            }
            dropdown.ClearOptions();
            dropdown.AddOptions(toLearn);
        }

        /// <summary>
        /// Обработчик изучения новой тематики или стиля
        /// </summary>
        private void OnDropdownLearn<T>(GameObject component, Action<T> action) {
            var dropdown = component.GetComponentInChildren<Dropdown>();
            var selectedValue = dropdown.captionText.text;
            var value = EnumExt.GetFromDescription<T>(selectedValue);
            OnSkillLearn(action, value);
        }

        /// <summary>
        /// Обработчик завершения изучения новой тематики
        /// </summary>
        private void NewThemeFinish(TrackTheme theme) {
            PlayerManager.GetSkills().TrackThemes.Add(theme);
            ShowActionResult($"Изучена новая тематика: {theme.GetDescription()}");
        }

        /// <summary>
        /// Обработчик завершения изучения нового стиля
        /// </summary>
        private void NewStyleFinish(TrackStyle style) {
            PlayerManager.GetSkills().TrackStyles.Add(style);
            ShowActionResult($"Изучен новый стиль: {style.GetDescription()}");
        }

        /// <summary>
        /// Обработчик завершения изучения нового стиля
        /// </summary>
        private void NewBattleStrategyFinish(BattleStrategy strategy) {
            PlayerManager.GetSkills().BattleStrategies.Add(strategy);
            ShowActionResult($"Изучена новая стратегия: {strategy.GetDescription()}");
        }

        /// <summary>
        /// Увеличение навыка
        /// </summary>
        private void OnSkillLearn<T>(Action<T> action, T value = default(T)) {
            gameObject.SetActive(false);
            ActionProgressManager.StartAction(TRANING_DURATION, ActionType.Traning, () => action(value));
            gameObject.GetComponentInParent<ActionsMenu>().TriggerChildVisible();
        }

        /// <summary>
        /// Завершение увеличения навыка флоу
        /// </summary>
        private void OnFlowUpdate() {
            var playerSkills = PlayerManager.GetSkills();
            playerSkills.Flow += 1;
            ShowActionResult($"Флоу увеличен до {playerSkills.Flow} ур.");
        }

        /// <summary>
        /// Завершение увеличения навыка флоу
        /// </summary>
        private void OnVocabularyUpdate() {
            var playerSkills = PlayerManager.GetSkills();
            playerSkills.Vocabulary += 1;
            ShowActionResult($"Словарный запас увеличен до {playerSkills.Vocabulary} ур.");
        }

        /// <summary>
        /// Завершение увеличения навыка флоу
        /// </summary>
        private void OnBitMakingUpdate() {
            var playerSkills = PlayerManager.GetSkills();
            playerSkills.BitMaking += 1;
            if (playerSkills.BitMaking == 1) {
                playerSkills.BitSources.Add(BitSource.Self);
                AlertManager.ShowMessage("Поздравляем! Теперь Вы можете самостоятельно создавать биты!");
            }
            ShowActionResult($"Битмэйкинг увеличен до {playerSkills.BitMaking} ур.");
        }

        /// <summary>
        /// Отобразить окно результата
        /// </summary>
        private void ShowActionResult(string message) {
            ActionResult.Show(new ActionResultModel {
                Action = ActionType.Traning,
                Traning = message
            });
        }

        /// <summary>
        /// Игнорируем
        /// </summary>
        protected override void ParseActionModel() { }
        protected override void CalculateDurationAndPrice() { }
    }
}