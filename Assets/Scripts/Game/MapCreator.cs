using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Values;

namespace Game{
    public class MapCreator : MonoBehaviour
    {
        [SerializeField]
        private ScriptableMap _loadMap;

        [SerializeField]
        private Toucher _touchElement;

        [SerializeField]
        private GameObject _let;

        private void Start(){
            if(_loadMap.map.Count==0)
                _loadMap.ReadMap();
            GenerateMap();
        }
        private void GenerateMap(){
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


    }
}