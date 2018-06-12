using System;
using Assets.Scripts.Enums;
using Assets.Scripts.Models;

namespace Assets.Scripts.Managers {
    /// <summary>
    /// Логика вычисления
    /// </summary>
    public class ClipSuccessAnalyzer : BaseSuccessAnalyzer {
        private const int SETTING_POINTS = 10;

        /// <summary>
        /// Анализирует успешность клипа
        /// </summary>
        public static SuccessGrade AnalyzeClip(NewClipModel clip) {
            if (RandomizeGrade()) return GetRandomGrade();
            var points = GetTrackPoints(clip.Track);
            points += clip.HasScreenwritter ? SETTING_POINTS : 0;
            points += clip.HasOperator ? SETTING_POINTS : 0;
            points += clip.HasProducer ? SETTING_POINTS : 0;
            points += clip.HasSoundProducer ? SETTING_POINTS : 0;
            return GetGrade(points);
        }

        /// <summary>
        /// Возвращает очки за трэк
        /// </summary>
        private static int GetTrackPoints(NewTrackModel track) {
            switch (track.Grade) {
                case SuccessGrade.Lowest:
                    return 15;
                case SuccessGrade.Low:
                    return 30;
                case SuccessGrade.Middle:
                    return 40;
                case SuccessGrade.Hight:
                    return 50;
                case SuccessGrade.Highest:
                    return 60;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}