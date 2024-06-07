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
    /*
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
    }*/
    public static void SaveMinigame(string name){
        string path = Application.persistentDataPath + "/player.minigames";
        SaveData data = LoadMinigame() ?? new SaveData(name); // Load existing data or create a new instance with default values

        // Update the specific minigame state
        if (name == "styx")
        {
            data.styx = true;
        }
        else if (name == "doublePong")
        {
            data.doublePong = true;
        }
        else if (name == "dashr")
        {
            data.dashr = true;
        }

        // Save the updated data
        BinaryFormatter formatter = new BinaryFormatter();
        using (FileStream stream = new FileStream(path, FileMode.Create))
        {
            formatter.Serialize(stream, data);
        }
    }
    public static SaveData LoadMinigame(){
        string path = Application.persistentDataPath + "/player.minigames";
        if (File.Exists(path))
        {
            Debug.Log("Minigame savefile exists");
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream stream = new FileStream(path, FileMode.Open))
            {
                return formatter.Deserialize(stream) as SaveData;
            }
        }
        else
        {
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
    /*
    public static void SaveLevel(int level, string time)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.level";
        FileStream stream = new FileStream(path, FileMode.Create);

        SaveData data = new SaveData(level, time);
        formatter.Serialize(stream, data);
        stream.Close();

        Debug.Log("Game saved at: " + path);
    }
    */
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

    // hints
    public static void SaveHint (string monster, string hint){
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath+"/player.hints";
        FileStream stream = new FileStream(path, FileMode.Create);

        SaveData data = new SaveData(monster, hint);
        formatter.Serialize(stream, data);
        stream.Close();
    }
    public static SaveData LoadHint()
    {
        string path = Application.persistentDataPath + "/player.hints";
        if (File.Exists(path))
        {
            Debug.Log("Hint savefile exists");
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SaveData data = formatter.Deserialize(stream) as SaveData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.Log("No hint save file.");
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
