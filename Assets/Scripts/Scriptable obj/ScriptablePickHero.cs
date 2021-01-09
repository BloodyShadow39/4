using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;

namespace Values {
    [CreateAssetMenu(fileName = "PickHero")]
    public class ScriptablePickHero : ScriptableObject {
        /// <summary>
        /// —сылаетс€ на текущего геро€
        /// </summary>
        public Hero SelectHero=null;
    }
}
