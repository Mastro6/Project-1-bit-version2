using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : Object
{ 


    private GridManager _gridManager;
    private string _identifier;

    public Rock(int x, int y, int durability, GridManager gridManager)
    {
        _posX = x;
        _posY = y;
        this.durability = durability;
        _gridManager = gridManager;
        _identifier = "R"; //rock
    }

    public override void ReceiveDamage(int damage)
    {
        if (damage == 0) return;

        //danneggia le caselle adiacenti usando _gridObjects
        durability -= damage;
        if (durability <= 0)
        {
            _identifier = "D"; //debris
        }
    }

    public override string Visualize()
    {
        string str = "";
        str += _identifier;
        str += durability.ToString();
        return str;
    }
}
