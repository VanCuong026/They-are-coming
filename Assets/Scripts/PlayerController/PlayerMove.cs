using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public static PlayerMove instance;
    float _Speed = 10f;
    public bool _Stop = false;
    
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(_Stop==false)
            _PlayerMove();
    }

    void _PlayerMove()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Vector3 temp = transform.localPosition;
            temp.x += Time.deltaTime * _Speed;
            if (temp.x > 3.5f)
                temp.x = 3.5f;
            transform.localPosition = temp;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            Vector3 temp = transform.localPosition;
            temp.x -= Time.deltaTime * _Speed;
            if (temp.x < -3.5f)
                temp.x = -3.5f;
            transform.localPosition = temp;
        }
    }
}
