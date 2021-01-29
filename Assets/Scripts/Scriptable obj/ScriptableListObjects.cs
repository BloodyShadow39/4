using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;

namespace Values {
    [CreateAssetMenu(fileName ="List Objects")]
    public class ScriptableListObjects : ScriptableObject {
        public List<UsefullObject> listprefabs=new List<UsefullObject>();

        public UsefullObject FindByName(string name) {
            foreach(UsefullObject obj in listprefabs) {
                if (obj.gameObject.name == name)
                    return obj;
            }
            return null;
        }
    }
}