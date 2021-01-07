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
            if(_loadMap.map.Count==0)
                _loadMap.ReadMap();

            GenerateMap();
            //FormirateMap();
        }
        public void GenerateMap(){
            for(int i=0;i<_loadMap.map.Count;i++){
                for(int j=0;j<_loadMap.map[i].Count;j++){
                    if(_loadMap.map[i][j]>=0){
                        Toucher tmp=Instantiate(_touchElement,gameObject.transform);
                        tmp.gameObject.transform.position=new Vector3(i,-1,j);
                    }
                    else{
                        GameObject tmp=Instantiate(_let,gameObject.transform);
                        tmp.gameObject.transform.position=new Vector3(i,0,j);
                    }
                }
            }
        }

        public void FormirateMap(){
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
            _loadMap.map.Clear();
            for(int i=0;i<maxy+1;i++){
                _loadMap.map.Add(new List<int>());
                for(int j=0;j<maxx+1;j++){
                    _loadMap.map[i].Add(int.MaxValue);
                }
            }
            foreach(Transform child in transform){
                if(!child.gameObject.TryGetComponent(out Toucher touch)){
                    Debug.Log($"{_loadMap.map.Count} - {(int)child.position.z}; ");
                    if (((int)child.position.x >= 0) && ((int)child.position.z >= 0)) {
                        Debug.Log($"{_loadMap.map.Count} - {(int)child.position.z}; ") ;
                        _loadMap.map[(int)child.position.z][(int)child.position.x] = -1;
                    }
                }
            }
            _loadMap.RewriteMap();
        }

    }
}