using System.Collections.Generic;
using Assets.Scripts.Enums;

namespace Assets.Scripts.Managers {
    /// <summary>
    /// Логика определения прироста
    /// </summary>
    public class GradePercentManager {
        /// <summary>
        /// Процентная награда за трэки
        /// </summary>
        private static readonly Dictionary<SuccessGrade, float> trackPercents = new Dictionary<SuccessGrade, float> {
            {SuccessGrade.Lowest, .2f},
            {SuccessGrade.Low, .4f},
            {SuccessGrade.Middle, .6f},
            {SuccessGrade.Hight, .8f},
            {SuccessGrade.Highest, 1f}
        };

        /// <summary>
        /// Процентная награда за клипы
        /// </summary>
        private static readonly Dictionary<SuccessGrade, float> clipPercents = new Dictionary<SuccessGrade, float> {
            {SuccessGrade.Lowest, .3f},
            {SuccessGrade.Low, .5f},
            {SuccessGrade.Middle, .7f},
            {SuccessGrade.Hight, .9f},
            {SuccessGrade.Highest, 1.1f}
        };

        /// <summary>
        /// Процентная награда за батл
        /// </summary>
        private static readonly Dictionary<SuccessGrade, float> battlePercents = new Dictionary<SuccessGrade, float> {
            {SuccessGrade.Lowest, -1f},
            {SuccessGrade.Low, -0.5f},
            {SuccessGrade.Middle, .1f},
            {SuccessGrade.Hight, .5f},
            {SuccessGrade.Highest, 1f}
        };

        /// <summary>
        /// Возвращает процент награды для трэка
        /// </summary>
        public static float GetRewardPercent(SuccessGrade grade, ActionType type) {
            var percent = 0f;
            switch (type) {
                case ActionType.NewTrack:
                case ActionType.Feat:
                    percent = trackPercents[grade];
                    break;
                case ActionType.NewClip:
                    percent = clipPercents[grade];
                    break;
                case ActionType.Battle:
                    percent = battlePercents[grade];
                    break;
            }
            return percent * GetFansCoef();
        }

        /// <summary>
        /// Возвращает коэффициент в зависимости от количества фанатов
        /// </summary>
        private static int GetFansCoef() {
            var fansCount = PlayerManager.GetInfo().Fans;
            if (fansCount >= 1000000) return 1;
            return fansCount >= 100000 ? 5 : 10;
        }
    }
}