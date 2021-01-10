using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;
using Values;

namespace Game{
public class UsefullObject : MonoBehaviour
{
    [SerializeField]
    private ScriptablePickHero _selectedHero;

    private float _distanceForUseful=2f;

    [SerializeField]
    private string _type="";
    [SerializeField]
    private int value;

    private void OnMouseDown(){
        if (_selectedHero.SelectHero != null) {
        int x=-1;
        int y=-1;
        float distance = 
        ((transform.position.x -1)-_selectedHero.SelectHero.transform.position.x)*
        ((transform.position.x -1)-_selectedHero.SelectHero.transform.position.x)+
        ((transform.position.z -1)-_selectedHero.SelectHero.transform.position.z)*
        ((transform.position.z -1)-_selectedHero.SelectHero.transform.position.z);
        if(
            ((transform.position.x -1)-_selectedHero.SelectHero.transform.position.x)*
        ((transform.position.x -1)-_selectedHero.SelectHero.transform.position.x)+
        ((transform.position.z)-_selectedHero.SelectHero.transform.position.z)*
        ((transform.position.z)-_selectedHero.SelectHero.transform.position.z)<distance
        )
        {
            distance=
            ((transform.position.x -1)-_selectedHero.SelectHero.transform.position.x)*
            ((transform.position.x -1)-_selectedHero.SelectHero.transform.position.x)+
            ((transform.position.z)-_selectedHero.SelectHero.transform.position.z)*
            ((transform.position.z)-_selectedHero.SelectHero.transform.position.z);
            x=-1;
            y=0;
        }
        if(
            ((transform.position.x)-_selectedHero.SelectHero.transform.position.x)*
        ((transform.position.x)-_selectedHero.SelectHero.transform.position.x)+
        ((transform.position.z-1)-_selectedHero.SelectHero.transform.position.z)*
        ((transform.position.z-1)-_selectedHero.SelectHero.transform.position.z)<distance
        )
        {
            distance=
            ((transform.position.x)-_selectedHero.SelectHero.transform.position.x)*
            ((transform.position.x)-_selectedHero.SelectHero.transform.position.x)+
            ((transform.position.z-1)-_selectedHero.SelectHero.transform.position.z)*
            ((transform.position.z-1)-_selectedHero.SelectHero.transform.position.z);
            x=0;
            y=-1;
        }
        if(
            ((transform.position.x)-_selectedHero.SelectHero.transform.position.x)*
        ((transform.position.x)-_selectedHero.SelectHero.transform.position.x)+
        ((transform.position.z+1)-_selectedHero.SelectHero.transform.position.z)*
        ((transform.position.z+1)-_selectedHero.SelectHero.transform.position.z)<distance
        )
        {
            distance=
            ((transform.position.x)-_selectedHero.SelectHero.transform.position.x)*
            ((transform.position.x)-_selectedHero.SelectHero.transform.position.x)+
            ((transform.position.z+1)-_selectedHero.SelectHero.transform.position.z)*
            ((transform.position.z+1)-_selectedHero.SelectHero.transform.position.z);
            x=0;
            y=1;
        }
        if(
        ((transform.position.x+1)-_selectedHero.SelectHero.transform.position.x)*
        ((transform.position.x+1)-_selectedHero.SelectHero.transform.position.x)+
        ((transform.position.z)-_selectedHero.SelectHero.transform.position.z)*
        ((transform.position.z)-_selectedHero.SelectHero.transform.position.z)<distance)
        {
            distance=
            ((transform.position.x+1)-_selectedHero.SelectHero.transform.position.x)*
            ((transform.position.x+1)-_selectedHero.SelectHero.transform.position.x)+
            ((transform.position.z)-_selectedHero.SelectHero.transform.position.z)*
            ((transform.position.z)-_selectedHero.SelectHero.transform.position.z);
            x=1;
            y=0;
        }
        if(((transform.position.x+1)-_selectedHero.SelectHero.transform.position.x)*
        ((transform.position.x+1)-_selectedHero.SelectHero.transform.position.x)+
        ((transform.position.z+1)-_selectedHero.SelectHero.transform.position.z)*
        ((transform.position.z+1)-_selectedHero.SelectHero.transform.position.z)<distance)
        {
            distance=
            ((transform.position.x+1)-_selectedHero.SelectHero.transform.position.x)*
            ((transform.position.x+1)-_selectedHero.SelectHero.transform.position.x)+
            ((transform.position.z+1)-_selectedHero.SelectHero.transform.position.z)*
            ((transform.position.z+1)-_selectedHero.SelectHero.transform.position.z);
            x=1;
            y=1;
        }
        if(((transform.position.x-1)-_selectedHero.SelectHero.transform.position.x)*
        ((transform.position.x-1)-_selectedHero.SelectHero.transform.position.x)+
        ((transform.position.z+1)-_selectedHero.SelectHero.transform.position.z)*
        ((transform.position.z+1)-_selectedHero.SelectHero.transform.position.z)<distance)
        {
            distance=
            ((transform.position.x-1)-_selectedHero.SelectHero.transform.position.x)*
            ((transform.position.x-1)-_selectedHero.SelectHero.transform.position.x)+
            ((transform.position.z+1)-_selectedHero.SelectHero.transform.position.z)*
            ((transform.position.z+1)-_selectedHero.SelectHero.transform.position.z);
            x=-1;
            y=1;
        }
        if(((transform.position.x+1)-_selectedHero.SelectHero.transform.position.x)*
        ((transform.position.x+1)-_selectedHero.SelectHero.transform.position.x)+
        ((transform.position.z-1)-_selectedHero.SelectHero.transform.position.z)*
        ((transform.position.z-1)-_selectedHero.SelectHero.transform.position.z)<distance)
            {
                distance=
            ((transform.position.x+1)-_selectedHero.SelectHero.transform.position.x)*
            ((transform.position.x+1)-_selectedHero.SelectHero.transform.position.x)+
            ((transform.position.z-1)-_selectedHero.SelectHero.transform.position.z)*
            ((transform.position.z-1)-_selectedHero.SelectHero.transform.position.z);
            x=1;
            y=-1;
            }
            distance=((transform.position.x)-_selectedHero.SelectHero.transform.position.x)*
        ((transform.position.x)-_selectedHero.SelectHero.transform.position.x)+
        ((transform.position.z)-_selectedHero.SelectHero.transform.position.z)*
        ((transform.position.z)-_selectedHero.SelectHero.transform.position.z);
                if(distance<_distanceForUseful){
                    Use();
                }
                else
                    if (_selectedHero.SelectHero.GetTarget() != new Vector2Int((int)transform.position.x+x,(int)transform.position.z+y))
                        _selectedHero.SelectHero.SetTouch((int)transform.position.x+x,(int)transform.position.z+y);
                    else
                        _selectedHero.SelectHero.MoveAllWay();
            }
            else {
                Debug.LogError("Hero not pick");
            }
    }

    private void Use(){
        if(_type=="Gold"||_type=="gold"){
            _selectedHero.SelectHero.player.gold+=value;
        }
    }
}
}