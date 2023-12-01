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

        LoadLevel();
        
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

        if (Input.GetKeyDown(KeyCode.R))
        {
            print("input key R detected");
            ResetGridDamage();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {

            print(LevelToString());

        }
    }


    /// <summary>
    /// Takes a string and it will devide in a list<string>
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    private List<string> DivideString(string str)
    {

        List<char> listChar = StringToListChar(str);

        List<string> sol = new List<string>();

        int len = str.Length;
        int index = 0;

        int deapth = 100;
        while (index < len && deapth > 0)
        {
            deapth--;
            char lettera = listChar[index];
            index++;
            int beginindex = index;
            while (index < len && listChar[index] >= '0' && listChar[index] <= '9' && deapth > 0)
            {
                deapth--;
                index++;
            }

            string num = str.Substring(beginindex, index - beginindex);

            
            sol.Add(lettera + num);
        }

        return sol;
    }

    /// <summary>
    /// Thakes in a string and it will devide in a List<char>
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    private List<char> StringToListChar(string str)
    {
        List<char> sol = new List<char>();
        for (int i = 0; i < str.Length; i++)
        {
            sol.Add(str[i]);
        }
        return sol;
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
    /// Set every integers of the damage grid to zero
    /// </summary>
    private void ResetGridDamage()
    {
        
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

    private void LoadLevel()
    {

        string str = "3/4/S1B3B3/S1R10B3/S2B3/S1B2S1";
        //string str = "11/11/S11/S11/S2B1S2B1S1B1B1S2/S7B1S3/S4B1S2B1S3/S7B1S3/S2B1B1B1B1B1B1S3/S5B1S2B1S2/S5B1S2B1S2/S5B1S2B1S2/S11";

        string[] listStr = str.Split('/');
        int lenList = listStr.Length;
        _width = int.Parse(listStr[0]);
        _height = int.Parse(listStr[1]);

        _gridObjects = new Object[_width, _height];
        _gridDamages = new int[_width, _height];

        
        for (int i = 0; i < _height; i++)
        {
            List<string> tempStr = DivideString(listStr[i+2]);

            int x = 0;
            foreach (string s in tempStr)
            {

                if (s[0] == 'S')
                {
                    x += int.Parse(s[1..]);
                } else
                {
                    PlaceObject(s, x, i);
                    x++;
                }
            }
        }

        DamageCell(1, 0, 2);
    }


    /// <summary>
    /// Create and place an object in the position
    /// </summary>
    /// <param name="str"></param>
    /// <param name="posX"></param>
    /// <param name="posY"></param>
    /// <returns></returns>
    private Object PlaceObject(string str, int posX, int posY)
    {
        Debug.Log("sto spawnando un oggetto di stringa: " + str);
        Debug.Log("in posizione x: " + posX + " y: " + posY);

        return PopulateGridObject(StringToObject(str, posX, posY));
    }


    /// <summary>
    /// Return an object generated by the string and a number in the position Es: B3, 2, 3 will generate a Bomb with 3 durability at position 2, 3
    /// </summary>
    /// <param name="str">the string of 2 or more character, the first is the identificator while the second is the number (of durability)</param>
    /// <param name="posX"></param>
    /// <param name="posY"></param>
    /// <returns></returns>
    private Object StringToObject(string str, int posX, int posY)
    {

        int num = int.Parse(str[1..]);

        if (str[0] == 'B')
        {
            return new Bomb(posX, posY, num, this);
        } else if (str[0] == 'R')
        {
            return new Rock(posX, posY, num, this);
        }

        Debug.LogError("non +e stato registrato un oggetto nella funzione StringToObject()");
        return null;
    }

    private string LevelToString()
    {

        string sol = "";

        sol += _width.ToString() + "/" + _height.ToString();

        for (int y = 0; y < _height; y++)
        {
            sol += "/";

            int spaces = 0;
            string str = "";
            for (int x = 0; x < _width; x++)
            {
                Object obj = _gridObjects[x, y];

                if (obj == null)
                {
                    spaces++;
                } else
                {
                    if (spaces > 0)
                    {
                        str += "S" + spaces.ToString();
                        spaces = 0;
                    }

                    str += ObjectToString(_gridObjects[x, y]);
                }

                if(x == _width-1 && obj == null)
                {
                    str += "S" + spaces.ToString();
                }
            }
            sol += str;
        }

        return sol;

    }

    private string ObjectToString(Object obj)
    {
        if(obj is Bomb)
        {

            int durability = obj.Durability();
            return "B" + durability.ToString();
        } else if (obj is Rock)
        {
            int durability = obj.Durability();
            return "R" + durability.ToString();
        }


        Debug.LogError("un oggetto non +e stato registrato nel metodo ObjectToString(Object obj)");
        return null;
    }



}
