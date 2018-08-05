using System.Collections.Generic;
using Assets.Scripts.Enums;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Data {
    /// <summary>
    /// Возвращает рабочую фразу
    /// </summary>
    public class WorkingPhrasesGetter {
        private static readonly List<string> _trackPhrases = new List<string> {
            "Сводим трэк", "Записываем звук", "Придумываем текст", "Ищем блокнот и ручку",
            "Думаем над панчами", "Листаем черновики", "Чувствуем вдохновение"
        };

        private static readonly List<string> _clipPhrases = new List<string> {
            "Читаем сценарий", "Сидим в гримерке", "Работаем на камеру", "Пьем кофе между дублями",
            "Селфимся на съемочной площадке", "Спим в съемочном фургоне"
        };

        private static readonly List<string> _battlePhrases = new List<string> {
            "Сочиняем панчи", "Вырываем и выкидываем лист", "Ищем зашквары", "Читаем соц. сети",
            "Слушаем трэки оппонента", "Читаем интервью противника", "Смотрим другие батлы", "Репетируем куплеты"
        };

        private static readonly List<string> _concertPhrases = new List<string> {
            "Распеваемся", "Нервно ходим по гримерке", "Поем вместе с залом", "Прыгаем в толпу",
            "Общаемся с фанатами", "Раздаем автографы", "Выступаем на бис", "Поливаем толпу водой",
            "Презентуем новый альбом", "Раздаем мерч"
        };

        /// <summary>
        /// Возвращает рабочую фразу
        /// </summary>
        public static string GetPhrase(ActionType type) {
            switch (type) {
                case ActionType.NewTrack:
                case ActionType.Feat:
                    return _trackPhrases[Random.Range(0, _trackPhrases.Count)];
                case ActionType.NewClip:
                    return _clipPhrases[Random.Range(0, _clipPhrases.Count)];
                case ActionType.Concert:
                    return _concertPhrases[Random.Range(0, _concertPhrases.Count)];
                case ActionType.Battle:
                    return _battlePhrases[Random.Range(0, _battlePhrases.Count)];
                default:
                    return string.Empty;
            }
        }
    }
}