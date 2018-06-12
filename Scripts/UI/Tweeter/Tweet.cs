using Assets.Scripts.Enums;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Tweeter {
    /// <summary>
    /// Запись в ленте соц. сетей
    /// </summary>
    public class Tweet : MonoBehaviour {
        public Text Author;
        public Text Comment;
        public Text Likes;
        public Text Views;

        /// <summary>
        /// Создать запись
        /// </summary>
        public void Create(SuccessGrade grade, ActionType action) {
            Author.text = $"{TweeterData.Nicknames[Random.Range(0, TweeterData.Nicknames.Count)]}";
            Comment.text = GetComment(grade, action);
            Likes.text = $"Likes: {Random.Range(0, 200)}";
            Views.text = $"Views: {Random.Range(0, 3000)}";
        }

        /// <summary>
        /// Возвращает комментарий
        /// </summary>
        private static string GetComment(SuccessGrade grade, ActionType action) {
            var comments = TweeterData.Comments[action][grade];
            return comments[Random.Range(0, comments.Count)];
        }
    }
}
