namespace Assets.Scripts.Models {
    /// <summary>
    /// Модель концертного зала
    /// </summary>
    public class ConcertPlaceModel {
        /// <summary>
        /// Название места
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Город
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Общая вместимость
        /// </summary>
        public int Capacity { get; set; }

        /// <summary>
        /// Стоимость аренды
        /// </summary>
        public int Rent { get; set; }
    }
}