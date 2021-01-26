using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Values;
using System;
using System.IO;
using UnityEngine.SceneManagement;

namespace Game{
    public class MapCreator : MonoBehaviour
    {
        public static MapCreator Instance;
        [SerializeField]
        private Toucher _emptyObject;
        [SerializeField]
        private GameObject _closeObject;

        public static string nameOfGame = "4";

        public enum state { empty, useful, close }

        private string saveJson;

        [Range(0, 1000)]
        public int width;

        [Range(0, 1000)]
        public int height;

        private int[,] map = null;
        public state[,] mapSaved;
        public Vector3[,] mapPositions = null;//Не реализованная часть запоминания точек

        private void Awake() {
                if (Instance != null) {
                    Destroy(Instance);
                }
                Instance = this;
        }

        private void Start(){
            GenerateMap();
        }

        #region Generate/Formirate
        public void GenerateMap(){
            LoadFromJson();
            bool isCreated;
            if (map != null) {
                for (int i = 0; i < map.GetLength(0); i++) {
                    for (int j = 0; j < map.GetLength(1); j++) {
                        isCreated = false;
                        foreach (Transform child in transform) {
                            Type obj;
                            if ((child.position.x == i) && (child.position.z == j) && (child.gameObject.TryGetComponent(out obj))) {
                                isCreated = true;
                                break;
                            }
                        }
                        if (!isCreated)
                            if (mapSaved[i, j] == state.empty) {

                                Toucher tmp = Instantiate(_emptyObject, gameObject.transform);
                                tmp.gameObject.transform.position = new Vector3(i, -1, j);
                            }
                            else if (mapSaved[i, j] == state.close) {
                                GameObject tmp = Instantiate(_closeObject, gameObject.transform);
                                tmp.gameObject.transform.position = new Vector3(i, 0, j);
                            }
                            else if (mapSaved[i, j] == state.useful) {
                                Debug.LogWarning("Now haven't load useful objects from save, srry)");
                            }
                    }
                }

                Debug.Log($"Map {SceneManager.GetActiveScene().name} generated.");
            }
            else
                Debug.Log("Map not created");
        }

        private int FindMaxYChild() {
            
            int maxy = 0;

            foreach (Transform child in transform) {
                if (child.position.z > maxy) {
                    maxy = (int)child.position.z;
                }
            }

            return maxy;
        }
        private int FindMaxXChild() {

            int maxx = 0;

            foreach (Transform child in transform) {
                if (child.position.x > maxx) {
                    maxx = (int)child.position.x;
                }
            }

            return maxx;
        }

        public void FormirateMap(){
            bool isCorrect = true;
            foreach (Transform child1 in transform) {
                foreach (Transform child2 in transform) {
                    if (child1 != child2) {
                        Type tmp1;
                        Type tmp2;
                        if (child1.gameObject.TryGetComponent(out tmp1)) {
                            if (child2.gameObject.TryGetComponent(out tmp2))
                                if(tmp1.type!=state.useful&& tmp2.type != state.useful)
                                if ((child1.position.x == child2.position.x) && (child1.position.z == child2.position.z)) {
                                    Debug.LogError($"At ({child1.position.x},{child1.position.z}) position find 2 object, u can save only one object");
                                    isCorrect = false;
                                }
                        }
                        else {
                            Debug.LogError($"Find object without Type at ({child1.position.x},{child1.position.z})");
                            isCorrect = false;
                        }
                    }
                }
            }

            if (!isCorrect)
                return;

            int maxy= FindMaxYChild();
            int maxx= FindMaxXChild();
            int[,] map = new int[maxx+1,maxy+1];

            mapSaved = new state[maxx+1, maxy+1];

            for(int i=0;i<maxx+1;i++){
                for(int j=0;j<maxy+1;j++){
                    map[i, j] = int.MaxValue;
                    mapSaved[i, j] = state.empty;
                }
            }

            foreach(Transform child in transform){
                if(!child.gameObject.TryGetComponent(out Type obj)){
                    if (((int)(child.position.x) >= 0) && ((int)(child.position.z) >= 0)) {
                        map[(int)(child.position.x ),(int)(child.position.z)] = -1;
                        mapSaved[(int)(child.position.x), (int)(child.position.z)] = obj.type;
                    }
                }
            }
            SaveToJson();
            
            Debug.Log($"Map colums {mapSaved.GetLength(1)} rewrited.");

            Debug.Log($"Map {SceneManager.GetActiveScene().name} rewrited.");
        }

