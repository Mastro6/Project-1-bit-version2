using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{

    [SerializeField] private int _height;
    [SerializeField] private int _width;

    public int Height()
    {
        return _height;
    }

    public int Width()
    {
        return _width;
    }

    private Object[,] _gridObjects;

    private int[,] _gridDamages;


    // Start is called before the first frame update
    void Start()
    {
        _gridObjects = new Object[_width, _height];
        _gridDamages = new int[_width, _height];

        PopulateGridObject(new Bomb(1, 0, 3, this));
        PopulateGridObject(new Bomb(2, 0, 3, this));
        PopulateGridObject(new Bomb(2, 1, 3, this));
        PopulateGridObject(new Bomb(2, 2, 3, this));
        //PopulateGridObject(new Bomb(1, 2, 3, this));
        //PopulateGridObject(new Rock(2, 3, 10, this));
        DamageCell(1, 0, 2);



    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            print("input Space detected");
            VisualizeGrid();
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            print("input key U detected");

            ObjectsGetDamages();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            print("input key D detected");
            PrintGridDamages();
        }
    }

    
    /// <summary>
    /// Visualizza la griglia printando una matrice TODO: mettere nella Scene
    /// </summary>
    private void VisualizeGrid()
    {
        Debug.Log("---------------------------");

        for (int i = 0; i < _height; i++)
        {

            string str = "";
            for (int j = 0; j < _width; j++)
            {
                Object objecttemp = _gridObjects[j, i];
                if (objecttemp == null)
                {
                    str +="[   ] ";
                }
                else
                {
                    str +="["+ _gridObjects[j, i].Visualize()+"] ";
                    
                }

            }
            Debug.Log(str);
        }


        Debug.Log("---------------------------");
    }

    /// <summary>
    /// Add the damage to a Cell, on the next uptick the object inside a cell will receave that amout of damage 
    /// </summary>
    /// <param name="x">width of the target cell</param>
    /// <param name="y">height of target cell</param>
    /// <param name="damage">amount of damage</param>
    public void DamageCell(int x, int y, int damage)
    {
        if (x < 0 || x >= _width || y < 0 || y >= _height)
        {
            Debug.LogError("index out of bound");
            return;
        }
        _gridDamages[x, y] += damage;
    }



    /// <summary>
    /// every odject of the grid gets the appropriate amout of damages that was dealt the previous tick
    /// </summary>
    public void ObjectsGetDamages()
    {
        Debug.Log("ObjectsGetDamages() called");
        int[,] _gridDamagesCopia = CopyGridDamage();

        ResetGridDamage();

        for (int i = 0; i < _width; i++)
        {
            for (int j = 0; j < _height; j++)
            {
                if (_gridObjects[i, j] != null)
                {   
                    _gridObjects[i,j].ReceiveDamage(_gridDamagesCopia[i, j]);
                }
                
            }
        }

    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns> A copy of the grid of damages</returns>
    private int[,] CopyGridDamage()
    {
        int[,] _gridDamagesCopy = new int[_width, _height];
        for(int i = 0; i < _width; i++)
        {
            for(int j = 0; j < _height; j++)
            {
                _gridDamagesCopy[i, j] = _gridDamages[i, j];
            }
        }
        return _gridDamagesCopy;
    }

    /// <summary>
    /// Set every integers of the grid to zero
    /// </summary>
    private void ResetGridDamage()
    {
        Debug.Log("griglia resettata");
        for (int i = 0; i < _width; i++)
        {
            for (int j = 0; j < _height; j++)
            {
                _gridDamages[i, j] = 0;
            }
        }
    }

    /// <summary>
    /// print the damages in the grid of the current tick
    /// </summary>
    private void PrintGridDamages()
    {
        Debug.Log("---------------------------");

        for (int i = 0; i < _height; i++)
        {

            string str = "";
            for (int j = 0; j < _width; j++)
            {
                int damage = _gridDamages[j, i];

                str += "[" + damage.ToString()+ "]";

            }
            Debug.Log(str);
        }


        Debug.Log("---------------------------");
    }


    /// <summary>
    /// put the object in the correct position given by the object themself
    /// </summary>
    /// <param name="object">the object we are gonna put in the grid of objects</param>
    /// <returns>the object (same as the parameter)</returns>
    private Object PopulateGridObject(Object @object)
    {
        _gridObjects[@object.X(), @object.Y()] = @object;
        return @object;
    }

    public void DestroyObjectAt(int x, int y)
    {
        _gridObjects[x, y] = null;
    }


}
