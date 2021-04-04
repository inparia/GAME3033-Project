using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SaveGameLevel(GameManager gameManager)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/game.fun";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayData data = new PlayData(gameManager);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayData LoadData()
    {
        string path = Application.persistentDataPath + "/game.fun";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayData data = formatter.Deserialize(stream) as PlayData;
            stream.Close();
            return data;
        }

        else
        {
            Debug.LogError("Save File Not Found in " + path);
            return null;
        }
    }
}
