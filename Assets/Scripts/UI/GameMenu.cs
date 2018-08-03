using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.UI {
    /// <summary>
    /// Логика меню управления внутри игры
    /// </summary>
    public class GameMenu : MonoBehaviour {
        public Button ToggleButton;
        public Image Page;
        public Button MainMenuButton;
        public Button RateButton;
        public Button ExitButton;

        private bool isOpen;

        public void Awake() {
            Page.gameObject.SetActive(false);
            ToggleButton.onClick.AddListener(TriggerVisible);
            MainMenuButton.onClick.AddListener(() => SceneManager.LoadSceneAsync(0));
            ExitButton.onClick.AddListener(Application.Quit);
        }

        /// <summary>
        /// Управляет видимостью меню
        /// </summary>
        private void TriggerVisible() {
            isOpen = !isOpen;
            Page.gameObject.SetActive(isOpen);
        }
    }
}
