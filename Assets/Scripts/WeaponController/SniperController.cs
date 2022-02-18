using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperController : MonoBehaviour
{
    [SerializeField]
    private GameObject _SpawnerPosition;
    float _TimeCounting = 0, _TimeRandom = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject Bullet = GameObject.Find("Bullet");
        GameObject ZeroPoint = GameObject.Find("ZeroPoint");
        _TimeRandom = Random.Range(1f, 5f);
        _TimeCounting += Time.deltaTime;
        if (_TimeCounting > _TimeRandom)
        {
            GameObject gob = Pooling.instance._Pull("SniperBullet");
            gob.SetActive(true);
            gob.GetComponent<Rigidbody>().velocity=Vector3.zero;
            gob.GetComponent<Rigidbody>().AddForce(-ZeroPointMove.instance.transform.forward * 500);
            gob.transform.position = _SpawnerPosition.transform.position;
            gob.transform.rotation = Quaternion.Euler(0, 90f + ZeroPoint.transform.rotation.eulerAngles.y, 90f + ZeroPoint.transform.rotation.eulerAngles.z);
            gob.transform.parent = Bullet.transform;
            _TimeCounting = 0;
        }
    }
}
