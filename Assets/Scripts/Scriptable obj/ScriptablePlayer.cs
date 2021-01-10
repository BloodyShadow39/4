using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;


namespace Values{
    [CreateAssetMenu(fileName="New Player")]
    public class ScriptablePlayer : ScriptableObject
    {
        public string playerName="";

        public List<Hero> heroes;

        public int gold=0;
    }
}