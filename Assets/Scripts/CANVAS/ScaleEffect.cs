using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ScaleEffect : MonoBehaviour
{
    public static ScaleEffect Instance;
    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {

    }

    public void VisibleOP()
    {
        transform.DOScale(Vector3.one,0.6f).SetEase(Ease.OutElastic).OnComplete( () => {
            transform.DOScale(Vector3.zero,0.6f);
        });
    }
}
