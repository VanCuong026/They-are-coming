using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    int _WeaponCode = 0;
    int _PreviousWeapon;
    [SerializeField] private Animator _anim;
    [SerializeField] private GameObject _Player;
    // Start is called before the first frame update
    void Start()
    {
        //if (GameManager.instance.IsPlaying)
            WeaponSwitch();
        _anim.SetBool("BackWard", false);
    }
    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.IsPlaying)
        {
            _anim.SetBool("BackWard", true);
            _WeaponCode = ZeroPointMove.instance._WeaponID;
            if (_PreviousWeapon != _WeaponCode)
            {
                WeaponSwitch();
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                _WeaponCode++;
                if (_WeaponCode >= 5)
                    _WeaponCode = 0;
            }
        }else _anim.SetBool("BackWard", false);
    }

    void WeaponSwitch()
    {
        int i = 0;
        foreach(Transform weapon in transform)
        {
            if (i == _WeaponCode)
            {
                weapon.gameObject.SetActive(true);
                _PreviousWeapon = _WeaponCode;
                if (weapon.name == "TacticalKnife_Fade" || weapon.name == "Shuriken2")
                {
                    _anim.SetBool("Shuriken", true);
                    _Player.transform.localRotation = Quaternion.Euler(0, -30f, 0);
                }
                else
                {
                    _anim.SetBool("Shuriken", false);
                    _Player.transform.localRotation = Quaternion.Euler(0, 30f, 0);
                }
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
            i++;
        }
    }
}
