using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BLOCK : MonoBehaviour
{
    Vector3 defaultPos;
    Color defaultColor;
    Renderer MyRender;
    [SerializeField] bool FlyMode,up;
    [SerializeField] float PlayerDistance;
    Rigidbody rigidbody;
    Collider collider;
    Transform playerPos;

    // Start is called before the first frame update

    private void Awake()
    {
        MyRender = GetComponent<Renderer>();
        MyRender.material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        defaultColor = MyRender.material.color;
        if (FlyMode)
        {
            playerPos = GameObject.FindGameObjectWithTag("Player").transform;
            defaultPos = transform.position;
            collider = GetComponent<Collider>();
            rigidbody = gameObject.AddComponent<Rigidbody>();
            rigidbody.constraints = RigidbodyConstraints.FreezeAll;
            transform.position += new Vector3(Random.Range(-2,3),Random.Range(6,11),0);
            transform.eulerAngles = Vector3.one * Random.Range(0,361);
            StartCoroutine(nameof(Move));
        }

    }

    void Start()
    {

    }

    private void FixedUpdate()
    {
        if(FlyMode)
        FlyOP();
    }

    void FlyOP()
    {
        if(Vector3.Distance(transform.position,playerPos.position) < PlayerDistance)
        {
            FlyMode = false;
            transform.DOMove(defaultPos,0.4f);
            transform.DORotate(Vector3.zero,0.4f);
        }
        else
        {
            transform.Rotate(Vector3.one * 270 * Time.deltaTime);
            if (up)
                transform.Translate(Vector3.up * 2 * Time.fixedDeltaTime);
            else
                transform.Translate(Vector3.down * 2 * Time.fixedDeltaTime);
        }
    }

    IEnumerator Move()
    {
        yield return new WaitForSeconds(Random.Range(0,101) / 100);
        while (FlyMode)
        {
            up = true;
            yield return new WaitForSeconds(0.25f);
            up = false;
            yield return new WaitForSeconds(0.25f);
        }
    }


    public void ColorEffectOP()
    {
        MyRender.material.DOColor(Color.white,0.25f).OnComplete( () => {
            MyRender.material.DOColor(defaultColor,0.25F);
        });
    }
}
