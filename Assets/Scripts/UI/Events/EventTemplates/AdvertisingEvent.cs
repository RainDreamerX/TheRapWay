using Assets.Scripts.Enums;
using Assets.Scripts.Managers;
using Assets.Scripts.Utils;

namespace Assets.Scripts.UI.Events.EventTemplates {
    public class AdvertisingEvent : BaseEvent {
        /// <summary>
        /// Название события
        /// </summary>
        public override string Name { get; } = "Сомнительная реклама";

        /// <summary>
        /// Описание события
        /// </summary>
        public override string Content { get; } = "С вами связались представители одного интернет-казино и предложили сотрудничество. " +
                                                  "Они хотят, чтобы вы записали небольшой рекламный ролик, в котором читаете хвалебный рэп о них. Что скажете?";

        /// <summary>
        /// Тип события
        /// </summary>
        public override EventType Type { get; } = EventType.Advertising;

        /// <summary>
        /// Контент первого действия
        /// </summary>
        public override string FirstButtonText { get; } = "Вежливо отказаться от сотрудничества";

        /// <summary>
        /// Контент второго действия
        /// </summary>
        public override string SecondButtonText { get; } = "Согласиться и начать писать текст";

        /// <summary>
        /// Обработчик первого варианта действия
        /// </summary>
        public override void OnFirtsButtonClick(EventManager eventManager) {
            HideEventButtons(eventManager.FirstButton, eventManager.SecondButton, eventManager.OkButton);
            eventManager.EventContent.text = "Оказалось, что огромное количество людей ненавидят это казино из-за вездесущей рекламы. " +
                                             "Узнав о вашем отказе, вас стали больше уважать!";
            IncreaseFans(eventManager);
        }

        /// <summary>
        /// Обработчик второго варианта действия
        /// </summary>
        public override void OnSecondButtonClick(EventManager eventManager) {
            HideEventButtons(eventManager.FirstButton, eventManager.SecondButton, eventManager.OkButton);
            eventManager.EventContent.text = "Рекламный ролик стал вирусным, а вы - мемом. Многие фанаты остались сильно разочарованы...";
            var playerInfo = PlayerManager.GetInfo();
            var fansDecrease = PlayerManager.GetFansPercentValue() * 3;
            var income = playerInfo.Money > 500 ? playerInfo.Money / 100 * 5 : 50;
            if (playerInfo.Fans < fansDecrease) fansDecrease = playerInfo.Fans;
            playerInfo.Fans -= fansDecrease;
            playerInfo.Money += income;
            eventManager.EventReward.text = $"От вас ушло {NumberFormatter.FormatValue(fansDecrease)} фанатов. Заработано: {NumberFormatter.FormatValue(income)}";
            eventManager.StatsManager.UpdateStats();
        }
    }
}