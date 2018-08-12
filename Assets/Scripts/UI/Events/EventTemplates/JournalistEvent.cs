using Assets.Scripts.Enums;

namespace Assets.Scripts.UI.Events.EventTemplates {
    public class JournalistEvent : BaseEvent {
        /// <summary>
        /// Название события
        /// </summary>
        public override string Name { get; } = "Наглый журналист";

        /// <summary>
        /// Описание события
        /// </summary>
        public override string Content { get; } = "Вас будят настойчивые звонки в дверь. С помятым видом, вы открываете её " +
                                                  "и видите журналиста, который сразу начинает снимать вас на телефон.";

        /// <summary>
        /// Тип события
        /// </summary>
        public override EventType Type { get; } = EventType.Journalist;

        /// <summary>
        /// Контент первого действия
        /// </summary>
        public override string FirstButtonText { get; } = "Выхватить телефон из его рук и разбить";

        /// <summary>
        /// Контент второго действия
        /// </summary>
        public override string SecondButtonText { get; } = "Захлопнуть дверь перед его лицом";

        /// <summary>
        /// Обработчик первого варианта действия
        /// </summary>
        public override void OnFirtsButtonClick(EventManager eventManager) {
            HideEventButtons(eventManager.FirstButton, eventManager.SecondButton, eventManager.OkButton);
            eventManager.EventContent.text = "Журналист написал гневный пост о разбитом телефоне. Но большинство людей возмутились его варварским " +
                                             "нарушением личного пространства и приняли вашу сторону!";
            IncreaseFans(eventManager);
        }

        /// <summary>
        /// Обработчик второго варианта действия
        /// </summary>
        public override void OnSecondButtonClick(EventManager eventManager) {
            HideEventButtons(eventManager.FirstButton, eventManager.SecondButton, eventManager.OkButton);
            eventManager.EventContent.text = "Журналист оказался из желтой прессы и написал статью о том, что вы ведете разгульный образ жизни, " +
                                             "дополнив ее этими не самыми удачными снимками. Многие поверили ему!";
            DecreaseFans(eventManager, 2);
        }
    }
}