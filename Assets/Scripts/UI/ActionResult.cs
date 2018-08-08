using System;
using System.Linq;
using Assets.Scripts.Data;
using Assets.Scripts.Enums;
using Assets.Scripts.Extentions;
using Assets.Scripts.Managers;
using Assets.Scripts.Models;
using Assets.Scripts.UI.Tweeter;
using Assets.Scripts.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI {
    /// <summary>
    /// Обработчик результата выполнения действия
    /// </summary>
    public class ActionResult : MonoBehaviour {
        public StatsManager StatsManager;
        public TweetsFeed TweetsFeed;

        public Text Title;
        public Text Popularity;
        public Text Top;
        public Text Traning;
        public GameObject Reward;

        public Sprite[] WindowSprites;

        private Image _window;
        private Text _fans;
        private Text _money;
        private Button _button;

        public void Awake() {
            _window = GetComponent<Image>();
            _fans = Reward.GetComponentsInChildren<Component>().First(e => e.name == "Fans").GetComponentInChildren<Text>();
            _money = Reward.GetComponentsInChildren<Component>().First(e => e.name == "Money").GetComponentInChildren<Text>();
            _button = GetComponentInChildren<Button>();
            _button.onClick.AddListener(() => gameObject.SetActive(false));
            gameObject.SetActive(false);
        }

        /// <summary>
        /// Показать окно результата
        /// </summary>
        public void Show(ActionResultModel result) {
            _window.sprite = GetSprite(result.Action, result.Grade);
            StatsManager.UpdateStats();
            Title.text = "Завершено: " + result.Action.GetDescription();
            if (result.Action != ActionType.Traning) {
                ShowActionResult(result);
                if (PlayerManager.GetInfo().TweetsUnlocked) TweetsFeed.AddTweet(result.Grade, result.Action);
            }
            else {
                ShowTraningResult(result);
            }
            gameObject.SetActive(true);
        }

        /// <summary>
        /// Возвращает нужный фон окна
        /// </summary>
        private Sprite GetSprite(ActionType type, SuccessGrade grade) {
            switch (type) {
                case ActionType.NewTrack:
                case ActionType.Feat:
                    return WindowSprites[ActionSpriteKey.TRACK];
                case ActionType.NewClip:
                    return WindowSprites[ActionSpriteKey.CLIP];
                case ActionType.Concert:
                    var concertKey = grade < SuccessGrade.Middle 
                        ? ActionSpriteKey.BAD_CONCERT 
                        : grade > SuccessGrade.Middle 
                            ? ActionSpriteKey.SUCCESS_CONCERT 
                            : ActionSpriteKey.NORMAL_CONCERT;
                    return WindowSprites[concertKey];
                case ActionType.Battle:
                    var battleKey = grade >= SuccessGrade.Middle ? ActionSpriteKey.WIN_BATTLE : ActionSpriteKey.LOSE_BATTLE;
                    return WindowSprites[battleKey];
                default:
                    return WindowSprites[ActionSpriteKey.DEFAULT];
            }
        }

        /// <summary>
        /// Отображает окно с результатами действия
        /// </summary>
        private void ShowActionResult(ActionResultModel result) {
            Popularity.text = GetPopularityString(result);
            Top.text = GetTopString(result);
            Reward.gameObject.SetActive(true);
            _fans.text = $"{GetSign(result.FansIncrease)} {NumberFormatter.FormatValue(result.FansIncrease)}";
            _money.text = $"{GetSign(result.Income)} {NumberFormatter.FormatValue(result.Income)}";
            Traning.text = string.Empty;
        }

        /// <summary>
        /// Возвращает знак
        /// </summary>
        private static string GetSign(int value) {
            return value >= 0 ? "+" : "-";
        }

        /// <summary>
        /// Отображает окно с результатами тренировки
        /// </summary>
        private void ShowTraningResult(ActionResultModel result) {
            Popularity.text = string.Empty;
            Top.text = string.Empty;
            Reward.gameObject.SetActive(false);
            _fans.text = string.Empty;
            _money.text = string.Empty;
            Traning.text = result.Traning;
        }

        /// <summary>
        /// Возвращает контент популярности
        /// </summary>
        private static string GetPopularityString(ActionResultModel result) {
            var popularityPrefix = string.Empty;
            switch (result.Action) {
                case ActionType.NewTrack:
                case ActionType.Feat:
                    popularityPrefix =  "Скачиваний: ";
                    break;
                case ActionType.NewClip:
                case ActionType.Battle:
                    popularityPrefix = "Просмотров: ";
                    break;
                case ActionType.Concert:
                    popularityPrefix = "Посещение: ";
                    break;
                case ActionType.Traning:
                    return string.Empty;
            }
            return $"{popularityPrefix}{NumberFormatter.FormatValue(result.Popularity)}";
        }

        /// <summary>
        /// Возвращает сообщение об успешности
        /// </summary>
        private static string GetTopString(ActionResultModel actionResult) {
            var result = string.Empty;
            if (actionResult.Action == ActionType.Feat || actionResult.Action == ActionType.NewTrack) {
                result = $"Место в рейтинге: {(actionResult.Top >= 1 && actionResult.Top <= 100 ? actionResult.Top.ToString() : "отсутствует")}";
            }
            if (actionResult.Action == ActionType.Concert) {
                result = GetConcertTopMessage(actionResult.Grade);
            }
            if (actionResult.Action == ActionType.Battle) {
                result = GetBattleTopMessage(actionResult.Grade);
            }
            return result;
        }

        /// <summary>
        /// Возвращает сообщение успешности для
        /// </summary>
        private static string GetConcertTopMessage(SuccessGrade grade) {
            switch (grade) {
                case SuccessGrade.Lowest:
                    return "Концерт провалился, зал был пуст...";
                case SuccessGrade.Low:
                    return "Пришло очень мало людей, но это лучше, чем ничего!";
                case SuccessGrade.Middle:
                    return "Неплохо! Вам удалось заполнить зал наполовину";
                case SuccessGrade.Hight:
                    return "В зале было совсем немного свободных мест. Хороший результат!";
                case SuccessGrade.Highest:
                    return "Это солд-аут! Зал битком забит людьми, а на улице огромная очередь желающих попасть внутрь. Оглушительный успех!";
                default:
                    throw new ArgumentOutOfRangeException(nameof(grade), grade, null);
            }
        }

        /// <summary>
        /// Возвращает сообщение успешности для
        /// </summary>
        private static string GetBattleTopMessage(SuccessGrade grade) {
            switch (grade) {
                case SuccessGrade.Lowest:
                    return "Разгромное поражение. У вас не было шансов";
                case SuccessGrade.Low:
                    return "У вас была пара хороших моментов, но в целом все выглядело слабее, чем у соперника";
                case SuccessGrade.Middle:
                    return "Неоднозначный батл. Каждый зритель решил для себя сам, кто победил";
                case SuccessGrade.Hight:
                    return "Это была практически равная битва, но все же вы оказались сильнее";
                case SuccessGrade.Highest:
                    return "Вы уничтожили оппонента! Ему просто нечего было предоставить в ответ";
                default:
                    throw new ArgumentOutOfRangeException(nameof(grade), grade, null);
            }
        }
    }
}
