using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Values;

namespace Game {
    public class Hero : MonoBehaviour {
        /// <summary>
        /// ����� ������������� ���� �����������
        /// </summary>
        private Toucher _currentTouch;
        /// <summary>
        /// ���������� ������� Toucher
        /// </summary>
        /// <returns>���������� ������� Toucher</returns>
        public Toucher GetToucher() { return _currentTouch; }
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

        private bool _moveLock = false;

        [SerializeField]
        private ScriptablePickHero _selectHero;

        private void OnMouseDown() {
            _selectHero.SelectHero = this;
        }

        /// <summary>
        /// �������������� ����� ��������� �����������
        /// </summary>
        /// <param name="toucher">������ �������������� �������</param>
        public void SetTouch(Toucher toucher) {
            _currentTouch = toucher;
            SetWay();
        }

        #region Move
        /// <summary>
        /// ���������� ������ �� �������� ����
        /// </summary>
        public void MoveAllWay() {
            _moveLock = true;
                StartCoroutine(MoveCoroutineAllWay());
        }
        /// <summary>
        /// �������� ��� ������� ����� �����������
        /// </summary>
        /// <returns>��������</returns>
        private IEnumerator MoveCoroutineAllWay() {
            for (int i = 0; i < way.Count; i++) {
                Debug.Log(way[i]);
                StartCoroutine(MoveCoroutine(time, new Vector3(way[i].x,transform.position.y,way[i].y)));
                yield return new WaitForSeconds(time);
                _moveLock = false;
            }
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
                int c = (int)_currentTouch.transform.position.x;
                int d = (int)_currentTouch.transform.position.z;

                List<List<int>> map = _map.matrixMove(a, b);
                for(int i=0;i<map.Count;i++)
                    for (int j=0;j<map[i].Count;j++)
                        Debug.Log($"{i},{j}: {map[i][j]}");

                List<Vector2Int> invetway = findWay(c, d, map);
                way.Clear();
                for (int i = 0; i < invetway.Count; i++) {
                    way.Add(invetway[invetway.Count - 1 - i]);
                }
            }
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
            if ((x >= 0) && (x < map.Count) && (y >= 0) && (y < map[x].Count)) {
                way = findWayIterate(x, y, map, way);
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
        private List<Vector2Int> findWayIterate(int x, int y, List<List<int>> map, List<Vector2Int> way) {
            List<Vector2Int> currentway = way;
            currentway.Add(new Vector2Int(x, y));
            if (map[x][y] == 0)
                return currentway;
            if (x - 1 >= 0) {
                if ((map[x - 1][y] < map[x][y]) && (map[x - 1][y] >= 0)) {
                    currentway = findWayIterate(x - 1, y, map, currentway);
                    return currentway;
                }

                if (y - 1 >= 0) {
                    if ((map[x - 1][y - 1] < map[x][y]) && (map[x - 1][y - 1] >= 0)) {
                        currentway = findWayIterate(x - 1, y - 1, map, currentway);
                        return currentway;
                    }
                }

                if (y + 1 < map[x - 1].Count) {
                    if ((map[x - 1][y + 1] < map[x][y]) && (map[x - 1][y + 1] >= 0)) {
                        currentway = findWayIterate(x - 1, y + 1, map, currentway);
                        return currentway;
                    }
                }
            }

            if (x + 1 < map.Count) {
                if ((map[x + 1][y] < map[x][y]) && (map[x + 1][y] >= 0)) {
                    currentway = findWayIterate(x + 1, y, map, currentway);
                    return currentway;
                }

                if (y - 1 >= 0) {
                    if ((map[x + 1][y - 1] < map[x][y]) && (map[x + 1][y - 1] >= 0)) {
                        currentway = findWayIterate(x + 1, y - 1, map, currentway);
                        return currentway;
                    }
                }

                if (y + 1 < map[x].Count) {
                    if ((map[x + 1][y + 1] < map[x][y]) && (map[x + 1][y + 1] >= 0)) {
                        currentway = findWayIterate(x + 1, y + 1, map, currentway);
                        return currentway;
                    }
                }
            }

            if (y - 1 >= 0) {
                if ((map[x][y - 1] < map[x][y]) && (map[x][y - 1] >= 0)) {
                    currentway = findWayIterate(x, y - 1, map, currentway);
                    return currentway;
                }
            }

            if (y + 1 < map[x].Count) {
                if ((map[x][y + 1] < map[x][y]) && (map[x][y - 1] >= 0)) {
                    currentway = findWayIterate(x, y + 1, map, currentway);
                    return currentway;
                }
            }
            return currentway;
        }
        #endregion FindWay
    }
}