using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Managers;
using Assets.Scripts.Models;
using Assets.Scripts.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Rating {
    public class RatingManager : MonoBehaviour {
        public Image Page;
        public Transform RatingPanel;
        public Button ToggleButton;
        public Button CloseButton;

        public SimpleObjectPool ObjectPool;

        private bool isOpen;

        public void Awake() {
            ToggleButton.onClick.AddListener(TriggerVisible);
            CloseButton.onClick.AddListener(Close);
            Page.gameObject.SetActive(false);
        }

        /// <summary>
        /// Изменяет видимость окна рейтинга
        /// </summary>
        private void TriggerVisible() {
            if (!isOpen) {
                FillRating();
            }
            isOpen = !isOpen;
            Page.gameObject.SetActive(isOpen);
        }

        /// <summary>
        /// Закрывает страницу
        /// </summary>
        private void Close() {
            isOpen = false;
            Page.gameObject.SetActive(false);
        }

        /// <summary>
        /// Заполняет список рейтинга
        /// </summary>
        private void FillRating() {
            RatingPanel.DetachChildren();
            var playerInfo = PlayerManager.GetInfo();
            var ratingList = GetRatingList(playerInfo);
            var position = 1;
            foreach (var rapper in ratingList.OrderByDescending(e => e.Fans)) {
                var ratingRow = ObjectPool.GetObject();
                ratingRow.transform.SetParent(RatingPanel);
                ratingRow.transform.localScale = Vector3.one;
                ratingRow.GetComponent<RatingRapper>().Setup(rapper, position, playerInfo.Name == rapper.Name);
                position++;
            }
        }

        /// <summary>
        /// Обновляет данные игрока
        /// </summary>
        private static IEnumerable<RapperModel> GetRatingList(PlayerInfo playerInfo) {
            var rappers = RappersManager.GetRappers();
            var ratingList = new RapperModel[rappers.Count + 1];
            rappers.CopyTo(ratingList);
            ratingList[rappers.Count] = new RapperModel {Name = playerInfo.Name, Fans = playerInfo.Fans};
            return ratingList;
        }
    }
}
