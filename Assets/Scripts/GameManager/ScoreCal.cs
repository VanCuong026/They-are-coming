using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCal : MonoBehaviour
{
    [SerializeField]
    private Text _ScoreText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _ScoreText.text = ZeroPointMove.instance._NumberOfPlayerisAlive.ToString();
    }
}
