using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI;
using Events;

namespace Game {
    public class Player : MonoBehaviour {

        public string playerName = "";

        public List<Hero> heroes;

        public List<UsefullObject> _havingBilfings;

        public int gold = 0;

        public int wood = 0;

        public int ore = 0;

    }
}