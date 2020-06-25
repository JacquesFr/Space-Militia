using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NotificationType
{
    START_GAME,
    QUIT_GAME,
    PLAYER_DEAD,
    ENEMY_DEAD,
    LEVEL1_START,
    LEVEL2_START,
    TERMINAL_TEXT,
    INTRO_DONE,
    FIRST_CHECKPOINT_DONE,
    SECOND_CHECKPOINT_DONE,
    POWERBASE_1_DONE,
    POWERBASE_2_DONE,
    DISPLAY_TERMINAL_DIRECTIONS,
    TERMINAL_1_DONE,
    TERMINAL_2_DONE,
    LEVEL_1_TEXT,
    LEVEL1_COMPLETE,
    LEVEL2_COMPLETE,
    BUTTON_PRESSED,
    DOOR_OPEN,
    LEVEL_2_COMPLETED,
    LEVEL_2_BEGIN_COMPLETED,
    PART_2_START,
    PART_3_START,
    LEVEL_2_GO_TO_POWER_ROOM,
    ENERGY_COMPONENTS,
    POWER_RESTORED,
    TRASH_COMPACTOR_UI,
    FINAL_STRETCH,
    STATION_SAFE,
    WIN_GAME,
    GAME_OVER
    
};

public abstract class Observer : MonoBehaviour
{
    public abstract void OnNotify(NotificationType type);
}

public abstract class Subject : MonoBehaviour
{
    private List<Observer> observers = new List<Observer>();

    public void AddObserver(Observer newObserver)
    {
        observers.Add(newObserver);
    }

    public void Notify(NotificationType type)
    {
        foreach(Observer watchdog in observers)
        {
            watchdog.OnNotify(type);
        }
    }
}