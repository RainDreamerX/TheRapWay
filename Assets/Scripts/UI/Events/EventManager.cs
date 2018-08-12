using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Managers;
using Assets.Scripts.UI.Events.EventTemplates;
using UnityEngine;
using UnityEngine.UI;
using EventType = Assets.Scripts.Enums.EventType;
using Random = UnityEngine.Random;

namespace Assets.Scripts.UI.Events {
    /// <summary>
    /// Логика случайных событий
    /// </summary>
    public class EventManager : MonoBehaviour {
        public StatsManager StatsManager;

        public Text EventName;
        public Text EventContent;
        public Text EventReward;
        public Image Background;
        public Button FirstButton;
        public Button SecondButton;
        public Button OkButton;

        public Sprite[] EventSprites;

        private readonly List<BaseEvent> _events = new List<BaseEvent> {
            new TrackRemixEvent(), new AdvertisingEvent(), new TrollingEvent(), new FilmEvent(),
            new JournalistEvent(), new InterviewEvent(), new KidsFansEvent(), new DissEvent(),
            new GameStreamEvent(), new GirlFriendEvent(), new AdvertisingDisplayEvent()
        };

        public void Awake() {
            OkButton.onClick.AddListener(() => gameObject.SetActive(false));
            gameObject.SetActive(false);
        }

        /// <summary>
        /// Показать случайно выбранное событие
        /// </summary>
        public void ShowEvent(BaseEvent @event = null) {
            if (!PlayerManager.GetInfo().EventUnlocked) return;
            if (@event == null) @event = _events[Random.Range(0, _events.Count)];
            EventName.text = @event.Name;
            EventContent.text = @event.Content;
            EventReward.text = string.Empty;
            Background.sprite = GetEventSpite(@event.Type);
            FirstButton.GetComponentInChildren<Text>(true).text = @event.FirstButtonText;
            SecondButton.GetComponentInChildren<Text>(true).text = @event.SecondButtonText;
            RefreshListener(FirstButton, @event.OnFirtsButtonClick);
            RefreshListener(SecondButton, @event.OnSecondButtonClick);
            ShowEventButtons();
            gameObject.SetActive(true);
        }

        /// <summary>
        /// Обновить обработчик кнопки
        /// </summary>
        private void RefreshListener(Button button, Action<EventManager> action) {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => action(this));
        }

        /// <summary>
        /// Показать кнопки выбора
        /// </summary>
        private void ShowEventButtons() {
            FirstButton.gameObject.SetActive(true);
            SecondButton.gameObject.SetActive(true);
            OkButton.gameObject.SetActive(false);
        }

        /// <summary>
        /// Возвращает спрайт для бг окна эвента
        /// </summary>
        private Sprite GetEventSpite(EventType type) {
            switch (type) {
                case EventType.Bitmaker:
                case EventType.Ghostwritter:
                case EventType.TrackRemix:
                case EventType.Trolling:
                case EventType.AdvertisingDisplay:
                case EventType.GameStream:
                    return EventSprites[0];
                case EventType.Advertising:
                    return EventSprites[1];
                case EventType.Interview:
                    return EventSprites[2];
                case EventType.Film:
                    return EventSprites[3];
                case EventType.GirlFriend:
                    return EventSprites[4];
                case EventType.Diss:
                    return EventSprites[5];
                case EventType.Journalist:
                    return EventSprites[6];
                case EventType.KidsFans:
                    return EventSprites[7];
                default:
                    return EventSprites.Last();
            }
        }
    }
}
