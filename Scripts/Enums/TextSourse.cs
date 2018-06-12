using System.ComponentModel;

namespace Assets.Scripts.Enums {
    /// <summary>
    /// Источник текста
    /// </summary>
    public enum TextSourse {
        [Description("Написать свой")]
        Self = 0,

        [Description("Заказать у гострайтера")]
        Ghostwriter
    }
}