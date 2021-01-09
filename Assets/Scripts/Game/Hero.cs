using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Values;

namespace Game {
    public class Hero : MonoBehaviour {
        /// <summary>
        /// Точка отслеживающая цель перемещения
        /// </summary>
        private Toucher _currentTouch;
        /// <summary>
        /// Возвращает текущий Toucher
        /// </summary>
        /// <returns>Возвращает текущий Toucher</returns>
        public Toucher GetToucher() { return _currentTouch; }
        /// <summary>
        /// Карта перемещения
        /// </summary>
        [SerializeField]
        private ScriptableMap _map;
        /// <summary>
        /// Текущий путь до точки
        /// </summary>
        public List<Vector2Int> way;
        /// <summary>
        /// Время необходимое для прохождения 1ой клетки
        /// </summary>
        public float time =5;

        private bool _moveLock = false;

        [SerializeField]
        private ScriptablePickHero _selectHero;

        private void OnMouseDown() {
            _selectHero.SelectHero = this;
        }

        /// <summary>
        /// Устатнавливает точку указатель перемещения
        /// </summary>
        /// <param name="toucher">Объект регестрирующий нажатие</param>
        public void SetTouch(Toucher toucher) {
            _currentTouch = toucher;
            SetWay();
        }

        #region Move
        /// <summary>
        /// Перемещает обьект по текущему пути
        /// </summary>
        public void MoveAllWay() {
            _moveLock = true;
                StartCoroutine(MoveCoroutineAllWay());
        }
        /// <summary>
        /// Корутина для запуска всего перемещения
        /// </summary>
        /// <returns>Корутина</returns>
        private IEnumerator MoveCoroutineAllWay() {
            for (int i = 0; i < way.Count; i++) {
                Debug.Log(way[i]);
                StartCoroutine(MoveCoroutine(time, new Vector3(way[i].x,transform.position.y,way[i].y)));
                yield return new WaitForSeconds(time);
                _moveLock = false;
            }
        }
        /// <summary>
        /// Покадрового передвигает обьект за время t в новую позицию (Vector3) nextPosition 
        /// </summary>
        /// <param name="time">Время выполнения, не может быть отрицательным</param>
        /// <param name="nextPosition">Следующая позиция</param>
        /// <returns>Корутина</returns>
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
        /// Просчитывает текущий путь на текущей карте
        /// </summary>
        /// <returns>Лист точек перемещаения</returns>
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

        //Ищет пусть по матрице от точки b,c
        #region FindWay
        /// <summary>
        /// Ищет путь по матрице от точки x, y по карте map
        /// </summary>
        /// <param name="x">x координата точки</param>
        /// <param name="y">y координата точки</param>
        /// <param name="map">Карта перемещения содержащая иформацию о дистации от 0вевой точки до любой другой (отрицательное значение означает заблокированную клетку)</param>
        /// <returns>Список точек по которым надо пройти чтобы добраться до точки назначения по текущей карте</returns>
        private List<Vector2Int> findWay(int x, int y, List<List<int>> map) {
            List<Vector2Int> way = new List<Vector2Int>();
            if ((x >= 0) && (x < map.Count) && (y >= 0) && (y < map[x].Count)) {
                way = findWayIterate(x, y, map, way);
            }
            return way;
        }
        /// <summary>
        /// Итерация для поиска точкек перемещения
        /// </summary>
        /// <param name="x">x координата точки</param>
        /// <param name="y">y координата точки</param>
        /// <param name="map">Карта перемещения</param>
        /// <param name="way">пусть который уже составлен</param>
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