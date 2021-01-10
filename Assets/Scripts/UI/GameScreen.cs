using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Values;
using Events;

namespace UI {
    public class GameScreen : MonoBehaviour {

        public static GameScreen Instance;

        [SerializeField]
        private List<RawImage> _heroIcons;

        [SerializeField]
        private SelectPlayer _currentPlayer;

        [SerializeField]
        private EventListener _playerChanged;

        private void Awake() {
            if (Instance != null) {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void OnEnable() {
            _playerChanged.OnEventHappened += FillIconsHeroes;
        }

        private void OnDisable() {
            _playerChanged.OnEventHappened -= FillIconsHeroes;
        }

        private void FillIconsHeroes() {
            
            for(int i = 0; i < _currentPlayer.currentPlayer.heroes.Count; i++) {
                Debug.Log($"{ _heroIcons};{i}");
                if (_heroIcons.Count > i) {
                    _heroIcons[i].texture = _currentPlayer.currentPlayer.heroes[i].RenderCamera().Render();
                    Debug.Log(true);
                }
                else
                    break;
            }
        }

    }
}