using System;
using System.Threading.Tasks;
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
        public SaveManager SaveManager;

        private float progressStep;

        public void Awake() {
            gameObject.SetActive(false);
        }

        /// <summary>
        /// Устанавливает выполнение действия
        /// </summary>
        public async void StartAction(int daysCount, Action callback) {
            DaysLeft = daysCount;
            progressStep = 1f / daysCount;
            gameObject.SetActive(true);
            var result = await ProcessAction();
            if (result) {
                callback();
                SaveManager.Save();
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
                await Task.Delay(800);
                await FillProgress();
                DaysLeft--;
                DaysManager.NextDay();
            }
            return true;
        }

        /// <summary>
        /// Плавное заполнение прогресс бара
        /// </summary>
        private async Task FillProgress() {
            var nextValue = ProgressBar.fillAmount + progressStep;
            if (nextValue > 1) nextValue = 1;
            while (ProgressBar.fillAmount < nextValue) {
                ProgressBar.fillAmount += 0.15f * Time.deltaTime;
                await Task.Delay(10);
            }
        }
    }
}