using System.ComponentModel;

namespace Assets.Scripts.Enums {
    /// <summary>
    /// Стратегия в баттле
    /// </summary>
    public enum BattleStrategy {
        [Description("Обычная")]
        Common,

        [Description("Агрессивная")]
        Agressive,

        [Description("О себе")]
        Self,

        [Description("По фактам")]
        OnFacts,

        [Description("О близких")]
        Relatives,

        [Description("Философствовать")]
        Philosophy,

        [Description("Насмехаться")]
        Mock
    }
}