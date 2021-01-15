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
            GUILayout.BeginHorizontal();
            _thisMap = (MapCreator)target;
            if (GUILayout.Button("Formirate Map")) {
                _thisMap.FormirateMap();
                AssetDatabase.Refresh();
            }
            GUILayout.Label("Rewrite map in connected Asset");
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Generate")) {
                _thisMap.GenerateMap();
                AssetDatabase.Refresh();
            }
            GUILayout.Label("Generate map from connected Asset");
            GUILayout.EndHorizontal();
        }
    }
}