using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game {
    public class Figth : MonoBehaviour {

        public List<Entity> rightSide;

        public List<Entity> leftSide;

        public Hero rightHero=null;

        public Hero leftHero=null;

        public Player rightPlayer=null;

        public Player leftPlayer=null;

        public Toucher toucher;

        public GameObject let;
        
        [Range(0,50)]
        public int width=0;

        [Range(0, 50)]
        public int height=0;

        public bool[,] map=null;

        public void FillEmptyMap() {
            map = new bool[width, height];
        }

        public void GenerateMap() {
            foreach(Transform child in transform) {
                Destroy(child.gameObject);
            }
            for(int i = 0; i < width; i++) {
                for(int j = 0; j < height; j++) {
                    if (map[i, j]) {
                        Instantiate(let, transform.position+ new Vector3(i, 0, j), Quaternion.identity, transform);
                    }
                    else
                        Instantiate(toucher, transform.position + new Vector3(i, 0, j), Quaternion.identity, transform);
                }
            }
        } 

    }
}