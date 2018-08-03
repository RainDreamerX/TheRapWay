using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Models;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Managers {
    /// <summary>
    /// Логика работы с остальными рэперами
    /// </summary>
    public class RappersManager {
        /// <summary>
        /// Остальные рэперы
        /// </summary>
        private static readonly List<RapperModel> rappers = new List<RapperModel> {
            new RapperModel {Name = "Oxxxymiron", Fans = 1700000, Flow = 10},
            new RapperModel {Name = "Элджей", Fans = 1500000, Flow = 10},
            new RapperModel {Name = "Тимати", Fans = 1400000, Flow = 9},
            new RapperModel {Name = "Баста", Fans = 1300000, Flow = 9},
            new RapperModel {Name = "Скриптонит", Fans = 1100000, Flow = 7},
            new RapperModel {Name = "PHARAOH", Fans = 1000000, Flow = 8},
            new RapperModel {Name = "L'One", Fans = 850000, Flow = 8},
            new RapperModel {Name = "LSP", Fans = 800000, Flow = 6},
            new RapperModel {Name = "МОТ", Fans = 730000, Flow = 7},
            new RapperModel {Name = "Miyagi & Эндшпиль", Fans = 700000, Flow = 9},
            new RapperModel {Name = "Скруджи", Fans = 650000, Flow = 5},
            new RapperModel {Name = "Big Russian Boss", Fans = 600000, Flow = 8},
            new RapperModel {Name = "Джиган", Fans = 550000, Flow = 3},
            new RapperModel {Name = "FACE", Fans = 500000, Flow = 4},
            new RapperModel {Name = "ATL", Fans = 450000, Flow = 7},
            new RapperModel {Name = "Noize MC", Fans = 390000, Flow = 9},
            new RapperModel {Name = "Jah Khalib", Fans = 350000, Flow = 6},
            new RapperModel {Name = "ST", Fans = 320000, Flow = 6},
            new RapperModel {Name = "Рем Дигга", Fans = 280000, Flow = 5},
            new RapperModel {Name = "25/17", Fans = 270000, Flow = 8},
            new RapperModel {Name = "Каспийский Груз", Fans = 250000, Flow = 4},
            new RapperModel {Name = "OBLADAET", Fans = 230000, Flow = 5},
            new RapperModel {Name = "Слава КПСС", Fans = 200000, Flow = 7},
            new RapperModel {Name = "Карандаш", Fans = 120000, Flow = 6},
            new RapperModel {Name = "Markul", Fans = 100000, Flow = 6},
            new RapperModel {Name = "SIDxRAM", Fans = 97000, Flow = 7},
            new RapperModel {Name = "Смоки Мо", Fans = 95000, Flow = 4},
            new RapperModel {Name = "AK-47", Fans = 90000, Flow = 6},
            new RapperModel {Name = "Yanix", Fans = 85000, Flow = 6},
            new RapperModel {Name = "Гарри Топор", Fans = 80000, Flow = 6},
            new RapperModel {Name = "Rickey F", Fans = 70000, Flow = 7},
            new RapperModel {Name = "Chemodan", Fans = 60000, Flow = 5},
            new RapperModel {Name = "Slim", Fans = 60000, Flow = 4},
            new RapperModel {Name = "Птаха", Fans = 55000, Flow = 1},
            new RapperModel {Name = "СД", Fans = 40000, Flow = 5},
            new RapperModel {Name = "Паша Техник", Fans = 30000, Flow = 1}
        };

        /// <summary>
        /// Возвращает коллекцию с рэпперами
        /// </summary>
        public static List<RapperModel> GetRappers() {
            return rappers;
        }

        /// <summary>
        /// Возвращает рэппера по имени
        /// </summary>
        public static RapperModel GetByName(string name) {
            return rappers.FirstOrDefault(e => e.Name == name);
        }

        /// <summary>
        /// Обновляет показатель фанатов рэпперов
        /// </summary>
        public static void UpdateFans() {
            foreach (var rapper in rappers) {
                var fansPercent = rapper.Fans / 100;
                var increase = Random.Range(0, 2) > 0;
                if (increase)
                    rapper.Fans += fansPercent * 5;
                else
                    rapper.Fans -= fansPercent;
            }
        }

        /// <summary>
        /// Вычисляет согласие рэппера на фит или батл
        /// </summary>
        public static bool IsAgree(RapperModel rapper, int playerFans) {
            var percentage = (float) playerFans / rapper.Fans * 100;
            if (percentage < 1) return false;
            if (percentage > 100) return true;
            return percentage >= Random.Range(1, 101);
        }
    }
}