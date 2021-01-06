using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;

public class HandleField : MonoBehaviour
{
    
    public void Handle(string input) {
        if (input == "close") {
            gameObject.SetActive(false);
        }
        if(input == "formirate map") {
            if(MapCreator.Instance!=null)
                MapCreator.Instance.FormirateMap();
        }
        if (input.Contains("load")) {
            if (input.IndexOf("load") + 5 < input.Length) {
                string name = input.Substring(input.IndexOf("load") + 5);
                LoadManager.Instance.Load(name);
            }
        }
    }
}
