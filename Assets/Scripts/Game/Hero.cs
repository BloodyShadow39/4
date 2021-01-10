using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Values;
using Events;

namespace Game {
    public class Hero : MonoBehaviour {
        /// <summary>
        /// ����� ������������� ���� �����������
        /// </summary>
        private Vector2Int _target;

        public ScriptablePlayer player;
        /// <summary>
        /// ���������� ������� Toucher
        /// </summary>
        /// <returns>���������� ������� Toucher</returns>
        public Vector2Int GetTarget() { return _target; }
        /// <summary>
        /// ����� �����������
        /// </summary>
        [SerializeField]
        private ScriptableMap _map;
        /// <summary>
        /// ������� ���� �� �����
        /// </summary>
        public List<Vector2Int> way;
        /// <summary>
        /// ����� ����������� ��� ����������� 1�� ������
        /// </summary>
        public float time =5;

        [SerializeField]
        private EventDispatcher _wayDefenited;

        private bool _moveLock = false;

        [SerializeField]
        private ScriptablePickHero _selectHero;

        private void OnMouseDown() {
            _selectHero.SelectHero = this;
        }
        [Range(0,100)]
        public int movePoints=0;

        /// <summary>
        /// �������������� ����� ��������� �����������
        /// </summary>
        /// <param name="toucher">������ �������������� �������</param>
        public void SetTouch(Toucher toucher) {
            _target = new Vector2Int((int)toucher.transform.position.x,(int)toucher.transform.position.z);
            SetWay();
        }

        public void SetTouch(Vector2Int target) {
            _target = target;
            SetWay();
        }

        public void SetTouch(int x, int y) {
            _target = new Vector2Int(x,y);
            SetWay();
        }

        #region Move
        /// <summary>
        /// ���������� ������ �� �������� ����
        /// </summary>
        public void MoveAllWay() {
            if(!_moveLock)
                StartCoroutine(MoveCoroutineAllWay());
        }
        /// <summary>
        /// �������� ��� ������� ����� �����������
        /// </summary>
        /// <returns>��������</returns>
        private IEnumerator MoveCoroutineAllWay() {
            _moveLock = true;
            while ((way.Count > 0)&&(movePoints>0)) {
                StartCoroutine(MoveCoroutine(time, new Vector3(way[0].x, transform.position.y, way[0].y)));
                yield return new WaitForSeconds(time);
                way.RemoveAt(0);
                movePoints-=1;
                
            }
            if(movePoints==0){
                    Debug.LogWarning("NO Move Points");
            }
            _moveLock = false;
        }
        /// <summary>
        /// ����������� ����������� ������ �� ����� t � ����� ������� (Vector3) nextPosition 
        /// </summary>
        /// <param name="time">����� ����������, �� ����� ���� �������������</param>
        /// <param name="nextPosition">��������� �������</param>
        /// <returns>��������</returns>
        private IEnumerator MoveCoroutine(float time,Vector3 nextPosition) {
            float moveTime = 0f;
            Vector3 firstPosition = transform.position;
            if (time >= 0) {
                while (moveTime < time) {
                    transform.position = new Vector3(Mathf.Lerp(firstPosition.x, nextPosition.x, moveTime), transform.position.y, Mathf.Lerp(firstPosition.z, nextPosition.z, moveTime));
                    moveTime += Time.deltaTime;
                    yield return null;
                }
                transform.position = nextPosition;
            }
            else {
                Debug.LogError("Time to move cannot be negative");
                yield return null;
            }
        }
        #endregion Move

        /// <summary>
        /// ������������ ������� ���� �� ������� �����
        /// </summary>
        /// <returns>���� ����� ������������</returns>
        public List<Vector2Int> SetWay() {
            if (!_moveLock) {
                int a = (int)transform.position.x;
                int b = (int)transform.position.z;
                int c = (int)_target.x;
                int d = (int)_target.y;

                List<List<int>> map = _map.matrixMove(a, b);

                List<Vector2Int> invetway = findWay(c, d, map);
                way.Clear();
                for (int i = 0; i < invetway.Count; i++) {
                    way.Add(invetway[invetway.Count - 1 - i]);
                }
            }
            _wayDefenited.Dispatch();
            return way;
        }

