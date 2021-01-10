using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managers {
    public class RenderCamera : MonoBehaviour {
        [SerializeField]
        private Camera _renderCamera;

        [SerializeField]
        private int _width = 64;

        [SerializeField]
        private int _height = 64;

        public  RenderTexture Render() {
            RenderTexture _texture = RenderTexture.GetTemporary(_width, _height);
            _texture.Create();
            _renderCamera.targetTexture = _texture;
            _renderCamera.Render();
            _renderCamera.targetTexture = null;
            return _texture;
        }
    }
}