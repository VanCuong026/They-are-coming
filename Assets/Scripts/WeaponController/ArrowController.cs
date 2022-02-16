using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    public float _ArrowForce = 500000f;
    private Rigidbody _ArrowRigidBody;
    // Start is called before the first frame update
    void Start()
    {
        _ArrowRigidBody = GetComponent<Rigidbody>();
        _ArrowRigidBody.AddForce(-PlayerMove.instance.transform.forward * _ArrowForce);
    }

    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            //Destroy(other.gameObject);
            //Destroy(gameObject);
        }
    }
}
