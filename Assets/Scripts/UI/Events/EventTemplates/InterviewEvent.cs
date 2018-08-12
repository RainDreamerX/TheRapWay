using Assets.Scripts.Enums;

namespace Assets.Scripts.UI.Events.EventTemplates {
    public class InterviewEvent : BaseEvent {
        /// <summary>
        /// Название события
        /// </summary>
        public override string Name { get; } = "Интервью";

        /// <summary>
        /// Описание события
        /// </summary>
        public override string Content { get; } = "Вы пришли на интервью к ведущему одного популярного канала на Youtube. " +
                                                  "Было задано много вопросов, в том числе не самых удобных и приятных. Станете ли вы отвечать на них?";

        /// <summary>
        /// Тип события
        /// </summary>
        public override EventType Type { get; } = EventType.Interview;

        /// <summary>
        /// Контент первого действия
        /// </summary>
        public override string FirstButtonText { get; } = "Не отвечать на каверзные вопросы";

        /// <summary>
        /// Контент второго действия
        /// </summary>
        public override string SecondButtonText { get; } = "Отвечать на любые вопросы";

        /// <summary>
        /// Обработчик первого варианта действия
        /// </summary>
        public override void OnFirtsButtonClick(EventManager eventManager) {
            HideEventButtons(eventManager.FirstButton, eventManager.SecondButton, eventManager.OkButton);
            eventManager.EventContent.text = "Вы отвечали только на поверхностные и скучные вопросы. Интервью вышло довольно скудным и многие остались разочарованы...";
            DecreaseFans(eventManager);
        }

        /// <summary>
        /// Обработчик второго варианта действия
        /// </summary>
        public override void OnSecondButtonClick(EventManager eventManager) {
            HideEventButtons(eventManager.FirstButton, eventManager.SecondButton, eventManager.OkButton);
            eventManager.EventContent.text = "Фанаты и зрители канала оценили вашу честность и открытость, и остались довольны. " +
                                             "Получилось очень интересное интервью!";
            IncreaseFans(eventManager, 2);
        }
    }
}