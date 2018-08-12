using Assets.Scripts.Enums;

namespace Assets.Scripts.UI.Events.EventTemplates {
    public class TrollingEvent : BaseEvent {
        /// <summary>
        /// Название события
        /// </summary>
        public override string Name { get; } = "Толстые тролли";

        /// <summary>
        /// Описание события
        /// </summary>
        public override string Content { get; } = "В различных социальных сетях под вашими записями иногда мелькают неприятные " +
                                                  "и оскорбительные комментарии. Какова будет ваша реакция?";

        /// <summary>
        /// Тип события
        /// </summary>
        public override EventType Type { get; } = EventType.Trolling;

        /// <summary>
        /// Контент первого действия
        /// </summary>
        public override string FirstButtonText { get; } = "Ответить комментаторам в их стиле";

        /// <summary>
        /// Контент второго действия
        /// </summary>
        public override string SecondButtonText { get; } = "Игнорировать комментарии";

        /// <summary>
        /// Обработчик первого варианта действия
        /// </summary>
        public override void OnFirtsButtonClick(EventManager eventManager) {
            HideEventButtons(eventManager.FirstButton, eventManager.SecondButton, eventManager.OkButton);
            eventManager.EventContent.text = "Вы вступаете в яростную перепалку с обидчиками и это напоминает детский сад. " +
                                             "Тролли видят, что вам не все равно и стараются еще больше!";
            DecreaseFans(eventManager);
        }

        /// <summary>
        /// Обработчик второго варианта действия
        /// </summary>
        public override void OnSecondButtonClick(EventManager eventManager) {
            HideEventButtons(eventManager.FirstButton, eventManager.SecondButton, eventManager.OkButton);
            eventManager.EventContent.text = "Ваши преданные фанаты заступаются за вас и их гораздо больше. " +
                                             "Похоже, игнорировать шутников оказалось хорошим решением";
            eventManager.EventReward.text = "Изменений нет";
            eventManager.StatsManager.UpdateStats();
        }
    }
}