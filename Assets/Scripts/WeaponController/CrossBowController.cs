using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossBowController : MonoBehaviour
{
    float _TimeCounting = 0, _TimeRandom = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.IsPlaying)
        {
            _TimeRandom = Random.Range(1f, 5f);
            _TimeCounting += Time.deltaTime;
            if (_TimeCounting > _TimeRandom)
            {
                Vector3 temp = transform.position;
                temp.y = 0.5f;
                GameObject Temporary_Arrow = Pooling.instance._Pull("Arrow");
                Temporary_Arrow.transform.parent = GameObject.Find("Bullet").transform;
                Temporary_Arrow.SetActive(true);
                Temporary_Arrow.GetComponent<Rigidbody>().velocity = Vector3.zero;
                Temporary_Arrow.GetComponent<Rigidbody>().AddForce(-ZeroPointMove.instance.transform.forward * 500);
                Temporary_Arrow.transform.position = temp;
                Temporary_Arrow.transform.rotation = Quaternion.Euler(0, 90f + ZeroPointMove.instance.transform.rotation.eulerAngles.y, 0);
                _TimeCounting = 0;
            }
        }
    }
}
