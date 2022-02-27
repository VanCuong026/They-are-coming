using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pooling : MonoBehaviour
{
    public static Pooling instance;

    Dictionary<string, Queue<GameObject>> _Everythings = new Dictionary<string, Queue<GameObject>>();
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void _Push(string key,GameObject gob)
    {
        if (!_Everythings.ContainsKey(key))
        {
            _Everythings.Add(key, new Queue<GameObject>());
        }
        _Everythings[key].Enqueue(gob);
    }

    public GameObject _Pull(string key)
    {
        string path = "";
        if (key == "Enemy") path = "Prefabs/Enemy";
        if (key == "Arrow") path = "Prefabs/Arrow";
        if (key == "SniperBullet") path = "Prefabs/SniperBullet";
        if (key == "TacticalKnife_Fade") path = "Prefabs/TacticalKnife_Fade";
        if (key == "Shuriken2") path = "Prefabs/Shuriken2";
        if (key == "Player") path = "Prefabs/Player";
        if (_Everythings.ContainsKey(key))
        {
            if (_Everythings[key].Count > 0)
            {
                return _Everythings[key].Dequeue();
            }
            else
            {
                GameObject gobCopy = Instantiate(Resources.Load<GameObject>(path));
                return gobCopy;
            }
        }
        else
        {
            GameObject gobCopy = Instantiate(Resources.Load<GameObject>(path));
            return gobCopy;
        }
    }
}
