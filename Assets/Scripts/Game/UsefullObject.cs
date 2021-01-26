using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;
using Values;
using Events;
using UI;

namespace Game {
    public class UsefullObject : MonoBehaviour {
        [SerializeField]
        private ScriptablePickHero _selectedHero;

        private float _distanceForUseful = 2f;

        [SerializeField]
        private EventDispatcher _changed;


        private enum type { None, Gold, Entity };

        [SerializeField]
        private type _type;

        [SerializeField]
        private int value;

        private void OnMouseDown() {
            Ray scrRay = UsefulCamera.Instance.cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            //If we touched NGUI, down allow to move camera and touch to work
            if (Physics.Raycast(scrRay.origin, scrRay.direction, out hit, Mathf.Infinity, 1 << LayerMask.NameToLayer("UI"))) {
                Debug.Log("UI");
            }

            else

            if (_selectedHero.SelectHero != null) {
                int x = -1;
                int y = -1;
                float distance = float.MaxValue;
                if(((int)transform.position.x - 1 < MapCreator.Instance.mapSaved.GetLength(0))&& ((int)transform.position.x - 1 >=0) && ((int)transform.position.z - 1 < MapCreator.Instance.mapSaved.GetLength(1)) && ((int)transform.position.z - 1 >= 0))
                    if (MapCreator.Instance.mapSaved[(int)transform.position.x - 1,(int)transform.position.z - 1] == MapCreator.state.empty)
                        distance =
                        ((transform.position.x - 1) - _selectedHero.SelectHero.transform.position.x) *
                        ((transform.position.x - 1) - _selectedHero.SelectHero.transform.position.x) +
                        ((transform.position.z - 1) - _selectedHero.SelectHero.transform.position.z) *
                        ((transform.position.z - 1) - _selectedHero.SelectHero.transform.position.z);

                if (((int)transform.position.x - 1 < MapCreator.Instance.mapSaved.GetLength(0)) && ((int)transform.position.x - 1 >= 0) && ((int)transform.position.z< MapCreator.Instance.mapSaved.GetLength(1)) && ((int)transform.position.z >= 0))
                    if (
                        ((transform.position.x - 1) - _selectedHero.SelectHero.transform.position.x) *
                        ((transform.position.x - 1) - _selectedHero.SelectHero.transform.position.x) +
                        ((transform.position.z) - _selectedHero.SelectHero.transform.position.z) *
                        ((transform.position.z) - _selectedHero.SelectHero.transform.position.z) < distance
                        &&
                        MapCreator.Instance.mapSaved[(int)transform.position.x - 1,(int)transform.position.z] == MapCreator.state.empty
                        ) {
                            distance =
                            ((transform.position.x - 1) - _selectedHero.SelectHero.transform.position.x) *
                            ((transform.position.x - 1) - _selectedHero.SelectHero.transform.position.x) +
                            ((transform.position.z) - _selectedHero.SelectHero.transform.position.z) *
                            ((transform.position.z) - _selectedHero.SelectHero.transform.position.z);
                            x = -1;
                            y = 0;
                        }
                if (((int)transform.position.x < MapCreator.Instance.mapSaved.GetLength(0)) && ((int)transform.position.x >= 0) && ((int)transform.position.z - 1 < MapCreator.Instance.mapSaved.GetLength(1)) && ((int)transform.position.z - 1 >= 0))
                    if (
                ((transform.position.x) - _selectedHero.SelectHero.transform.position.x) *
                ((transform.position.x) - _selectedHero.SelectHero.transform.position.x) +
                ((transform.position.z - 1) - _selectedHero.SelectHero.transform.position.z) *
                ((transform.position.z - 1) - _selectedHero.SelectHero.transform.position.z) < distance
                &&
                MapCreator.Instance.mapSaved[(int)transform.position.x,(int)transform.position.z - 1] == MapCreator.state.empty
                ) {
                    distance =
                    ((transform.position.x) - _selectedHero.SelectHero.transform.position.x) *
                    ((transform.position.x) - _selectedHero.SelectHero.transform.position.x) +
                    ((transform.position.z - 1) - _selectedHero.SelectHero.transform.position.z) *
                    ((transform.position.z - 1) - _selectedHero.SelectHero.transform.position.z);
                    x = 0;
                    y = -1;
                }
                if (((int)transform.position.x < MapCreator.Instance.mapSaved.GetLength(0)) && ((int)transform.position.x >= 0) && ((int)transform.position.z + 1 < MapCreator.Instance.mapSaved.GetLength(1)) && ((int)transform.position.z + 1 >= 0))
                    if (
                    ((transform.position.x) - _selectedHero.SelectHero.transform.position.x) *
                ((transform.position.x) - _selectedHero.SelectHero.transform.position.x) +
                ((transform.position.z + 1) - _selectedHero.SelectHero.transform.position.z) *
                ((transform.position.z + 1) - _selectedHero.SelectHero.transform.position.z) < distance
                &&
                MapCreator.Instance. mapSaved[(int)transform.position.x,(int)transform.position.z + 1] == MapCreator.state.empty
                ) {
                    distance =
                    ((transform.position.x) - _selectedHero.SelectHero.transform.position.x) *
                    ((transform.position.x) - _selectedHero.SelectHero.transform.position.x) +
                    ((transform.position.z + 1) - _selectedHero.SelectHero.transform.position.z) *
                    ((transform.position.z + 1) - _selectedHero.SelectHero.transform.position.z);
                    x = 0;
                    y = 1;
                }
                if (((int)transform.position.x + 1 < MapCreator.Instance.mapSaved.GetLength(0)) && ((int)transform.position.x + 1 >= 0) && ((int)transform.position.z < MapCreator.Instance.mapSaved.GetLength(1)) && ((int)transform.position.z >= 0))
                    if (
                ((transform.position.x + 1) - _selectedHero.SelectHero.transform.position.x) *
                ((transform.position.x + 1) - _selectedHero.SelectHero.transform.position.x) +
                ((transform.position.z) - _selectedHero.SelectHero.transform.position.z) *
                ((transform.position.z) - _selectedHero.SelectHero.transform.position.z) < distance
                &&
                MapCreator.Instance.mapSaved[(int)transform.position.x + 1,(int)transform.position.z] == MapCreator.state.empty
                    ) {
                        distance =
                        ((transform.position.x + 1) - _selectedHero.SelectHero.transform.position.x) *
                        ((transform.position.x + 1) - _selectedHero.SelectHero.transform.position.x) +
                        ((transform.position.z) - _selectedHero.SelectHero.transform.position.z) *
                        ((transform.position.z) - _selectedHero.SelectHero.transform.position.z);
                        x = 1;
                        y = 0;
                    }

                if (((int)transform.position.x + 1 < MapCreator.Instance.mapSaved.GetLength(0)) && ((int)transform.position.x + 1 >= 0) && ((int)transform.position.z + 1 < MapCreator.Instance.mapSaved.GetLength(1)) && ((int)transform.position.z + 1 >= 0))
                    if (((transform.position.x + 1) - _selectedHero.SelectHero.transform.position.x) *
                ((transform.position.x + 1) - _selectedHero.SelectHero.transform.position.x) +
                ((transform.position.z + 1) - _selectedHero.SelectHero.transform.position.z) *
                ((transform.position.z + 1) - _selectedHero.SelectHero.transform.position.z) < distance
                &&
                MapCreator.Instance.mapSaved[(int)transform.position.x + 1,(int)transform.position.z + 1] == MapCreator.state.empty
                ) {
                    distance =
                    ((transform.position.x + 1) - _selectedHero.SelectHero.transform.position.x) *
                    ((transform.position.x + 1) - _selectedHero.SelectHero.transform.position.x) +
                    ((transform.position.z + 1) - _selectedHero.SelectHero.transform.position.z) *
                    ((transform.position.z + 1) - _selectedHero.SelectHero.transform.position.z);
                    x = 1;
                    y = 1;
                }
                if (((int)transform.position.x - 1 < MapCreator.Instance.mapSaved.GetLength(0)) && ((int)transform.position.x - 1 >= 0) && ((int)transform.position.z + 1 < MapCreator.Instance.mapSaved.GetLength(1)) && ((int)transform.position.z + 1 >= 0))
                    if (((transform.position.x - 1) - _selectedHero.SelectHero.transform.position.x) *
                ((transform.position.x - 1) - _selectedHero.SelectHero.transform.position.x) +
                ((transform.position.z + 1) - _selectedHero.SelectHero.transform.position.z) *
                ((transform.position.z + 1) - _selectedHero.SelectHero.transform.position.z) < distance
                &&
                MapCreator.Instance.mapSaved[(int)transform.position.x - 1,(int)transform.position.z + 1] == MapCreator.state.empty
                ) {
                    distance =
                    ((transform.position.x - 1) - _selectedHero.SelectHero.transform.position.x) *
                    ((transform.position.x - 1) - _selectedHero.SelectHero.transform.position.x) +
                    ((transform.position.z + 1) - _selectedHero.SelectHero.transform.position.z) *
                    ((transform.position.z + 1) - _selectedHero.SelectHero.transform.position.z);
                    x = -1;
                    y = 1;
                }
                if (((int)transform.position.x + 1 < MapCreator.Instance.mapSaved.GetLength(0)) && ((int)transform.position.x + 1 >= 0) && ((int)transform.position.z - 1 < MapCreator.Instance.mapSaved.GetLength(1)) && ((int)transform.position.z - 1 >= 0))
                    if (((transform.position.x + 1) - _selectedHero.SelectHero.transform.position.x) *
                ((transform.position.x + 1) - _selectedHero.SelectHero.transform.position.x) +
                ((transform.position.z - 1) - _selectedHero.SelectHero.transform.position.z) *
                ((transform.position.z - 1) - _selectedHero.SelectHero.transform.position.z) < distance
                &&
                MapCreator.Instance.mapSaved[(int)transform.position.x + 1,(int)transform.position.z - 1] == MapCreator.state.empty
                ) {
                    
                    x = 1;
                    y = -1;
                }
                Debug.Log($"{x}-{y}");
                if (distance == int.MaxValue) {
                    Debug.LogWarning("Path not find");
                    return;
                }
                distance = ((transform.position.x) - _selectedHero.SelectHero.transform.position.x) *
                                ((transform.position.x) - _selectedHero.SelectHero.transform.position.x) +
                                ((transform.position.z) - _selectedHero.SelectHero.transform.position.z) *
                                ((transform.position.z) - _selectedHero.SelectHero.transform.position.z);

                if (distance <= _distanceForUseful) {
                    Use();
                }
                else
                    if (_selectedHero.SelectHero.GetTarget() != new Vector2Int((int)transform.position.x + x, (int)transform.position.z + y))
                    _selectedHero.SelectHero.SetTouch((int)transform.position.x + x, (int)transform.position.z + y);
                else
                    _selectedHero.SelectHero.MoveAllWay();
            }
            else {
                Debug.LogError("Hero not pick");
            }
        }

        private void Use() {
            if (_type == type.Gold) {
                _selectedHero.SelectHero.player.gold += value;
                MapCreator.Instance.mapSaved[(int)transform.position.x,(int)transform.position.z] = MapCreator.state.empty;
                if(_changed.Event!=null)
                    _changed.Dispatch();
                Destroy(gameObject);
            }
        }
    }
}