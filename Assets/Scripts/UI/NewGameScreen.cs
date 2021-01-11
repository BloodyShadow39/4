using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
    public class NewGameScreen : MonoBehaviour {

        [SerializeField]
        private Button _scenarios;

        [SerializeField]
        private Button _campaigns;

        [SerializeField]
        private Button _tutorial;

        [SerializeField]
        private Button _exit;

        [SerializeField]
        private MenuScreen _menuScreen;

        private void ExitMenu() {
            _menuScreen.HideNewGameScreen();
        }

        private void Awake() {
            _exit.onClick.AddListener(ExitMenu);
        }
    }
}

