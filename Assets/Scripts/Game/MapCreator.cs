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

        [Range(0, 100)]
        public int width;

        [Range(0, 100)]
        public int height;

        private int[,] map = null;
        public state[,] mapSaved;

        private struct usefulName {
            public int x;
            public int y;
            public string name;

            public usefulName(int dx, int dy, string dname) {
                x = dx;
                y = dy;
                name = dname;
            }
        }

        private List<usefulName> _loadUseful;

        public string FindAtPosition(int dx, int dy) {
            foreach(usefulName name in _loadUseful) {
                if (name.x == dx && name.y == dy)
                    return name.name;
            }
            return null;
        }

        public ScriptableListObjects list;

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
                                string name = FindAtPosition(i, j);
                                if (name != null) {
                                    if (list.FindByName(name) != null) {
                                        UsefullObject tmp = Instantiate(list.FindByName(name), gameObject.transform);
                                        tmp.gameObject.transform.position = new Vector3(i, 0, j);
                                    }
                                }
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
                if(child.gameObject.TryGetComponent(out Type obj)){
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

        /// <summary>
        /// Find Usefull object at children at x and y positions
        /// </summary>
        /// <param name="x">position at X coordinate</param>
        /// <param name="z">position at Z coordinate</param>
        /// <returns>Useful object if find at position, null else</returns>
        private UsefullObject findUseful(int x, int z) {
            UsefullObject usefull;
            foreach(Transform child in transform) {
                if (child.TryGetComponent(out usefull))
                    if (child.position.x == x && child.position.z == z)
                        return usefull;
            }
            return null;
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
            public string uniqueKey;

            public SaveMap(int i, int j, state t, string uniqKey = "") {
                x = i;
                y = j;
                type = t;
                uniqueKey = uniqKey;
            }
        }

        public void SaveToJson() {
            SaveMap[] save = new SaveMap[mapSaved.GetLength(0) * mapSaved.GetLength(1)];

            for (int i = 0; i < mapSaved.GetLength(0); i++) {
                for (int j = 0; j < mapSaved.GetLength(1); j++) {
                    if (mapSaved[i, j] != state.useful)
                        save[i * mapSaved.GetLength(1) + j] = new SaveMap(i, j, mapSaved[i, j]);
                    else if (findUseful(i, j) != null) {
                        string name = findUseful(i, j).gameObject.name;
                        if (name.Contains("(Clone)")) {
                            name.Remove(name.IndexOf("(Clone)"));
                        }
                            save[i * mapSaved.GetLength(1) + j] = new SaveMap(i, j, mapSaved[i, j], name);
                        }
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
            _loadUseful = new List<usefulName>();
            for (int i = 0; i < save.Length; i++) {
                mapSaved[save[i].x, save[i].y] = save[i].type;
                if (save[i].type == state.useful) {
                    _loadUseful.Add(new usefulName(save[i].x, save[i].y, save[i].uniqueKey));
                }
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

        /// <summary>
        /// Method set objects at current object at int position at x and z coordinates
        /// </summary>
        public void CheckToCurrentPosition() {
            foreach (Transform child in transform) {
                child.position = new Vector3(Mathf.Round(child.position.x), child.position.y, Mathf.Round(child.position.z));
            }
        }

        public void SetCorrectMapSize() {
            CheckToCurrentPosition();
            List<GameObject> ToDestroy=new List<GameObject>();
            foreach (Transform child in transform) {
                if (child.position.x > width) {
                    ToDestroy.Add(child.gameObject);
                }
                if (child.position.z > height) {
                    ToDestroy.Add(child.gameObject);
                } 
            }
            while (ToDestroy.Count > 0) {
                GameObject tmp = ToDestroy[0];
                ToDestroy.RemoveAt(0);
                DestroyImmediate(tmp);
            }
            for(int x = 0; x <= width; x++) {
                for(int z = 0; z <= height; z++) {
                    bool noObject = true;
                    foreach (Transform child in transform) {
                        if (child.position.x == x && child.position.z == z)
                            noObject = false;
                    }
                    if (noObject) {
                        Instantiate(_emptyObject, new Vector3(x, -1, z), Quaternion.identity, transform);
                    }
                }
            }
        }


    }
}