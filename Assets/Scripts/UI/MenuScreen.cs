using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
    public class MenuScreen : MonoBehaviour {

        [SerializeField]
        private Button _exit;

        [SerializeField]
        private Button _yes;

        [SerializeField]
        private Button _no;

        [SerializeField]
        private GameObject _sureMenu;

        [SerializeField]
        private Button _newGameButton;

        [SerializeField]
        private GameObject _newGameScreen;

        [SerializeField]
        private GameObject _settingsButton;

        [SerializeField]
        private GameObject _settingsScreen;

        private void SureMenu() {
            _sureMenu.SetActive(true);
        }

        private void YesButton() {
            Application.Quit();
        }

        private void NoButton() {
            _sureMenu.SetActive(false);
        }

        private void ShowNewGameScreen() {
            _newGameScreen.SetActive(true);
        }

        public void HideNewGameScreen() {
            _newGameScreen.SetActive(false);
        }

        public void HideSettingsScreen() {
            _settingsScreen.SetActive(false);
        }

        private void Awake() {
            _exit.onClick.AddListener(SureMenu);
            _no.onClick.AddListener(NoButton);
            _yes.onClick.AddListener(YesButton);
            _newGameButton.onClick.AddListener(ShowNewGameScreen);
        }
    }
}
