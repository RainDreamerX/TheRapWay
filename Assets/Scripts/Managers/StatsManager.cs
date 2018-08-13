using Assets.Scripts.UI.Tweeter;
using Assets.Scripts.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Managers {
    /// <summary>
    /// Основная статистика игрока
    /// </summary>
    public class StatsManager : MonoBehaviour {
        public AlertManager AlertManager;
        public TweetsFeed TweetsFeed;

        public Text PlayerName;
        public Text Fans;
        public Text Money;
        public Button AddFansButton;
        public Button AddMoneyButton;

        public void Awake() {
            AddFansButton.onClick.AddListener(OnAddFans);
            AddMoneyButton.onClick.AddListener(OnAddMoney);
            UpdateStats();
        }

        /// <summary>
        /// Обновляет основную статистику
        /// </summary>
        public void UpdateStats() {
            var playerInfo = PlayerManager.GetInfo();
            PlayerName.text = playerInfo.Name;
            Fans.text = NumberFormatter.FormatValue(playerInfo.Fans);
            Money.text = NumberFormatter.FormatValue(playerInfo.Money);
            if (!playerInfo.TweetsUnlocked && playerInfo.Fans >= 10000) {
                AlertManager.ShowMessage("О вас стали говорить в соц. сетях!", 10);
                TweetsFeed.Open();
            }
            if (!playerInfo.EventUnlocked && playerInfo.Fans >= 5000) playerInfo.EventUnlocked = true;
        }

        /// <summary>
        /// Обработчик добавления фанатов
        /// </summary>
        private void OnAddFans() {

        }

        /// <summary>
        /// Обработчик добавления денег
        /// </summary>
        private void OnAddMoney() {

        }
    }
}