        //���� ����� �� ������� �� ����� b,c
        #region FindWay
        /// <summary>
        /// ���� ���� �� ������� �� ����� x, y �� ����� map
        /// </summary>
        /// <param name="x">x ���������� �����</param>
        /// <param name="y">y ���������� �����</param>
        /// <param name="map">����� ����������� ���������� ��������� � �������� �� 0����� ����� �� ����� ������ (������������� �������� �������� ��������������� ������)</param>
        /// <returns>������ ����� �� ������� ���� ������ ����� ��������� �� ����� ���������� �� ������� �����</returns>
        private List<Vector2Int> findWay(int x, int y, List<List<int>> map) {
            List<Vector2Int> way = new List<Vector2Int>();
            List<List<char>> currentMapOfObjects = _map.mapOfObjects;
            if ((x >= 0) && (x < map.Count) && (y >= 0) && (y < map[x].Count)) {
                way = findWayIterate(x, y, map, way, currentMapOfObjects);
            }
            return way;
        }
        /// <summary>
        /// �������� ��� ������ ������ �����������
        /// </summary>
        /// <param name="x">x ���������� �����</param>
        /// <param name="y">y ���������� �����</param>
        /// <param name="map">����� �����������</param>
        /// <param name="way">����� ������� ��� ���������</param>
        /// <returns></returns>
        private List<Vector2Int> findWayIterate(int x, int y, List<List<int>> map, List<Vector2Int> way, List<List<char>> mapOfObjects) {
            List<Vector2Int> currentway = way;
            currentway.Add(new Vector2Int(x, y));
            if (map[x][y] == 0)
                return currentway;

            if (y - 1 >= 0) {
                if ((map[x][y - 1] < map[x][y]) && (map[x][y - 1] >= 0) && mapOfObjects[x][y - 1] != _map.GetUseful()) {
                    currentway = findWayIterate(x, y - 1, map, currentway, mapOfObjects);
                    return currentway;
                }
            }

            if (y + 1 < map[x].Count) {
                if ((map[x][y + 1] < map[x][y]) && (map[x][y + 1] >= 0) && mapOfObjects[x][y + 1] != _map.GetUseful()) {
                    currentway = findWayIterate(x, y + 1, map, currentway, mapOfObjects);
                    return currentway;
                }
            }

            if (x - 1 >= 0) {
                if ((map[x - 1][y] < map[x][y]) && (map[x - 1][y] >= 0) && mapOfObjects[x - 1][y] != _map.GetUseful()) {
                    currentway = findWayIterate(x - 1, y, map, currentway, mapOfObjects);
                    return currentway;
                }

                if (y - 1 >= 0) {
                    if ((map[x - 1][y - 1] < map[x][y]) && (map[x - 1][y - 1] >= 0) && mapOfObjects[x - 1][y - 1] != _map.GetUseful()) {
                        currentway = findWayIterate(x - 1, y - 1, map, currentway, mapOfObjects);
                        return currentway;
                    }
                }

                if (y + 1 < map[x - 1].Count) {
                    if ((map[x - 1][y + 1] < map[x][y]) && (map[x - 1][y + 1] >= 0) && mapOfObjects[x - 1][y + 1] != _map.GetUseful()) {
                        currentway = findWayIterate(x - 1, y + 1, map, currentway, mapOfObjects);
                        return currentway;
                    }
                }
            }

            if (x + 1 < map.Count) {
                if ((map[x + 1][y] < map[x][y]) && (map[x + 1][y] >= 0) && mapOfObjects[x + 1][y] != _map.GetUseful()) {
                    currentway = findWayIterate(x + 1, y, map, currentway, mapOfObjects);
                    return currentway;
                }

                if (y - 1 >= 0) {
                    if ((map[x + 1][y - 1] < map[x][y]) && (map[x + 1][y - 1] >= 0) && mapOfObjects[x + 1][y - 1] != _map.GetUseful()) {
                        currentway = findWayIterate(x + 1, y - 1, map, currentway, mapOfObjects);
                        return currentway;
                    }
                }

                if (y + 1 < map[x].Count) {
                    if ((map[x + 1][y + 1] < map[x][y]) && (map[x + 1][y + 1] >= 0) && mapOfObjects[x + 1][y + 1] != _map.GetUseful()) {
                        currentway = findWayIterate(x + 1, y + 1, map, currentway, mapOfObjects);
                        return currentway;
                    }
                }
            }

            

            
            return currentway;
        }
        #endregion FindWay
    }
}