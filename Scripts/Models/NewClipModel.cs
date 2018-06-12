namespace Assets.Scripts.Models {
    /// <summary>
    /// Модель данных нового клипа
    /// </summary>
    public class NewClipModel {
        /// <summary>
        /// Трэк, на который снимается клип
        /// </summary>
        public NewTrackModel Track { get; set; }

        /// <summary>
        /// Есть ли сценарист
        /// </summary>
        public bool HasScreenwritter { get; set; }

        /// <summary>
        /// Есть ли оператор
        /// </summary>
        public bool HasOperator { get; set; }

        /// <summary>
        /// Есть ли режиссер
        /// </summary>
        public bool HasProducer { get; set; }

        /// <summary>
        /// Есть ли звукорежиссер
        /// </summary>
        public bool HasSoundProducer { get; set; }
    }
}