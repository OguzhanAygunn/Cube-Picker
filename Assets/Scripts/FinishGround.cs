using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FinishGround : MonoBehaviour
{
    public static FinishGround Instance;

    Material material;
    Vector3 targetPos;
    [HideInInspector] public Transform myTransform;
    // Start is called before the first frame update

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        material = transform.GetChild(1).gameObject.GetComponent<MeshRenderer>().material;
        targetPos = transform.position;
        targetPos.y += 5f;
        myTransform = transform;
    }

    public void FinishOP()
    {
        material.DOColor(Color.black,1f).SetDelay(1f);
        transform.DOMove(targetPos,1f).SetDelay(1.6f);
    }
}