        #endregion Generate/Formirate

        public void FillEmptyMap() {
            mapSaved = new state[width, height];
            map = new int[width, height];
            for (int i = 0; i < width; i++) {
                for (int j = 0; j < height; j++) {
                    mapSaved[i, j] = state.empty;
                }
            }
        }


        private int[,] FilledMap() {
            int maxy = FindMaxYChild();
            int maxx = FindMaxXChild();
            int[,] map = new int[maxx + 1, maxy + 1];
            foreach (Transform child in transform) {
                Type type;
                if(child.gameObject.TryGetComponent(out type)) {
                    if (type.type == state.close) {
                        map[(int)child.position.x, (int)child.position.z] = -1;
                    } else {
                        map[(int)child.position.x, (int)child.position.z] = int.MaxValue;
                    }
                }
            }
            return map;
        }

        #region MatrixMove

        public int[,] matrixMove(int a, int b) {

            if ((a >= map.GetLength(0)) || (b >= map.GetLength(1))) {
                Debug.LogError("Writen Map less when finden point");
                return new int[0, 0];
            }

            int[,] currentMap = FilledMap();
            currentMap[a, b] = 0;
            currentMap = matrixMoveIterate(a, b, currentMap);
            return currentMap;
        }

        public int[,] matrixMove(int a, int b, int[,] map) {
            int[,] currentMap = map;
            if ((a >= map.GetLength(0)) || (b >= map.GetLength(1))) {
                Debug.LogError("Writen Map less when finden point");
                return map;
            }
            currentMap[a, b] = 0;
            currentMap = matrixMoveIterate(a, b, map);
            return currentMap;
        }

        private int[,] matrixMoveIterate(int a, int b, int[,] map) {
            if (a - 1 >= 0) {
                if (b - 1 >= 0) {
                    if (map[a - 1, b - 1] > map[a, b]) {
                        map[a - 1, b - 1] = map[a, b] + 1;
                        matrixMoveIterate(a - 1, b - 1, map);
                    }
                }
                if (b + 1 < map.GetLength(1)) {
                    if (map[a - 1, b + 1] > map[a, b]) {
                        map[a - 1, b + 1] = map[a, b] + 1;
                        matrixMoveIterate(a - 1, b + 1, map);
                    }
                }
                if (map[a - 1, b] > map[a, b]) {
                    map[a - 1, b] = map[a, b] + 1;
                    matrixMoveIterate(a - 1, b, map);
                }
            }

            if (a + 1 < map.GetLength(0)) {
                if (b + 1 < map.GetLength(1)) {
                    if (map[a + 1, b + 1] > map[a, b]) {
                        map[a + 1, b + 1] = map[a, b] + 1;
                        matrixMoveIterate(a + 1, b + 1, map);
                    }
                }
                if (b - 1 >= 0) {
                    if (map[a + 1, b - 1] > map[a, b]) {
                        map[a + 1, b - 1] = map[a, b] + 1;
                        matrixMoveIterate(a + 1, b - 1, map);
                    }
                }
                if (map[a + 1, b] > map[a, b]) {
                    map[a + 1, b] = map[a, b] + 1;
                    matrixMoveIterate(a + 1, b, map);
                }
            }

            if (b - 1 >= 0) {
                if (map[a, b - 1] > map[a, b]) {
                    map[a, b - 1] = map[a, b] + 1;
                    matrixMoveIterate(a, b - 1, map);
                }
            }

            if (b + 1 < map.GetLength(1)) {
                if (map[a, b + 1] > map[a, b]) {
                    map[a, b + 1] = map[a, b] + 1;
                    matrixMoveIterate(a, b + 1, map);
                }
            }
            return map;
        }

