using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Values {

    [CreateAssetMenu(fileName = "New Map", menuName = "Scriptable Map")]
    public class ScriptableMap : ScriptableObject {
        [SerializeField]//For testing
        private int width;//Ўирина текущей карты
        [SerializeField]//For testing
        private int height;

        public struct cell {
            public int x;
            public int y;
            public GameObject item;
            public bool availability;
            public int type;

            public cell(int dx,int dy, GameObject ditem, bool davailability, int dtype) {
                x = dx;
                y = dy;
                item = ditem;
                availability = davailability;
                type = dtype;
            }

            public cell(cell dcell) {
                x = dcell.x;
                y = dcell.y;
                item = dcell.item;
                availability = dcell.availability;
                type = dcell.type;
            }
        }

        public List<cell> map;

    }
}