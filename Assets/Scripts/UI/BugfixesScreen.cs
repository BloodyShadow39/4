using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace UI {
    public class BugfixesScreen : MonoBehaviour {

        [SerializeField]
        private Button _close;

        [SerializeField]
        private Button _open;

        [SerializeField]
        private RectTransform _selfrectTransform;

        private float _defaultRightSide; //0.5f

        private float _defaultLeftSide; //0f

        [SerializeField]
        private float _rightSide; //0.1f

        [SerializeField]
        private float _leftSide; //-0.4f


        private void CloseScreen() {
            _selfrectTransform.anchorMax = new Vector2 (_rightSide, _selfrectTransform.anchorMax.y);
            _selfrectTransform.anchorMin = new Vector2(_leftSide, _selfrectTransform.anchorMin.y);
            _close.gameObject.SetActive(false);
            _open.gameObject.SetActive(true);
        }

        private void OpenScreen() {
            _selfrectTransform.anchorMax = new Vector2(_defaultRightSide, _selfrectTransform.anchorMax.y);
            _selfrectTransform.anchorMin = new Vector2(_defaultLeftSide, _selfrectTransform.anchorMin.y);
            _open.gameObject.SetActive(false);
            _close.gameObject.SetActive(true);
        }

        private void Awake() {
            _close.onClick.AddListener(CloseScreen);
            _open.onClick.AddListener(OpenScreen);
            _defaultRightSide = _selfrectTransform.anchorMax.x;
            _defaultLeftSide = _selfrectTransform.anchorMin.x;

        }
    }
}