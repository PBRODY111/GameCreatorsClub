using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    //Level and progress
    public int level;
    public int ending;
    public bool styx;
    public bool doublePong;
    public bool dashr;

    //Options menu
    public float masterVol;
    public float musicVol;
    public float sfxVol;
    public float sensitivity;
    public float fov;

    public SaveData(PauseMenu pauseMenu){
        masterVol = pauseMenu.masterVol;
        musicVol = pauseMenu.musicVol;
        sfxVol = pauseMenu.sfxVol;
        sensitivity = pauseMenu.sensitivity;
        fov = pauseMenu.fov0;
    }
}
