using System;
using Assets.Scripts.Enums;

namespace Assets.Scripts.Models {
    /// <summary>
    /// Объект для сохранения
    /// </summary>
    [Serializable]
    public class DataToSave {
        /// <summary>
        /// Данные игрока
        /// </summary>
        public PlayerInfo PlayerInfo;

        /// <summary>
        /// Количество сыгранных дней
        /// </summary>
        public int DaysPlayed;

        /// <summary>
        /// Трендовая тема
        /// </summary>
        public TrackTheme TrandTheme;

        /// <summary>
        /// Трендовый стиль
        /// </summary>
        public TrackStyle TrandStyle;

        /// <summary>
        /// В трэнде ли автотюн
        /// </summary>
        public bool AutotuneTrand;
    }
}