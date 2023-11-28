using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Object
{
    protected int _posX;
    protected int _posY;


    public abstract void ReceiveDamage(int damage);
    public abstract string Visualize();

    public int X()
    {
        return _posX;
    }
    
    public int Y()
    {
        return _posY;
    }
}
