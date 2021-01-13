using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.SceneManagement;

namespace Values {
    [CreateAssetMenu(fileName = "ScriptableMap")]
    public class ScriptableMap : ScriptableObject {

        public List<List<int>> map=new List<List<int>>();

        public List<List<char>> mapOfObjects = new List<List<char>>();

        public static string nameOfGame = "4";

        public enum state { empty, useful, close }

        [Range(0,1000)]
        public int width;

        [Range(0, 1000)]
        public int height;

        public state[,] mapStates=null;

        public void FillEmptyMap() {
            mapStates = new state[width, height];
            for(int i = 0; i < width; i++) {
                for(int j = 0; j < height; j++) {
                    mapStates[i,j] = state.empty;
                }
            }
        }

        [SerializeField]
        private char empty = 'E';
        public char GetEmpty() {
            return empty;
        }

        [SerializeField]
        private char close = 'C';

        [SerializeField]
        private char useful = 'U';
        public char GetUseful() {
            return useful;
        }


        public void ReadMap() {
            string filepath = "Maps/" + SceneManager.GetActiveScene().name;
            TextAsset textAsset = Resources.Load(filepath) as TextAsset;
            string data = textAsset.text;
            map.Add(new List<int>());

            for (int i = 0; i < data.Length; i++) {
                if (data[i]== empty)  {
                    map[map.Count - 1].Add(int.MaxValue);
                }
                if (data[i] == close) {
                    map[map.Count - 1].Add(-1);
                }
                if (data[i] == useful) {
                    map[map.Count - 1].Add(int.MaxValue);
                }
                if (data[i] == ';') {
                map.Add(new List<int>());
                }
            }
            map.RemoveAt(map.Count - 1);

            mapOfObjects.Add(new List<char>());

            for (int i = 0; i < data.Length; i++) {
                if (data[i] == empty) {
                    mapOfObjects[mapOfObjects.Count - 1].Add(empty);
                }
                if (data[i] == close) {
                    mapOfObjects[mapOfObjects.Count - 1].Add(close);
                }
                if (data[i] == useful) {
                    mapOfObjects[mapOfObjects.Count - 1].Add(useful);
                }
                if (data[i] == ';') {
                    mapOfObjects.Add(new List<char>());
                }
            }
            mapOfObjects.RemoveAt(mapOfObjects.Count - 1);
        }

        #region Rewrite
        /// <summary>
        /// «аписывает в файл карту в текущем ассете
        /// </summary>
        public void RewriteMap(){
            string filepath = "Maps/" + SceneManager.GetActiveScene().name + ".txt";
            map.Clear();
            string newMap="";
            bool isNewLine;
            for(int i=0;i<map.Count;i++){
                isNewLine = true;
                for (int j=0;j<map[i].Count;j++){
                    if (isNewLine) {
                        if (map[i][j] >= 0) {
                            newMap = newMap + empty;
                        }
                        else
                            newMap = newMap + close;
                        isNewLine = false;
                    }
                    else {
                        if (map[i][j] >= 0) {
                            newMap = newMap  + empty;
                        }
                        else
                            newMap = newMap  + close;
                    }
                }
                newMap=newMap+";";
            }
            File.WriteAllText(@"../"+nameOfGame +"/Assets/Resouces/"+ filepath, newMap);
        }
        /// <summary>
        /// «амен€ет карту в текущем ассете и записывает ее в файл
        /// </summary>
        public void RewriteMap(List<List<int>> tmap) {
            string filepath = "Maps/" + SceneManager.GetActiveScene().name + ".txt";
            map.Clear();
            map = tmap;
            string newMap = "";
            bool isNewLine;
            for (int i = 0; i < map.Count; i++) {
                isNewLine = true;
                for (int j = 0; j < map[i].Count; j++) {
                    if (isNewLine) {
                        if (map[i][j] >= 0) {
                            newMap = newMap + empty;
                        }
                        else
                            newMap = newMap + close;
                        isNewLine = false;
                    }
                    else {
                        if (map[i][j] >= 0) {
                            newMap = newMap + empty;
                        }
                        else
                            newMap = newMap + close;
                    }
                }
                newMap = newMap + ";";
            }
            File.WriteAllText(@"../" + nameOfGame + "/Assets/Resources/" + filepath, newMap);
        }

