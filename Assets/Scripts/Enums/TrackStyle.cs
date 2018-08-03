using System.ComponentModel;

namespace Assets.Scripts.Enums {
    /// <summary>
    /// Стили трэка
    /// </summary>
    public enum TrackStyle {
        [Description("Обычный")]
        Common = 0,

        [Description("Агрессивный")]
        Argessive,

        [Description("Грустный")]
        Boring,

        [Description("Веселый")]
        Funny,

        [Description("Быстрый")]
        Fast,

        [Description("Спокойный")]
        Calm
    }
}