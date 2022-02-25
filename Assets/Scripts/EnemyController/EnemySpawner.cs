using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner instance;
    public int _SpawnerAmount = 300,_EnemyAmount = 1;
    public int _EachTimeSpawnerAmount = 20;
    [SerializeField] private GameObject _EnemyGroup;
    [SerializeField] private GameObject _EnemySpawner;
    bool _StartSpawning = false,_SpawnerSwitch=false;
    float _TimeCounting = 0;
    private int _EnemyCounting = 0;
    GameObject _MovePoint;
    void Update()
    {
        if (_SpawnerAmount > 0 && _StartSpawning&& ZeroPointMove.instance._NumberOfPlayerisAlive>0)
        {
            _Spawn();
            if (_SpawnerAmount % _EachTimeSpawnerAmount == 0)
            {
                _StartSpawning = false;
                _TimeCounting = 0;
            }
        }
        if (_SpawnerSwitch == true)
            _TimeCounting += Time.deltaTime;
        if (_TimeCounting > 5f)
        {
            _StartSpawning = true;
        }
        EnemyCounting();
        if(_EnemyAmount==0)
        {
            GameManager.instance.IsPlaying = false;
            GameManager.instance.IsWin = true;
            Debug.Log("IsWin=" + GameManager.instance.IsWin);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MovePoint") &&this.name== "EnemySpawner")
        {
            _StartSpawning = true;
            _SpawnerSwitch = true;
        }else if(other.CompareTag("MovePoint") &&this.name == "EnemySpawner (1)")
        {
            _StartSpawning = true;
            _SpawnerSwitch = true;
        }
        else if (other.CompareTag("MovePoint") && this.name == "EnemySpawner (2)")
        {
            _StartSpawning = true;
            _SpawnerSwitch = true;
        }
    }
    void _Spawn()
    {
        Vector3 temp = new Vector3(Random.Range(-3.5f, 3.5f), 0, Random.Range(-5f, 5f)); //Random vị trí Spawn.
        GameObject gob = Pooling.instance._Pull("Enemy");
        gob.SetActive(true);
        gob.GetComponent<NavMeshAgent>().radius = 0.5f;
        gob.GetComponent<Collider>().enabled = true;
        gob.GetComponent<EnemyController>()._isAlive = true;
        gob.GetComponent<Animator>().SetBool("Die",false);
        gob.GetComponent<Animator>().SetBool("Stand", true);
        gob.transform.parent = _EnemySpawner.transform;
        gob.transform.localPosition = temp;
        gob.transform.parent = _EnemyGroup.transform;
        _SpawnerAmount--;
    }

    void EnemyCounting()
    {
        _EnemyCounting = 0;
        for (int i = 0; i < _EnemyGroup.transform.childCount; i++)
        {
            if (_EnemyGroup.transform.GetChild(i).gameObject.activeSelf) _EnemyCounting++;
        }
        _EnemyAmount = _EnemyCounting;
    }
}
