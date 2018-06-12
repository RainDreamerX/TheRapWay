using System;
using Assets.Scripts.Enums;

namespace Assets.Scripts.Models {
    /// <summary>
    /// Модель нового трэка
    /// </summary>
    [Serializable]
    public class NewTrackModel {
        /// <summary>
        /// Идентификатор трэка
        /// </summary>
        public int Id;

        /// <summary>
        /// Тема
        /// </summary>
        public TrackTheme Theme;

        /// <summary>
        /// Стилистика
        /// </summary>
        public TrackStyle Style;

        /// <summary>
        /// Источник текста
        /// </summary>
        public TextSourse TextSourse;

        /// <summary>
        /// Источник бита
        /// </summary>
        public BitSource BitSource;

        /// <summary>
        /// Присутствует ли автотюн
        /// </summary>
        public bool Autotune;

        /// <summary>
        /// Участник фита, если есть
        /// </summary>
        public RapperModel Feat;

        /// <summary>
        /// Оценка
        /// </summary>
        public SuccessGrade Grade;
    }
}