using System.Linq;
using Assets.Scripts.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Actions {
    public abstract class BaseAction : MonoBehaviour {
        private Button backButton;

        public ActionProgressManager ActionProgressManager;
        public ActionResult ActionResult;
        public AlertManager AlertManager;
        public StatsManager StatsManager;

        public void Awake() {
            backButton = GetComponentsInChildren<Button>().First(e => e.name == "BackButton");
            backButton.onClick.AddListener(HidePage);
            ChildAwake();
            HidePage();
        }

        /// <summary>
        /// Инициализация дочернего компонента
        /// </summary>
        public abstract void ChildAwake();

        /// <summary>
        /// Открывает страницу
        /// </summary>
        public abstract void OpenPage();

        /// <summary>
        /// Обработчик смены настроек батла
        /// </summary>
        protected void OnSettingChange() {
            ParseActionModel();
            CalculateDurationAndPrice();
        }

        /// <summary>
        /// Обновляет данные модели действия
        /// </summary>
        protected abstract void ParseActionModel();

        /// <summary>
        /// Рассчитывает длительность и стоимость
        /// </summary>
        protected abstract void CalculateDurationAndPrice();

        /// <summary>
        /// Рассчитывает увеличение параметра статистики игрока
        /// </summary>
        protected static int CalculateStatIncrease(float gradePercent, float fansPercent, float spreadCoef) {
            var increase = gradePercent * fansPercent;
            var spread = increase * spreadCoef;
            return (int) Random.Range(increase - spread, increase + spread);
        }

        /// <summary>
        /// Скрыть окно
        /// </summary>
        private void HidePage() {
            gameObject.SetActive(false);
        }
    }
}
