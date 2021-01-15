using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Values;
using Events;
using Managers;

namespace Game {
    public class Hero : MonoBehaviour {

        private Vector2Int _target;

        public Player player;

        public Vector2Int GetTarget() { return _target; }

        [SerializeField]
        private ScriptableMap _map;

        public List<Vector2Int> way;

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
        [Range(0,100)]
        public int startPoints = 10;

        [SerializeField]
        private RenderCamera _renderCamera;

        public RenderCamera RenderCamera() { return _renderCamera; }


        #region BasicParametrs

        public string gameName = "";

        public string specialization = "";

        public int heroLevel() {
            return _expToNextLvl * (experiance * (experiance + 1)) / 2;
        }

        [SerializeField]
        private int _expToNextLvl = 500;

        [Range(0, 1000000)]
        public int experiance = 0;

        public int defense = 0;

        public int attack = 0;

        public int luck = 0;

        public int knowledge = 0;

        public int magicPower = 0;

        public int magicResitance = 0;

        public int defenseFromDamage = 0;

        public int closeDistanceDamage = 0;

        public int longDistanceDamage = 0;

        public int morale = 0;

        public int healt = 0;

        public int mane = 0;

        public int localMove = 0;

        public int countOfArrows = 0;

        public string info = "";

        //Лист навыков

        public GameObject head = null;

        public GameObject bib = null;

        public GameObject belt = null;

        public GameObject legs = null;

        public GameObject leftHand = null;

        public GameObject rightHand = null;

        public GameObject ringLeft = null;

        public GameObject ringRight = null;

        public List<GameObject> additionalSlots = new List<GameObject>(5);

        public GameObject forLongAttack = null;

        #endregion BasicParametrs


        #region SetTouch


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

        #endregion SetTouch


        #region Move



        public void MoveAllWay() {
            if(!_moveLock)
                StartCoroutine(MoveCoroutineAllWay());
        }

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


        #region FindWay


        public List<Vector2Int> SetWay() {
            if (!_moveLock) {
                int a = (int)transform.position.x;
                int b = (int)transform.position.z;
                int c = (int)_target.x;
                int d = (int)_target.y;

                List<List<int>> map = _map.matrixMove(a, b);
                if (map[c][d] != int.MaxValue && map[c][d] >= 0) {

                    List<Vector2Int> invetway = findWay(c, d, map);
                    invetway.RemoveAt(invetway.Count - 1);

                    way.Clear();
                    for (int i = 0; i < invetway.Count; i++) {
                        way.Add(invetway[invetway.Count - 1 - i]);
                    }
                }
                else {
                    way.Clear();
                    Debug.LogError("Way cannot found, point cannot be come");
                }
            }
            _wayDefenited.Dispatch();
            return way;
        }

        private List<Vector2Int> findWay(int x, int y, List<List<int>> map) {
            List<Vector2Int> way = new List<Vector2Int>();
            ScriptableMap.state[,] mapStates = _map.mapSaved;
            if ((x >= 0) && (x < map.Count) && (y >= 0) && (y < map[x].Count)) {
                way = findWayIterate(x, y, map, way, mapStates);
            }
            return way;
        }

        private List<Vector2Int> findWayIterate(int x, int y, List<List<int>> map, List<Vector2Int> way, ScriptableMap.state[,] mapStates) {
            List<Vector2Int> currentway = way;
            currentway.Add(new Vector2Int(x, y));
            if (map[x][y] == 0)
                return currentway;

            if (y - 1 >= 0) {
                if ((map[x][y - 1] < map[x][y]) && (map[x][y - 1] >= 0) && mapStates[x,y - 1] != ScriptableMap.state.useful) {
                    currentway = findWayIterate(x, y - 1, map, currentway, mapStates);
                    return currentway;
                }
            }

            if (y + 1 < map[x].Count) {
                if ((map[x][y + 1] < map[x][y]) && (map[x][y + 1] >= 0) && mapStates[x,y + 1] != ScriptableMap.state.useful) {
                    currentway = findWayIterate(x, y + 1, map, currentway, mapStates);
                    return currentway;
                }
            }

            if (x - 1 >= 0) {
                if ((map[x - 1][y] < map[x][y]) && (map[x - 1][y] >= 0) && mapStates[x - 1,y] != ScriptableMap.state.useful) {
                    currentway = findWayIterate(x - 1, y, map, currentway, mapStates);
                    return currentway;
                }

                if (y - 1 >= 0) {
                    if ((map[x - 1][y - 1] < map[x][y]) && (map[x - 1][y - 1] >= 0) && mapStates[x - 1,y - 1] != ScriptableMap.state.useful) {
                        currentway = findWayIterate(x - 1, y - 1, map, currentway, mapStates);
                        return currentway;
                    }
                }

                if (y + 1 < map[x - 1].Count) {
                    if ((map[x - 1][y + 1] < map[x][y]) && (map[x - 1][y + 1] >= 0) && mapStates[x - 1,y + 1] != ScriptableMap.state.useful) {
                        currentway = findWayIterate(x - 1, y + 1, map, currentway, mapStates);
                        return currentway;
                    }
                }
            }

            if (x + 1 < map.Count) {
                if ((map[x + 1][y] < map[x][y]) && (map[x + 1][y] >= 0) && mapStates[x + 1,y] != ScriptableMap.state.useful) {
                    currentway = findWayIterate(x + 1, y, map, currentway, mapStates);
                    return currentway;
                }

                if (y - 1 >= 0) {
                    if ((map[x + 1][y - 1] < map[x][y]) && (map[x + 1][y - 1] >= 0) && mapStates[x + 1,y - 1] != ScriptableMap.state.useful) {
                        currentway = findWayIterate(x + 1, y - 1, map, currentway, mapStates);
                        return currentway;
                    }
                }

                if (y + 1 < map[x].Count) {
                    if ((map[x + 1][y + 1] < map[x][y]) && (map[x + 1][y + 1] >= 0) && mapStates[x + 1,y + 1] != ScriptableMap.state.useful) {
                        currentway = findWayIterate(x + 1, y + 1, map, currentway, mapStates);
                        return currentway;
                    }
                }
            }

            

            
            return currentway;
        }
        #endregion FindWay
    }
}