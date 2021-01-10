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

        private void SureMenu() {
            _sureMenu.SetActive(true);
        }

        private void YesButton() {
            Application.Quit();
        }

        private void NoButton() {
            _sureMenu.SetActive(false);
        }

        private void Awake() {
            _exit.onClick.AddListener(SureMenu);
            _no.onClick.AddListener(NoButton);
            _yes.onClick.AddListener(YesButton);
        }
    }
}
