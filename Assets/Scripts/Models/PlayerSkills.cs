using System;
using System.Collections.Generic;
using Assets.Scripts.Enums;

namespace Assets.Scripts.Models {
    /// <summary>
    /// Навыки игрока
    /// </summary>
    [Serializable]
    public class PlayerSkills {
        /// <summary>
        /// Навык читки
        /// </summary>
        public int Flow;

        /// <summary>
        /// Словарный запас
        /// </summary>
        public int Vocabulary;

        /// <summary>
        /// Мастерство создания битов
        /// </summary>
        public int BitMaking;

        /// <summary>
        /// Доступна ли возможность проверять соц. сети оппонента перед баттлом
        /// </summary>
        public bool CanCheckSocials;

        /// <summary>
        /// Доступные темы трэков
        /// </summary>
        public List<TrackTheme> TrackThemes;

        /// <summary>
        /// Доступные стили трэков
        /// </summary>
        public List<TrackStyle> TrackStyles;

        /// <summary>
        /// Варианты получения текста
        /// </summary>
        public List<TextSourse> TextSourses;

        /// <summary>
        /// Варианты получения бита
        /// </summary>
        public List<BitSource> BitSources;

        /// <summary>
        /// Стратегии в баттле
        /// </summary>
        public List<BattleStrategy> BattleStrategies;
    }
}