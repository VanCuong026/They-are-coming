using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField]
    private Transform _MovePoint;
    int _SpawnQuatity=0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (_SpawnQuatity > 0)
        {
            GameObject gob = Pooling.instance._Pull("Player");
            Vector3 temp = new Vector3(_MovePoint.position.x + Random.Range(-0.05f, 0.05f), _MovePoint.position.y, _MovePoint.position.z + Random.Range(-0.05f, 0.05f));
            gob.transform.position = temp;
            gob.transform.parent = _MovePoint;
            gob.transform.localRotation = Quaternion.Euler(0, 180f, 0);
             _SpawnQuatity--;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == "x2")
        {
            _SpawnQuatity = 1;
        }
        else if(other.tag == "x3")
        {
            _SpawnQuatity = 2;
        }
        else if (other.tag == "+25")
        {
            _SpawnQuatity = 25;
        }
        else if (other.tag == "+30")
        {
            _SpawnQuatity = 30;
        }
    }
}
