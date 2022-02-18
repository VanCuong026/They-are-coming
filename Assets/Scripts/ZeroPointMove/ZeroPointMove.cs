using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZeroPointMove : MonoBehaviour
{
    public static ZeroPointMove instance;
    public string _MoveDirection = "Straight",_Direction = "+Z";
    float _PlayerSpeed = 5f, _RotationSpeed = 100, _RotateAngle = 0;
    Vector3 RotatePoint;
    public int _WeaponID = 0;
    GameObject _MovePoint;
    public int _NumberOfPlayerisAlive;
    private int _PlayerCounting,_PositionCounting;
    Vector3[] _DefPos=new Vector3[100];
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        _PositionCal();
    }

    // Update is called once per frame
    void Update()
    {
        _TheNumberOfPlayerisAlive();
        Debug.Log(_NumberOfPlayerisAlive);
        if (_NumberOfPlayerisAlive > 0&& _MoveDirection != "Stop") MoveFollowLine();
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
            _MovetoDefPos();
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

    void MoveFollowLine()
    {
        if (_MoveDirection == "Straight")
        {
            _GoStraight();
        }
        else if (_MoveDirection == "TurnLeft")
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

    public void _TheNumberOfPlayerisAlive()
    {
        _MovePoint = GameObject.Find("MovePoint");
        _PlayerCounting = 0;
        for (int i = 0; i < _MovePoint.transform.childCount; i++) //Xét từng Player trong MovePoint.
        {
            if (_MovePoint.transform.GetChild(i).gameObject.activeSelf) // Nếu có bất kỳ Player nào đang Active thì ++
            {
                _PlayerCounting++;
            }
        }
        _NumberOfPlayerisAlive = _PlayerCounting;
    }

    void _PositionCal()
    {
        _PositionCounting = 0;
        for (int i = 0; i < 11; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                _DefPos[i*9+j] = new Vector3(75f + i * 0.8f, -0.05f, 56.8f + j * 0.8f);
                _PositionCounting++;
            }
        }
    }

    void _MovetoDefPos()
    {
        _TheNumberOfPlayerisAlive();
        int i = 0;
        for (int j = 0; j < _MovePoint.transform.childCount; j++)
        {
            if (_MovePoint.transform.GetChild(j).gameObject.activeSelf)
            {// Nếu có bất kỳ Player nào đang Active thì ++
                _MovePoint.transform.GetChild(j).gameObject.GetComponent<PlayerController>().gameObject.GetComponent<CapsuleCollider>().isTrigger = true;
                _MovePoint.transform.GetChild(j).gameObject.GetComponent<PlayerController>()._DefencePos = _DefPos[i];
                i++;
                continue;
            }
        }
    }
}
