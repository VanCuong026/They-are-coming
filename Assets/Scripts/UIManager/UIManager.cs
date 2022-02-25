using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text _ScoreText;
    [SerializeField] private GameObject GameOverPanel;
    [SerializeField] private GameObject WinPanel;
    [SerializeField] private GameObject HoldAndMove;
    [SerializeField] private GameObject RestartCornorButton;
    [SerializeField] private GameObject NextLevelButton;
    [SerializeField] private GameObject RestartButton;
    [SerializeField] private Animator _anim;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.IsPlaying)
        {
            if (ZeroPointMove.instance._NumberOfPlayerisAlive == 0) 
            {
                GameOverPanel.SetActive(true);
                RestartButton.SetActive(true);
            } 
        }
        if (GameManager.instance.IsWin)
        {
            WinPanel.SetActive(true);
            NextLevelButton.SetActive(true);
            RestartButton.SetActive(true);
        }
        _ScoreText.text = ZeroPointMove.instance._NumberOfPlayerisAlive.ToString();
        if (Input.GetMouseButtonDown(0)&& GameManager.instance.IsPlaying==false)
        {
            GameManager.instance.IsPlaying = true;
            if (GameObject.Find("HoldAndMove") != null) GameObject.Find("HoldAndMove").gameObject.SetActive(false);
        }
    }
    public void ResetButton()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    public void NextlevelButton()
    {
        if (gameObject.scene.name == "Level2") SceneManager.LoadScene("Level1");
        if (gameObject.scene.name == "Level1") SceneManager.LoadScene("Level2");
    }
}
