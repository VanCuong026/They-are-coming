using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private float _EnemyToPlayerDistance = 50, _TimeCounting = 0, _NearestPlayerDistance;
    private Animator _anim;
    public NavMeshAgent agent;
    private int _NextDestination=0,_NearestPlayerIndex;
    public bool _isAlive=true;
    private GameObject _MovePoint, _DesireDestination;
    private Vector3[] _DestinationList;
    private Vector3[] _PlayerList;
    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        _NextDestination = 0;
        _DesireDestination = GameObject.Find("DesireDestinations");
        _DestinationList = new Vector3[_DesireDestination.transform.childCount];
        for (int i = 0; i < _DesireDestination.transform.childCount; i++)
        {
            _DestinationList[i] = _DesireDestination.transform.GetChild(i).transform.position;
        }
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
            }
            else //Nếu ko có con nào nằm trong bán kính định sẵn thì chạy theo đường đã định sẵn
            {
                agent.SetDestination(_DestinationList[_NextDestination]);
                if(Vector3.Distance(transform.position, _DestinationList[_NextDestination]) <= 3f)
                {
                    if(_NextDestination<_DesireDestination.transform.childCount-1)
                        _NextDestination++;
                }
            }    
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
            _TimeCounting = 0;
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
