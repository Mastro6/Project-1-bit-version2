using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Object
{
    private int _durability;
    public Bomb(int durability)
    {
        this._durability = durability;
    }


    public override void Execute()
    {
        Debug.Log("ciao bomba executed");
        Debug.Log("durability: " + _durability);
    }
}
