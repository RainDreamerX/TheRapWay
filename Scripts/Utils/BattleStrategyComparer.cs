using System;
using Assets.Scripts.Enums;

namespace Assets.Scripts.Utils {
    /// <summary>
    /// Компаратор стратегий батла
    /// </summary>
    public class BattleStrategyComparer {
        /// <summary>
        /// Сравнивает стратегии
        /// Возвращает 1, если победил игрок
        /// - 1, если победил противник
        /// и 0 - если нет победителей
        /// </summary>
        public static int Compare(BattleStrategy playerStrategy, BattleStrategy rivalStrategy) {
            switch (playerStrategy) {
                case BattleStrategy.Common:
                    return CompareCommon(rivalStrategy);
                case BattleStrategy.Agressive:
                    return CompareAgressive(rivalStrategy);
                case BattleStrategy.Self:
                    return CompareSefl(rivalStrategy);
                case BattleStrategy.OnFacts:
                    return CompareOnFacts(rivalStrategy);
                case BattleStrategy.Relatives:
                    return CompareRelatives(rivalStrategy);
                case BattleStrategy.Philosophy:
                    return ComparePhilosophy(rivalStrategy);
                case BattleStrategy.Mock:
                    return CompareMock(rivalStrategy);
                default:
                    throw new ArgumentOutOfRangeException(nameof(playerStrategy), playerStrategy, null);
            }
        }

        /// <summary>
        /// Сравнивает обычную стратегию
        /// </summary>
        private static int CompareCommon(BattleStrategy rivalStrategy) {
            switch (rivalStrategy) {
                case BattleStrategy.Common:
                    return 0;
                case BattleStrategy.Agressive:
                    return -1;
                case BattleStrategy.Self:
                    return 0;
                case BattleStrategy.OnFacts:
                    return -1;
                case BattleStrategy.Relatives:
                    return -1;
                case BattleStrategy.Philosophy:
                    return 1;
                case BattleStrategy.Mock:
                    return -1;
                default:
                    throw new ArgumentOutOfRangeException(nameof(rivalStrategy), rivalStrategy, null);
            }
        }

        /// <summary>
        /// Сравнивает агрессивную стратегию
        /// </summary>
        private static int CompareAgressive(BattleStrategy rivalStrategy) {
            switch (rivalStrategy) {
                case BattleStrategy.Common:
                    return -1;
                case BattleStrategy.Agressive:
                    return 0;
                case BattleStrategy.Self:
                    return 1;
                case BattleStrategy.OnFacts:
                    return 1;
                case BattleStrategy.Relatives:
                    return 1;
                case BattleStrategy.Philosophy:
                    return -1;
                case BattleStrategy.Mock:
                    return -1;
                default:
                    throw new ArgumentOutOfRangeException(nameof(rivalStrategy), rivalStrategy, null);
            }
        }

        /// <summary>
        /// Сравнивает стратегию "О себе"
        /// </summary>
        private static int CompareSefl(BattleStrategy rivalStrategy) {
            switch (rivalStrategy) {
                case BattleStrategy.Common:
                    return 1;
                case BattleStrategy.Agressive:
                    return -1;
                case BattleStrategy.Self:
                    return 0;
                case BattleStrategy.OnFacts:
                    return 1;
                case BattleStrategy.Relatives:
                    return 1;
                case BattleStrategy.Philosophy:
                    return -1;
                case BattleStrategy.Mock:
                    return 1;
                default:
                    throw new ArgumentOutOfRangeException(nameof(rivalStrategy), rivalStrategy, null);
            }
        }

        /// <summary>
        /// Сравнивает стратегию "По фактам"
        /// </summary>
        private static int CompareOnFacts(BattleStrategy rivalStrategy) {
            switch (rivalStrategy) {
                case BattleStrategy.Common:
                    return 1;
                case BattleStrategy.Agressive:
                    return -1;
                case BattleStrategy.Self:
                    return -1;
                case BattleStrategy.OnFacts:
                    return 0;
                case BattleStrategy.Relatives:
                    return 0;
                case BattleStrategy.Philosophy:
                    return 1;
                case BattleStrategy.Mock:
                    return -1;
                default:
                    throw new ArgumentOutOfRangeException(nameof(rivalStrategy), rivalStrategy, null);
            }
        }

        /// <summary>
        /// Сравнивает стратегию "Затронуть близких"
        /// </summary>
        private static int CompareRelatives(BattleStrategy rivalStrategy) {
            switch (rivalStrategy) {
                case BattleStrategy.Common:
                    return 1;
                case BattleStrategy.Agressive:
                    return 0;
                case BattleStrategy.Self:
                    return -1;
                case BattleStrategy.OnFacts:
                    return 0;
                case BattleStrategy.Relatives:
                    return 0;
                case BattleStrategy.Philosophy:
                    return -1;
                case BattleStrategy.Mock:
                    return 1;
                default:
                    throw new ArgumentOutOfRangeException(nameof(rivalStrategy), rivalStrategy, null);
            }
        }

        /// <summary>
        /// Сравнивает стратегию "Философствовать"
        /// </summary>
        private static int ComparePhilosophy(BattleStrategy rivalStrategy) {
            switch (rivalStrategy) {
                case BattleStrategy.Common:
                    return -1;
                case BattleStrategy.Agressive:
                    return 1;
                case BattleStrategy.Self:
                    return 1;
                case BattleStrategy.OnFacts:
                    return -1;
                case BattleStrategy.Relatives:
                    return 1;
                case BattleStrategy.Philosophy:
                    return 0;
                case BattleStrategy.Mock:
                    return -1;
                default:
                    throw new ArgumentOutOfRangeException(nameof(rivalStrategy), rivalStrategy, null);
            }
        }

        /// <summary>
        /// Сравнивает стратегию "Насмехаться"
        /// </summary>
        private static int CompareMock(BattleStrategy rivalStrategy) {
            switch (rivalStrategy) {
                case BattleStrategy.Common:
                    return 1;
                case BattleStrategy.Agressive:
                    return 1;
                case BattleStrategy.Self:
                    return -1;
                case BattleStrategy.OnFacts:
                    return 1;
                case BattleStrategy.Relatives:
                    return -1;
                case BattleStrategy.Philosophy:
                    return 1;
                case BattleStrategy.Mock:
                    return 0;
                default:
                    throw new ArgumentOutOfRangeException(nameof(rivalStrategy), rivalStrategy, null);
            }
        }
    }
}