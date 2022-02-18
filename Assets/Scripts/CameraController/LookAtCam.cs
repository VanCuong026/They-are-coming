using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCam : MonoBehaviour
{
    public Transform _cam;

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(_cam);
    }
}
