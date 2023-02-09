using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BarrierGround : MonoBehaviour
{
    Transform playerPos;
    Vector3 defaultPos;
    private float distance = 16;
    bool active;
    // Start is called before the first frame update
    private void Awake()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        defaultPos = transform.position;
        transform.position = new Vector3(defaultPos.x, defaultPos.y - transform.localScale.y, defaultPos.z);
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        mainOP();
    }

    void mainOP()
    {
        if(!active && Vector3.Distance(transform.position,playerPos.position) < distance)
        {
            active = true;
            transform.DOMove(defaultPos,0.6f);
        }
    }
}
