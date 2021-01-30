using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;
using Values;
namespace Game {

    public class Camera_control : MonoBehaviour {

        [SerializeField]
        private EventListener _updateInputListeners;

        [SerializeField]
        private ScriptableFloatValue _sinsentivity; 

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

        private void InputControl() {
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
            if (Input.GetAxis("Mouse ScrollWheel") > 0.1) {
                var temp = transform.position;
                transform.Translate(Vector3.forward * 5);
                Debug.Log($"{temp}    {Vector3.forward}    {transform.position}");
                if (transform.position.y < 6) transform.position = temp;
            }
            if (Input.GetAxis("Mouse ScrollWheel") < -0.1) {
                var temp = transform.position;
                transform.Translate(Vector3.forward * -5);
                if (transform.position.y > 25) transform.position = temp;
            }
            if (Input.GetKey(KeyCode.Q)) {
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles - new Vector3(0,1,0));
            }
            if (Input.GetKey(KeyCode.E)) {
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0, 1, 0));
            }
        }
    }
}
