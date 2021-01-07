using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;

namespace Values {
    [CreateAssetMenu(fileName = "PickHero")]
    public class ScriptablePickHero : ScriptableObject {

        public Hero SelectHero;
    }
}
