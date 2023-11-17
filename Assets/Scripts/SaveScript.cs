using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveScript : MonoBehaviour
{

    //Ok vamos ver se consigo explicar isto xD

    static public SaveScript instance;
    int data;
    
    void Start()
    {
        if (instance == null)// Let only one time of this gameobject exist
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    
    public void SaveBinaryGame(int HighScore) // Get one HighScore Create a BinaryFormatter, creath a path /HighScore.brocode, create a fileStream in the path and Saves the score and close
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/HighScore.brocode";
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, HighScore);
        stream.Close();
    }

    public int GetBinarySave()//if the file exists save the data in form of int, close the strean and return the data
    {
        string path = Application.persistentDataPath + "/HighScore.brocode";
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            data = (int)formatter.Deserialize(stream);
            
            stream.Close();
            return data;
        }
        else
        {
            return 0;
        }
    }

}
