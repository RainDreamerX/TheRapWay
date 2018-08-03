using System.ComponentModel;

namespace Assets.Scripts.Enums {
    /// <summary>
    /// Источник бита
    /// </summary>
    public enum BitSource {
        [Description("Скачать бесплатный")]
        Free = 0,

        [Description("Создать свой")]
        Self,

        [Description("Заказать у битмейкера")]
        Bitmaker
    }
}