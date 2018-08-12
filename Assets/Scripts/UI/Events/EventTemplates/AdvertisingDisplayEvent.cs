using Assets.Scripts.Enums;
using Assets.Scripts.Managers;
using Assets.Scripts.Utils;

namespace Assets.Scripts.UI.Events.EventTemplates {
    /// <summary>
    /// Событие просмотра рекламы
    /// </summary>
    public class AdvertisingDisplayEvent : BaseEvent {
        /// <summary>
        /// Название события
        /// </summary>
        public override string Name { get; } = "Случайный доход";

        /// <summary>
        /// Описание события
        /// </summary>
        public override string Content { get; } = "Одним свободним вечером, вы лестали ленту новостей и наткнулись на рекламное предложение. " +
                                                  "Там говорилось, что за просмотр рекламного ролика, каждый получит вознаграждение. Что вы выберете?";

        /// <summary>
        /// Тип события
        /// </summary>
        public override EventType Type { get; } = EventType.AdvertisingDisplay;

        /// <summary>
        /// Контент первого действия
        /// </summary>
        public override string FirstButtonText { get; } = "Посмотреть рекламу";

        /// <summary>
        /// Контент второго действия
        /// </summary>
        public override string SecondButtonText { get; } = "Пролистать дальше";

        /// <summary>
        /// Обработчик первого варианта действия
        /// </summary>
        public override void OnFirtsButtonClick(EventManager eventManager) {
            HideEventButtons(eventManager.FirstButton, eventManager.SecondButton, eventManager.OkButton);
            //AdsManager.GetInstance().ShowAd(Appodeal.NON_SKIPPABLE_VIDEO);
            eventManager.EventContent.text = "Вы досмотрели ролик до конца и получили заслуженную награду!";
            var playerInfo = PlayerManager.GetInfo();
            var income = playerInfo.Money > 500 ? playerInfo.Money / 100 * 10 : 200;
            playerInfo.Money += income;
            eventManager.EventReward.text = $"Заработано: {NumberFormatter.FormatValue(income)}";
            eventManager.StatsManager.UpdateStats();
        }

        /// <summary>
        /// Обработчик второго варианта действия
        /// </summary>
        public override void OnSecondButtonClick(EventManager eventManager) {
            HideEventButtons(eventManager.FirstButton, eventManager.SecondButton, eventManager.OkButton);
            eventManager.EventContent.text = "Вы просто продолжили листать ленту новостей...";
        }
    }
}