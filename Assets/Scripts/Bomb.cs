using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Object
{

    private GridManager _gridManager;
    private string _identifier;
    public Bomb(int x, int y, int durability, GridManager gridManager)
    {

        _posX = x;
        _posY = y;
        this.durability = durability;
        _gridManager = gridManager;
        _identifier = "B"; //bomb
    }

    public override void ReceiveDamage(int damage)
    {
        if(_identifier == "E")
        {
            _gridManager.DestroyObjectAt(_posX, _posY);
            return; //QUESTO RETURN +E FONDAMENTALE, QUANDO +E IN QUESTO STATO VOGLIO ELIMINARLO NON FACENDOLO FARE NULLA
        }
        if (damage == 0) return;

        //danneggia le caselle adiacenti usando _gridObjects
        durability -= damage;
        
        _identifier = "E"; //explosion
        

        for(int i = -1; i<=1; i += 1)
        {
            for (int j = -1; j <=1; j++)
            {
                int x = _posX + i;
                int y = _posY + j;

                if (x < 0 || x >= _gridManager.Width() || y < 0 || y >= _gridManager.Height())
                {
                    continue;
                }

                if (i == 0 && j == 0) continue; //skip the bomb cell

                _gridManager.DamageCell(x, y, 1);
            }

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
