using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Values;

namespace Game{
    public class MapCreator : MonoBehaviour
    {
        public static MapCreator Instance;
        [SerializeField]
        private ScriptableMap _loadMap;
        [SerializeField]
        private Toucher _touchElement;
        [SerializeField]
        private GameObject _let;

        private void Awake() {
                if (Instance != null) {
                    Destroy(Instance);
                }
                Instance = this;
        }

        private void Start(){
            GenerateMap();
        }

        public void GenerateMap(){
            bool isCreated;
            for (int i=0;i<_loadMap.map.Count;i++){
                for(int j=0;j<_loadMap.map[i].Count;j++){
                    isCreated = false;
                    foreach (Transform child in transform) {
                        if ((child.position.x == i ) && (child.position.z == j) && (_loadMap.mapSaved[i,j]!=ScriptableMap.state.useful)) {
                            isCreated = true;
                            break;
                        }
                    }
                    if(!isCreated)
                        if (_loadMap.map[i][j]>=0){
                        
                            Toucher tmp=Instantiate(_touchElement,gameObject.transform);
                            tmp.gameObject.transform.position=new Vector3(i, -1,j);
                        }
                        else{
                            GameObject tmp=Instantiate(_let,gameObject.transform);
                            tmp.gameObject.transform.position=new Vector3(i, 0,j);
                        }
                }
            }
        }

        public void FormirateMap(){
            int maxy=0;
            int maxx=0;

            List<List<int>> map=new List<List<int>>();

            

            foreach(Transform child in transform){
                if(child.position.x>maxx){
                    maxx=(int)child.position.x;
                }
                if(child.position.z>maxy){
                    maxy=(int)child.position.z;
                }
            }

            ScriptableMap.state[,] mapStates = new ScriptableMap.state[maxx, maxy];

            for(int i=0;i<maxx+1;i++){
                map.Add(new List<int>());
                for(int j=0;j<maxy+1;j++){
                    map[i].Add(int.MaxValue);
                    mapStates[i, j] = ScriptableMap.state.empty;
                }
            }
            foreach(Transform child in transform){
                if(!child.gameObject.TryGetComponent(out Type obj)){
                    if (((int)(child.position.x) >= 0) && ((int)(child.position.z) >= 0)) {
                        map[(int)(child.position.x )][(int)(child.position.z)] = -1;
                        mapStates[(int)(child.position.x), (int)(child.position.z)] = obj.type;
                    }
                }
            }

            _loadMap.map = map;
            _loadMap.mapSaved = mapStates;

        }

    }
}