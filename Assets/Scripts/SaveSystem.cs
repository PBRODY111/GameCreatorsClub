using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveSystem
{
    public static void SaveOptions (PauseMenu pauseMenu){
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath+"/player.preferences";
        FileStream stream = new FileStream(path, FileMode.Create);

        SaveData data = new SaveData(pauseMenu);
        formatter.Serialize(stream, data);
        stream.Close();
    }
    public static SaveData LoadOptions(){
        string path = Application.persistentDataPath+"/player.preferences";
        if(File.Exists(path)){
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SaveData data = formatter.Deserialize(stream) as SaveData;
            stream.Close();

            return data;
        } else{
            Debug.Log("No preference save file.");
            return null;
        }
    }
}
