using System.Collections.Generic;
using Assets.Scripts.Enums;

namespace Assets.Scripts.UI.Tweeter {
    /// <summary>
    /// Вспомогательные данные для ленты соц. сетей
    /// </summary>
    public class TweeterData {
        /// <summary>
        /// Ники пользователей
        /// </summary>
        public static readonly List<string> Nicknames = new List<string> {
            "Bad Mr. Frosty",
            "Kraken",
            "Boomer",
            "Lumberjack",
            "Boomerang",
            "Mammoth",
            "Boss",
            "Mastadon",
            "Budweiser",
            "Master",
            "Bullseye",
            "Meatball",
            "Buster",
            "Mooch",
            "Butch",
            "Mr. President",
            "Buzz",
            "Outlaw",
            "Canine",
            "Ratman",
            "Captian RedBeard",
            "Renegade",
            "Champ",
            "Sabertooth",
            "Coma",
            "Scratch",
            "Crusher",
            "Sentinel",
            "Diesel",
            "Speed",
            "Doctor",
            "Spike",
            "Dreads",
            "Subwoofer",
            "Frankenstein",
            "Thunderbird",
            "Froggy",
            "Tornado",
            "General",
            "Troubleshoot",
            "Godzilla",
            "Vice",
            "Hammerhead",
            "Viper",
            "Handy Man",
            "Wasp",
            "Hound Dog",
            "Wizard",
            "Indominus",
            "Zodiac",
            "King Kong"
        };

        /// <summary>
        /// Комментарии к различным действиям и оценкам
        /// </summary>
        public static readonly Dictionary<ActionType, Dictionary<SuccessGrade, List<string>>> Comments = 
            new Dictionary<ActionType, Dictionary<SuccessGrade, List<string>>> {
                [ActionType.NewTrack] = GetTrackComments(),
                [ActionType.Feat] = GetTrackComments(),
                [ActionType.NewClip] = new Dictionary<SuccessGrade, List<string>> {
                    [SuccessGrade.Lowest] = new List<string> {
                        "Это снимали на старый телефон?",
                        "Клип может стать чемпионом по дизлайкам!",
                        "А есть разрешение выше, чем 240?",
                        "Лучше бы это удалили сразу после съемок",
                        "Японская реклама выглядит адекватнее, чем это!"
                    },
                    [SuccessGrade.Low] = new List<string> {
                        "Какая песня - такой и клип",
                        "Выключил в самом начале",
                        "Только у меня звук не совпадает с картинкой??",
                        "Какая-то ерудна",
                        "Кому-то такое нравится?"
                    },
                    [SuccessGrade.Middle] = new List<string> {
                        "Средненький клип...",
                        "Картинку бы получше",
                        "Конечно, бывают клипы и хуже",
                        "Песня понравилась больше, чем клип",
                        "Двоякие ощущения после просмотра"
                    },
                    [SuccessGrade.Hight] = new List<string> {
                        "В целом круто, но таким никого не удивишь!",
                        "Немножно не дотянули, но все равно здорово",
                        "Эти ребята знают толк!",
                        "Иногда пересматриваю клип повторно",
                        "Мне определенно нравится!"
                    },
                    [SuccessGrade.Highest] = new List<string> {
                        "Это просто шикарно!",
                        "Нереально красиво, 10 из 10",
                        "Клип идеально подходит к песне",
                        "Всем срочно смотреть!!!",
                        "Отличная работа, парни!"
                    }
                },
                [ActionType.Concert] = new Dictionary<SuccessGrade, List<string>> {
                    [SuccessGrade.Lowest] = new List<string> {
                        "Было..очень...стрёмно",
                        "Почему на концерт пришел только я?",
                        "Верните деньги за билет!!",
                        "Концерты точно не его конёк",
                        "А можно в следующий раз выступать трезвым?"
                    },
                    [SuccessGrade.Low] = new List<string> {
                        "Весь вечер просидела в телефоне..",
                        "Мне не понравился концерт",
                        "У меня там украли кошелек((",
                        "Никому не советую идти на концерт",
                        "Слабоватое выступление"
                    },
                    [SuccessGrade.Middle] = new List<string> {
                        "Вполне себе неплохо",
                        "Для галочки можно разок сходить",
                        "После концерта остались двоякие чувства",
                        "Никакого заряда не получил, но и жаловаться особо не на что",
                        "Если вам совсем скучно, то сходите"
                    },
                    [SuccessGrade.Hight] = new List<string> {
                        "Было очень весело!",
                        "Точно пойду еще..и друзей потащу!",
                        "Я чуть не оглох от звука, но оно того стоило",
                        "Славно повеселились с компанией",
                        "Всем советую идти! Вы не пожалеете"
                    },
                    [SuccessGrade.Highest] = new List<string> {
                        "Это было лучшее выступление в моей жизни!",
                        "Я до сих пор на эмоциях от концерта!",
                        "Нереальный заряд позитива, круто!",
                        "Там была целая программа, давно такого не видел!",
                        "Да этот парень просто рожден для сцены!"
                    }
                },
                [ActionType.Battle] = new Dictionary<SuccessGrade, List<string>> {
                    [SuccessGrade.Lowest] = new List<string> {
                        "Кто его пустил на батл?",
                        "Перематывал раунды, такая нудятина..",
                        "Это фиаско, братан!",
                        "Полнейший провал",
                        "Отсто-о-о-ой!"
                    },
                    [SuccessGrade.Low] = new List<string> {
                        "Опять какой-то блогер лезет в рэп?",
                        "Я не услышал ни одного панча",
                        "Ему стоит для начала полистать словарик или найти гострайтера",
                        "Все раунды этого парня - сплошная вода",
                        "Он справедливо продул"
                    },
                    [SuccessGrade.Middle] = new List<string> {
                        "Вот это была равная борьба!",
                        "Я считаю, что победила дружба, оба красавцы!",
                        "Так...я не понял, а кто победил?",
                        "Ему стоит почаще ходить на батлы!",
                        "Равные соперники, ничего не скажешь"
                    },
                    [SuccessGrade.Hight] = new List<string> {
                        "В последнем раунде ему удалось перевесить чашу весов в свою пользу!",
                        "Я думаю он заслужил эту победу",
                        "Сегодня этот парень был все же сильнее!",
                        "Он выдал очень сильный второй раунд",
                        "Было много хороших панчей"
                    },
                    [SuccessGrade.Highest] = new List<string> {
                        "Да он просто уничтожил соперника!",
                        "Это было похоже на избиение младенца!",
                        "Так заорал с одного панча, что подавился пельмешкой))",
                        "Я знал, что он живого места не оставит!",
                        "Как же сильно он пишет!"
                    }
                }
            };

