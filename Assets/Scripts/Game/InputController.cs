using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;
using Values;

public class InputController : MonoBehaviour
{
    public static InputController Instance;

    private void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
}
