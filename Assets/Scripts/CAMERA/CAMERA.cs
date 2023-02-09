using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CAMERA : MonoBehaviour
{
    public static CAMERA Instance;

    Transform Target,LookPos;
    public Vector3 offset,LookOffset;
    public float FollowSpeed,RotateSpeed;
    public Vector3 smallOffset, mediumOffset, largeOffset;
    public bool Isfreeze;
    // Start is called before the first frame update

    private void Awake()
    {
        smallOffset  = offset;
        mediumOffset = smallOffset + new Vector3(0,3,-3);
        largeOffset  = mediumOffset + new Vector3(0,3,-3);
        Instance     = this;    
    }

    void Start()
    {
        Target = GameObject.FindGameObjectWithTag("Player").transform;
        LookPos = Target;
        transform.LookAt(LookPos);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        FOLLOW();
        LOOK();
    }

    public void ChangeOffset(PLAYER.phases phase)
    {
        if (phase == PLAYER.phases.small)
            offset = smallOffset;
        else if (phase == PLAYER.phases.medium)
            offset = mediumOffset;
        else if (phase == PLAYER.phases.large)
            offset = largeOffset;
    }

    void FOLLOW()
    {
        if (Target && !Isfreeze)
        {
            Vector3 FollowPos = Target.position + offset;
            transform.position = Vector3.MoveTowards(transform.position, FollowPos, FollowSpeed * Time.deltaTime);
        }

    }

    void LOOK()
    {
        if(LookPos)
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(LookPos.position - transform.position + (LookOffset)), RotateSpeed * Time.deltaTime);
    }

    public void CallShakeOP()
    {
        StartCoroutine(ShakeOP());
    }

    public void FinishOP()
    {
        offset.y -= offset.y / 1.3f;
        offset.z += offset.z / 1.5f;
    }

    public void changeTarget(Transform newTarget)
    {
        Target = newTarget;
    }

    public void changeLook(Transform newLook)
    {
        LookPos = newLook;
    }

    IEnumerator ShakeOP()
    {
        transform.position += Vector3.right / 2f;
        yield return new WaitForSeconds(0.01f);
        transform.position += Vector3.up / 2f;
        yield return new WaitForSeconds(0.01f);
        transform.position += Vector3.forward / 2f;
        yield return new WaitForSeconds(0.01f);
        transform.position += Vector3.left / 2f;
        yield return new WaitForSeconds(0.01f);
        transform.position += Vector3.back / 2f;
        yield return new WaitForSeconds(0.01f);
        transform.position += Vector3.down / 2f;
        yield return new WaitForSeconds(0.01f);
    }
}
