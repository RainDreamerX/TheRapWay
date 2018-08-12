using Assets.Scripts.Enums;

namespace Assets.Scripts.UI.Events.EventTemplates {
    public class KidsFansEvent : BaseEvent {
        /// <summary>
        /// Название события
        /// </summary>
        public override string Name { get; } = "Юные фанаты";

        /// <summary>
        /// Описание события
        /// </summary>
        public override string Content { get; } = "Вы опаздываете в аэропорт, выбегаете из подъезда во двор и вас окружает местные детишки. " +
                                                  "Они шумят, наперебой просят автограф или совместную фотографию. Ваши действия?";

        /// <summary>
        /// Тип события
        /// </summary>
        public override EventType Type { get; } = EventType.KidsFans;

        /// <summary>
        /// Контент первого действия
        /// </summary>
        public override string FirstButtonText { get; } = "Задержаться и раздать автографы с фотографиями";

        /// <summary>
        /// Контент второго действия
        /// </summary>
        public override string SecondButtonText { get; } = "Отмахнуться от детей и поспешить в аэропорт";

        /// <summary>
        /// Обработчик первого варианта действия
        /// </summary>
        public override void OnFirtsButtonClick(EventManager eventManager) {
            HideEventButtons(eventManager.FirstButton, eventManager.SecondButton, eventManager.OkButton);
            eventManager.EventContent.text = "Вы опоздали на самолет, но вам удалось обменять билет на дополнительный рейс часом позже. " +
                                             "В свою очередь, дети выложили совместные фото в соц. сетях и написали много приятных слов.";
            IncreaseFans(eventManager, 3);
        }

        /// <summary>
        /// Обработчик второго варианта действия
        /// </summary>
        public override void OnSecondButtonClick(EventManager eventManager) {
            HideEventButtons(eventManager.FirstButton, eventManager.SecondButton, eventManager.OkButton);
            eventManager.EventContent.text = "Дети проводили вас разочарованными возгласами и грустными взглядами. Зато вы успели на самолет";
            DecreaseFans(eventManager, 2);
        }
    }
}