using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    static string fileName = "/highscore.cg";

    //loads highScore data from file
    public static void SaveData(List<HighScore> highScoreList)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + fileName;
        
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, highScoreList);
        stream.Close();
    }

    //stores high score data to file
    public static List<HighScore> LoadData()
    {
        string path = Application.persistentDataPath + fileName;

        if (File.Exists(path)) {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            List<HighScore> highScoreList = formatter.Deserialize(stream) as List<HighScore>;
            stream.Close();

            return highScoreList;
        }
        else {
            Debug.LogError("Save File not found in " + path);
            return null;
        }
    }

    //delete high score file
    public static void DeleteData()
    {
        string path = Application.persistentDataPath + fileName;
        File.Delete(path);
    }
}
