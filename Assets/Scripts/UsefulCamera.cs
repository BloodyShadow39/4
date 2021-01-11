using UnityEngine;


    public class UsefulCamera : MonoBehaviour {

        public static UsefulCamera Instance;

        public Camera cam=null;

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