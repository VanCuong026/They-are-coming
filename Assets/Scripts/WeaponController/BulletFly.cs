using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFly : MonoBehaviour
{
    private float _TimeCounting = 0;
    string _WeaponType = "";
    // Start is called before the first frame update
    void Start()
    {
        if (this.name == "AK47Bullet(Clone)"|| this.name == "SniperBullet(Clone)")
        {
            _WeaponType = "SniperBullet";
        }
        else if(this.name == "TacticalKnife_Fade(Clone)")
        {
            _WeaponType = "TacticalKnife_Fade";
        }
        else if (this.name == "Shuriken2(Clone)")
        {
            _WeaponType = "Shuriken2";
        }
    }

    void FixedUpdate()
    {
        _TimeCounting += Time.deltaTime;
        if (_TimeCounting > 2f)
        {
            Pooling.instance._Push(_WeaponType, gameObject);
            _TimeCounting = 0;
            gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Pooling.instance._Push(_WeaponType, gameObject);
            gameObject.SetActive(false);
        }
    }
}
