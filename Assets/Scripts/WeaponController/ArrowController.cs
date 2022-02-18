using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    public float _ArrowForce = 500f;
    private Rigidbody _ArrowRigidBody;
    float _TimeCounting=0;
    // Start is called before the first frame update
    void Start()
    {
        _ArrowRigidBody = GetComponent<Rigidbody>();
        _ArrowRigidBody.AddForce(-PlayerMove.instance.transform.forward * _ArrowForce);
    }

    void Update()
    {
        _TimeCounting += Time.deltaTime;
        if (_TimeCounting > 2f)
        {
            Pooling.instance._Push("Arrow", gameObject);
            gameObject.SetActive(false);
            _TimeCounting = 0;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Pooling.instance._Push("Arrow", gameObject);
            gameObject.SetActive(false);
            _TimeCounting = 0;
        }
    }
}
