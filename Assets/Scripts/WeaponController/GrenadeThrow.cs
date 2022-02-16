using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeThrow : MonoBehaviour
{
    private Rigidbody _Grenade;
    // Start is called before the first frame update
    void Start()
    {
        _Grenade = GetComponent<Rigidbody>();
        _Grenade.AddForce(Vector3.back * 700f);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
