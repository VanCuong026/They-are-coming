using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [HideInInspector]public bool IsPlaying=false;
    [HideInInspector]public bool IsGameOver=false;
    [HideInInspector]public bool IsWin=false;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
