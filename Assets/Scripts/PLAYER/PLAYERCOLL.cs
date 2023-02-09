using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLAYERCOLL : MonoBehaviour
{
    public GameObject blockEffect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Barrier") && !PLAYER.Instance.isTakeHit)
        {
            PLAYER.Instance.CallTakeHitOP(other.gameObject);
            ScreenEffect.Instance.HitEffect(Color.red);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Block"))
        {
            Vector3 effectPos = other.gameObject.transform.position;
            BLOCK block = other.gameObject.GetComponent<BLOCK>();
            Collider blockCollider = block.GetComponent<Collider>();
            blockCollider.enabled = false;
            ParticleSystem ps = Instantiate(blockEffect,effectPos,Quaternion.identity).gameObject.GetComponent<ParticleSystem>();
            ps.startColor = other.gameObject.GetComponent<MeshRenderer>().material.color;
            other.transform.parent = transform;
            block.ColorEffectOP();

            BLOCKHANDLER.Instance.AddBlock(other.gameObject);
        }
        else if(other.CompareTag("Finish") && !GAMEMANAGER.Instance.Finish)
        {
            PLAYER.Instance.FinishOP();
            //Win.Instance.ActiveOP();
        }
    }
}
