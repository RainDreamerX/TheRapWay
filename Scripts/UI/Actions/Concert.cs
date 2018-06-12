using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Enums;
using Assets.Scripts.Managers;
using Assets.Scripts.Models;
using Assets.Scripts.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Actions {
    /// <summary>
    /// Организация концерта
    /// </summary>
    public class Concert : BaseAction {
        private const int CONCERT_DAYS_DELAY = 30;
        private const int CONCERT_DURATION = 7;
        private const int TICKET_COST = 70;

        public Dropdown PlaceSelector;
        public Text City;
        public Text Capacity;
        public Toggle Organizers;
        public Toggle Advertising;
        public Text PriceText;
        public Button StartButton;

        [Header("Победный экран")]
        public Image ConcertWinImage;
        public Button ConcertWinButton;

        private int concertDay;
        private ConcertPlaceModel place;
        private int price;

        /// <summary>
        /// Концертные залы
        /// </summary>
        private readonly List<ConcertPlaceModel> places = new List<ConcertPlaceModel> {
            new ConcertPlaceModel {Name = "КСК Экспресс", City = "Ростов-на-Дону", Capacity = 1500, Rent = 64000},
            new ConcertPlaceModel {Name = "ДС Труд", City = "Иркутск", Capacity = 2000, Rent = 85000},
            new ConcertPlaceModel {Name = "МТЛ Арена", City = "Самара", Capacity = 2200, Rent = 90000},
            new ConcertPlaceModel {Name = "Event Hall", City = "Воронеж", Capacity = 2200, Rent = 90000},
            new ConcertPlaceModel {Name = "ГлавClub", City = "Москва", Capacity = 3000, Rent = 130000},
            new ConcertPlaceModel {Name = "ДС Олимп", City = "Краснодар", Capacity = 3000, Rent = 130000},
            new ConcertPlaceModel {Name = "Ray Just Arena", City = "Москва", Capacity = 3500, Rent = 250000},
            new ConcertPlaceModel {Name = "КРК Уралец", City = "Екатеринбург", Capacity = 6000, Rent = 250000},
            new ConcertPlaceModel {Name = "Crocus City Hall", City = "Москва", Capacity = 7300, Rent = 400000},
            new ConcertPlaceModel {Name = "ЛДС Сибирь", City = "Новосибирск", Capacity = 7500, Rent = 320000},
            new ConcertPlaceModel {Name = "Баскет-холл", City = "Казань", Capacity = 7500, Rent = 320000},
            new ConcertPlaceModel {Name = "Stadium Live", City = "Москва", Capacity = 8000, Rent = 440000},
            new ConcertPlaceModel {Name = "Дворец спорта", City = "Киев", Capacity = 12000, Rent = 510000},
            new ConcertPlaceModel {Name = "Ледовый", City = "Санкт-Петербург", Capacity = 15000, Rent = 650000},
            new ConcertPlaceModel {Name = "Олимпийский", City = "Москва", Capacity = 23000, Rent = 1000000}
        };

        /// <summary>
        /// Инициализация компонента
        /// </summary>
        public override void ChildAwake() {
            PlaceSelector.onValueChanged.AddListener(e => OnSettingChange());
            Organizers.onValueChanged.AddListener(e => OnSettingChange());
            Advertising.onValueChanged.AddListener(e => OnSettingChange());
            StartButton.onClick.AddListener(StartConcert);
            ConcertWinButton.onClick.AddListener(() => ConcertWinImage.gameObject.SetActive(false));
            ConcertWinImage.gameObject.SetActive(false);
        }

        /// <summary>
        /// Открывает страницу
        /// </summary>
        public override void OpenPage() {
            PlaceSelector.ClearOptions();
            PlaceSelector.AddOptions(places.Select(e => e.Name).ToList());
            OnSettingChange();
            gameObject.SetActive(true);
        }

        /// <summary>
        /// Отображает информацию о выбранном месте концерта
        /// </summary>
        protected override void ParseActionModel() {
            place = places.First(e => e.Name == PlaceSelector.captionText.text);
            City.text = place.City;
            Capacity.text = $"Вместимость: {NumberFormatter.FormatValue(place.Capacity)}";
        }

        /// <summary>
        /// Рассчитывает цену
        /// </summary>
        protected override void CalculateDurationAndPrice() {
            price = place.Rent;
            var rentPercent = place.Rent / 100 * 5;
            price += Organizers.isOn ? rentPercent : 0;
            price += Advertising.isOn ? rentPercent : 0;
            PriceText.text = $"Стоимость: {NumberFormatter.FormatValue(price)}";
        }

        /// <summary>
        /// Запускает концерт
        /// </summary>
        private void StartConcert() {
            if (concertDay != 0 && DaysManager.CurrentDay - concertDay < CONCERT_DAYS_DELAY) {
                AlertManager.ShowMessage($"Нельзя давать концерты чаще, чем раз в {CONCERT_DAYS_DELAY} дней");
                return;
            }
            if (!PlayerManager.EnoughMoney(price)) {
                AlertManager.ShowMessage("У вас недостаточно денег для организации концерта");
                return;
            }
            PlayerManager.SpendMoney(price);
            StatsManager.UpdateStats();
            gameObject.SetActive(false);
            ActionProgressManager.StartAction(CONCERT_DURATION, FinishConcert);
            gameObject.GetComponentInParent<ActionsMenu>().TriggerChildVisible();
        }

        /// <summary>
        /// Обработчик завершения концерта
        /// </summary>
        private void FinishConcert() {
            concertDay = DaysManager.CurrentDay;
            var result = GetConcertResult();
            var playerInfo = PlayerManager.GetInfo();
            playerInfo.Fans += result.FansIncrease;
            playerInfo.Money += result.Income;
            ProcessWinScreen(playerInfo);
            StatsManager.UpdateStats();
            ActionResult.Show(result);
        }

        /// <summary>
        /// Обработчик победного экрана
        /// </summary>
        private void ProcessWinScreen(PlayerInfo playerInfo) {
            if (!playerInfo.ConcertScreenWin && place.Name == "Олимпийский") {
                ConcertWinImage.GetComponentsInChildren<Text>().First(e => e.name == "Content").text =
                    "Столько людей в одном месте! Это фантастическое, незабываемое зрелище! Не многим артистам за всю карьеру удается сделать такое.";
                ConcertWinImage.gameObject.SetActive(true);
                playerInfo.ConcertScreenWin = true;
            }
        }

        /// <summary>
        /// Возвращает результат концерта
        /// </summary>
        private ActionResultModel GetConcertResult() {
            var fans = PlayerManager.GetFansPercentValue() * GetAttendanceCoef();
            var fansSpread = fans / 100 * 5;
            fans = Random.Range(fans - fansSpread, fans + fansSpread);
            if (fans > place.Capacity) fans = place.Capacity;
            return new ActionResultModel {
                Action = ActionType.Concert,
                Popularity = fans,
                FansIncrease = fans / 100 * 50,
                Income = fans * TICKET_COST,
                Grade = GetSuccessGrade(fans)
            };
        }

        /// <summary>
        /// Возвращает коэффициент посещаемости
        /// </summary>
        private int GetAttendanceCoef() {
            return place.City == "Москва" ? 40 : 20;
        }

        /// <summary>
        /// Возвращает оценку успешности
        /// </summary>
        private SuccessGrade GetSuccessGrade(int fans) {
            var percentage = (float) fans / place.Capacity * 100;
            if (percentage >= 80) return SuccessGrade.Highest;
            if (percentage >= 60) return SuccessGrade.Hight;
            if (percentage >= 40) return SuccessGrade.Middle;
            if (percentage >= 20) return SuccessGrade.Low;
            return SuccessGrade.Lowest;
        }
    }
}
