using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeController : MonoBehaviour
{
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
        _TimeRandom = Random.Range(1f, 5f);
        _TimeCounting += Time.deltaTime;
        if (_TimeCounting > _TimeRandom)
        {
            GameObject gob = Pooling.instance._Pull("TacticalKnife_Fade");
            gob.SetActive(true);
            gob.transform.position = _SpawnerPosition.transform.position;
            gob.GetComponent<Rigidbody>().velocity = Vector3.zero;
            gob.GetComponent<Rigidbody>().AddForce(-ZeroPointMove.instance.transform.forward * 500);
            gob.transform.rotation = Quaternion.Euler(0, 180f + ZeroPointMove.instance.transform.rotation.eulerAngles.y, 90f + ZeroPointMove.instance.transform.rotation.eulerAngles.z);
            gob.transform.parent = GameObject.Find("Bullet").transform;
            _TimeCounting = 0;
        }
    }
}
