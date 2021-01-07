using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System;

namespace Values {
    [CreateAssetMenu(fileName = "ScriptableMap")]
    public class ScriptableMap : ScriptableObject {

        public List<List<int>> map=new List<List<int>>();

        public TextAsset text;

        public static string nameOfGame = "4";

        private string filepath;

        public void ReadMap() {

            string data=text.text;
            int tmp = 0;
            bool isNegative = false;
            map.Add(new List<int>());
            for (int i = 0; i < data.Length; i++) {
                if ((data[i] == '0') || (data[i] == '1') || (data[i] == '2') || (data[i] == '3') || (data[i] == '4') || (data[i] == '5') || (data[i] == '6') || (data[i] == '7') || (data[i] == '8') || (data[i] == '9') || (data[i] == '-')) {
                    if (data[i] == '-') {
                        isNegative = true;
                    }
                    else
                    tmp = tmp * 10 + (int)Char.GetNumericValue(data[i]);
                }
                else
                    if (data[i] == ' ') {
                    if (isNegative) {
                        tmp = -1 * tmp;
                        isNegative = false;
                    }
                    map[map.Count - 1].Add(tmp);
                    tmp = 0;
                }
                else
                    if (data[i] == ';') {
                    if (isNegative) {
                        tmp = -1 * tmp;
                        isNegative = false;
                    }
                    map[map.Count - 1].Add(tmp);
                    map.Add(new List<int>());
                    tmp = 0;
                }
        }

        map.RemoveAt(map.Count - 1);
        
            /*Debug.Log(map);
        List<List<int>> mapTransponitive=map;
        for(int i=0;i<map.Count;i++){
            for(int j=0;j<map.Count;j++){
             mapTransponitive[i][j]=map[i][map.Count-j-1];   
            }
        }
        map=mapTransponitive;
        Debug.Log(map);*/
        }

        public void RewriteMap(){
            filepath= @"../" + nameOfGame + "/Assets/Resourses/Maps/"+ text.name+".txt";
            string newMap="";
            bool isNewLine = true;
            for(int i=0;i<map.Count;i++){
                isNewLine = true;
                for (int j=0;j<map[i].Count;j++){
                    if (isNewLine) {
                         newMap = newMap + map[i][j].ToString();
                        isNewLine = false;
                    }
                    else
                        newMap = newMap + " " + map[i][j].ToString();
                }
                newMap=newMap+";";
            }
            File.WriteAllText(filepath,newMap);
        }

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
