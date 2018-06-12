using System.Collections.Generic;
using Assets.Scripts.Enums;
using Assets.Scripts.Managers;
using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts.UI.Tweeter {
    /// <summary>
    /// Лента соц. сетей
    /// </summary>
    public class TweetsFeed : MonoBehaviour {
        public Transform Panel;
        public SimpleObjectPool ObjectPool;

        private readonly List<GameObject> tweets = new List<GameObject>(50);
        private static readonly Vector3 scale = new Vector3(1.5f, 1.5f, 1.5f);

        public void Awake() {
            gameObject.SetActive(PlayerManager.GetInfo().TweetsUnlocked);
        }

        /// <summary>
        /// Открывает новостную ленту
        /// </summary>
        public void Open() {
            gameObject.SetActive(true);
            PlayerManager.GetInfo().TweetsUnlocked = true;
        }

        /// <summary>
        /// Добавляет запись в ленту
        /// </summary>
        public void AddTweet(SuccessGrade grade, ActionType action) {
            var newObject = ObjectPool.GetObject();
            newObject.transform.localScale = scale;
            newObject.GetComponent<Tweet>().Create(grade, action);
            if (tweets.Count == 50) tweets.RemoveAt(0);
            tweets.Add(newObject);
            RefreshFeed();
        }

        /// <summary>
        /// Обновляет ленту
        /// </summary>
        private void RefreshFeed() {
            Panel.DetachChildren();
            for (var i = tweets.Count - 1; i >= 0; i--) {
                tweets[i].transform.SetParent(Panel);
            }
        }
    }
}
