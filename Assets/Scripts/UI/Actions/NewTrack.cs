using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Enums;
using Assets.Scripts.Extentions;
using Assets.Scripts.Managers;
using Assets.Scripts.Models;
using Assets.Scripts.Utils;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Assets.Scripts.UI.Actions {
    /// <summary>
    /// Создание нового трэка
    /// </summary>
    public class NewTrack : BaseAction {
        private const string NO_FEAT = "Не выбран";

        public InputField TrackName;
        public Dropdown TrackTheme;
        public Dropdown TrackStyle;
        public Dropdown Feat;
        public Dropdown TrackText;
        public Dropdown TrackBit;
        public Toggle AutotuneToggle;
        public Text DurationText;
        public Text PriceText;
        public Button StartButton;

        private NewTrackModel _track;
        private int _duration = 5;
        private int _price;
        private bool _alreadyFeatOffer;

        internal static readonly Dictionary<TextSourse, SettingCost> TextCosts = new Dictionary<TextSourse, SettingCost> {
            {TextSourse.Self, new SettingCost {Duration = 5, PricePercent = 0}},
            {TextSourse.Ghostwriter, new SettingCost {Duration = 2, PricePercent = 2}}
        };

        private readonly Dictionary<BitSource, SettingCost> _bitCosts = new Dictionary<BitSource, SettingCost> {
            {BitSource.Free, new SettingCost {Duration = 0, PricePercent = 0}},
            {BitSource.Self, new SettingCost {Duration = 5, PricePercent = 0}},
            {BitSource.Bitmaker, new SettingCost {Duration = 3, PricePercent = 4}}
        };

        /// <summary>
        /// Инициализация дочернего компонента
        /// </summary>
        public override void ChildAwake() {
            TrackTheme.onValueChanged.AddListener(e => OnSettingChange());
            TrackStyle.onValueChanged.AddListener(e => OnSettingChange());
            Feat.onValueChanged.AddListener(e => OnSettingChange());
            TrackText.onValueChanged.AddListener(e => OnSettingChange());
            TrackBit.onValueChanged.AddListener(e => OnSettingChange());
            AutotuneToggle.onValueChanged.AddListener(e => OnSettingChange());
            StartButton.onClick.AddListener(CreateNewTrack);
        }

        /// <summary>
        /// Открыть страницу
        /// </summary>
        public override void OpenPage() {
            var playerSkills = PlayerManager.GetSkills();
            FillDropdown(TrackTheme, playerSkills.TrackThemes);
            FillDropdown(TrackStyle, playerSkills.TrackStyles);
            FillDropdown(TrackText, playerSkills.TextSourses);
            FillDropdown(TrackBit, playerSkills.BitSources);
            var rappers = RappersManager.GetRappers().Select(e => e.Name).ToList();
            rappers.Insert(0, NO_FEAT);
            FillDropDown(Feat, rappers);
            ShowDurationAndPriceInfo();
            AutotuneToggle.gameObject.SetActive(PlayerManager.GetProperty().HasAutotune);
            gameObject.SetActive(true);
        }

        /// <summary>
        /// Заполняет выпадающие списки значениями
        /// </summary>
        private static void FillDropdown<T>(Dropdown dropdown, IEnumerable<T> source) {
            FillDropDown(dropdown, source.Select(e => (e as Enum).GetDescription()).ToList());
        }

        /// <summary>
        /// Заполняет выпадающие списки значениями
        /// </summary>
        private static void FillDropDown(Dropdown dropdown, List<string> values) {
            dropdown.ClearOptions();
            dropdown.AddOptions(values);
        }

        /// <summary>
        /// Собирает выбранные параметры в модель трэка
        /// </summary>
        protected override void ParseActionModel() {
            if (_track == null) _track = new NewTrackModel();
            _track.Name = TrackName.text;
            _track.Theme = EnumExt.GetFromDescription<TrackTheme>(TrackTheme.captionText.text);
            _track.Style = EnumExt.GetFromDescription<TrackStyle>(TrackStyle.captionText.text);
            _track.TextSourse = EnumExt.GetFromDescription<TextSourse>(TrackText.captionText.text);
            _track.BitSource = EnumExt.GetFromDescription<BitSource>(TrackBit.captionText.text);
            _track.Feat = RappersManager.GetByName(Feat.captionText.text);
            _track.Autotune = AutotuneToggle.isOn;
        }

        /// <summary>
        /// Рассчитывает длительность создания и стоимость
        /// </summary>
        protected override void CalculateDurationAndPrice() {
            var textCost = TextCosts[_track.TextSourse];
            var bitCost = _bitCosts[_track.BitSource];
            _duration = textCost.Duration + bitCost.Duration;
            _price = PlayerManager.GetFansPercentValue() / 4 * (textCost.PricePercent + bitCost.PricePercent);
            ShowDurationAndPriceInfo();
        }

        /// <summary>
        /// Выводит информацию о длительности и стоимости
        /// </summary>
        private void ShowDurationAndPriceInfo() {
            DurationText.text = $"Кол-во дней: {_duration}";
            PriceText.text = _price > 0 ? $"Стоимость: {NumberFormatter.FormatValue(_price)}" : string.Empty;
        }

        /// <summary>
        /// Запускает создание нового трэка
        /// </summary>
        private void CreateNewTrack() {
            ParseActionModel();
            if (!CheckFeat()) return;
            if (!PlayerManager.EnoughMoney(_price)) {
                AlertManager.ShowMessage("У вас недостаточно денег");
                return;
            }
            PlayerManager.SpendMoney(_price);
            StatsManager.UpdateStats();
            gameObject.SetActive(false);
            ActionProgressManager.StartAction(_duration, ActionType.NewTrack, FinishTrack);
            gameObject.GetComponentInParent<ActionsMenu>().TriggerChildVisible();
        }

        /// <summary>
        /// Проверяет, что есть согласие на фит, если он выбран
        /// </summary>
        private bool CheckFeat() {
            if (_track.Feat == null) return true;
            if (_alreadyFeatOffer) return false;
            _alreadyFeatOffer = true;
            if (RappersManager.IsAgree(_track.Feat, PlayerManager.GetInfo().Fans)) {
                AlertManager.ShowMessage($"{_track.Feat.Name} согласился на фит");
                return true;
            }
            AlertManager.ShowMessage($"{_track.Feat.Name} отказался от совместного трэка");
            return false;
        }

        /// <summary>
        /// Завершить создание трэка
        /// </summary>
        private void FinishTrack() {
            _alreadyFeatOffer = false;
            var grade = TrackSuccessAnalyzer.AnalyzeTrack(_track);
            var result = GetTrackResult(grade);
            var playerInfo = PlayerManager.GetInfo();
            _track.Id = PlayerManager.GetInfo().LastTrack?.Id + 1 ?? 1;
            _track.Grade = grade;
            playerInfo.Fans += result.FansIncrease;
            playerInfo.Money += result.Income;
            playerInfo.LastTrack = _track;
            ActionResult.Show(result);
        }

        /// <summary>
        /// Возвращает результат трэка
        /// </summary>
        private ActionResultModel GetTrackResult(SuccessGrade grade) {
            var gradePercent = GradePercentManager.GetRewardPercent(grade, ActionType.NewTrack);
            var fansPercentValue = PlayerManager.GetFansPercentValue();
            var featBonus = _track.Feat?.Fans / 150 ?? 0;
            return new ActionResultModel {
                Action = _track.Feat == null ? ActionType.NewTrack : ActionType.Feat,
                FansIncrease = CalculateStatIncrease(gradePercent, fansPercentValue, 0.1f) + featBonus,
                Income = CalculateStatIncrease(gradePercent, fansPercentValue, 0.2f) * Random.Range(3, 6),
                Top = PlayerManager.GetInfo().Fans >= 50000 ? GetPlaceInTop(grade) : -1,
                Popularity = CalculateStatIncrease(gradePercent, fansPercentValue, 0.1f) * Random.Range(2, 5),
                Grade = grade
            };
        }

        /// <summary>
        /// Возвращает место в топе
        /// </summary>
        private static int GetPlaceInTop(SuccessGrade grade) {
            if (grade == SuccessGrade.Highest) return Random.Range(1, 10);
            if (grade == SuccessGrade.Hight) return Random.Range(11, 40);
            if (grade == SuccessGrade.Middle) return Random.Range(41, 90);
            if (grade == SuccessGrade.Low) return Random.Range(91, 200);
            return -1;
        }
    }
}