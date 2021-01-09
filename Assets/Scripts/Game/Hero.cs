using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Values;

namespace Game {
    public class Hero : MonoBehaviour {
        /// <summary>
        /// “очка отслеживающа€ цель перемещени€
        /// </summary>
        private Toucher _currentTouch;
        /// <summary>
        ///  арта перемещени€
        /// </summary>
        [SerializeField]
        private ScriptableMap _map;
        /// <summary>
        /// “екущий путь до точки
        /// </summary>
        public List<Vector2Int> way;

        /// <summary>
        /// ”статнавливает точку указатель перемещени€
        /// </summary>
        /// <param name="toucher">ќбъект регестрирующий нажатие</param>
        public void SetTouch(Toucher toucher) {
            _currentTouch = toucher;
        }

        /// <summary>
        /// ѕокадрового передвигает обьект за врем€ t в новую позицию (Vector3) nextPosition 
        /// </summary>
        /// <param name="time">¬рем€ выполнени€, не может быть отрицательным</param>
        /// <param name="nextPosition">—ледующа€ позици€</param>
        /// <returns> орутина</returns>
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

        /// <summary>
        /// ѕросчитывает текущий путь на текущей карте
        /// </summary>
        /// <returns>Ћист точек перемещаени€</returns>
        public List<Vector2Int> SetWay() {
            int a = (int)transform.position.x;
            int b = (int)transform.position.z;
            int c = (int)_currentTouch.transform.position.x;
            int d = (int)_currentTouch.transform.position.z;

            List<List<int>> map = _map.matrixMove(a, b);
            way = findWay(c, d, map);
            return way;
        }

        //»щет пусть по матрице от точки b,c
        #region FindWay
        /// <summary>
        /// »щет пусть по матрице от точки b, c по карте map
        /// </summary>
        /// <param name="x">x координата точки</param>
        /// <param name="y">y координата точки</param>
        /// <param name="map"> арта перемещени€ содержаща€ иформацию о дистации от 0вевой точки до любой другой (отрицательное значение означает заблокированную клетку)</param>
        /// <returns>—писок точек по которым надо пройти чтобы добратьс€ до точки назначени€ по текущей карте</returns>
        private List<Vector2Int> findWay(int x, int y, List<List<int>> map) {
            List<Vector2Int> way = new List<Vector2Int>();
            if ((x >= 0) && (x < map.Count) && (y >= 0) && (y < map[x].Count)) {
                way = findWayIterate(x, y, map, way);
            }
            return way;
        }
        /// <summary>
        /// »тераци€ дл€ поиска точкек перемещени€
        /// </summary>
        /// <param name="x">x координата точки</param>
        /// <param name="y">y координата точки</param>
        /// <param name="map"> арта перемещени€</param>
        /// <param name="way">пусть который уже составлен</param>
        /// <returns></returns>
        private List<Vector2Int> findWayIterate(int x, int y, List<List<int>> map, List<Vector2Int> way) {
            List<Vector2Int> currentway = way;
            currentway.Add(new Vector2Int(x, y));
            if (map[x][y] == 0)
                return currentway;
            if (x - 1 >= 0) {
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

                if ((map[x - 1][y] < map[x][y]) && (map[x - 1][y] >= 0)) {
                    currentway = findWayIterate(x - 1, y, map, currentway);
                    return currentway;
                }

            }

            if (x + 1 < map.Count) {
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

                if ((map[x + 1][y] < map[x][y]) && (map[x + 1][y] >= 0)) {
                    currentway = findWayIterate(x + 1, y, map, currentway);
                    return currentway;
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