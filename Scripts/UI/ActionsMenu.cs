using System.Linq;
using Assets.Scripts.Managers;
using Assets.Scripts.UI.Actions;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI {
    /// <summary>
    /// Меню основных действий
    /// </summary>
    public class ActionsMenu : MonoBehaviour {
        public ActionProgressManager ActionProgressManager;

        public Button NewTrackButton;
        public Button NewClipButton;
        public Button ConcertButton;
        public Button VersusButton;
        public Button TraningButton;
        public Button ShopButton;
        public Button CloseMenuButton;

        private bool isOpen;
        private Component actionsPages;

        public void Awake() {
            gameObject.SetActive(false);
            NewTrackButton.onClick.AddListener(() => GetPage("NewTrack").OpenPage());
            NewClipButton.onClick.AddListener(() => GetPage("NewClip").OpenPage());
            ConcertButton.onClick.AddListener(() => GetPage("Concert").OpenPage());
            VersusButton.onClick.AddListener(() => GetPage("VersusBattle").OpenPage());
            TraningButton.onClick.AddListener(() => GetPage("Traning").OpenPage());
            ShopButton.onClick.AddListener(() => GetPage("Shop").OpenPage());
            CloseMenuButton.onClick.AddListener(CloseMenu);
            actionsPages = GetComponentsInChildren<Component>().First(e => e.name == "ActionsPages");
        }

        /// <summary>
        /// Управление видимостью окна
        /// </summary>
        public void TriggerVisible() {
            isOpen = !isOpen;
            gameObject.SetActive(isOpen);
            TriggerChildVisible();
        }

        /// <summary>
        /// Переключение видимости дочерних компонентов
        /// </summary>
        public void TriggerChildVisible() {
            var inProgress = ActionProgressManager.DaysLeft != 0;
            gameObject.GetComponentsInChildren<Component>(true).First(e => e.name == "Actions").gameObject.SetActive(!inProgress);
            gameObject.GetComponentInChildren<WorkProgress>(true).SetActive(inProgress);
        }

        /// <summary>
        /// Возвращает страницу главного меню
        /// </summary>
        private BaseAction GetPage(string pageName) {
            return actionsPages.GetComponentsInChildren<Image>(true).First(e => e.name == pageName).GetComponent<BaseAction>();
        }

        /// <summary>
        /// Закрыть меню действий
        /// </summary>
        private void CloseMenu() {
            isOpen = false;
            gameObject.SetActive(isOpen);
        }
    }
}
