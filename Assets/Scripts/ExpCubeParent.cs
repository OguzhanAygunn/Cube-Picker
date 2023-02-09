using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpCubeParent : MonoBehaviour
{
    ExpCube[] cubes;
    Transform playerPos;
    [SerializeField] float TriggerRadius;
    private void Awake()
    {
        cubes = GetComponentsInChildren<ExpCube>();
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void FixedUpdate()
    {
        DistanceOP();
    }

    void DistanceOP()
    {
        if(Vector3.Distance(transform.position,playerPos.position) < TriggerRadius)
        {
            foreach (ExpCube cube in cubes)
            {
                cube.ActiveOP();
            }
            Destroy(this);
        }
    }
}
