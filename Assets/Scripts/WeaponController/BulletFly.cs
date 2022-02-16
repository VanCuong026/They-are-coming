using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFly : MonoBehaviour
{
    private float _TimeCounting = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    void FixedUpdate()
    {
        _TimeCounting += Time.deltaTime;
        if (_TimeCounting > 2f)
        {
            Pooling.instance._Push("SniperBullet", gameObject);
            _TimeCounting = 0;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Pooling.instance._Push("SniperBullet", gameObject);
        }
    }
}
