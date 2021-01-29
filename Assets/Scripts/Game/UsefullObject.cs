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

        private float _distanceForUseful = 2f;//sqrded values

        [SerializeField]
        private EventDispatcher _changed;

        public enum Type { None, Gold, Wood, Fight};

        [SerializeField]
        public Type type;

        [SerializeField]
        private int count;

        private void OnMouseDown() {

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
            if (type == Type.Gold) {
                _selectedHero.SelectHero.player.gold += count;
                MapCreator.Instance.mapSaved[(int)transform.position.x,(int)transform.position.z] = MapCreator.state.empty;
                if(_changed.Event!=null)
                    _changed.Dispatch();
                Destroy(gameObject);
            }
            if (type == Type.Wood) {
                _selectedHero.SelectHero.player.wood += count;
                MapCreator.Instance.mapSaved[(int)transform.position.x, (int)transform.position.z] = MapCreator.state.empty;
                if (_changed.Event != null)
                    _changed.Dispatch();
                Destroy(gameObject);
            }
        }
    }
}