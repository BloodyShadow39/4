using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Values;

namespace Game {
    public class Hero : MonoBehaviour {

        private Toucher _currentTouch;

        [SerializeField]
        private ScriptableMap _map;

        public List<Vector2Int> way;

        public void SetTouch(Toucher toucher) {
            _currentTouch = toucher;
        }

        public void SetWay() {
            int a = (int)transform.position.x;
            int b = (int)transform.position.z;
            int c = (int)_currentTouch.transform.position.x;
            int d = (int)_currentTouch.transform.position.z;

            List<List<int>> map = _map.matrixMove(a, b);
            way = findWay(c, d, map);
        }

        //»щет пусть по матрице от точки b,c
        #region FindWay
        private List<Vector2Int> findWay(int b, int c, List<List<int>> map) {
            List<Vector2Int> way = new List<Vector2Int>();
            if ((b >= 0) && (b < map.Count) && (c >= 0) && (c < map[b].Count)) {
                way = findWayIterate(b, c, map, way);
            }
            return way;
        }
        private List<Vector2Int> findWayIterate(int b, int c, List<List<int>> map, List<Vector2Int> way) {
            List<Vector2Int> currentway = way;
            currentway.Add(new Vector2Int(b, c));
            Debug.Log($"{b},{c}");
            if (map[b][c] == 0)
                return currentway;
            if (b - 1 >= 0) {
                if (c - 1 >= 0) {
                    if ((map[b - 1][c - 1] < map[b][c]) && (map[b - 1][c - 1] >= 0)) {
                        currentway = findWayIterate(b - 1, c - 1, map, currentway);
                        return currentway;
                    }
                }

                if (c + 1 < map[b - 1].Count) {
                    if ((map[b - 1][c + 1] < map[b][c]) && (map[b - 1][c + 1] >= 0)) {
                        currentway = findWayIterate(b - 1, c + 1, map, currentway);
                        return currentway;
                    }
                }

                if ((map[b - 1][c] < map[b][c]) && (map[b - 1][c] >= 0)) {
                    currentway = findWayIterate(b - 1, c, map, currentway);
                    return currentway;
                }

            }

            if (b + 1 < map.Count) {
                Debug.Log($"b:{b + 1}");
                if (c - 1 >= 0) {
                    if ((map[b + 1][c - 1] < map[b][c]) && (map[b + 1][c - 1] >= 0)) {
                        currentway = findWayIterate(b + 1, c - 1, map, currentway);
                        return currentway;
                    }
                }

                if (c + 1 < map[b].Count) {
                    if ((map[b + 1][c + 1] < map[b][c]) && (map[b + 1][c + 1] >= 0)) {
                        currentway = findWayIterate(b + 1, c + 1, map, currentway);
                        return currentway;
                    }
                }

                if ((map[b + 1][c] < map[b][c]) && (map[b + 1][c] >= 0)) {
                    currentway = findWayIterate(b + 1, c, map, currentway);
                    return currentway;
                }

            }

            if (c - 1 >= 0) {
                if ((map[b][c - 1] < map[b][c]) && (map[b][c - 1] >= 0)) {
                    currentway = findWayIterate(b, c - 1, map, currentway);
                    return currentway;
                }
            }

            if (c + 1 < map[b].Count) {
                if ((map[b][c + 1] < map[b][c]) && (map[b][c - 1] >= 0)) {
                    currentway = findWayIterate(b, c + 1, map, currentway);
                    return currentway;
                }
            }
            return currentway;
        }
        #endregion FindWay
    }
}