using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FinishCylinder : MonoBehaviour
{
    public static FinishCylinder Instance;
    Vector3 defaultPos;
    Transform targetCameraPos;

    [SerializeField] GameObject[] Figures;
    [SerializeField] Transform FigurePos;

    private void Awake()
    {
        Instance = this;

    }

    public void SpawnFigure(int index)
    {
        Instantiate(Figures[index], FigurePos.position, Figures[index].transform.rotation);
        GAMEMANAGER.Instance.touchAble = false;
    }

    private void Start()
    {
        defaultPos = transform.position;
        transform.position -= Vector3.up * 15;
        targetCameraPos = transform.GetChild(transform.childCount - 1).gameObject.transform;
    }

    public void ActiveOP()
    {
        transform.DOMove(defaultPos,1f).SetDelay(2.2f).OnComplete( () => {
            BLOCKHANDLER.Instance.CallBlocksToFigure();
            PLAYER.Instance.components.PLAYERROTATE.RandomRotate = true;
            /*CAMERA.Instance.changeTarget(targetCameraPos);
            CAMERA.Instance.changeLook(transform);*/
        });
    }
}
