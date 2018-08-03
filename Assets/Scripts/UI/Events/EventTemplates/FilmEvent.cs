using Assets.Scripts.Managers;
using Assets.Scripts.Utils;

namespace Assets.Scripts.UI.Events.EventTemplates {
    public class FilmEvent : BaseEvent {
        /// <summary>
        /// Название события
        /// </summary>
        public override string Name { get; } = "Кинозвезда";

        /// <summary>
        /// Описание события
        /// </summary>
        public override string Content { get; } = "Вам предложили сыграть роль уличного МС в одном отечественном фильме. " +
                                                  "Экранного времени совсем не много, но обещают неплохо заплатить. Режиссер ждет ваш ответ.";

        /// <summary>
        /// Контент первого действия
        /// </summary>
        public override string FirstButtonText { get; } = "Принять предложение и поехать на съемки";

        /// <summary>
        /// Контент второго действия
        /// </summary>
        public override string SecondButtonText { get; } = "Отказаться от роли";

        /// <summary>
        /// Обработчик первого варианта действия
        /// </summary>
        public override void OnFirtsButtonClick(EventManager eventManager) {
            HideEventButtons(eventManager.FirstButton, eventManager.SecondButton, eventManager.OkButton);
            eventManager.EventContent.text = "Фильм получился не очень успешным, но вы смотрелись не плохо! " +
                                             "Критика фильма обошла вас стороной, можно считать это везением.";
            var playerInfo = PlayerManager.GetInfo();
            var fansIncrease = PlayerManager.GetFansPercentValue();
            var income = playerInfo.Money > 200 ? playerInfo.Money / 100 * 2 : 50;
            if (playerInfo.Fans < fansIncrease) fansIncrease = playerInfo.Fans;
            playerInfo.Fans += fansIncrease;
            playerInfo.Money += income;
            eventManager.EventReward.text = $"Пришло {NumberFormatter.FormatValue(fansIncrease)} новых фанатов. Заработано: {NumberFormatter.FormatValue(income)}";
            eventManager.StatsManager.UpdateStats();
        }

        /// <summary>
        /// Обработчик второго варианта действия
        /// </summary>
        public override void OnSecondButtonClick(EventManager eventManager) {
            HideEventButtons(eventManager.FirstButton, eventManager.SecondButton, eventManager.OkButton);
            eventManager.EventContent.text = "Фильм получился очень посредственным и провалился в прокате. " +
                                             "Ваши фанаты активно хвалят вас за отказ от участия!";
            IncreaseFans(eventManager, 2);
        }
    }
}