using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SLOT : MonoBehaviour
{
    Vector3 defaultScale;
    Vector2 targetPos;
    public int index;
    RectTransform Rect;
    // Start is called before the first frame update

    private void Awake()
    {
        Rect = GetComponent<RectTransform>();
        targetPos = Rect.anchoredPosition;
        targetPos.y += 500;
        targetPos.x = 0;
        defaultScale = transform.localScale;
    }
    void Start()
    {
        
    }

    public void TouchDown()
    {
        if (GAMEMANAGER.Instance.touchAble)
        {
            Debug.Log("231123");
            transform.parent = GameObject.Find("Canvas").transform;
            FinishCylinder.Instance.SpawnFigure(index);
            Rect.DOAnchorPos(targetPos, 1f);
            transform.DOScale(defaultScale * 1.3f, 1.2f).SetDelay(0.3f).SetEase(Ease.OutElastic);
            transform.DORotate(Vector3.up * 90, 1f).SetDelay(0.7f);
            Start_.Instance.ActiveOP(false);
            GAMEMANAGER.Instance.touchAble = false;
        }
    }
}
