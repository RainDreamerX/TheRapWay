using System.Collections.Generic;
using Assets.Scripts.Enums;
using Assets.Scripts.Models;

namespace Assets.Scripts.Managers {
    /// <summary>
    /// Логика игрока
    /// </summary>
    public class PlayerManager {
        /// <summary>
        /// Данные игрока
        /// </summary>
        private static PlayerInfo player;

        /// <summary>
        /// Сбросить данные
        /// </summary>
        public static void CreateNew(string name) {
            player = new PlayerInfo {
                Name = name,
                Money = 0,
                Fans = 0,
                TweetsUnlocked = false,
                EventUnlocked = false,
                PlayerSkills = new PlayerSkills {
                    TrackThemes = new List<TrackTheme> {TrackTheme.AboutSelf, TrackTheme.Lyric, TrackTheme.Past},
                    TrackStyles = new List<TrackStyle> {TrackStyle.Common, TrackStyle.Boring},
                    TextSourses = new List<TextSourse> {TextSourse.Self, TextSourse.Ghostwriter},
                    BitSources = new List<BitSource> {BitSource.Free, BitSource.Bitmaker},
                    BattleStrategies = new List<BattleStrategy> {BattleStrategy.Common},
                    Flow = 1,
                    Vocabulary = 1,
                    BitMaking = 0,
                    CanCheckSocials = true
                },
                PlayerProperty = new PlayerProperty {HasAutotune = false}
            };
        }

        /// <summary>
        /// Возвращает информацию игрока
        /// </summary>
        public static PlayerInfo GetInfo() {
            return player;
        }
        
        /// <summary>
        /// Устанавливает данные игрока
        /// </summary>
        public static void SetInfo(PlayerInfo info) {
            player = info;
        }

        /// <summary>
        /// Возвращает навыки
        /// </summary>
        public static PlayerSkills GetSkills() {
            return player.PlayerSkills;
        }

        /// <summary>
        /// Возвращает имущество
        /// </summary>
        public static PlayerProperty GetProperty() {
            return player.PlayerProperty;
        }

        /// <summary>
        /// Возвращает значение одного процента от числа фанатов или минимальное значение
        /// </summary>
        public static int GetFansPercentValue() {
            var value = player.Fans / 100;
            if (value < 10) value = 10;
            return value;
        }

        /// <summary>
        /// Проверяет наличие денег
        /// </summary>
        public static bool EnoughMoney(int price) {
            return player.Money >= price;
        }

        /// <summary>
        /// Списать сумму с баланса
        /// </summary>
        public static void SpendMoney(int value) {
            player.Money -= value;
        }
    }
}