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
        
        [Range(0,50)]
        public int width=0;

        [Range(0, 50)]
        public int height=0;

        public bool[,] map=null;

        public void FillEmptyMap() {
            map = new bool[width, height];
        }

    }
}