using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody myBody;
    public Vector3 _DefencePos = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        myBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_DefencePos != Vector3.zero)
        {
            myBody.velocity = _DefencePos - transform.position;
        }
        else
        {
            //myBody.velocity = Vector3.zero;
            myBody.velocity = GameObject.Find("MovePoint").gameObject.transform.position - transform.position;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Trap" || other.tag=="Enemy")
        {
            gameObject.SetActive(false);
            Pooling.instance._Push("Player", gameObject);
        }
        if (other.tag == "Sniperitem")
        {
            ZeroPointMove.instance._WeaponID =1;
            Destroy(other.gameObject);
        }
        else if (other.tag == "CrossBowItem")
        {
            ZeroPointMove.instance._WeaponID = 2;
            Destroy(other.gameObject);
        }
        else if (other.tag == "ShurikenItem")
        {
            ZeroPointMove.instance._WeaponID = 4;
            Destroy(other.gameObject);
        }
        else if (other.tag == "KnifeItem")
        {
            ZeroPointMove.instance._WeaponID = 3;
            Destroy(other.gameObject);
        }
    }
}
