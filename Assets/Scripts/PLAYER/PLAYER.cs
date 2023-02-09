using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLAYER : MonoBehaviour
{
    public static PLAYER Instance;
    public enum phases
    {
        small,
        medium,
        large
    }

    public enum Speed
    {
        normal,
        fast
    }

    public phases phase = phases.small;
    public Speed speed = Speed.normal;

    [System.Serializable]
    public class Components
    {
        public Rigidbody Rigidbody;
        public SphereCollider Collider;
        public PLAYERMOVE PLAYERMOVE;
        public PLAYERROTATE PLAYERROTATE;
        public PLAYERSIZE PLAYERSIZE;
        public PLAYERCOLL PLAYERCOLL;
    }
    [System.Serializable]
    public class Effects
    {
        public GameObject HitEffect;
        public GameObject DestroyEffect;
    }
    public Components components;
    public Effects effects;
    RigidbodyConstraints constraints;
    [HideInInspector] public bool isTakeHit = false, isFreeze = false;
    [SerializeField] LayerMask GroundLayer;
    private void Awake()
    {
        Instance = this;
        constraints = components.Rigidbody.constraints;
    }

    private void FixedUpdate()
    {
        RayOP();
    }

    void RayOP()
    {
        if(Physics.Raycast(transform.position,Vector3.forward,transform.localScale.z / 2f + (0.1f), GroundLayer)){
            DeathOP();
        }
    }

    public void CallTakeHitOP(GameObject collObj)
    {
        StartCoroutine(TakeHitOP(collObj));
    }

    public void ChangePhase(phases _phase)
    {
        components.PLAYERSIZE.ChangePhase(_phase);
    }

    public void DeathOP()
    {
        GAMEMANAGER.Instance.GameLose = true;
        components.Rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        BLOCKHANDLER.Instance.DeathOP();
        GameObject effect = Instantiate(effects.DestroyEffect,transform.position,Quaternion.identity);
        ScreenEffect.Instance.HitEffect(Color.red);
        CAMERA.Instance.CallShakeOP();
        ScreenEffect.Instance.LoseOP();
        components.Collider.enabled = false;
        gameObject.SetActive(false);
    }

    public void FinishOP()
    {
        GAMEMANAGER.Instance.Finish = true;
        components.PLAYERROTATE.FinishOP();
        components.PLAYERMOVE.FinishOP();
        components.Rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        transform.parent = FinishGround.Instance.myTransform;
        CAMERA.Instance.FinishOP();
        FinishGround.Instance.FinishOP();
        FinishCylinder.Instance.ActiveOP();
    }

    IEnumerator TakeHitOP(GameObject collObj)
    {
        isTakeHit = true;
        //Instantiate(effects.HitEffect, transform.position, Quaternion.identity);
        components.Rigidbody.constraints = RigidbodyConstraints.None;
        components.Rigidbody.velocity = Vector3.zero;
        components.Rigidbody.velocity = (transform.position - collObj.transform.position) * 6f;
        CAMERA.Instance.CallShakeOP();
        BLOCKHANDLER.Instance.DropTheBlocks(Random.Range(10,21));
        yield return new WaitForSeconds(0.66f);
        components.Rigidbody.constraints = constraints;
        isTakeHit = false;
    }
}