        #endregion Rewrite

        #region MatrixMove
        /// <summary>
        /// ѕросчитывает карту перемещени€ от начальной точки
        /// </summary>
        /// <param name="a">x координата</param>
        /// <param name="b">y координата</param>
        /// <returns> арта перемещени€ от начальной точки</returns>
        public List<List<int>> matrixMove(int a, int b) {
            List<List<int>> currentMap = new List<List<int>>();
            for(int i = 0; i < map.Count; i++) {
                currentMap.Add(new List<int>());
                for(int j = 0; j < map[i].Count; j++) {
                    currentMap[i].Add(map[i][j]);
                }
            }
            currentMap[a][b] = 0;
            currentMap = matrixMoveIterate(a, b, currentMap);
            return currentMap;
        }
        /// <summary>
        /// ѕолностью перепосчитывает карту на основании текущей
        /// </summary>
        /// <param name="a">x координата текущей точки</param>
        /// <param name="b">y координата текущей точки</param>
        /// <param name="map"> арта перемещени€ (начальна€)</param>
        /// <returns> арта перемещени€ (полна€)</returns>
        public List<List<int>> matrixMove(int a, int b, List<List<int>> map) {
            List<List<int>> currentMap = map;
            currentMap[a][b] = 0;
            currentMap = matrixMoveIterate(a, b, map);
            return currentMap;
        }
        /// <summary>
        /// ѕроизводит итерацию просчета от текущей точки
        /// </summary>
        /// <param name="a">x координата текущей точки</param>
        /// <param name="b">y координата текущей точки</param>
        /// <param name="map"> арта перемещени€ (не полна€)</param>
        /// <returns> арту перемещени€ (не полна€)</returns>
        private List<List<int>> matrixMoveIterate(int a, int b, List<List<int>> map) {
            if (a - 1 >= 0) {
                if (b - 1 >= 0) {
                    if (map[a - 1][b - 1] > map[a][b]) {
                        map[a - 1][b - 1] = map[a][b] + 1;
                        matrixMoveIterate(a - 1, b - 1, map);
                    }
                }
                if (b + 1 < map[a - 1].Count) {
                    if (map[a - 1][b + 1] > map[a][b]) {
                        map[a - 1][b + 1] = map[a][b] + 1;
                        matrixMoveIterate(a - 1, b + 1, map);
                    }
                }
                if (map[a - 1][b] > map[a][b]) {
                    map[a - 1][b] = map[a][b] + 1;
                    matrixMoveIterate(a - 1, b, map);
                }
            }

            if (a + 1 < map.Count) {
                if (b + 1 < map[a + 1].Count) {
                    if (map[a + 1][b + 1] > map[a][b]) {
                        map[a + 1][b + 1] = map[a][b] + 1;
                        matrixMoveIterate(a + 1, b + 1, map);
                    }
                }
                if (b - 1 >= 0) {
                    if (map[a + 1][b - 1] > map[a][b]) {
                        map[a + 1][b - 1] = map[a][b] + 1;
                        matrixMoveIterate(a + 1, b - 1, map);
                    }
                }
                if (map[a + 1][b] > map[a][b]) {
                    map[a + 1][b] = map[a][b] + 1;
                    matrixMoveIterate(a + 1, b, map);
                }
            }

            if (b - 1 >= 0) {
                if (map[a][b - 1] > map[a][b]) {
                    map[a][b - 1] = map[a][b] + 1;
                    matrixMoveIterate(a, b - 1, map);
                }
            }

            if (b + 1 < map[a].Count) {
                if (map[a][b + 1] > map[a][b]) {
                    map[a][b + 1] = map[a][b] + 1;
                    matrixMoveIterate(a, b + 1, map);
                }
            }
            return map;
        }

        #endregion MatrixMove

    }
}
