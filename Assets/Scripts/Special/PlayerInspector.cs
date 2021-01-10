using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Game;

namespace Editors {
    [CustomEditor(typeof(Player))]
    public class PlayerInspector : Editor {
        private Player _thisPlayer;

        public override void OnInspectorGUI() {
            base.OnInspectorGUI();
            _thisPlayer = (Player)target;
            if (GUILayout.Button("Set Player Current")) {
                _thisPlayer.SetPlayerCurrent();
                AssetDatabase.Refresh();
            }
        }
    }
}
