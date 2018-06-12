//using AppodealAds.Unity.Api;

namespace Assets.Scripts.Managers {
    /// <summary>
    /// Логика управления рекламой
    /// </summary>
    public class AdsManager {
        private const string APP_KEY = "816caf9f873c7a56f868227a2fd1f4d58323f8eef5f154e5";

        private static AdsManager instance;

        private AdsManager() {
            //Appodeal.disableLocationPermissionCheck();
            //Appodeal.initialize(APP_KEY, Appodeal.INTERSTITIAL | Appodeal.NON_SKIPPABLE_VIDEO);
        }

        /// <summary>
        /// Создает единственный экземпляр класса
        /// </summary>
        public static void CreateInstance() {
            instance = new AdsManager();
        }

        /// <summary>
        /// Возвращает единственный экземпляр класса
        /// </summary>
        public static AdsManager GetInstance() {
            return instance ?? (instance = new AdsManager());
        }

        /// <summary>
        /// Показать рекламу
        /// </summary>
        public void ShowAd(int adType = 0/*Appodeal.INTERSTITIAL*/) {
            //Appodeal.show(adType);
        }

        /// <summary>
        /// Проверяет загрузилась ли реклама
        /// </summary>
        public bool AdsIsLoad() {
            return true; //Appodeal.isLoaded(Appodeal.INTERSTITIAL | Appodeal.NON_SKIPPABLE_VIDEO);
        }
    }
}