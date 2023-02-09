using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PLAYERSIZE : MonoBehaviour
{
    Vector3 defaultScale;
    public GameObject FirstCollEffect;
    Vector3 smallSize, mediumSize, largeSize;
    // Start is called before the first frame update
    private void Awake()
    {
        defaultScale = transform.localScale;
        smallSize = defaultScale;
        mediumSize = smallSize * 1.5f;
        largeSize = mediumSize * 1.3f;        
    }
    void Start()
    {
        
    }

    public void FirstSizeCollEffect()
    {
        Vector3 targetScale = defaultScale;
        targetScale.x *= 2f;
        targetScale.y *= 1.4f;
        Instantiate(FirstCollEffect,transform.position,Quaternion.identity);
        transform.DOScale(targetScale,0.15f).OnComplete( () => {
            transform.DOScale(defaultScale,0.15f);
        });
    }

    public void SizeUp()
    {
        Vector3 targetScale = defaultScale;
        targetScale.x *= 2f;
        targetScale.y *= 1.4f;
        defaultScale *= 1.35f;
        transform.DOScale(defaultScale,0.15f).OnComplete( () => {
            transform.DOScale(defaultScale,0.15f);
        });
    }

    public void ChangePhase(PLAYER.phases phase)
    {
        PLAYER.phases nowPhase = PLAYER.Instance.phase;
        if(nowPhase != phase)
        {
            PLAYER.Instance.phase = phase;
            CAMERA.Instance.ChangeOffset(phase);
            Vector3 targetScale;
            switch (phase)
            {
                case PLAYER.phases.small:
                    targetScale = smallSize;
                    break;
                case PLAYER.phases.medium:
                    targetScale = mediumSize;
                    break;
                case PLAYER.phases.large:
                    targetScale = largeSize;
                    break;
                default:
                    targetScale = smallSize;
                    break;
            }
            ScaleEffect.Instance.VisibleOP();
            transform.DOScale(targetScale,1f);
        }


    }
}
