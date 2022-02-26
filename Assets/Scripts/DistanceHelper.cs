using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class DistanceHelper : MonoBehaviour {

	public Transform[] Drawing;
    public float Length;

	public void Update()
    {
        GetLength();
    }

    public void GetLength()
    {
        float distance = 0;
        for(int i = 0; i < Drawing.Length; i++)
        {
            Vector3 first = Drawing[i].transform.position;
            Vector3 second = Drawing[(i+1) % Drawing.Length].transform.position;
            float distToAdd = Vector3.Distance(first, second);
            distance += distToAdd;
        }
        Length = distance;
    }
}
