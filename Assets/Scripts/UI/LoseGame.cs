using Assets.Scripts.Managers;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.UI {
    public class LoseGame : MonoBehaviour {
        public Button MainMenuButton;

        public void Awake() {
            gameObject.SetActive(false);
            MainMenuButton.onClick.AddListener(() => {
                    SceneManager.LoadSceneAsync(0);
                    AdsManager.GetInstance().ShowAd();
                }
            );
        }
        

        /// <summary>
        /// Показать экран проигрыша
        /// </summary>
        public void Show() {
            gameObject.SetActive(true);
            DaysManager.CurrentDay = 1;
            PlayerPrefs.DeleteKey("Save");
        }
    }
}
