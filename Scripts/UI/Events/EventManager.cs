using System;
using System.Collections.Generic;
using Assets.Scripts.Managers;
using Assets.Scripts.UI.Events.EventTemplates;
using UnityEngine;
using UnityEngine.UI;
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
        public Button FirstButton;
        public Button SecondButton;
        public Button OkButton;

        private readonly List<BaseEvent> events = new List<BaseEvent> {
            new TrackRemixEvent(), new AdvertisingEvent(), new TrollingEvent(), new FilmEvent(),
            new JournalistEvent(), new InterviewEvent(), new KidsFansEvent(), new DissEvent(),
            new GameStreamEvent(), new FriendEvent(), new AdvertisingDisplayEvent()
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
            if (@event == null) @event = events[Random.Range(0, events.Count)];
            EventName.text = @event.Name;
            EventContent.text = @event.Content;
            EventReward.text = string.Empty;
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
    }
}
