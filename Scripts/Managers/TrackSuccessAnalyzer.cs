using System;
using Assets.Scripts.Enums;
using Assets.Scripts.Models;
using Assets.Scripts.UI;

namespace Assets.Scripts.Managers {
    /// <summary>
    /// Логика анализа успешности действия
    /// </summary>
    public class TrackSuccessAnalyzer : BaseSuccessAnalyzer {
        private const int MAX_CATEGORY_POINTS = 20;
        private const int MAX_TRAND_POINTS = 7;

        /// <summary>
        /// Анализировать успешность трека
        /// </summary>
        public static SuccessGrade AnalyzeTrack(NewTrackModel track) {
            if (RandomizeGrade()) return GetRandomGrade();
            var playerSkills = PlayerManager.GetSkills();
            var points = playerSkills.Flow * 2;
            points += track.TextSourse == TextSourse.Self ? 2 * playerSkills.Vocabulary : MAX_CATEGORY_POINTS;
            points += GetPointsForBit(track.BitSource, playerSkills);
            points += GetTrandsPoints(track);
            points += GetFeatPoints(track);
            return GetGrade(points);
        }

        /// <summary>
        /// Возвращает очки за текст
        /// </summary>
        private static int GetPointsForBit(BitSource sourse, PlayerSkills playerSkills) {
            switch (sourse) {
                case BitSource.Free:
                    return 0;
                case BitSource.Self:
                    return 2 * playerSkills.BitMaking;
                case BitSource.Bitmaker:
                    return MAX_CATEGORY_POINTS;
                default:
                    throw new ArgumentOutOfRangeException(nameof(sourse), sourse, null);
            }
        }

        /// <summary>
        /// Возвращает количество очков за фит
        /// </summary>
        private static int GetFeatPoints(NewTrackModel track) {
            if (track.Feat == null) return 0;
            var playerFans = PlayerManager.GetInfo().Fans;
            if (Math.Abs(playerFans - track.Feat.Fans) < GetFansInaccurancy(playerFans)) {
                return 20;
            }
            return playerFans < track.Feat.Fans ? MAX_CATEGORY_POINTS * 2 : 10;
        }

        /// <summary>
        /// Вычисляет погрешность в количестве фанатов
        /// </summary>
        private static int GetFansInaccurancy(int playerFans) {
            return playerFans / 100 * 10;
        }

        /// <summary>
        /// Возвращает очки в соответствии с трэндами
        /// </summary>
        private static int GetTrandsPoints(NewTrackModel track) {
            var result = track.Theme == Trands.TrandTheme ? MAX_TRAND_POINTS : 0;
            result += track.Style == Trands.TrandStyle ? MAX_TRAND_POINTS : 0;
            result += track.Autotune && Trands.AutotuneTrand ? MAX_TRAND_POINTS : 0;
            return result;
        }
    }
}