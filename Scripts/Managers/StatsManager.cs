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

        public void Awake() {
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
            if (!playerInfo.EventUnlocked && playerInfo.Fans >= 5000) playerInfo.TweetsUnlocked = true;
        }
    }
}