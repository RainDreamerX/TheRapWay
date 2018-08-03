using System.ComponentModel;

namespace Assets.Scripts.Enums {
    /// <summary>
    /// Степени успешности действия
    /// </summary>
    public enum SuccessGrade {
        [Description("Очень низкая")]
        Lowest = 0,

        [Description("Низкая")]
        Low,

        [Description("Средняя")]
        Middle,

        [Description("Высокая")]
        Hight,

        [Description("Очень высокая")]
        Highest
    }
}