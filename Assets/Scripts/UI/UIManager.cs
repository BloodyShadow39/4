using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI {
    public class UIManager : MonoBehaviour {

        public static UIManager Instance;

        [SerializeField]
        private GameObject _bugfixesScreen;

        [SerializeField]
        private GameObject _settingsScreen;

        [SerializeField]
        private GameObject _loadGameScreen;

        [SerializeField]
        private GameObject _newGameScreen;
    }

}
