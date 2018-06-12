using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI {
    public class MainButton : MonoBehaviour {
        public ActionsMenu ActionsMenu;

        private Button button;

        public void Awake() {
            button = GetComponent<Button>();
            button.onClick.AddListener(OnClick);
        }

        /// <summary>
        /// Обработчик нажатия основной кнопки
        /// </summary>
        private void OnClick() {
            ActionsMenu.TriggerVisible();
        }
    }
}
