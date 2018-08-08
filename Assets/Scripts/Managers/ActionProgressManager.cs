using System;
using System.Threading.Tasks;
using Assets.Scripts.Data;
using Assets.Scripts.Enums;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Managers {
    /// <summary>
    /// Управление выполнением прогресса
    /// </summary>
    public class ActionProgressManager : MonoBehaviour {
        public DaysManager DaysManager;
        public int DaysLeft;
        public Image ProgressBar;
        public Text WorkingPhrase;

        private float _progressStep;
        private ActionType _actionType;

        public void Awake() {
            gameObject.SetActive(false);
        }

        /// <summary>
        /// Устанавливает выполнение действия
        /// </summary>
        public async void StartAction(int daysCount, ActionType type, Action callback) {
            DaysLeft = daysCount;
            _actionType = type;
            _progressStep = 1f / daysCount;
            gameObject.SetActive(true);
            var result = await ProcessAction();
            if (result) {
                callback();
                SaveManager.Instance.Save();
            }
            gameObject.SetActive(false);
        }

        /// <summary>
        /// Обработчик выполнения действия
        /// </summary> 
        private async Task<bool> ProcessAction() {
            ProgressBar.fillAmount = 0;
            while (DaysLeft > 0) {
                if (DaysManager.IsGameLosed()) return false;
                ShowWorkingPhrase();
                await Task.Delay(800);
                await FillProgress();
                DaysLeft--;
                DaysManager.NextDay();
            }
            WorkingPhrase.text = string.Empty;
            return true;
        }

        /// <summary>
        /// Показывает фразу в окне прогресса
        /// </summary>
        private void ShowWorkingPhrase() {
            var phrase = string.Empty;
            if (DaysLeft == 1 && _actionType != ActionType.Traning) {
                phrase = "Выкладываем в сеть";
            }
            else if (DaysLeft % 2 == 0) {
                phrase = WorkingPhrasesGetter.GetPhrase(_actionType);
            }
            if (!string.IsNullOrEmpty(phrase)) WorkingPhrase.text = phrase;
        }

        /// <summary>
        /// Плавное заполнение прогресс бара
        /// </summary>
        private async Task FillProgress() {
            var nextValue = ProgressBar.fillAmount + _progressStep;
            if (nextValue > 1) nextValue = 1;
            while (ProgressBar.fillAmount < nextValue) {
                ProgressBar.fillAmount += 0.15f * Time.deltaTime;
                await Task.Delay(10);
            }
        }
    }
}