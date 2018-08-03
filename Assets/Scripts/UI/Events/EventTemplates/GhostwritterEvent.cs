using Assets.Scripts.Enums;
using Assets.Scripts.Managers;

namespace Assets.Scripts.UI.Events.EventTemplates {
    public class GhostwritterEvent : BaseEvent {
        /// <summary>
        /// Название события
        /// </summary>
        public override string Name { get; } = "Гострайтер";

        /// <summary>
        /// Описание события
        /// </summary>
        public override string Content { get; } = "К вам добавился в друзья парень и написал, что занимается созданием текстов на заказ для некоторых исполнителей. " +
                                                  "За небольшую плату он готов писать и для вас.";

        /// <summary>
        /// Контент первого действия
        /// </summary>
        public override string FirstButtonText { get; } = "Добавить его в друзья";

        /// <summary>
        /// Контент второго действия
        /// </summary>
        public override string SecondButtonText { get; } = "Оставить в подписчиках";

        /// <summary>
        /// Обработчик первого варианта действия
        /// </summary>
        public override void OnFirtsButtonClick(EventManager eventManager) {
            HideEventButtons(eventManager.FirstButton, eventManager.SecondButton, eventManager.OkButton);
            eventManager.EventContent.text = "Теперь у вас в друзьях есть гострайтер. Вы можете воспользоваться его услугами " +
                                             "при создании нового трэка или при подготовке к батлу.";
            AddGhostwritterSkill(eventManager);
        }

        /// <summary>
        /// Обработчик второго варианта действия
        /// </summary>
        public override void OnSecondButtonClick(EventManager eventManager) {
            HideEventButtons(eventManager.FirstButton, eventManager.SecondButton, eventManager.OkButton);
            eventManager.EventContent.text = "Вы ничего не ответили гострайтеру и оставили его в подписчиках, однако запомнили о новой возможности.";
            AddGhostwritterSkill(eventManager);
        }

        /// <summary>
        /// Добавляет новый источник текстов
        /// </summary>
        private static void AddGhostwritterSkill(EventManager eventManager) {
            eventManager.EventReward.text = "Новый источник текста: Гострайтер";
            PlayerManager.GetSkills().TextSourses.Add(TextSourse.Ghostwriter);
        }
    }
}