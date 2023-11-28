using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : Object
{ 
    private int _durability;
    private GridManager _gridManager;
    private string _identifier;

    public Rock(int x, int y, int durability, GridManager gridManager)
    {
        _posX = x;
        _posY = y;
        _durability = durability;
        _gridManager = gridManager;
        _identifier = "R"; //rock
    }

    public override void ReceiveDamage(int damage)
    {
        if (damage == 0) return;

        //danneggia le caselle adiacenti usando _gridObjects
        _durability -= damage;
        if (_durability <= 0)
        {
            _identifier = "D"; //debris
        }
    }

    public override string Visualize()
    {
        string str = "";
        str += _identifier;
        str += _durability.ToString();
        return str;
    }
}
