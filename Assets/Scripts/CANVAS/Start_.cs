using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Start_ : MonoBehaviour
{
    public static Start_ Instance;
    Image image;
    GameObject other;
    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
        image = GetComponent<Image>();
        other = transform.GetChild(0).gameObject;
    }
    void Start()
    {
        
    }

    public void ActiveOP(bool active)
    {
        if (active)
        {
            Color color = image.color;
            color.a = 0.8f;
            image.DOColor(color,1f);
            other.transform.DOScale(Vector3.one,1f);
            GAMEMANAGER.Instance.touchAble = true;
        }
        else
        {
            Color color = image.color;
            color.a = 0;
            image.DOColor(color,1f);
            other.transform.DOScale(Vector3.zero, 1f).OnComplete( () => {
                Destroy(gameObject);
            });
            GAMEMANAGER.Instance.GameStart = true;
        }
    }
}
