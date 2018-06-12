namespace Assets.Scripts.UI.Events.EventTemplates {
    /// <summary>
    /// Посиделка с другом перед концертом
    /// </summary>
    public class FriendEvent : BaseEvent {
        /// <summary>
        /// Название события
        /// </summary>
        public override string Name { get; } = "Тот самый друг";

        /// <summary>
        /// Описание события
        /// </summary>
        public override string Content { get; } = "За день до вашего выступления на одном мероприятии, вам позвонил ваш давний хороший друг, сказал, " +
                                                  "что давно не виделись и предложил встретиться и отдохнуть. Каков будет ответ?";

        /// <summary>
        /// Контент первого действия
        /// </summary>
        public override string FirstButtonText { get; } = "Перенести встречу на потом";

        /// <summary>
        /// Контент второго действия
        /// </summary>
        public override string SecondButtonText { get; } = "Согласиться провести время с другом";

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
            eventManager.EventContent.text = "Вы собрались дома, пили пиво и увлеченно болтали. Бутылка за бутылкой и вы уже оказались в клубе за барной стойкой. " +
                                             "Проснувшись на утро, вы осознали, что проспали мероприятие, а на телефоне была куча пропущенных звонков от организаторов...";
            DecreaseFans(eventManager, 3);
        }
    }
}