using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody myBody;
    // Start is called before the first frame update
    void Start()
    {
        myBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 temp = Vector3.zero - transform.localPosition;
        myBody.velocity = Vector3.zero;
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Trap" || other.tag=="Enemy")
        {
            gameObject.SetActive(false);
            Pooling.instance._Push("Player", gameObject);
        }
    }


}
