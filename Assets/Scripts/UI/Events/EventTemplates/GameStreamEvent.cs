using Assets.Scripts.Enums;
using Assets.Scripts.Managers;
using Assets.Scripts.Utils;

namespace Assets.Scripts.UI.Events.EventTemplates {
    /// <summary>
    /// Предложение разработчиков постримить их игру
    /// </summary>
    public class GameStreamEvent : BaseEvent {
        /// <summary>
        /// Название события
        /// </summary>
        public override string Name { get; } = "Киберспортсмен";

        /// <summary>
        /// Описание события
        /// </summary>
        public override string Content { get; } = "Вам написали разработчики одной популярной на данный момент игры и попросили провести стрим. " +
                                                  "За пару часов игры они обещают очень хорошо заплатить. Вы согласны?";

        /// <summary>
        /// Тип события
        /// </summary>
        public override EventType Type { get; } = EventType.GameStream;

        /// <summary>
        /// Контент первого действия
        /// </summary>
        public override string FirstButtonText { get; } = "Согласиться на стрим";

        /// <summary>
        /// Контент второго действия
        /// </summary>
        public override string SecondButtonText { get; } = "Отказаться от стрима";

        /// <summary>
        /// Обработчик первого варианта действия
        /// </summary>
        public override void OnFirtsButtonClick(EventManager eventManager) {
            HideEventButtons(eventManager.FirstButton, eventManager.SecondButton, eventManager.OkButton);
            eventManager.EventContent.text = "Стрим почти никто не смотрел, пока другой популярный стример не призвал своих фанатов поглумиться над вами. " +
                                             "В конечном счете все превратилось в дурдом и закончилось совсем не так, как ожидалось";
            var playerInfo = PlayerManager.GetInfo();
            var fansDecrease = PlayerManager.GetFansPercentValue();
            var income = playerInfo.Money > 500 ? playerInfo.Money / 100 * 8 : 50;
            if (playerInfo.Fans < fansDecrease) fansDecrease = playerInfo.Fans;
            playerInfo.Fans -= fansDecrease;
            playerInfo.Money += income;
            eventManager.EventReward.text = $"От вас ушло {NumberFormatter.FormatValue(fansDecrease)} фанатов. Заработано: {NumberFormatter.FormatValue(income)}";
            eventManager.StatsManager.UpdateStats();
        }

        /// <summary>
        /// Обработчик второго варианта действия
        /// </summary>
        public override void OnSecondButtonClick(EventManager eventManager) {
            HideEventButtons(eventManager.FirstButton, eventManager.SecondButton, eventManager.OkButton);
            eventManager.EventContent.text = "Вы отказались от стрима и ваши фанаты одобрили это решение. " +
                                             "Им нравится, что вы не кидаетесь на любое предложение ради денег и занимаетесь своим делом.";
            IncreaseFans(eventManager);
        }
    }
}