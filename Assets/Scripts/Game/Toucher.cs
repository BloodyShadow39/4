using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;
using Values;

namespace Game {
    public class Toucher : MonoBehaviour {
 
        [SerializeField]
        private EventDispatcher _pickToucher;

        [SerializeField]
        private EventListener _way;

        [SerializeField]
        private ScriptablePickHero _hero;

        [SerializeField]
        private GameObject _point;

        public string owner=null;

        private void OnEnable() {
            _way.OnEventHappened += SetOnWay;
        }

        private void OnDisable() {
            _way.OnEventHappened -= SetOnWay;
        }

        private void OnMouseDown() {

            if (_hero.SelectHero != null) {
                if (_hero.SelectHero.GetTarget() != new Vector2Int((int)transform.position.x,(int)transform.position.z))
                    _hero.SelectHero.SetTouch(this);
                else
                    _hero.SelectHero.MoveAllWay();
            }
            else {
                Debug.LogError("Hero not pick");
            }
            _pickToucher.Dispatch();
        }

        private void Update() {
            SetOnWay();
        }

        public void SetOnWay() {
            if (_hero.SelectHero != null) {
                bool tmp = false;
                for (int i = 0; i < _hero.SelectHero.way.Count; i++) {
                    if ((_hero.SelectHero.way[i].x == (int)transform.position.x) && (_hero.SelectHero.way[i].y == (int)transform.position.z)) {
                        tmp = true;
                        break;
                    }
                }
                if (tmp)
                    _point.SetActive(true);
                else
                    _point.SetActive(false);
            }
        }
    }
}