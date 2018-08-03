using Assets.Scripts.Enums;

namespace Assets.Scripts.Models {
    /// <summary>
    /// Модель состоявщегося баттла
    /// </summary>
    public class VersusBattleModel {
        /// <summary>
        /// Оппонент
        /// </summary>
        public RapperModel Rival { get; set; }

        /// <summary>
        /// Источник текста
        /// </summary>
        public TextSourse TextSourse { set; get; }

        /// <summary>
        /// Стратегия
        /// </summary>
        public BattleStrategy Strategy { get; set; }

        /// <summary>
        /// Проверка соц. сетей оппонента
        /// </summary>
        public bool CheckSocials { get; set; }
    }
}