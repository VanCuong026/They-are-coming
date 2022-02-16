using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private Animator _anim;
    public NavMeshAgent agent;
    int _NextDestination;
    public Vector3[] _Destination;
    bool _isAlive=true;
    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        _NextDestination = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (false) //Khi den gan player
        {
            _NextDestination = 0; //Can gan them vi tri cua Player gan nha
        }
        if(_isAlive)
            agent.SetDestination(_Destination[_NextDestination]);
        else
        {
            agent.SetDestination(transform.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet"|| other.tag == "trap")
        {
            _anim.SetBool("Die",true);
            gameObject.GetComponent<Collider>().enabled = false;
            _isAlive = false;
        }
        if (other.tag == "ChangeDestination1")
        {
            _NextDestination = 2;
        }else if(other.tag == "ChangeDestination2")
        {
            _NextDestination = 3;
        }else if (other.tag == "ChangeDestination3")
        {
            _NextDestination = 4;
        }
        else if (other.tag == "ChangeDestination4")
        {
            _NextDestination = 5;
        }
        
    }
}
