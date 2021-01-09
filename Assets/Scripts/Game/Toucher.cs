using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;
using Values;

namespace Game {
    public class Toucher : MonoBehaviour {

        [SerializeField]
        private EventDispatcher _selected;
        [SerializeField]
        private EventListener _select;
        [SerializeField]
        private ScriptablePickHero _hero;

        private void OnEnable() {
            _select.OnEventHappened+=SetOnWay;
        }

        private void OnDisable() {
            _select.OnEventHappened -= SetOnWay;
        }

        private void OnMouseDown() {
            if (_hero.SelectHero != null) {
                if (_hero.SelectHero.GetToucher() != this)
                    _hero.SelectHero.SetTouch(this);
                else
                    _hero.SelectHero.MoveAllWay();
                
            }
            else {
                Debug.LogError("Hero not pick");
            }
            _selected.Dispatch();
        }

        public void SetOnWay() {
            if (_hero != null) {
                bool tmp = false;
                for (int i = 0; i < _hero.SelectHero.way.Count; i++) {
                    if ((_hero.SelectHero.way[i].x == (int)transform.position.x) && (_hero.SelectHero.way[i].y == (int)transform.position.z)) {
                        tmp = true;
                        break;
                    }
                }
                if (tmp)
                    if (transform.position.y < -0.01f)
                        transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
                    else
                    if (transform.position.y > -0.49f)
                        transform.position = new Vector3(transform.position.x, -0.5f, transform.position.z);
            }
        }
    }
}