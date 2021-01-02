using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Values {
    [CreateAssetMenu(fileName = "ScriptableMap")]
    public class ScriptableMap : ScriptableObject {

        public List<List<int>> map=new List<List<int>>();
    }
}