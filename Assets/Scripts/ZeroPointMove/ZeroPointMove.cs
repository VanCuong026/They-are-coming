using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZeroPointMove : MonoBehaviour
{
    public static ZeroPointMove instance;
    [HideInInspector]public string _MoveDirection = "Straight",_Direction = "+Z";
    [HideInInspector]public int _NumberOfPlayerisAlive=1;
    float _PlayerSpeed = 5f, _RotationSpeed = 200f, _RotateAngle = 0;
    Vector3 RotatePoint;
    Transform _DefensePosition;
    [HideInInspector]public int _WeaponID = 0, _PlayerCounting, _PositionCounting;
    private GameObject _MovePoint;
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
        if (GameManager.instance.IsPlaying)
        {
            _TheNumberOfPlayerisAlive();
            if (_NumberOfPlayerisAlive > 0 && _MoveDirection != "Stop") MoveFollowLine();
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
            transform.RotateAround(RotatePoint, Vector3.up, _RotateAngle);
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
            transform.RotateAround(RotatePoint, Vector3.up, -_RotateAngle);
            _FindTheDirection();
        }
    }

    #region Find Rotation Point
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
    #endregion

    #region Find Direction
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
    #endregion

    #region Follow Line
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
    #endregion
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
    #region Calculate the Defense Position
    void _PositionCal() //Tính toán vị trí từng điểm để sắp xếp Player
    {
        _PositionCounting = 0;
        _DefensePosition = GameObject.Find("DefensePosition").transform; //Lấy vị trí của GameObject
        if (_DefensePosition.rotation.eulerAngles.y == 90) //Tùy vào hướng của map mà thay đổi cách đặt vị trí
        {
            for (int i = 0; i < 11; i++)
            {
                for (int j = 0; j < 9; j++)
                { //Tính toán vị trí đặt player. Mỗi player cách nhau 1 khoảng 0.8
                    _DefPos[i * 9 + j] = new Vector3(_DefensePosition.position.x + i * 0.8f, -0.05f, _DefensePosition.position.z + j * 0.8f);
                    _PositionCounting++;
                }
            }
        }else if(_DefensePosition.rotation.eulerAngles.y == 0)
        {
            for (int i = 0; i < 11; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    _DefPos[i * 9 + j] = new Vector3(_DefensePosition.position.x + j * 0.8f, -0.05f, _DefensePosition.position.z + i * 0.8f);
                    _PositionCounting++;
                }
            }
        }
    }
    #endregion
    void _MovetoDefPos()
    {
        _TheNumberOfPlayerisAlive(); //Lấy số lượng player đang sống
        int i = 0;
        for (int j = 0; j < _MovePoint.transform.childCount; j++) //Xét từng đối tượng con nằm trong MovePoint
        {
            if (_MovePoint.transform.GetChild(j).gameObject.activeSelf) //Nếu có đối tượng nào đang Active thì nghĩa là gameObject đang còn sống
            {// Nếu có bất kỳ Player nào đang Active thì 
                _MovePoint.transform.GetChild(j).gameObject.GetComponent<PlayerController>().gameObject.GetComponent<CapsuleCollider>().isTrigger = true; //Thay đổi Collider trong Player thành Is trigger để có thể di chuyển vào vị trí Defense Position
                _MovePoint.transform.GetChild(j).gameObject.GetComponent<PlayerController>()._DefencePos = _DefPos[i]; //Trong mỗi con Player đang Active thì lấy ra Script PlayerController rồi gán cho biến _DefencePos vị trí đã được tính toán từ trước
                i++;
                continue;
            }
        }
    }
}
