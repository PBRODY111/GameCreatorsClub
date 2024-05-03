using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    //Level and progress
    public int level;
    public int ending;
    public bool styx = false;
    public bool doublePong = false;
    public bool dashr = false;
    
    public string time1 = "";
    public string time2 = "";
    public string time3 = "";
    public string time4 = "";
    public string time5 = "";

    //Options menu
    public float masterVol;
    public float musicVol;
    public float sfxVol;
    public float sensitivity = 100;
    public float fov = 70;

    // options menu
    public SaveData(PauseMenu pauseMenu){
        masterVol = pauseMenu.masterVol;
        musicVol = pauseMenu.musicVol;
        sfxVol = pauseMenu.sfxVol;
        sensitivity = pauseMenu.sensitivity;
        fov = pauseMenu.fov0;
    }
    // minigame
    public SaveData(string name){
        if(name == "styx"){
            styx = true;
        } else if(name == "doublePong"){
            doublePong = true;
        } else if(name == "dashr"){
            dashr = true;
        }
    }
    // level progress
    public SaveData(int lev, string time){
        // save level
        if(lev > level){
            level = lev;
            Debug.Log("level saved: "+level);
        }
        // save PR (this should be optimized somehow)
        if(lev == 2){
            if(time1 != ""){
                TimeSpan timeSpan1 = TimeSpan.ParseExact(time1, "m':'ss'.'ff", null);
                TimeSpan timeSpan2 = TimeSpan.ParseExact(time, "m':'ss'.'ff", null);
                if(timeSpan2<timeSpan1){
                    time1 = time;
                }
            }
        }
        if(lev == 3){
            if(time2 != ""){
                TimeSpan timeSpan1 = TimeSpan.ParseExact(time2, "m':'ss'.'ff", null);
                TimeSpan timeSpan2 = TimeSpan.ParseExact(time, "m':'ss'.'ff", null);
                if(timeSpan2<timeSpan1){
                    time2 = time;
                }
            }
        }
        if(lev == 4){
            if(time3 != ""){
                TimeSpan timeSpan1 = TimeSpan.ParseExact(time3, "m':'ss'.'ff", null);
                TimeSpan timeSpan2 = TimeSpan.ParseExact(time, "m':'ss'.'ff", null);
                if(timeSpan2<timeSpan1){
                    time3 = time;
                }
            }
        }
        if(lev == 5){
            if(time4 != ""){
                TimeSpan timeSpan1 = TimeSpan.ParseExact(time4, "m':'ss'.'ff", null);
                TimeSpan timeSpan2 = TimeSpan.ParseExact(time, "m':'ss'.'ff", null);
                if(timeSpan2<timeSpan1){
                    time4 = time;
                }
            }
        }
        if(lev == 6){
            if(time5 != ""){
                TimeSpan timeSpan1 = TimeSpan.ParseExact(time5, "m':'ss'.'ff", null);
                TimeSpan timeSpan2 = TimeSpan.ParseExact(time, "m':'ss'.'ff", null);
                if(timeSpan2<timeSpan1){
                    time5 = time;
                }
            }
        }
    }
    // endings
    public SaveData(int end){
        if(end > ending){
            ending = end;
        }
    }
}
