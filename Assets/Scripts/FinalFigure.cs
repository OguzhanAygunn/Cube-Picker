using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalFigure : MonoBehaviour
{
    public static FinalFigure Instance;

    public List<Color> Colors = new List<Color>();
    public List<Vector3> PosS = new List<Vector3>();
    int index;

    private void Awake()
    {
        Instance = this;
        while (index < transform.childCount)
        {
            GameObject obj = transform.GetChild(index).gameObject;
            Colors.Add(obj.GetComponent<MeshRenderer>().material.color);
            PosS.Add(obj.transform.position);
            Destroy(obj);
            index++;
        }
    }
}
