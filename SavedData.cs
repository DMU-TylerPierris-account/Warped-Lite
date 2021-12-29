
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

[System.Serializable]
public static class SavedData
{
    // private string lastTimeString;

    //public DateTime LastTime { get => DateTime.Parse(lastTimeString); set => lastTimeString = value.ToString(); }

    public static void SaveData(PlayerInfo info)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        //Where file will be saved, unity uses a handy function to get path to data director on operating system that wont change
        string path = Application.persistentDataPath + "/system.data";

        //create file
        FileStream stream = new FileStream(path, FileMode.Create);

        SystemData data = new SystemData(info);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static SystemData LoadData() //void not needed as we want to return this data
    {
        string path = Application.persistentDataPath + "/system.data";

        //checking if it can find save file
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SystemData data = formatter.Deserialize(stream) as SystemData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}
