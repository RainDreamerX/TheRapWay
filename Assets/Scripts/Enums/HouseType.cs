using System.ComponentModel;

namespace Assets.Scripts.Enums {
    /// <summary>
    /// Тип дома
    /// </summary>
    public enum HouseType {
        [Description("Дешевый")]
        Poor,

        [Description("Обычный")]
        Common,

        [Description("Комфортный")]
        Comfort,

        [Description("Дорогой")]
        Expensive,

        [Description("Оч. дорогой")]
        VeryExpensive
    }
}