using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.SceneManagement;

namespace Values {
    [CreateAssetMenu(fileName = "ScriptableMap")]
    public class ScriptableMap : ScriptableObject {

        public List<List<int>> map=new List<List<int>>();

        public static string nameOfGame = "4";

        [SerializeField]
        private char empty = 'E';

        [SerializeField]
        private char close = 'C';

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
                if (data[i] == ';') {
                map.Add(new List<int>());
                }
            }
            map.RemoveAt(map.Count - 1);

        }

        #region Rewrite

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

        public List<List<int>> matrixMove(int a, int b) {
            List<List<int>> currentMap = map;
            currentMap[a][b] = 0;
            currentMap = matrixMoveIterate(a, b, currentMap);
            return currentMap;
        }

        public List<List<int>> matrixMove(int a, int b, List<List<int>> map) {
            map[a][b] = 0;
            map = matrixMoveIterate(a, b, map);
            return map;
        }

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
