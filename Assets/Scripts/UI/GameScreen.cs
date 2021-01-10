using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Values;
using Events;

namespace UI {
    public class GameScreen : MonoBehaviour {

        [SerializeField]
        private List<RawImage> _heroIcons;

        [SerializeField]
        private ScriptablePlayer _currentPlayer;

        [SerializeField]
        private EventListener _playerChanged;

        private void FillIconsHeroes() {
            for(int i = 0; i < _currentPlayer.heroes.Count; i++) {
                if (_heroIcons.Count > i)
                    _heroIcons[i].texture = _currentPlayer.heroes[i].RenderCamera().Render();
                else
                    break;
            }
        }

    }
}