        #endregion MatrixMove

        #region Save

        public static class JsonHelper {
            public static T[] FromJson<T>(string json) {
                Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
                return wrapper.Items;
            }

            public static string ToJson<T>(T[] array) {
                Wrapper<T> wrapper = new Wrapper<T>();
                wrapper.Items = array;
                return JsonUtility.ToJson(wrapper);
            }

            public static string ToJson<T>(T[] array, bool prettyPrint) {
                Wrapper<T> wrapper = new Wrapper<T>();
                wrapper.Items = array;
                return JsonUtility.ToJson(wrapper, prettyPrint);
            }

            [Serializable]
            private class Wrapper<T> {
                public T[] Items;
            }
        }

        [Serializable]
        public class SaveMap {
            public int x;
            public int y;
            public state type;

            public SaveMap(int i,int j,state t) {
                x = i;
                y = j;
                type = t;
            }
        }

        public void SaveToJson() {
            SaveMap[] save = new SaveMap[mapSaved.GetLength(0) * mapSaved.GetLength(1)];

            for (int i = 0; i < mapSaved.GetLength(0); i++) {
                for (int j = 0; j < mapSaved.GetLength(1); j++) {
                    save[i * mapSaved.GetLength(1) + j] = new SaveMap(i,j, mapSaved[i, j]);
                }
            }
            
            string saveToJson = JsonHelper.ToJson(save, true);
            Debug.Log(saveToJson);
            saveJson = "Maps/" + SceneManager.GetActiveScene().name + ".txt";
            File.WriteAllText(@"../" + nameOfGame + "/Assets/Resources/" + saveJson, saveToJson);
            UnityEditor.AssetDatabase.Refresh();
            Debug.Log(saveToJson);
        }

        public void SaveToJson(state[,] mapSaved) {
            SaveMap[] save = new SaveMap[mapSaved.GetLength(0) * mapSaved.GetLength(1)];

            for (int i = 0; i < mapSaved.GetLength(0); i++) {
                for (int j = 0; j < mapSaved.GetLength(1); j++) {
                    save[i * mapSaved.GetLength(1) + j].type = mapSaved[i, j];
                    save[i * mapSaved.GetLength(1) + j].x = i;
                    save[i * mapSaved.GetLength(1) + j].y = j;
                }
            }
            string saveToJson = JsonHelper.ToJson(save, true);
            saveJson = "Maps/" + SceneManager.GetActiveScene().name + ".txt";
            File.WriteAllText(@"../" + nameOfGame + "/Assets/Resources/" + saveJson, saveToJson);
            UnityEditor.AssetDatabase.Refresh();
            Debug.Log(saveToJson);
        }

        public void LoadFromJson() {
            UnityEditor.AssetDatabase.Refresh();
            saveJson = "Maps/" + SceneManager.GetActiveScene().name;
            TextAsset json = Resources.Load(saveJson) as TextAsset;
            Debug.Log(json);
            SaveMap[] save = JsonHelper.FromJson<SaveMap>(Resources.Load(saveJson).ToString());

            int maxx = 0;
            int maxy = 0;

            for (int i = 0; i < save.Length; i++) {
                if (save[i].x > maxx)
                    maxx = save[i].x;
                if (save[i].y > maxy)
                    maxy = save[i].y;
            }

            mapSaved = new state[maxx + 1, maxy + 1];
            for (int i = 0; i < save.Length; i++) {
                mapSaved[save[i].x, save[i].y] = save[i].type;
            }

            RecleanInt();
        }

        private void RecleanInt() {
            map = new int[mapSaved.GetLength(0), mapSaved.GetLength(1)];
            for (int i = 0; i < mapSaved.GetLength(0); i++) {
                for (int j = 0; j < mapSaved.GetLength(1); j++) {
                    if (mapSaved[i, j] == state.close) {
                        map[i, j] = -1;
                    } else
                        map[i, j] = int.MaxValue;
                }
            }
        }

        #endregion Save

    }
}