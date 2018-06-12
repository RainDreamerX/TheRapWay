namespace Assets.Scripts.UI.Events.EventTemplates {
    public class DissEvent : BaseEvent {
        /// <summary>
        /// Название события
        /// </summary>
        public override string Name { get; } = "Оскорбительный дисс";

        /// <summary>
        /// Описание события
        /// </summary>
        public override string Content { get; } = "Ваш конкурент написал на вас дисс. В нем он прошелся по вам, вашему творчеству и близких. " +
                                                  "Вокруг этого дисса поднялось много шума в сети и теперь все ждут вашу реакцию";

        /// <summary>
        /// Контент первого действия
        /// </summary>
        public override string FirstButtonText { get; } = "Подождать пока все забудут";

        /// <summary>
        /// Контент второго действия
        /// </summary>
        public override string SecondButtonText { get; } = "Написать ответный дисс";

        /// <summary>
        /// Обработчик первого варианта действия
        /// </summary>
        public override void OnFirtsButtonClick(EventManager eventManager) {
            HideEventButtons(eventManager.FirstButton, eventManager.SecondButton, eventManager.OkButton);
            eventManager.EventContent.text = "Фанаты сильно разочарованы вашим бездействием. Многие посчитали, что вы просто испугались";
            DecreaseFans(eventManager, 2);
        }

        /// <summary>
        /// Обработчик второго варианта действия
        /// </summary>
        public override void OnSecondButtonClick(EventManager eventManager) {
            HideEventButtons(eventManager.FirstButton, eventManager.SecondButton, eventManager.OkButton);
            eventManager.EventContent.text = "Ваши фанаты в восторге и считают, что вы поставили выскочку на своё место. Многие довольны тем, с какой " +
                                             "ответственностью вы отнеслись к этому событию";
            IncreaseFans(eventManager, 2);
        }
    }
}