        /// <summary>
        /// Возвращает комментарии для трэка
        /// </summary>
        private static Dictionary<SuccessGrade, List<string>> GetTrackComments() {
            return new Dictionary<SuccessGrade, List<string>> {
                [SuccessGrade.Lowest] = new List<string> {
                    "Кажется у меня из ушей пошла кровь",
                    "В этом трэке ужасно абсолютно всё!",
                    "Зачем я вообще это послушал?!",
                    "Это абсолютно бездарно...",
                    "Этому парню лучше бы вернуться за прилавок закусочной"
                },
                [SuccessGrade.Low] = new List<string> {
                    "Песенка, конечно, так себе",
                    "Не стал бы покупать альбом этого парня...",
                    "Мой 12-ти летний сосед исполняет лучше",
                    "Я готов платить за то, чтоб новые трэки этого парня не появлялись",
                    "Сломал колонку, когда судорожно пытался убавить звук :("
                },
                [SuccessGrade.Middle] = new List<string> {
                    "Думаю этот парень может лучше!",
                    "А если чуть больше постараться?",
                    "Ну такое себе...",
                    "Средненький трэк",
                    "Ни рыба, ни мясо. Ничего не цепляет",
                    "Отстой, но мне нравится!"
                },
                [SuccessGrade.Hight] = new List<string> {
                    "Весьма не плохо, господа!",
                    "Парнишка умеет поднять настроение",
                    "Скачал на флешку и кручу в машине",
                    "Такая музыка мне по душе!",
                    "Я не прочь послушать это еще разок"
                },
                [SuccessGrade.Highest] = new List<string> {
                    "Это просто бомба!",
                    "Уже несколько суток крутится у меня в голове",
                    "Потрясно! Я уже выучил все слова наизусть!",
                    "Он в ударе! Скорее бы еще один трэк",
                    "Безумие! Все вокруг ее слушают"
                }
            };
        }
    }
}