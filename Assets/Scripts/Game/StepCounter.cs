using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI;
using Events;

namespace Game {
    public class StepCounter : MonoBehaviour {

        [SerializeField]
        private List<Player> _listOfPlayers;

        private int _currentPlayerNumber=-1;

        [SerializeField]
        private EventListener _step;

        [SerializeField]
        private EventDispatcher _playerChanged;

        private int _day = 1;

        private int _week = 1;

        private int _mounth = 1;

        private void Start() {
            NextPlayer();
        }

        private void OnEnable() {
            _step.OnEventHappened += NextPlayer;
        }

        private void OnDisable() {
            _step.OnEventHappened -= NextPlayer;
        }

        private void NextPlayer() {
            if (_currentPlayerNumber + 1 >= _listOfPlayers.Count) {
                _currentPlayerNumber = 0;
                _day++;
                if (_day > 7) {
                    _day = 1;
                    _week++;
                    if (_week > 4) {
                        _mounth++;
                        _week = 1;
                    }
                }
                GameScreen.Instance.SetTime(_day, _week, _mounth);
            }
            else
                _currentPlayerNumber++;
            for (int i = 0; i < _listOfPlayers[_currentPlayerNumber].heroes.Count; i++)
                _listOfPlayers[_currentPlayerNumber].heroes[i].movePoints = _listOfPlayers[_currentPlayerNumber].heroes[i].startPoints;
            GameScreen.Instance._currentPlayer = _listOfPlayers[_currentPlayerNumber];
            _playerChanged.Dispatch();
        }
    }
}