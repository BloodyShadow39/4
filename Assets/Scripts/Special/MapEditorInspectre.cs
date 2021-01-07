using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Game;

namespace Editors {
    [CustomEditor(typeof(MapCreator))]
    public class MapEditorInspectre : Editor {
        private MapCreator _thisMap;

        public override void OnInspectorGUI() {
            base.OnInspectorGUI();
            _thisMap = (MapCreator)target;
            if (GUILayout.Button("Formirate Map")) {
                _thisMap.FormirateMap();
            }
        }
    }
}