using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ObjectInWater : MonoBehaviour
{
    [SerializeField] bool Move, Rotate;
    Vector3 defaultPos, targetPos;
    Vector3 defaultRot;
    [SerializeField] Vector3 targetRot;
    // Start is called before the first frame update
    void Start()
    {
        defaultPos = transform.position;
        targetPos = defaultPos;

        defaultRot = transform.eulerAngles;
        targetPos.y += transform.lossyScale.y / 4;
        float delay = Random.Range(0,1000) / 1000;
        if (Move)
            Invoke("MoveOP", delay);
        if (Rotate)
            Invoke("RotateOP", delay);

    }


    void MoveOP()
    {
        transform.DOMove(targetPos, 2).OnComplete(() =>{
            transform.DOMove(defaultPos, 2f).OnComplete(() =>{
                MoveOP();
            });
        });
    }

    void RotateOP()
    {
        transform.DORotate(targetRot, 3f).OnComplete( () => {
            transform.DORotate(defaultRot, 3f).OnComplete( () => {
                RotateOP();
            });
        });
    }

}
