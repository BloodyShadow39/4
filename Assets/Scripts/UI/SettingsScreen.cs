using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
    public class SettingsScreen : MonoBehaviour {

        [SerializeField]
        private Button _gameSettings;

        [SerializeField]
        private Button _highScores;

        [SerializeField]
        private Button _credits;

        [SerializeField]
        private Button _exit;

        [SerializeField]
        private MenuScreen _menuScreen;

        private void ExitMenu() {
            _menuScreen.HideSettingsScreen();
        }

        private void Awake() {
            _exit.onClick.AddListener(ExitMenu);
        }
    }
}