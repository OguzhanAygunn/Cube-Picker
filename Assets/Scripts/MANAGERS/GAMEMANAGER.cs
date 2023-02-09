using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GAMEMANAGER : MonoBehaviour
{
    public static GAMEMANAGER Instance;
    public bool GameStart, GameLose, Finish, touchAble;
    // Start is called before the first frame update

    private void Awake()
    {
        touchAble = true;
        Instance = this;
        //Application.targetFrameRate = 60;
    }

    private void FixedUpdate()
    {
        /*if(Input.touchCount > 0 && !GameStart && touchAble)
        {
            GameStart = true;
            touchAble = false;
            Start_.Instance.ActiveOP(false);
        }*/
    }
}
