using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;
using Values;
using UnityEngine.UI;
namespace Game {

    public class Camera_control : MonoBehaviour {

        [SerializeField]
        private EventListener _updateInputListeners;

        [SerializeField]
        private ScriptableFloatValue _sinsentivity;

        [SerializeField]
        private float _turnDistance;

        [SerializeField]
        private float _sinsivityTurn;

        private Vector3 _setPosition=new Vector3(Screen.width/2,Screen.height/2,0);

        [SerializeField]
        private float _distanceMove;

        private void Awake() {
            _updateInputListeners.OnEventHappened += SubscribeAction;
        }

        private void OnDestroy() {
            _updateInputListeners.OnEventHappened -= SubscribeAction;
        }

        private void SubscribeAction() {
            InputControl();
        }

        private Vector3 TranSlate (float x, float y,float z) {
            Vector3 result=Vector3.zero;
            var aqual = -Mathf.Deg2Rad * transform.rotation.eulerAngles.y;
            result.x = (x * Mathf.Cos(aqual) - z * Mathf.Sin(aqual));
            result.z = (z * Mathf.Cos(aqual) + x * Mathf.Sin(aqual));
            return result;
        }

        private Vector3 TranSlate(Vector3 v) {
            Vector3 result = Vector3.zero;
            var aqual = -Mathf.Deg2Rad * transform.rotation.eulerAngles.y;
            result.x = (v.x * Mathf.Cos(aqual) - v.z * Mathf.Sin(aqual));
            result.z = (v.z * Mathf.Cos(aqual) + v.x * Mathf.Sin(aqual));
            return result;
        }

        private void Keyboard() {
            if (Input.GetKey(KeyCode.W)) {
                transform.position += TranSlate(0, 0, _sinsentivity.value);
            }
            if (Input.GetKey(KeyCode.S)) {
                transform.position -= TranSlate(0, 0, _sinsentivity.value);
            }
            if (Input.GetKey(KeyCode.A)) {
                transform.position -= TranSlate(_sinsentivity.value, 0, 0);
            }
            if (Input.GetKey(KeyCode.D)) {
                transform.position += TranSlate(_sinsentivity.value, 0, 0);
            }
            if (Input.GetKey(KeyCode.Q)) {
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles - new Vector3(0, _sinsentivity.value*5, 0));
            }
            if (Input.GetKey(KeyCode.E)) {
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0, _sinsentivity.value*5, 0));
            }
        }

        private void SizeMapCamera() {
            if (Input.GetAxis("Mouse ScrollWheel") > 0.1) {
                var temp = transform.position;
                transform.Translate(Vector3.forward * 5);
                if (transform.position.y < 6) transform.position = temp;
            }
            if (Input.GetAxis("Mouse ScrollWheel") < -0.1) {
                var temp = transform.position;
                transform.Translate(Vector3.forward * -5);
                if (transform.position.y > 25) transform.position = temp;
            }
        }

        private void MouseControll() {
            var cordinateMouse = Input.mousePosition;
            if (Input.GetKeyDown(KeyCode.Mouse2)) {
                _setPosition = cordinateMouse;
            }
            if (Input.GetKey(KeyCode.Mouse2)) {
                if (cordinateMouse.x - _setPosition.x > _turnDistance) {
                    transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0, _sinsivityTurn, 0));
                }
                else
                    if (cordinateMouse.x - _setPosition.x < -_turnDistance) {
                    transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles - new Vector3(0, _sinsivityTurn, 0));
                }
            }
            if (cordinateMouse.x > Screen.width - _distanceMove) {
                transform.position += TranSlate(_sinsentivity.value, 0, 0);
            }
            if (cordinateMouse.x< _distanceMove) {
                transform.position -= TranSlate(_sinsentivity.value, 0, 0);
            }
            if (cordinateMouse.y > Screen.height - _distanceMove) {
                transform.position += TranSlate(0, 0, _sinsentivity.value);
            }
            if (cordinateMouse.y < _distanceMove) {
                transform.position -= TranSlate(0, 0, _sinsentivity.value);
            }
        }

        private void InputControl() {
            StartCoroutine(InputControlCoroutine());
        }

        private IEnumerator InputControlCoroutine() {
            Keyboard();
            SizeMapCamera();
            MouseControll();
            return null;
        }
    }
}
