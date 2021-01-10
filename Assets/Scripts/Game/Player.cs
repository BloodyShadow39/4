using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI;
using Events;

namespace Game {
    public class Player : MonoBehaviour {

        public string playerName = "";

        public List<Hero> heroes;

        public int gold = 0;

        [SerializeField]
        private EventDispatcher _playerChanged;

        public void SetPlayerCurrent() {
            GameScreen.Instance._currentPlayer = this;
            _playerChanged.Dispatch();
        }

    }
}