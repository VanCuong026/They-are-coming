using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField]
    private GameObject Bullet;
    [SerializeField]
    private Transform _zeroPoint;
    [SerializeField]
    private Transform _SpawnerPosition;
    float _TimeCounting = 0, _TimeRandom = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _TimeRandom = Random.Range(0.2f, 0.5f);
        _TimeCounting += Time.deltaTime;
        if (_TimeCounting > _TimeRandom)
        {
            GameObject gob = Pooling.instance._Pull("TacticalKnife_Fade");
            gob.transform.position = _SpawnerPosition.transform.position;
            gob.transform.rotation = Quaternion.Euler(0, 180f + _zeroPoint.transform.rotation.eulerAngles.y, 90f + _zeroPoint.transform.rotation.eulerAngles.z);
            gob.transform.parent = Bullet.transform;
            _TimeCounting = 0;
        }
    }
}
