using Assets.Scripts.Managers;
using Assets.Scripts.Utils;

namespace Assets.Scripts.UI.Events.EventTemplates {
    public class TrackRemixEvent : BaseEvent {
        /// <summary>
        /// Название события
        /// </summary>
        public override string Name { get; } = "Ремиксовая напасть";

        /// <summary>
        /// Описание события
        /// </summary>
        public override string Content { get; } = "В сети появилось большое количество ремиксов на ваши трэки. " +
                                                  "Этим занимается одно небольшое сообщество умельцев. Что предпочтете с ними сделать?";

        /// <summary>
        /// Контент первого действия
        /// </summary>
        public override string FirstButtonText { get; } = "Пригрозить сообществу судом и заставить их прекратить";

        /// <summary>
        /// Контент второго действия
        /// </summary>
        public override string SecondButtonText { get; } = "Пропиарить сообщество и похвалить организаторов";

        /// <summary>
        /// Обработчик первого варианта действия
        /// </summary>
        public override void OnFirtsButtonClick(EventManager eventManager) {
            HideEventButtons(eventManager.FirstButton, eventManager.SecondButton, eventManager.OkButton);
            eventManager.EventContent.text = "Оказалось, что многим людям нравились творения этого сообщества. Ваши действия вызвали небольшой хейт в сети.";
            DecreaseFans(eventManager);
        }

        /// <summary>
        /// Обработчик второго варианта действия
        /// </summary>
        public override void OnSecondButtonClick(EventManager eventManager) {
            HideEventButtons(eventManager.FirstButton, eventManager.SecondButton, eventManager.OkButton);
            eventManager.EventContent.text = "Некоторые ремиксы получаются очень качественными и люди, которые их слущают, начинают " +
                                             "искать оригинал и становятся вашими фанатами. Одобрив сообщество, вы заслужили расположение людей!";
            IncreaseFans(eventManager);
        }
    }
}