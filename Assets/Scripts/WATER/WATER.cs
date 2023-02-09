using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WATER : MonoBehaviour
{
    public static WATER Instance;
    [SerializeField] GameObject waterEffect;
    private void Awake()
    {
        Instance = this;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<BLOCK>())
        {
            CollObj(other.gameObject);
        }
    }

    public void CollObj(GameObject other, bool destroy = true)
    {
        Vector3 effectSpawnPos = other.gameObject.transform.position;
        effectSpawnPos.y = transform.position.y;
        Instantiate(waterEffect, effectSpawnPos, Quaternion.Euler(90, 0, 0));
        if(destroy)
        Destroy(other.gameObject, 0.33f);
    }
}
