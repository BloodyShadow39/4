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
        private Toucher _emptyObject;
        [SerializeField]
        private GameObject _closeObject;

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
            if (_loadMap.map != null) {
                for (int i = 0; i < _loadMap.map.GetLength(0); i++) {
                    for (int j = 0; j < _loadMap.map.GetLength(1); j++) {
                        isCreated = false;
                        foreach (Transform child in transform) {
                            Type obj;
                            if ((child.position.x == i) && (child.position.z == j) && (child.gameObject.TryGetComponent(out obj)) && (obj.type != ScriptableMap.state.entity) && (obj.type != ScriptableMap.state.hero)) {
                                isCreated = true;
                                break;
                            }
                        }
                        if (!isCreated)
                            if (_loadMap.mapSaved[i, j] == ScriptableMap.state.empty) {

                                Toucher tmp = Instantiate(_emptyObject, gameObject.transform);
                                tmp.gameObject.transform.position = new Vector3(i, -1, j);
                            }
                            else if (_loadMap.mapSaved[i, j] == ScriptableMap.state.close) {
                                GameObject tmp = Instantiate(_closeObject, gameObject.transform);
                                tmp.gameObject.transform.position = new Vector3(i, 0, j);
                            }
                    }
                }

                Debug.Log($"Map {_loadMap.name} generated.");
            }
            else
                Debug.Log("Map not created");
        }

        public void FormirateMap(){
            bool isCorrect = true;
            foreach (Transform child1 in transform) {
                foreach (Transform child2 in transform) {
                    if(child1!=child2)
                        if((child1.position.x== child2.position.x)&&(child1.position.z == child2.position.z)) {
                            Debug.LogError($"At ({child1.position.x},{child1.position.z}) position find 2 object, u can save only one object");
                            isCorrect = false;
                        }
                }
            }

            if (!isCorrect)
                return;

            int maxy=0;
            int maxx=0;

            

            foreach(Transform child in transform){
                if(child.position.x>maxx){
                    maxx=(int)child.position.x;
                }
                if(child.position.z>maxy){
                    maxy=(int)child.position.z;
                }
            }
            int[,] map = new int[maxx+1,maxy+1];

            ScriptableMap.state[,] mapStates = new ScriptableMap.state[maxx+1, maxy+1];

            for(int i=0;i<maxx+1;i++){
                for(int j=0;j<maxy+1;j++){
                    map[i, j] = int.MaxValue;
                    mapStates[i, j] = ScriptableMap.state.empty;
                }
            }

            foreach(Transform child in transform){
                if(!child.gameObject.TryGetComponent(out Type obj)){
                    if (((int)(child.position.x) >= 0) && ((int)(child.position.z) >= 0)) {
                        map[(int)(child.position.x ),(int)(child.position.z)] = -1;
                        mapStates[(int)(child.position.x), (int)(child.position.z)] = obj.type;
                    }
                }
            }

            _loadMap.map = map;
            if((_loadMap.mapSaved!=null) &&(_loadMap.mapSaved.GetLength(0)==mapStates.GetLength(0)) && (_loadMap.mapSaved.GetLength(1) == mapStates.GetLength(1)))
             _loadMap.mapSaved = mapStates;
            else {
                _loadMap.mapSaved= new ScriptableMap.state[maxx + 1, maxy + 1];
                _loadMap.mapSaved = mapStates;
            }

            Debug.Log($"Map {_loadMap.name} rewrited.");
        }

    }
}