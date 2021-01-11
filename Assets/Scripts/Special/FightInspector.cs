using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Game;

[CustomEditor(typeof(Figth))]
public class FightInspector : Editor {
    private Figth _thisInspector;

    private int width=0;
    private int height=0;

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        _thisInspector = (Figth)target;

        if (width != _thisInspector.width) {
            width = _thisInspector.width;
            _thisInspector.FillEmptyMap();
        }
        if (height!=_thisInspector.height) {
            height = _thisInspector.height;
            _thisInspector.FillEmptyMap();
        }

        if (GUILayout.Button("Clear Map")) {
            _thisInspector.FillEmptyMap();
        }

        if (GUILayout.Button("Generate")) {
            _thisInspector.GenerateMap();
        }

        if (_thisInspector.map!=null)
            for (int i = 0; i < width; i++) {
                GUILayout.BeginHorizontal();
                for (int j = 0; j < height; j++) {
                    _thisInspector.map[i,j]=GUILayout.Toggle(_thisInspector.map[i,j],"");
                }
                GUILayout.EndHorizontal();
            }

    }
}
