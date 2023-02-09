using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FallingCube : MonoBehaviour
{
    Transform playerPos;
    Vector3 targetPos;
    [SerializeField] float DistanceOfPlayer;
    [SerializeField] Material FadeMat;
    bool fall;
    // Start is called before the first frame update
    private void Awake()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        fall = (Random.Range(0, 5) < 3) ? true : false;
        targetPos = transform.position + Vector3.down * 10f;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        DistanceOp();
    }

    void DistanceOp()
    {
        if (fall)
        {
            if(Vector3.Distance(transform.position, playerPos.position) < DistanceOfPlayer)
            {
                fall = false;
                transform.DOMove(targetPos, 1.4f);
            }
        }
    }
}
