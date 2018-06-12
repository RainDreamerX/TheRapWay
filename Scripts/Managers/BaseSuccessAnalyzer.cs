using System;
using Assets.Scripts.Enums;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Managers {
    /// <summary>
    /// Базовый анализатор действий игрока
    /// </summary>
    public class BaseSuccessAnalyzer {
        /// <summary>
        /// Возвращает истину, если нужно вернуть случайную оценку
        /// </summary>
        protected static bool RandomizeGrade() {
            var value = Random.Range(0, 100);
            return value < 20;
        }

        /// <summary>
        /// Возвращает случайную оценку
        /// </summary>
        protected static SuccessGrade GetRandomGrade() {
            var grades = Enum.GetValues(typeof(SuccessGrade));
            return (SuccessGrade) grades.GetValue(Random.Range(0, grades.Length));
        }

        /// <summary>
        /// Возвращает оценку успешности
        /// </summary>
        protected static SuccessGrade GetGrade(int points) {
            if (points > 80) return SuccessGrade.Highest;
            if (points > 60) return SuccessGrade.Hight;
            if (points > 40) return SuccessGrade.Middle;
            if (points > 20) return SuccessGrade.Low;
            return SuccessGrade.Lowest;
        }
    }
}