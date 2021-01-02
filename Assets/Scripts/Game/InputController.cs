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
        Debug.Log(true);
        int a = (int)_hero.transform.position.x;
        int b = (int)_hero.transform.position.z;
        int c = _currentTouch.x;
        int d = _currentTouch.y;

        map[a][b] = 0;
        setindex(a, b, c, d);
        for (int i = 0; i < map.Count; i++)
            Debug.Log($"{map[i][0]},{map[i][1]},{map[i][2]}");
    }


    //TODO: a-1 должно поверятся раньше чем *1, переписать условия в соответсвии с требованием массива
    private void setindex(int a, int b,int c, int d) {
        if (a - 1 < 0)
            return;
        if (b -1 < 0)
            return;
        if (a + 1 >= map.Count)
            return;
        if(b+1 >= map[a].Count)
            return;
        if ((a == c) && (b == d))
            return;
        //*1
        if ((map[a - 1][b - 1] <= map[a][b]) 
            && (map[a - 1][b] <= map[a][b]) 
            && (map[a][b-1] <= map[a][b]) 
            && (map[a + 1][b] <= map[a][b]) 
            && (map[a][b+1] <= map[a][b]) 
            && (map[a + 1][b+1] <= map[a][b]) 
            && (map[a - 1][b+1] <= map[a][b]) 
            && (map[a + 1][b-1] <= map[a][b]))
            return;
       if (map[a - 1][b - 1] > map[a][b]) {
            map[a - 1][b - 1] = map[a][b] + 1;
            setindex(a-1, b-1, c, d);
        }
        if (map[a - 1][b] > map[a][b]) {
            map[a - 1][b] = map[a][b] + 1;
            setindex(a - 1, b, c, d);
        }
        if (map[a][b - 1] > map[a][b]) {
            map[a][b - 1] = map[a][b] + 1;
            setindex(a, b - 1, c, d);
        }
        if (map[a - 1][b + 1] > map[a][b]) {
            map[a - 1][b + 1] = map[a][b] + 1;
            setindex(a - 1, b + 1, c, d);
        }
        if (map[a + 1][b - 1] > map[a][b]) {
            map[a + 1][b - 1] = map[a][b] + 1;
            setindex(a + 1, b - 1, c, d);
        }
        if (map[a + 1][b] > map[a][b]) {
            map[a + 1][b] = map[a][b] + 1;
            setindex(a + 1, b, c, d);
        }
        if (map[a][b + 1] > map[a][b]) {
            map[a][b + 1] = map[a][b] + 1;
            setindex(a, b + 1, c, d);
        }
        if (map[a + 1][b + 1] > map[a][b]) {
            map[a + 1][b + 1] = map[a][b] + 1;
            setindex(a + 1, b + 1, c, d);
        }
    }

}
