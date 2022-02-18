using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float _EnemyToPlayerDistance = 50, _NearestPlayerDistance;
    private Animator _anim;
    public NavMeshAgent agent;
    int _NextDestination,_NearestPlayerIndex;
    public Vector3[] _Destination;
    public bool _isAlive=true;
    GameObject _MovePoint;
    Vector3[] _PlayerList;
    public Vector3 _Desti;
    float _TimeCounting = 0;
    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        _NextDestination = 1;
    }

    // Update is called once per frame
    void Update()
    {
        _MovePoint = GameObject.Find("MovePoint");
        if (_isAlive && ZeroPointMove.instance._NumberOfPlayerisAlive > 0)
        {
            FindNearestPlayer(); //Tìm Player gần Enemy nhất(Chỉ Player nằm trong bán kính đã định sẵn)
            _anim.SetBool("Run", true);
            if (_NearestPlayerDistance < _EnemyToPlayerDistance) //Nếu có Player nằm trong bán kính định sẵn _EnemyToPlayerDistance thì tìm xem con nào gần nhất rồi xông vào con đó
            {
                agent.SetDestination(_PlayerList[_NearestPlayerIndex]);
                _Desti = _PlayerList[_NearestPlayerIndex];
            }
            else //Nếu ko có con nào nằm trong bán kính định sẵn thì chạy theo đường đã định sẵn
                agent.SetDestination(_Destination[_NextDestination]);
        }
        else //Enemy đã chết hoặc không có con Player nào đang Active thì cho Enemy dừng lại
        {
            agent.SetDestination(transform.position);
            _anim.SetBool("Run", false);
        }
        if(!_isAlive) _ReSpawn();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet"|| other.tag == "trap"|| other.tag=="Player")
        {
            _anim.SetBool("Stand", false);
            _anim.SetBool("Die",true);
            gameObject.GetComponent<NavMeshAgent>().radius = 0.01f;
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
    void FindNearestPlayer()
    {
        _NearestPlayerDistance = _EnemyToPlayerDistance;
        _PlayerList = new Vector3[_MovePoint.transform.childCount];
        for (int i = 0; i < _MovePoint.transform.childCount; i++)
        {
            _PlayerList[i] = _MovePoint.transform.GetChild(i).transform.position;
            if((Vector3.Distance(_PlayerList[i],transform.position)<_NearestPlayerDistance)&&_MovePoint.transform.GetChild(i).gameObject.activeSelf)
            { //Nếu Player còn sống và gần Enemy hơn thì ...
                _NearestPlayerDistance = Vector3.Distance(_PlayerList[i], transform.position);
                _NearestPlayerIndex = i;
            }
        }
    }

    void _ReSpawn()
    {
        _TimeCounting+=Time.deltaTime;
        if (_TimeCounting > 5f)
        {
            gameObject.SetActive(false);
            Pooling.instance._Push("Enemy", gameObject);
        }
    }
}
