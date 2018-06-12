using Assets.Scripts.Enums;

namespace Assets.Scripts.Models {
    /// <summary>
    /// Данные результата действия
    /// </summary>
    public class ActionResultModel {
        /// <summary>
        /// Название действия
        /// </summary>
        public ActionType Action { get; set; }

        /// <summary>
        /// Прирост фанатов
        /// </summary>
        public int FansIncrease { get; set; }

        /// <summary>
        /// Доход
        /// </summary>
        public int Income { get; set; }

        /// <summary>
        /// Место в рейтингах
        /// </summary>
        public int Top { get; set; }

        /// <summary>
        /// Численный коэф. популярности
        /// Это м.б. кол-во скачиваний / просмотров или посещение концерта
        /// </summary>
        public int Popularity { get; set; }

        /// <summary>
        /// Сообщение результата тренировки
        /// </summary>
        public string Traning { get; set; }

        /// <summary>
        /// Успешность
        /// </summary>
        public SuccessGrade Grade { get; set; }
    }
}