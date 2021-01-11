using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;
using Values;

namespace Game {
    public class Toucher : MonoBehaviour {
        /// <summary>
        /// ��������� �� ���������� �������
        /// </summary>
        [SerializeField]
        private EventDispatcher _pickToucher;

        /// <summary>
        /// ��������� �� �������� �������
        /// </summary>
        [SerializeField]
        private EventListener _way;

        /// <summary>
        /// ��������� �� ��������� ����� (�����)
        /// </summary>
        [SerializeField]
        private ScriptablePickHero _hero;

        [SerializeField]
        private GameObject _point;

        /// <summary>
        /// ������������� �� �������
        /// </summary>
        private void OnEnable() {
            _way.OnEventHappened += SetOnWay;
        }
        /// <summary>
        /// ������������ �� �������
        /// </summary>
        private void OnDisable() {
            _way.OnEventHappened -= SetOnWay;
        }
        /// <summary>
        /// ��������� ������� �� ������, ������� ������� �� ������ �������� � ����������� �������� �����
        /// </summary>
        private void OnMouseDown() {

            Ray scrRay = UsefulCamera.Instance.cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            //If we touched NGUI, down allow to move camera and touch to work
            if (Physics.Raycast(scrRay.origin, scrRay.direction, out hit, Mathf.Infinity, 1 << LayerMask.NameToLayer("UI"))) {
                Debug.Log("UI");
            }
            else

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
        /// <summary>
        /// ������������� ������� ����� � ��������� - �� ���� ���������� � �������� ������� ��� �� ����� ���������
        /// </summary>
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