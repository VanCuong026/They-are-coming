using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public static PlayerMove instance;
    float _Speed = 20f;
    public bool _Stop = false;
    Vector2 _moveInput, _moveTouchStartPosition,_LastPosition;
    Vector3 _PlayerPositionBeforeTouch,DesirePosition;
    private float _TimeCounting = 0;
    private FloatingJoystick _JoyStick;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        _JoyStick = FindObjectOfType<FloatingJoystick>();
    }
    // Update is called once per frame
    void Update()
    {
        if(ZeroPointMove.instance._MoveDirection!= "Stop")
        {
            if (_Stop == false)
                _PlayerMove();
        }
        
    }
    void _PlayerMove()
    {
        if (_JoyStick.Horizontal != 0)
        {
            DesirePosition.x = -_JoyStick.Horizontal * 3.5f;
            DesirePosition.y = 0;
            DesirePosition.z = 0;
            if (DesirePosition.x > 3.5f) DesirePosition.x = 3.5f;
            if (DesirePosition.x < -3.5f) DesirePosition.x = -3.5f;
            transform.localPosition = DesirePosition;
        }
    }
}
