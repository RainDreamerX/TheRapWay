using System;
using Assets.Scripts.Enums;
using Assets.Scripts.Extentions;
using Assets.Scripts.Managers;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Assets.Scripts.UI {
    /// <summary>
    /// Обновление и отображение трэндов
    /// </summary>
    public class Trands : MonoBehaviour {
        public AlertManager AlertManager;

        public Text Style;
        public Text Theme;
        public Text Autotune;

        public static TrackTheme TrandTheme;
        public static TrackStyle TrandStyle;
        public static bool AutotuneTrand;

        public void Awake() {
            TrandTheme = SaveManager.Data.TrandTheme;
            TrandStyle = SaveManager.Data.TrandStyle;
            AutotuneTrand = SaveManager.Data.AutotuneTrand;
            DisplayTrands();
        }

        /// <summary>
        /// Обновить трэнды
        /// </summary>
        public void UpdateTrands() {
            var themes = Enum.GetValues(typeof(TrackTheme));
            TrandTheme = (TrackTheme) themes.GetValue(Random.Range(0, themes.Length));
            var styles = Enum.GetValues(typeof(TrackStyle));
            TrandStyle = (TrackStyle) styles.GetValue(Random.Range(0, styles.Length));
            AutotuneTrand = Random.Range(0, 2) > 0;
            var trandMessage = $"Новости трэндов! Публика предпочитает стиль \"{TrandStyle.GetDescription()}\" " +
                               $"и тематику \"{TrandTheme.GetDescription()}\". Автотюн {(AutotuneTrand ? "" : " не ")} в моде";
            AlertManager.ShowMessage(trandMessage, 15);
            DisplayTrands();
        }

        /// <summary>
        /// Отобразить информацию в панели
        /// </summary>
        private void DisplayTrands() {
            Style.text = $"Стиль: {TrandStyle.GetDescription()}";
            Theme.text = $"Тематика: {TrandTheme.GetDescription()}";
            var prefix = AutotuneTrand ? string.Empty : "не ";
            Autotune.text = $"Автотюн {prefix}в моде";
        }
    }
}
