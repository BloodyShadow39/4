using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Game;

public class MapEditMenu : EditorWindow
{
    public GameObject selectGameObject =null;

    private int width = 0;

    private int height = 0;

    [MenuItem("Window/MapEdit")]
    public static void ShowWindow() {
        EditorWindow.GetWindow(typeof(MapEditMenu));
    }

    private void OnGUI() {
        GUILayout.Label("Choose object");
        selectGameObject = (GameObject)EditorGUILayout.ObjectField("select",selectGameObject, typeof(GameObject),true);

        Figth tmp;
        if(selectGameObject.TryGetComponent(out tmp)) {
            if (width != tmp.width) {
                width = tmp.width;
                tmp.FillEmptyMap();
            }
            if (height != tmp.height) {
                height = tmp.height;
                tmp.FillEmptyMap();
            }

            if (GUILayout.Button("Clear Map")) {
                tmp.FillEmptyMap();
            }

            

            if (tmp.map != null)
                for (int i = 0; i < width; i++) {
                    GUILayout.BeginHorizontal();
                    for (int j = 0; j < height; j++) {
                        tmp.map[i, j] = GUILayout.Toggle(tmp.map[i, j], "");
                    }
                    GUILayout.EndHorizontal();
                }

            tmp.width = EditorGUILayout.IntSlider("width",tmp.width, 0, 40);
            tmp.height = EditorGUILayout.IntSlider("height",tmp.height, 0, 40);
        }
        

    }

}
