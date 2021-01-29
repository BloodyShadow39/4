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
            if (GUILayout.Button("Rewrite Map", GUILayout.Width(150))) {
                _thisMap.FormirateMap();
                AssetDatabase.Refresh();
            }
            GUILayout.Label("Rewrite map in connected Asset");
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Generate Map", GUILayout.Width(150))) {
                _thisMap.GenerateMap();
                AssetDatabase.Refresh();
            }
            GUILayout.Label("Generate map from connected Asset");
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Check Possitions", GUILayout.Width(150))) {
                _thisMap.CheckToCurrentPosition();
                AssetDatabase.Refresh();
            }
            GUILayout.Label("Set normal position at X and Z coordinates");
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Set Correct Map Size", GUILayout.Width(150))) {
                _thisMap.SetCorrectMapSize();
                AssetDatabase.Refresh();
            }
            GUILayout.Label("Set Map current width and heigth");
            GUILayout.EndHorizontal();


        }
    }
}