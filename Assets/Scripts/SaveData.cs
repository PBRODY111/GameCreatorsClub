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

    public string hintString = " ";
    
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

    // ALL SAVE FUNCTIONS HERE ON DOWN!!!!
    
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
    // hints
    public SaveData(string monster, string hint){
        if(monster == "cer"){
            if(hint == "room1"){
                hintString = "HINT: YOU HAVE ~2 SECONDS AFTER ALL FOUR SCREWS ARE OUT. GET THE CROWBAR READY.";
            } else if(hint == "room2"){
                hintString = "HINT: IT WILL ATTACK WHEN YOU'RE USING THE COMPUTER OR HOT PLATE.";
            } else if(hint == "room3"){
                hintString = "HINT: ALWAYS KEEP KNOCKING ON THE DOOR.";
            } else if(hint == "room4"){
                hintString = "HINT: STAY CLEAR OF ITS PATH.";
            }
        } else if(monster == "ber"){
            if(hint == "room3"){
                hintString = "HINT: YOU CAN ONLY SCARE IT AWAY IF IT'S CLOSE ENOUGH.";
            } else if(hint == "room4"){
                hintString = "HINT: DON'T STAY IN THE SAME PLACE FOR TOO LONG.";
            }
        } else if(monster == "us"){
            if(hint == "room4"){
                hintString = "HINT: THE LIGHTS MUST BE ON. FLASHLIGHT DOESN'T COUNT.";
            }
        } else if(monster == "cerberus"){
            if(hint == "room5"){
                hintString = "HINT: WEEPING ANGEL. KEEP THE MUSIC BOX WOUND UP.";
            } else if(hint == "room6"){
                hintString = "HINT: TURN AROUND BEFORE YOU HIT THE GROUND.";
            }
        } else if(monster == "paradox"){
            if(hint == "room5"){
                hintString = "THE ROSES SHALL BURN.";
            } else if(hint == "room6"){
                hintString = "PARADOXON EXCITAT.";
            }
        } else{
            hintString = "";
        }

    }
    // level progress
    public SaveData(int lev, string time) {
        // Save level
        if (lev > level) {
            level = lev;
            Debug.Log("level saved: " + level);
        }

        // Save PR (this should be optimized somehow)
        if (lev == 2) {
            if (string.IsNullOrEmpty(time1) || TimeSpan.ParseExact(time, "m':'ss'.'ff", null) < TimeSpan.ParseExact(time1, "m':'ss'.'ff", null)) {
                time1 = time;
                Debug.Log(time1);
            }
            Debug.Log(time1);
        }
        if (lev == 3) {
            if (string.IsNullOrEmpty(time2) || TimeSpan.ParseExact(time, "m':'ss'.'ff", null) < TimeSpan.ParseExact(time2, "m':'ss'.'ff", null)) {
                time2 = time;
                Debug.Log(time2);
            }
            Debug.Log(time2);
        }
        if (lev == 4) {
            if (string.IsNullOrEmpty(time3) || TimeSpan.ParseExact(time, "m':'ss'.'ff", null) < TimeSpan.ParseExact(time3, "m':'ss'.'ff", null)) {
                time3 = time;
                Debug.Log(time3);
            }
            Debug.Log(time3);
        }
        if (lev == 5) {
            if (string.IsNullOrEmpty(time4) || TimeSpan.ParseExact(time, "m':'ss'.'ff", null) < TimeSpan.ParseExact(time4, "m':'ss'.'ff", null)) {
                time4 = time;
                Debug.Log(time4);
            }
            Debug.Log(time4);
        }
        if (lev == 6) {
            if (string.IsNullOrEmpty(time5) || TimeSpan.ParseExact(time, "m':'ss'.'ff", null) < TimeSpan.ParseExact(time5, "m':'ss'.'ff", null)) {
                time5 = time;
                Debug.Log(time5);
            }
            Debug.Log(time5);
        }
    }
    // endings
    public SaveData(int end){
        if(end > ending){
            ending = end;
        }
    }
}
