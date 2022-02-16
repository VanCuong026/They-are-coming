using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZeroPointMove : MonoBehaviour
{
    public static ZeroPointMove instance;
    public string _MoveDirection = "Straight",_Direction = "+Z";
    float _PlayerSpeed = 5f, _RotationSpeed = 100, _RotateAngle = 0;
    Vector3 RotatePoint;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(_MoveDirection == "Straight")
        {
            _GoStraight();
        }
        else if(_MoveDirection == "TurnLeft")
        {
            _TurnLeft();
        }
        else if (_MoveDirection == "TurnRight")
        {
            _TurnRight();
        }
        else if (_MoveDirection == "Stop")
        {
            PlayerMove.instance._Stop = true;
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (_MoveDirection == "Straight")
        {
            if (other.tag == "TurnLeft")
            {
                _MoveDirection = "TurnLeft";
                _FindRotatePoint();
                _RotateAngle = 90;
            }

            else if (other.tag == "TurnRight")
            {
                _MoveDirection = "TurnRight";
                _FindRotatePoint();
                _RotateAngle = 90;
            }
        }
        if (other.tag == "Stop")
        {
            _MoveDirection = "Stop";
        }
    }
    
    void _GoStraight()
    {
        Vector3 temp = transform.position;
        if(_Direction == "+Z")
            temp.z += Time.deltaTime * _PlayerSpeed;
        if (_Direction == "-Z")
            temp.z -= Time.deltaTime * _PlayerSpeed;
        if (_Direction == "+X")
            temp.x += Time.deltaTime * _PlayerSpeed;
        if (_Direction == "-X")
            temp.x -= Time.deltaTime * _PlayerSpeed;
        transform.position = temp;
    }
    void _TurnLeft()
    {
        if(_RotateAngle> _RotationSpeed * 0.02f)
        {
            transform.RotateAround(RotatePoint, Vector3.up, +_RotationSpeed * 0.02f);
            _RotateAngle -= _RotationSpeed * 0.02f;
        }
        else
        {
            transform.RotateAround(RotatePoint, Vector3.up, -_RotateAngle);
            _FindTheDirection();
        }
    }

    void _TurnRight()
    {
        if (_RotateAngle > _RotationSpeed * 0.02f)
        {
            transform.RotateAround(RotatePoint, Vector3.up, -_RotationSpeed * 0.02f);
            _RotateAngle -= _RotationSpeed * 0.02f;
        }
        else
        {
            transform.RotateAround(RotatePoint, Vector3.up, _RotateAngle);
            _FindTheDirection();
        }
    }

    void _FindRotatePoint()
    {
        if(_Direction == "+Z")
        {
            if (_MoveDirection == "TurnLeft")
            {
                RotatePoint.x = transform.position.x + 3.75f;
                RotatePoint.z = transform.position.z;
            }
            else if (_MoveDirection == "TurnRight")
            {
                RotatePoint.x = transform.position.x - 3.75f;
                RotatePoint.z = transform.position.z;
            }
        }
        else if (_Direction == "-Z")
        {
            if (_MoveDirection == "TurnLeft")
            {
                RotatePoint.x = transform.position.x - 3.75f;
                RotatePoint.z = transform.position.z;
            }
            else if (_MoveDirection == "TurnRight")
            {
                RotatePoint.x = transform.position.x + 3.75f;
                RotatePoint.z = transform.position.z;
            }
        }
        else if (_Direction == "+X")
        {
            if (_MoveDirection == "TurnLeft")
            {
                RotatePoint.x = transform.position.x ;
                RotatePoint.z = transform.position.z - 3.75f;
            }
            else if (_MoveDirection == "TurnRight")
            {
                RotatePoint.x = transform.position.x ;
                RotatePoint.z = transform.position.z + 3.75f;
            }
        }
        else if (_Direction == "-X")
        {
            if (_MoveDirection == "TurnLeft")
            {
                RotatePoint.x = transform.position.x ;
                RotatePoint.z = transform.position.z + 3.75f;
            }
            else if (_MoveDirection == "TurnRight")
            {
                RotatePoint.x = transform.position.x ;
                RotatePoint.z = transform.position.z - 3.75f;
            }
        }
    }

    void _FindTheDirection()
    {
        if(_MoveDirection == "TurnLeft")
        {
            if (_Direction == "+Z")
                _Direction = "+X";
            else if (_Direction == "-Z")
                _Direction = "-X";
            else if (_Direction == "+X")
                _Direction = "-Z";
            else if (_Direction == "-X")
                _Direction = "+Z";
        }
        else if (_MoveDirection == "TurnRight")
        {
            if (_Direction == "+Z")
                _Direction = "-X";
            else if (_Direction == "-Z")
                _Direction = "+X";
            else if (_Direction == "+X")
                _Direction = "+Z";
            else if (_Direction == "-X")
                _Direction = "-Z";
        }
        _MoveDirection = "Straight";
    }
}
