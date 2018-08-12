using Assets.Scripts.Enums;

namespace Assets.Scripts.UI.Events.EventTemplates {
    /// <summary>
    /// Посиделка с другом перед концертом
    /// </summary>
    public class GirlFriendEvent : BaseEvent {
        /// <summary>
        /// Название события
        /// </summary>
        public override string Name { get; } = "Близкая подруга";

        /// <summary>
        /// Описание события
        /// </summary>
        public override string Content { get; } = "За день до вашего выступления на одном мероприятии, вам позвонила ваша давняя хорошая подруга, сказала, " +
                                                  "что давно не виделись и предложила провести время вместе. Каков будет ответ?";

        /// <summary>
        /// Тип события
        /// </summary>
        public override EventType Type { get; } = EventType.GirlFriend;

        /// <summary>
        /// Контент первого действия
        /// </summary>
        public override string FirstButtonText { get; } = "Перенести встречу на потом";

        /// <summary>
        /// Контент второго действия
        /// </summary>
        public override string SecondButtonText { get; } = "Согласиться и поехать к подруге";

        /// <summary>
        /// Обработчик первого варианта действия
        /// </summary>
        public override void OnFirtsButtonClick(EventManager eventManager) {
            HideEventButtons(eventManager.FirstButton, eventManager.SecondButton, eventManager.OkButton);
            eventManager.EventContent.text = "Вы потратили вечер на репетицию перед выступлением и пораньше легли спать. На утро вы проснулись в отличном самочувствии, " +
                                             "вовремя приехали и здорово выступили. Фанаты и организаторы остались очень довольны";
            IncreaseFans(eventManager);
        }

        /// <summary>
        /// Обработчик второго варианта действия
        /// </summary>
        public override void OnSecondButtonClick(EventManager eventManager) {
            HideEventButtons(eventManager.FirstButton, eventManager.SecondButton, eventManager.OkButton);
            eventManager.EventContent.text = "Вы сидели у нее дома, выпивали и увлеченно болтали. Бутылка за бутылкой и вы уже оказались в ночном клубе. " +
                                             "После него вы гуляли по ночному городу и вместе встретили рассвет. " +
                                             "На концерте вы были совсем уставшим и думали лишь о том, как скорее лечь спать.";
            DecreaseFans(eventManager, 3);
        }
    }
}