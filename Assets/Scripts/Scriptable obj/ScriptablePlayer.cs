using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Values{
    [CreateAssetMenu(fileName="New Player")]
    public class ScriptablePlayer : ScriptableObject
    {
        public string playerName="";

        public int gold=0;
    }
}