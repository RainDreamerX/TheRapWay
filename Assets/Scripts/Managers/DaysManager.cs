using Assets.Scripts.UI;
using Assets.Scripts.UI.Events;
using Assets.Scripts.UI.Events.EventTemplates;
using Assets.Scripts.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Managers {
    /// <summary>
    /// Переключение дней
    /// </summary>
    public class DaysManager : MonoBehaviour {
        private const int TRANDS_CHANGE_STEP = 31;

        public StatsManager StatsManager;
        public WorkProgress WorkProgress;
        public AlertManager AlertManager;
        public EventManager EventManager;
        public LoseGame LoseGame;
        public Trands Trands;

        public static int CurrentDay = 1;

        private Text _counterText;
        private int _nextTrandsChangeDay = TRANDS_CHANGE_STEP;
        private bool _gameLosed;

        public void Awake () {
            _counterText = GetComponent<Text>();
            CurrentDay = SaveManager.Data.DaysPlayed;
            _counterText.text = $"День: {CurrentDay}";
        }

        /// <summary>
        /// Следующий день
        /// </summary>
        public void NextDay() {
            CurrentDay += 1;
            _counterText.text = $"День: {CurrentDay}";
            StatsManager.UpdateStats();
            WorkProgress.NextDay();
            if (CurrentDay % 30 == 0) {
                RappersManager.UpdateFans();
                ProcessExpenses();
            }
            if (CurrentDay % 50 == 0) EventManager.ShowEvent();
            if (CurrentDay >= _nextTrandsChangeDay) {
                Trands.UpdateTrands();
                _nextTrandsChangeDay = CurrentDay + TRANDS_CHANGE_STEP;
            }
            if (CurrentDay % 100 == 0) AdsManager.GetInstance().ShowAd();
            RaiseSpecialEvents();
        }

        /// <summary>
        /// Возвращает признак проигрыша игры
        /// </summary>
        public bool IsGameLosed() {
            return _gameLosed;
        }

        /// <summary>
        /// Запускает особые события в нужный день
        /// </summary>
        private void RaiseSpecialEvents() {
            switch (CurrentDay) {
                case 53:
                    EventManager.ShowEvent(new GhostwritterEvent());
                    break;
                case 103:
                    EventManager.ShowEvent(new BitmakerEvent());
                    break;
            }
        }

        /// <summary>
        /// Списание расходов за месяц
        /// </summary>
        private void ProcessExpenses() {
            var expenses = PlayerManager.GetFansPercentValue() * 5;
            if (PlayerManager.EnoughMoney(expenses)) {
                PlayerManager.SpendMoney(expenses);
                AlertManager.ShowMessage($"Ваши расходы за месяц: {NumberFormatter.FormatValue(expenses)}");
                StatsManager.UpdateStats();
            }
            else {
                _gameLosed = true;
                LoseGame.Show();
            }
        }
    }
}
