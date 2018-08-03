using Assets.Scripts.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI {
    public class WorkProgress : MonoBehaviour {
        public ActionProgressManager ActionProgressManager;
        public Text DaysToEnd;

        /// <summary>
        /// Установить активность компонента
        /// </summary>
        public void SetActive(bool isActive) {
            gameObject.SetActive(isActive);
            if (isActive) {
                SetDaysToEnd();
            }
        }

        /// <summary>
        /// Обработчик переключения дней
        /// </summary>
        public void NextDay() {
            if (ActionProgressManager.DaysLeft > 0)
                SetDaysToEnd();
            else {
                var actionMenu = gameObject.GetComponentInParent<ActionsMenu>();
                if (actionMenu != null) actionMenu.TriggerChildVisible();
            }
        }

        /// <summary>
        /// Вывести количество дней
        /// </summary>
        private void SetDaysToEnd() {
            DaysToEnd.text = "Дней до завершения: " + ActionProgressManager.DaysLeft;
        }
    }
}
