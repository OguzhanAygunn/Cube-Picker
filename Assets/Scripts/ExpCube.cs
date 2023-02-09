using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ExpCube : MonoBehaviour
{
    Vector3 defaultPos,defaultScale;
    MeshRenderer renderer;
    Color targetColor;
    private void Awake()
    {
        defaultPos = transform.position;
        defaultScale = transform.localScale;
        transform.localScale = Vector3.zero;

        transform.position += new Vector3(Random.Range(-3,4),Random.Range(8,12),Random.Range(0,4));
        transform.eulerAngles = new Vector3(Random.Range(-180,180), Random.Range(-180, 180), Random.Range(-180, 180));

        renderer = GetComponent<MeshRenderer>();
        //targetColor = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        targetColor = Color.red;
    }

    public void ActiveOP()
    {
        renderer.material.DOColor(targetColor,1f);
        transform.DOMove(defaultPos,1f);
        transform.DOScale(defaultScale, 0.7f);
        transform.DORotate(Vector3.zero,1f).OnComplete( () => {
            Destroy(this);
        });
    }
}
