using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveSystem
{
    // options menu
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
            Debug.Log("Preference savefile exists");
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

    // minigames
    public static void SaveMinigame (string name){
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath+"/player.minigames";
        FileStream stream = new FileStream(path, FileMode.Create);

        SaveData data = new SaveData(name);
        formatter.Serialize(stream, data);
        stream.Close();
    }
    public static SaveData LoadMinigame(){
        string path = Application.persistentDataPath+"/player.minigames";
        if(File.Exists(path)){
            Debug.Log("Minigame savefile exists");
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SaveData data = formatter.Deserialize(stream) as SaveData;
            stream.Close();

            return data;
        } else{
            Debug.Log("No minigame save file.");
            return null;
        }
    }

    // room level
    public static void SaveLevel (int level, string time){
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath+"/player.level";

        // ChatGPT code
        SaveData data;
        if (File.Exists(path))
        {
            FileStream stream = new FileStream(path, FileMode.Open);
            data = (SaveData)formatter.Deserialize(stream);
            stream.Close();
        }
        else
        {
            data = new SaveData(level, time);
        }

        // Update level property
        if (level > data.level)
        {
            data.level = level;
            Debug.Log("Level saved: " + level);
        }
        // ChatGPT code end

        FileStream filestream = new FileStream(path, FileMode.Create);
        Debug.Log(level);
        //SaveData data = new SaveData(level, time);
        formatter.Serialize(filestream, data);
        filestream.Close();
    }
    public static SaveData LoadLevel(){
        string path = Application.persistentDataPath+"/player.level";
        if(File.Exists(path)){
            Debug.Log("Level savefile exists");
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SaveData data = formatter.Deserialize(stream) as SaveData;
            stream.Close();

            return data;
        } else{
            Debug.Log("No level save file.");
            return null;
        }
    }

    // endings
    public static void SaveEndings (int ending){
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath+"/player.endings";
        SaveData data;
        if (File.Exists(path))
        {
            FileStream stream = new FileStream(path, FileMode.Open);
            data = (SaveData)formatter.Deserialize(stream);
            stream.Close();
        }
        else
        {
            data = new SaveData(ending);
        }

        // Update level property
        if (ending > data.ending)
        {
            data.ending = ending;
        }
        // ChatGPT code end

        FileStream filestream = new FileStream(path, FileMode.Create);
        formatter.Serialize(filestream, data);
        filestream.Close();
    }
    public static SaveData LoadEndings(){
        string path = Application.persistentDataPath+"/player.endings";
        if(File.Exists(path)){
            Debug.Log("Ending savefile exists");
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SaveData data = formatter.Deserialize(stream) as SaveData;
            stream.Close();

            return data;
        } else{
            Debug.Log("No ending save file.");
            return null;
        }
    }
}
