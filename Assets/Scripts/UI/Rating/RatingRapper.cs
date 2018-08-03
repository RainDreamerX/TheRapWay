using Assets.Scripts.Models;
using Assets.Scripts.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Rating {
    /// <summary>
    /// Запись в рейтинге реперов
    /// </summary>
    public class RatingRapper : MonoBehaviour {
        public Image PlayerIcon;
        public Text Name;
        public Text Fans;

        /// <summary>
        /// Заполняет строку в рейтинге
        /// </summary>
        public void Setup(RapperModel model, int position, bool isPlayer) {
            PlayerIcon.gameObject.SetActive(isPlayer);
            Name.text = $"{position}. {model.Name}";
            Fans.text = $"{NumberFormatter.FormatValue(model.Fans)}";
        }
    }
}
