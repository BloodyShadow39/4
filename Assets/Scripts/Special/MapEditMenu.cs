using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Game;
using Values;

public class MapEditMenu : EditorWindow
{
    public Object selectGameObject =null;

    private int width = 0;

    private int height = 0;

    [MenuItem("Window/MapEdit")]
    public static void ShowWindow() {
        EditorWindow.GetWindow(typeof(MapEditMenu));
    }

    private void OnGUI() {
        
        
        selectGameObject = EditorGUILayout.ObjectField("Select Object", selectGameObject, typeof(Object), true);

        if (selectGameObject != null) {
            if (selectGameObject.GetType() == typeof(GameObject)) {
                GameObject gameObject = selectGameObject as GameObject;
                Figth tmp;
                if (gameObject.TryGetComponent(out tmp)) {
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

                    tmp.width = EditorGUILayout.IntSlider("width", tmp.width, 0, 40);
                    tmp.height = EditorGUILayout.IntSlider("height", tmp.height, 0, 40);
                }
            }
            else
                if (selectGameObject.GetType() == typeof(ScriptableMap)) {

            }
            else
                GUILayout.Label("The object has not yet been entered into the working field or does not contain the parameter: map.");
        }
        else
            GUILayout.Label("Choose object");


    }

}
