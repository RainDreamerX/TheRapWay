using System;
using Assets.Scripts.Enums;
using Assets.Scripts.Models;
using Assets.Scripts.Utils;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Managers {
    /// <summary>
    /// Анализатор успеха батла
    /// </summary>
    public class BattleSuccessAnalyzer : BaseSuccessAnalyzer {
        /// <summary>
        /// Анализирует успешность трэка
        /// </summary>
        public static SuccessGrade AnalyzeBattle(VersusBattleModel battleModel) {
            if (RandomizeGrade()) return GetRandomGrade();
            var points = GetStrategyPoint(battleModel.Strategy);
            points += GetFlowPoints(battleModel.Rival);
            points += battleModel.TextSourse == TextSourse.Self ? PlayerManager.GetSkills().Vocabulary * 2 : 20;
            points += battleModel.CheckSocials ? 10 : 0;
            return GetGrade(points);
        }

        /// <summary>
        /// Сравнивает стратегии и возвращает очки
        /// </summary>
        private static int GetStrategyPoint(BattleStrategy playerStrategy) {
            var strategies = Enum.GetValues(typeof(BattleStrategy));
            var rivalStrategy = (BattleStrategy)strategies.GetValue(Random.Range(0, strategies.Length));
            var strategyPoint = BattleStrategyComparer.Compare(playerStrategy, rivalStrategy);
            switch (strategyPoint) {
                case 0:
                    return 20;
                case 1:
                    return 40;
                default:
                    return 0;
            }
        }

        /// <summary>
        /// Возвращает очки за флоу
        /// </summary>
        private static int GetFlowPoints(RapperModel rival) {
            var flowDifference = PlayerManager.GetSkills().Flow - rival.Flow;
            return 20 + flowDifference;
        }
    }
}