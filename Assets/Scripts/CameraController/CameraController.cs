using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private GameObject PlayerPoint;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        /*Vector3 temp = PlayerPoint.transform.position;
        transform.position = temp + new Vector3(0,5f,4.5f);*/
        transform.LookAt(PlayerPoint.transform.position);
    }
}
