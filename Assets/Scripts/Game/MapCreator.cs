using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Values;

namespace Game{
    public class MapCreator : MonoBehaviour
    {
        /// <summary>
        /// ��������� ������������� ������� �������
        /// </summary>
        public static MapCreator Instance;
        /// <summary>
        /// ������ �� ����� � ������� ��������� �����
        /// </summary>
        [SerializeField]
        private ScriptableMap _loadMap;
        /// <summary>
        /// ������ ����������� ������ ������ � ������� ����� �����������������
        /// </summary>
        [SerializeField]
        private Toucher _touchElement;
        /// <summary>
        /// ������ ����������� �������������� ������
        /// </summary>
        [SerializeField]
        private GameObject _let;

        /// <summary>
        /// ���������� ���� ��� ������� ��������� ����� �� ������� �����
        /// </summary>
        private void Awake() {
                if (Instance != null) {
                    Destroy(Instance);
                }
                Instance = this;
        }
        /// <summary>
        /// ��������� �� ������ ������� ����� �������� �����
        /// </summary>
        private void Start(){
            if(_loadMap.map.Count==0)
                _loadMap.ReadMap();

            GenerateMap();
        }
        /// <summary>
        /// ���������� ����� ���������� � ������
        /// </summary>
        public void GenerateMap(){
            bool isCreated;
            for (int i=0;i<_loadMap.map.Count;i++){
                for(int j=0;j<_loadMap.map[i].Count;j++){
                    isCreated = false;
                    Debug.Log($"{_loadMap.mapOfObjects[i][j]};{i};{j}");
                    foreach (Transform child in transform) {
                        if ((child.position.x == i ) && (child.position.z == j) && (_loadMap.mapOfObjects[i][j]!=_loadMap.GetUseful())) {
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
        /// <summary>
        /// ���������� ������� ����� � �����
        /// </summary>
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
            for(int i=0;i<maxx+1;i++){
                map.Add(new List<int>());
                for(int j=0;j<maxy+1;j++){
                    map[i].Add(int.MaxValue);
                }
            }
            foreach(Transform child in transform){
                if(!child.gameObject.TryGetComponent(out Toucher touch)){
                    if (((int)(child.position.x) >= 0) && ((int)(child.position.z) >= 0)) {
                        map[(int)(child.position.x )][(int)(child.position.z)] = -1;
                    }
                }
            }
            _loadMap.RewriteMap(map);
        }

    }
}