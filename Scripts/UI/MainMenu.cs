using Assets.Scripts.Managers;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.UI {
    /// <summary>
    /// Логика главного меню
    /// </summary>
    public class MainMenu : MonoBehaviour {
        public SaveManager SaveManager;

        public Button NewGameButton;
        public Button ContinueButton;
        public Button AboutGameButton;
        public Button ExitButton;
        public Image StartPage;
        public Button BackButton;
        public InputField NameField;
        public Button StartNewGameButton;

        private string playerName;

        public void Awake() {
            AdsManager.CreateInstance();
            NewGameButton.onClick.AddListener(() => StartPage.gameObject.SetActive(true));
            if (!SaveManager.HasSave()) {
                ContinueButton.interactable = false;
                ContinueButton.GetComponentInChildren<Text>().color = Color.gray;
            }
            ContinueButton.onClick.AddListener(ContinueGame);
            AboutGameButton.onClick.AddListener(() => {});
            ExitButton.onClick.AddListener(Application.Quit);
            StartPage.gameObject.SetActive(false);
            BackButton.onClick.AddListener(() => StartPage.gameObject.SetActive(false));
            NameField.onValueChanged.AddListener(SetName);
            StartNewGameButton.gameObject.SetActive(false);
            StartNewGameButton.onClick.AddListener(StartNewGame);
        }

        public void Start() {
            var instance = AdsManager.GetInstance();
            while (!instance.AdsIsLoad()) {}
            instance.ShowAd();
        }

        /// <summary>
        /// Начать новую игру
        /// </summary>
        private void StartNewGame() {
            PlayerManager.CreateNew(playerName);
            if (SaveManager.HasSave()) SaveManager.DeleteSave();
            SaveManager.Save();
            SceneManager.LoadSceneAsync(1);
        }

        /// <summary>
        /// Продолжить игру
        /// </summary>
        private void ContinueGame() {
            SaveManager.Load();
            SceneManager.LoadSceneAsync(1);
        }

        /// <summary>
        /// Обработчик ввода имени
        /// </summary>
        private void SetName(string value) {
            if (value.Length < 3 || value.Length > 25) {
                StartNewGameButton.gameObject.SetActive(false);
                return;
            }
            playerName = value;
            StartNewGameButton.gameObject.SetActive(true);
        }
    }
}
