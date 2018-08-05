using UnityEngine;

namespace Assets.Scripts.Utils {
    public class AudioSource : MonoBehaviour {
        private static AudioSource _instance;

        private void Awake() {
            if (_instance == null) {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
		    else if (_instance != this) {
                Destroy(gameObject);
            }
        }
    }
}
