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
        

        
    }
}
