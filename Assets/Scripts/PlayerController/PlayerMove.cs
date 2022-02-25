using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public static PlayerMove instance;
    float _Speed = 10f;
    public bool _Stop = false;
    Vector2 _moveInput, _moveTouchStartPosition,_LastPosition;
    Vector3 _PlayerPositionBeforeTouch,DesirePosition;
    private float _TimeCounting = 0;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }
    // Update is called once per frame
    void Update()
    {
        if(ZeroPointMove.instance._MoveDirection!= "Stop")
        {
            _MoveController();
            if (_Stop == false)
                _PlayerMove();
        }
    }
    void _PlayerMove()
    {
        DesirePosition = _PlayerPositionBeforeTouch;
        DesirePosition.x = _PlayerPositionBeforeTouch.x - _moveInput.x* _Speed / 500;
        if (DesirePosition.x > 3.5f) DesirePosition.x = 3.5f;
        if (DesirePosition.x < -3.5f) DesirePosition.x = -3.5f;
        transform.localPosition = DesirePosition;
    }
    void _MoveController()
    {
        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch _Touch = Input.GetTouch(i);
            switch (_Touch.phase)
            {
                case TouchPhase.Began:
                    _PlayerPositionBeforeTouch = transform.localPosition;
                    _moveTouchStartPosition = _Touch.position;
                    break;
                case TouchPhase.Ended:
                    _moveInput = Vector2.zero;
                    _moveTouchStartPosition = Vector2.zero;
                    _PlayerPositionBeforeTouch = transform.localPosition;
                    break;
                case TouchPhase.Canceled:
                case TouchPhase.Moved:
                    if (_LastPosition == _Touch.position) _TimeCounting += Time.deltaTime;
                    if (_TimeCounting>0.01f)
                    {
                        _PlayerPositionBeforeTouch = transform.localPosition;
                        _moveTouchStartPosition = _Touch.position;
                        _TimeCounting = 0;
                    }
                    _moveInput = _Touch.position - _moveTouchStartPosition;
                    _LastPosition = _Touch.position;
                    break;
            }
        }
    }
}
