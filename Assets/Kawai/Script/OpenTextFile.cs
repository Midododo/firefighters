using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.IO;


public class OpenTextFile : MonoBehaviour {


    void Start()
    {
    }

    void LoadFromFile (string path)
    {
        //string path = "Assets/Resources/test.txt";

        StreamReader reader = new StreamReader(path);

        string line;

        for (int i = 0; i < File.ReadAllLines(path).Length; i++)
        {
            line = reader.ReadLine();
            Debug.Log(line);

            char[] array = line.ToCharArray();
            for(int j = 0;j < line.Length; j++)
            {
                Debug.Log(array[j]);
            }
        }

        reader.Close();
    }

    public void ConvertCSVDataToIntArray(string path, int[][] data)
    //public void start(string path)
    {

        StreamReader reader = new StreamReader(path);

        string line;

        for (int i = 0; i < File.ReadAllLines(path).Length; i++)
        {
            line = reader.ReadLine();

            var numArray = line.Split(',').Select(s => int.Parse(s)).ToArray();
            data[i] = numArray;
        }

        reader.Close();

        //return Data[0][0];
    }

}
