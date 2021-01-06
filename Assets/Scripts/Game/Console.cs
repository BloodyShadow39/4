using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Console : MonoBehaviour
{
    public static Console Instance;

    private void Awake() {
            if (Instance != null) {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

    [SerializeField]
    private InputField _programmingConrtol;

    private void OnGUI() {
        if (Event.current.Equals(Event.KeyboardEvent(KeyCode.BackQuote.ToString()))){
            _programmingConrtol.gameObject.SetActive(!_programmingConrtol.gameObject.activeSelf);
        }
    }
}
