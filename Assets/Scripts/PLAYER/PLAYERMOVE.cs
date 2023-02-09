using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PLAYERMOVE : MonoBehaviour
{
    public float MoveSpeed;
    float speed;
    Touch touch;
    Vector3 firstPos;
    Vector3 MovePos;
    PLAYER Player;
    bool firstMove = true, freeze;
    // Start is called before the first frame update

    private void Awake()
    {
        firstPos = transform.position;
        transform.position = firstPos + Vector3.up * 100f;
        MovePos = transform.position;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!GAMEMANAGER.Instance.GameLose)
        {
            MOVE();
            SpeedOP();
            FallControl();
        }
        Clamp();
    }

    void MOVE()
    {

        if (firstMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, firstPos, MoveSpeed * 4f * Time.deltaTime);
            if(transform.position == firstPos)
            {
                firstMove = false;
                PLAYER.Instance.components.PLAYERSIZE.FirstSizeCollEffect();
                Start_.Instance.ActiveOP(true);
            }
            return;
        }

        if (GAMEMANAGER.Instance.GameStart && !PLAYER.Instance.isTakeHit)
        {
            MovePos = new Vector3(MovePos.x,transform.position.y,transform.position.z);
            if (Input.touchCount > 0)
            {
                touch = Input.GetTouch(Input.touchCount - 1);
                MovePos.x += touch.deltaPosition.x/2 *Time.deltaTime;
                MovePos.x = Mathf.Clamp(MovePos.x,-7,7);
                
            }

            if (freeze)
            {
                MovePos.y = 0;
                MovePos.x = 0;
                return;
            }
            transform.position = Vector3.MoveTowards(transform.position,MovePos,speed*Time.deltaTime);
            transform.localPosition += Vector3.forward* speed *Time.deltaTime;
        }
    }

    void SpeedOP()
    {
        if (GAMEMANAGER.Instance.GameStart && !GAMEMANAGER.Instance.GameLose && !GAMEMANAGER.Instance.Finish)
        {
            speed = Mathf.MoveTowards(speed, MoveSpeed, Time.deltaTime * MoveSpeed * 1.5f);
        }
    }

    void Clamp()
    {
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x,-7,7);
        pos.y = Mathf.Clamp(pos.y,-2.6f,Mathf.Infinity);
        if(transform.position.x > 6.2f || transform.position.x < -6.2f)
        {
            freeze = true;
        }
        transform.position = pos;
    }

    void FallControl()
    {
        if(transform.position.y < -2 && !GAMEMANAGER.Instance.GameLose)
        {
            PLAYER.Instance.DeathOP();
        }
    }

    public void FinishOP()
    {
        DOTween.To(() => speed, x => speed= x, 0, 2);
    }
}
