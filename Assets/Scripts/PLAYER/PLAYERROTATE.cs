using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PLAYERROTATE : MonoBehaviour
{
    Touch touch;
    float yRotP = 30,xRotP = 360;
    [HideInInspector] public bool RandomRotate;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ROTATE();
    }

    void ROTATE()
    {
        if (GAMEMANAGER.Instance.GameStart && !RandomRotate)
        {
            Vector3 delta = Vector3.right;

            if (Input.touchCount > 0)
            {
                touch = Input.GetTouch(Input.touchCount - 1);
                delta = touch.deltaPosition;
                delta.y = delta.x * Time.deltaTime * yRotP / 5;
            }
            delta.x = xRotP * Time.deltaTime;
            delta.x = Mathf.Clamp(delta.x,-5,5);

            transform.Rotate(delta,Space.World);
        }
        else if (RandomRotate)
        {
            transform.Rotate(Vector3.one * 180 * Time.deltaTime);
        }
    }

    public void FinishOP()
    {
        DOTween.To(() => xRotP, x => xRotP = x, 0, 2);
        DOTween.To(() => yRotP, x => yRotP = x, 0, 2);
    }
}
