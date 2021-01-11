using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Events;
using Game;

namespace UI {
    public class GameScreen : MonoBehaviour {

        public static GameScreen Instance;

        [HideInInspector]
        public Canvas _thisCanvas;

        [SerializeField]
        private List<RawImage> _heroIcons;

        [HideInInspector]
        public Player _currentPlayer;

        [SerializeField]
        private EventListener _playerChanged;

        [SerializeField]
        private Text _gold;

        [SerializeField]
        private Text _day;

        [SerializeField]
        private Text _week;

        [SerializeField]
        private Text _mounth;

        private void Awake() {
            if (Instance != null) {
                Destroy(gameObject);
                return;
            }
            
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void Start() {
            ChangingCamera();
        }

        private void OnEnable() {
            _playerChanged.OnEventHappened += FillIconsHeroes;
        }

        private void OnDisable() {
            _playerChanged.OnEventHappened -= FillIconsHeroes;
        }

        private void ChangingCamera() {
            if (!gameObject.TryGetComponent(out _thisCanvas)) {
                Debug.LogError("Object havent Canvas");
            }
            _thisCanvas.worldCamera = UsefulCamera.Instance.cam;
            _thisCanvas.planeDistance = 1f;
        }

        public void SetTime(int day,int week,int mounth) {
            _day.text = day.ToString();
            _week.text = week.ToString();
            _mounth.text = mounth.ToString();
        }

        private void FillIconsHeroes() {
            
            for(int i = 0; i < _currentPlayer.heroes.Count; i++) {
                if (_heroIcons.Count > i) {
                    _heroIcons[i].gameObject.SetActive(true);
                    _heroIcons[i].texture = _currentPlayer.heroes[i].RenderCamera().Render();
                }
                else
                    break;
            }

            if (_currentPlayer.heroes.Count < _heroIcons.Count) {
                for (int i = _currentPlayer.heroes.Count; i < _heroIcons.Count; i++) {
                    _heroIcons[i].gameObject.SetActive(false);
                }
            }

            _gold.text = _currentPlayer.gold.ToString();
        }

    }
}