using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;
using Values;

public class InputController : MonoBehaviour
{
    public static InputController Instance;

    private Vector2Int _currentTouch;

    public List<Vector2Int> way;

    public List<List<int>> map=new List<List<int>>() {
        new List<int>() { 999, 999, 999},
        new List<int>() { 999, 999, 999},
        new List<int>() { 999, -1, 999},
        new List<int>() { 999, -1, 999},
    };

    [SerializeField]
    private ScriptableMap _map;

    [SerializeField]
    private EventListener _selected;

    [SerializeField]
    private GameObject _hero;

    private void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        /*if (_map.map.Count < 1)
            for (int i = 0; i < 5; i++)
                _map.map.Add(new List<int>() { 999, 999, 999, 999 });
        map =_map.map;*/
    }

    private void OnEnable() {
        _selected.OnEventHappened += SetWay;
    }

    private void OnDisable() {
        _selected.OnEventHappened -= SetWay;
    }

    public void SetTouch(int x, int y) {
        _currentTouch = new Vector2Int(x, y);
    }

    public void SetWay() {
        int a = 3;//(int)_hero.transform.position.x;
        int b = 2;//(int)_hero.transform.position.z;
        int c = 3;//_currentTouch.x;
        int d = 0;// _currentTouch.y;

        map = matrixMove(a, b, map);
        List<Vector2Int> way = findWay(c, d, map);
        for (int i = 0; i < map.Count; i++)
            Debug.Log($"{map[i][0]},{map[i][1]},{map[i][2]}");
        for(int i = 0; i < way.Count; i++) {
           Debug.Log($"({way[i].x};{way[i].y})->");
        }
    }

    //TODO: Исправление ошибки с тек. параметрами
    //Ищет пусть по матрице от точки b,c
    #region FindWay
    private List<Vector2Int> findWay(int b, int c, List<List<int>> map) {
        List<Vector2Int>  way = new List<Vector2Int>();
        if ((b >= 0) && (b < map.Count)&&(c>=0)&&(c<map[b].Count)) {
            way = findWayIterate(b, c, map, way);
        }
        return way;
    }
    private List<Vector2Int> findWayIterate(int b,int c, List<List<int>> map, List<Vector2Int> way) {
        List<Vector2Int> currentway=way;
        currentway.Add(new Vector2Int(b, c));
        Debug.Log($"{b},{c}");
        if (b - 1 >= 0) {
            if(c - 1 >= 0){
                if ((map[b - 1][c - 1] < map[b][c])&&(map[b - 1][c - 1]>=0)) {
                    currentway= findWayIterate(b - 1, c - 1, map, currentway);
                    if (map[currentway[currentway.Count-1].x][currentway[currentway.Count-1].y] == 0)
                        return currentway;
                }
            }

            if (c + 1 < map[b-1].Count) {
                if ((map[b - 1][c + 1] < map[b][c])&& (map[b - 1][c + 1] >= 0)) {
                    currentway = findWayIterate(b - 1, c + 1, map, currentway);
                    if (map[currentway[currentway.Count-1].x][currentway[currentway.Count-1].y] == 0)
                        return currentway;
                }
            }

            if ((map[b - 1][c] < map[b][c])&& (map[b - 1][c] >= 0)) {
                currentway = findWayIterate(b - 1, c, map, currentway);
                if (map[currentway[currentway.Count-1].x][currentway[currentway.Count-1].y] == 0)
                    return currentway;
            }

        }

        if (b + 1 < map.Count) {
            Debug.Log($"b:{b+1}");
            if (c - 1 >= 0) {
                if ((map[b + 1][c - 1] < map[b][c])&& (map[b + 1][c - 1] >= 0)) {
                    currentway = findWayIterate(b + 1, c - 1, map, currentway);
                    if (map[currentway[currentway.Count-1].x][currentway[currentway.Count-1].y] == 0)
                        return currentway;
                }
            }

            if (c + 1 < map[b].Count) {
                if ((map[b + 1][c + 1] < map[b][c])&& (map[b + 1][c + 1] >= 0)) {
                    currentway = findWayIterate(b + 1, c + 1, map, currentway);
                    if (map[currentway[currentway.Count-1].x][currentway[currentway.Count-1].y] == 0)
                        return currentway;
                }
            }

            if ((map[b + 1][c] < map[b][c])&& (map[b + 1][c] >= 0)) {
                currentway = findWayIterate(b + 1, c, map, currentway);
                if (map[currentway[currentway.Count].x][currentway[currentway.Count].y] == 0)
                    return currentway;
            }

        }

        if (c - 1 >= 0) {
            if ((map[b][c - 1] < map[b][c]) && (map[b][c - 1] >= 0)) {
                currentway = findWayIterate(b, c - 1, map, currentway);
                if (map[currentway[currentway.Count-1].x][currentway[currentway.Count-1].y] == 0)
                    return currentway;
            }
        }

        if (c + 1 < map[b].Count) {
            if ((map[b][c + 1] < map[b][c])&& (map[b][c - 1] >= 0)) {
                currentway = findWayIterate(b, c + 1, map, currentway);
                if (map[currentway[currentway.Count-1].x][currentway[currentway.Count-1].y] == 0)
                    return currentway;
            }
        }

        return currentway;
    }
    #endregion FindWay
    //Функция просчитывает матрицу перемещения из позиции a,b из начальной матрицы map
    #region MatrixMove

    private List<List<int>> matrixMove(int a, int b, List<List<int>> map) {
        map[a][b] = 0;
        map = matrixMoveIterate(a, b, map);
        return map;
    }
    private List<List<int>> matrixMoveIterate(int a, int b, List<List<int>>map) {
        if (a - 1 >= 0) {
            if (b - 1 >= 0) {
                if (map[a - 1][b - 1] > map[a][b]) {
                    map[a - 1][b - 1] = map[a][b] + 1;
                    matrixMoveIterate(a - 1, b - 1, map);
                }
            }
            if (b + 1 < map[a - 1].Count) {
                if (map[a - 1][b + 1] > map[a][b]) {
                    map[a - 1][b + 1] = map[a][b] + 1;
                    matrixMoveIterate(a - 1, b + 1, map);
                }
            }
            if (map[a - 1][b] > map[a][b]) {
                map[a - 1][b] = map[a][b] + 1;
                matrixMoveIterate(a - 1, b, map);
            }
        }

        if (a + 1 < map.Count) {
            if (b + 1 < map[a + 1].Count) {
                if (map[a + 1][b + 1] > map[a][b]) {
                    map[a + 1][b + 1] = map[a][b] + 1;
                    matrixMoveIterate(a + 1, b + 1, map);
                }
            }
            if (b - 1 >= 0) {
                if (map[a + 1][b - 1] > map[a][b]) {
                    map[a + 1][b - 1] = map[a][b] + 1;
                    matrixMoveIterate(a + 1, b - 1, map);
                }
            }
            if (map[a + 1][b] > map[a][b]) {
                map[a + 1][b] = map[a][b] + 1;
                matrixMoveIterate(a + 1, b, map);
            }
        }

        if (b - 1 >= 0) {
            if (map[a][b - 1] > map[a][b]) {
                map[a][b - 1] = map[a][b] + 1;
                matrixMoveIterate(a, b - 1, map);
            }
        }

        if (b + 1 < map[a].Count) {
            if (map[a][b + 1] > map[a][b]) {
                map[a][b + 1] = map[a][b] + 1;
                matrixMoveIterate(a, b + 1,map);
            }
        }
        return map;
    }
    #endregion MatrixMove
}
