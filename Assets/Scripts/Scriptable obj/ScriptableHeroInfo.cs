using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Values{
    [CreateAssetMenu(fileName = "New Hero")]
    public class ScriptableHeroInfo : ScriptableObject
    {
        public string name="";

        public string specialization="";

        public int heroLevel(){
            return _expToNextLvl*(experiance*(experiance+1))/2;
        }

        [SerializeField]
        private int _expToNextLvl=500;

        [Range(0,1000000)]
        public int experiance=0;

        public int defense=0;

        public int attack=0;

        public int luck=0;

        public int knowledge=0;

        public int magicPower=0;

        public int magicResitance=0;

        public int defenseFromDamage=0;

        public int closeDistanceDamage=0;

        public int longDistanceDamage=0;

        public int morale=0;

        public int healt=0;

        public int mane=0;

        public int localMove=0;

        public int countOfArrows=0;

        public string info="";

        //Лист навыков

        public GameObject head=null;

        public GameObject bib=null;

        public GameObject belt=null;

        public GameObject legs=null;

        public GameObject leftHand=null;

        public GameObject rightHand=null;

        public GameObject ringLeft=null;

        public GameObject ringRight=null;

        public List<GameObject> additionalSlots=new List<GameObject>(5);

        public GameObject forLongAttack=null;

    }
}