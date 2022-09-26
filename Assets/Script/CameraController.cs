using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject target;
    public GameObject target1;
    public Vector3 offset;
    public bool isStarted = false;

    public static CameraController instance = null;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    void Update()
    {
        if (isStarted == true)
        {
            transform.position = Vector3.Lerp(transform.position, target.transform.position, 3f * Time.deltaTime);
            transform.LookAt(target1.transform);
        }

    }
}
