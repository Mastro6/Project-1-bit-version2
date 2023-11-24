using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{

    private Object[,] _gridObjects;
    [SerializeField] private int _height;
    [SerializeField] private int _width;


    [SerializeField] private Object[] prova;


    // Start is called before the first frame update
    void Start()
    {
        _gridObjects = new Object[_width, _height];
        _gridObjects[2, 2] = new Bomb(3);

        prova = new Object[5];
        prova[2] = new Bomb(2);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            print("input Space detected");
            ExecuteGrid();
        }
    }

    private void ExecuteGrid()
    {
        for(int i = 0; i < _width; i++)
        {
            for (int j = 0; j < _height; j++)
            {
                Object objecttemp = _gridObjects[i, j];
                if (objecttemp == null)
                {
                    Debug.Log("non c'+e oggetto nella posizione i: " + i + " j: " + j);
                } else
                {
                    Debug.Log("l'oggetto nella posizione i: " + i + " j: " + j + " sta facendo execute");
                    _gridObjects[i, j].Execute();
                }
            }
        }
    }


}
