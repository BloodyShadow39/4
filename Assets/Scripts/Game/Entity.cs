using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public string discription;

    [Range(0f, 100f)]
    public float distanceAttack = 1f;

    [Range(0f, 1000f)]
    public float heath = 100f;

    [Range(0f, 100f)]
    public float mindamage = 1f;

    [Range(0f,100f)]
    public float maxdamage = 2f;

    [Range(0,100000)]
    public int costOfGold = 0;

    [Range(0,100)]
    public int costOfOther = 0;

    [Range(0,100)]
    public int units = 1;

    [Range(0,100)]
    public int movePoints;
    
    [Range(0, 100)]
    public int localMovePoints;
    
    [Range(0, 300)]
    public int manaPoints;

    [Range(0,200)]
    public int countOfArrows;

    [Range(0, 10000)]
    public int expirianceOfOnceUnit;

    public string ability;

    public void Attack() {

    }
}
