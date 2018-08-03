using System;

namespace Assets.Scripts.Utils {
    /// <summary>
    /// Форматирование чисел в удобное представление
    /// </summary>
    public class NumberFormatter {
        public const int THOUSAND = 1000;
        public const int MILLION = 1000000;

        /// <summary>
        /// Форматирует значение
        /// </summary>
        public static string FormatValue(int value) {
            value = Math.Abs(value);
            if (value >= MILLION) {
                return $"{(float) value / MILLION:F}м";
            }
            if (value >= THOUSAND) {
                return $"{(float) value / THOUSAND:F}т";
            }
            return value.ToString();
        }
    }
}