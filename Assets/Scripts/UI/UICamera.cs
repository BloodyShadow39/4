using UnityEngine;

namespace UI {
    public class UICamera : MonoBehaviour {

        public static UICamera Instance;

        private void Awake() {
            if (Instance == null) {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else {
                Destroy(gameObject);
            }
        }

    }
}