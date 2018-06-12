using Assets.Scripts.Managers;
using Assets.Scripts.Utils;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Events.EventTemplates {
    /// <summary>
    /// Базовый класс событий
    /// </summary>
    public abstract class BaseEvent {
        /// <summary>
        /// Название события
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// Описание события
        /// </summary>
        public abstract string Content { get; }

        /// <summary>
        /// Контент первого действия
        /// </summary>
        public abstract string FirstButtonText { get; }

        /// <summary>
        /// Контент второго действия
        /// </summary>
        public abstract string SecondButtonText { get; }

        /// <summary>
        /// Обработчик первого варианта действия
        /// </summary>
        public abstract void OnFirtsButtonClick(EventManager eventManager);

        /// <summary>
        /// Обработчик второго варианта действия
        /// </summary>
        public abstract void OnSecondButtonClick(EventManager eventManager);

        /// <summary>
        /// Скрывает кнопки выбора действий события
        /// </summary>
        protected void HideEventButtons(Button firstButton, Button secondButton, Button okButton) {
            firstButton.gameObject.SetActive(false);
            secondButton.gameObject.SetActive(false);
            okButton.gameObject.SetActive(true);
        }
        
        /// <summary>
        /// Увеличивает количество фанатов
        /// </summary>
        protected static void IncreaseFans(EventManager eventManager, int percent = 1) {
            var fansIncrease = PlayerManager.GetFansPercentValue() * percent;
            PlayerManager.GetInfo().Fans += fansIncrease;
            eventManager.EventReward.text = $"Вы приобрели {NumberFormatter.FormatValue(fansIncrease)} новых фанатов";
            eventManager.StatsManager.UpdateStats();
        }

        /// <summary>
        /// Уменьшает количество фанатов
        /// </summary>
        protected static void DecreaseFans(EventManager eventManager, int percent = 1) {
            var playerInfo = PlayerManager.GetInfo();
            var fansDecrease = PlayerManager.GetFansPercentValue() * percent;
            if (playerInfo.Fans < fansDecrease) fansDecrease = playerInfo.Fans;
            playerInfo.Fans -= fansDecrease;
            eventManager.EventReward.text = $"От вас ушло {NumberFormatter.FormatValue(fansDecrease)} фанатов";
            eventManager.StatsManager.UpdateStats();
        }
    }
}