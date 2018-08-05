using System;
using Assets.Scripts.Enums;

namespace Assets.Scripts.Models {
    /// <summary>
    /// Модель игрока
    /// </summary>
    [Serializable]
    public class PlayerInfo {
        /// <summary>
        /// Имя
        /// </summary>
        public string Name;

        /// <summary>
        /// Количество фанатов
        /// </summary>
        public int Fans;

        /// <summary>
        /// Количество денег
        /// </summary>
        public int Money;

        /// <summary>
        /// Открыта ли лента соц. сетей
        /// </summary>
        public bool TweetsUnlocked;

        /// <summary>
        /// Открыты ли события
        /// </summary>
        public bool EventUnlocked;

        /// <summary>
        /// Показывался ли победный экран для концерта
        /// </summary>
        public bool ConcertScreenWin;

        /// <summary>
        /// Показывался ли победный экран для батла
        /// </summary>
        public bool BattleScreenWin;

        /// <summary>
        /// Навыки игрока
        /// </summary>
        public PlayerSkills PlayerSkills;

        /// <summary>
        /// Имущество игрока
        /// </summary>
        public PlayerProperty PlayerProperty;

        /// <summary>
        /// Последний трэк
        /// </summary>
        public NewTrackModel LastTrack;
    }
}