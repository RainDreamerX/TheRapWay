using System.ComponentModel;

namespace Assets.Scripts.Enums {
    /// <summary>
    /// Типы действий
    /// </summary>
    public enum ActionType {
        [Description("Новый трэк")]
        NewTrack = 0,

        [Description("Новый клип")]
        NewClip,

        [Description("Совместный трэк")]
        Feat,

        [Description("Концерт")]
        Concert,

        [Description("Баттл")]
        Battle,

        [Description("Обучение")]
        Traning
    }
}