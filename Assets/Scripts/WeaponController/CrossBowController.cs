using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossBowController : MonoBehaviour
{
    [SerializeField]
    private GameObject _Arrow;
    [SerializeField]
    private GameObject _zeroPoint;
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
        if (_TimeCounting> _TimeRandom)
        {
            Vector3 temp = transform.position;
            temp.y = 0.5f;
            GameObject Temporary_Arrow =  Instantiate(_Arrow, temp, Quaternion.Euler(0,90f+_zeroPoint.transform.rotation.eulerAngles.y, 0));
            Destroy(Temporary_Arrow, 10f);
            _TimeCounting = 0;
        }
    }
}
