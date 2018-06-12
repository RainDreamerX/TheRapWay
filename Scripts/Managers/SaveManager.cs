using Assets.Scripts.Models;
using Assets.Scripts.UI;
using UnityEngine;

namespace Assets.Scripts.Managers {
    /// <summary>
    /// Логика сохранения данных игрока
    /// </summary>
    public class SaveManager : MonoBehaviour {
        private const string SAVE_KEY = "Save";

        public static DataToSave Data = new DataToSave();

        /// <summary>
        /// Сохранить данные
        /// </summary>
        public void Save() {
            var saveObject = new DataToSave {
                PlayerInfo = PlayerManager.GetInfo(),
                DaysPlayed = DaysManager.CurrentDay,
                TrandTheme = Trands.TrandTheme,
                TrandStyle = Trands.TrandStyle,
                AutotuneTrand = Trands.AutotuneTrand
            };
            var dataToSave = JsonUtility.ToJson(saveObject);
            PlayerPrefs.SetString(SAVE_KEY, dataToSave);
        }

        /// <summary>
        /// Загрузить данные
        /// </summary>
        public void Load() {
            var savedData = PlayerPrefs.GetString(SAVE_KEY);
            Data = JsonUtility.FromJson<DataToSave>(savedData);
            PlayerManager.SetInfo(Data.PlayerInfo);
        }

        /// <summary>
        /// Удаляет сохранение
        /// </summary>
        public void DeleteSave() {
            PlayerPrefs.DeleteKey(SAVE_KEY);
        }

        /// <summary>
        /// Проверяет, есть ли у игрока сохранения
        /// </summary>
        public bool HasSave() {
            return PlayerPrefs.HasKey(SAVE_KEY);
        }
    }
}
