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
        public Text TrackName;
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

        private NewClipModel _clip;
        private int _lastTrackId = -1;
        private int _duration;
        private int _price;

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
            TrackName.text = lastTrack.Name;
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
            TrackName.gameObject.SetActive(hasTrack);
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
            if (_clip == null) _clip = new NewClipModel();
            _clip.Track = PlayerManager.GetInfo().LastTrack;
            _clip.HasScreenwritter = Screenwritter.isOn;
            _clip.HasOperator = Operator.isOn;
            _clip.HasProducer = Producer.isOn;
            _clip.HasSoundProducer = SoundProducer.isOn;
        }

        /// <summary>
        /// Рассчитывает длительность и стоимость
        /// </summary>
        protected override void CalculateDurationAndPrice() {
            var settingCosts = new List<SettingCost> {GetCost(Screenwritter.isOn), GetCost(Producer.isOn), GetCost(Operator.isOn), GetCost(SoundProducer.isOn)};
            _duration = settingCosts.Sum(e => e.Duration);
            _price = PlayerManager.GetFansPercentValue() / 5 * settingCosts.Sum(e => e.PricePercent);
            Duration.text = $"Кол-во дней: {_duration}д";
            Price.text = _price > 0 ? $"Стоимость: {NumberFormatter.FormatValue(_price)}" : string.Empty;
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
            PlayerManager.SpendMoney(_price);
            StatsManager.UpdateStats();
            gameObject.SetActive(false);
            ActionProgressManager.StartAction(_duration, ActionType.NewClip, FinishClip);
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
            if (_clip != null && _clip.Track.Id == _lastTrackId) {
                AlertManager.ShowMessage("Вы уже сняли клип на этот трэк");
                return false;
            }
            if (!PlayerManager.EnoughMoney(_price)) {
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
            var grade = ClipSuccessAnalyzer.AnalyzeClip(_clip);
            var result = GetClipResult(grade);
            _lastTrackId = _clip.Track.Id;
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
            var featBonus = _clip.Track.Feat?.Fans / 150 ?? 0;
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
