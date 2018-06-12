using System.ComponentModel;

namespace Assets.Scripts.Enums {
    /// <summary>
    /// Темы трэков
    /// </summary>
    public enum TrackTheme {
        [Description("Прошлое")]
        Past = 0,

        [Description("О себе")]
        AboutSelf,

        [Description("Любовь")]
        Love,

        [Description("Фантазия")]
        Fantasy,

        [Description("Деньги")]
        Money,

        [Description("Спорт")]
        Sport,

        [Description("Лирика")]
        Lyric,

        [Description("Девушки")]
        Girls,

        [Description("Друзья")]
        Friends,

        [Description("Дисс")]
        Diss,

        [Description("Ганг")]
        Gang
    }
}