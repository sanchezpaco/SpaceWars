using UnityEngine;
using System.Collections;

public class TimeManager : MonoBehaviour
{
        
    public static Vector3 playerSpawnPoint;
    public bool playerGotWeapon;

    private bool gamePaused;
    private float timePaused;
    private float _lastFrameTime;
    private float deltaTime;

    //Awake is always called before any Start functions
    void Awake()
    {            
        this.gamePaused = false;
    }

    void Update()
    {
        if (gamePaused)
        {
            timePaused -= deltaTime;
            if (timePaused < 0)
            {
                gamePaused = false;
                Time.timeScale = 1.0f;                    
            }
        }

        deltaTime = Time.realtimeSinceStartup - _lastFrameTime;
        _lastFrameTime = Time.realtimeSinceStartup;
    }

    public void Micropause(float time)
    {
        Time.timeScale = 0.0f;
        gamePaused = true;
        timePaused = time;
    }
        
        
}
