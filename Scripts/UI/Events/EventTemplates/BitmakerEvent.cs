using Assets.Scripts.Enums;
using Assets.Scripts.Managers;

namespace Assets.Scripts.UI.Events.EventTemplates {
    public class BitmakerEvent : BaseEvent {
        /// <summary>
        /// Название события
        /// </summary>
        public override string Name { get; } = "Битмэйкер";

        /// <summary>
        /// Описание события
        /// </summary>
        public override string Content { get; } = "С утра вы проверяли почту и обнаружили письмо. В нем говорилось о том, что человек профессионально " +
                                                  "занимается созданием качественных битов за вознаграждение и открыт для сотрудничества с вами.";

        /// <summary>
        /// Контент первого действия
        /// </summary>
        public override string FirstButtonText { get; } = "Написать ответное письмо с благодарностью";

        /// <summary>
        /// Контент второго действия
        /// </summary>
        public override string SecondButtonText { get; } = "Проигнорировать письмо";

        /// <summary>
        /// Обработчик первого варианта действия
        /// </summary>
        public override void OnFirtsButtonClick(EventManager eventManager) {
            HideEventButtons(eventManager.FirstButton, eventManager.SecondButton, eventManager.OkButton);
            eventManager.EventContent.text = "В вашем контактном листе появился битмэйкер. За небольшое вознаграждение он сделает для вас хороший бит для нового трэка.";
            AddBitmakerSkill(eventManager);
        }

        /// <summary>
        /// Обработчик второго варианта действия
        /// </summary>
        public override void OnSecondButtonClick(EventManager eventManager) {
            HideEventButtons(eventManager.FirstButton, eventManager.SecondButton, eventManager.OkButton);
            eventManager.EventContent.text = "Вы проигнорировали письмо, но записали контактные данные битмэйкера. " +
                                             "За небольшое вознаграждение он сделает хороший бит для вашего нового трэка.";
            AddBitmakerSkill(eventManager);
        }

        /// <summary>
        /// Добавляет новый источник битов
        /// </summary>
        private static void AddBitmakerSkill(EventManager eventManager) {
            eventManager.EventReward.text = "Новый источник битов: Битмэйкер";
            PlayerManager.GetSkills().BitSources.Add(BitSource.Bitmaker);
        }
    }
}