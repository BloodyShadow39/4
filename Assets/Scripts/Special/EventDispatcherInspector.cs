using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Events;

namespace Editors {
    [CustomEditor(typeof(EventDispatcher))]
    public class EventDispatcherInspector : Editor {
        private EventDispatcher _thisDispatcher;

        public override void OnInspectorGUI() {
            base.OnInspectorGUI();
            _thisDispatcher = (EventDispatcher)target;
            if (GUILayout.Button("Dispatch")) {
                _thisDispatcher.Dispatch();
                AssetDatabase.Refresh();
            }
        }
    }
}