using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Managers {
    /// <summary>
    /// Логика вывода оповещений
    /// </summary>
    public class AlertManager : MonoBehaviour {
        public Text AlertText;

        /// <summary>
        /// Выводит сообщение
        /// </summary>
        public void ShowMessage(string message, int duration = 5) {
            AlertText.text = message;
            StartCoroutine(HideMessage(duration));
        }

        /// <summary>
        /// Скрыть сообщение
        /// </summary>
        private IEnumerator HideMessage(int duration) {
            yield return new WaitForSeconds(duration);
            AlertText.text = string.Empty;
        }
    }
}
