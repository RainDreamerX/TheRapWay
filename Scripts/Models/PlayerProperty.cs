using System;
using Assets.Scripts.Enums;

namespace Assets.Scripts.Models {
    /// <summary>
    /// Купленное имущество
    /// </summary>
    [Serializable]
    public class PlayerProperty {
        /// <summary>
        /// Микрофон
        /// </summary>
        public PropertyLevel Micro;

        /// <summary>
        /// Лаунчпад
        /// </summary>
        public PropertyLevel Launchpad;

        /// <summary>
        /// Может ли использовать автотюн
        /// </summary>
        public bool HasAutotune;

        /// <summary>
        /// Текущий дом
        /// </summary>
        public int House;
    }
}