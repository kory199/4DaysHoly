using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject[] scoreUI = null;
    [SerializeField] GameObject[] map = null;
    [SerializeField] GameObject StartGame = null;
    [SerializeField] GameObject[] startButtonObject = null;
    [SerializeField] GameObject[] optionButtonObject = null;
    [SerializeField] GameObject endGameUI = null;

    [SerializeField] TextMeshProUGUI curScoreText = null;
    [SerializeField] TextMeshProUGUI curScoreText1 = null;
    [SerializeField] TextMeshProUGUI highScoreText = null;
    [SerializeField] TextMeshProUGUI highScoreText1 = null;
    [SerializeField] TextMeshProUGUI inputMouse = null;

    public float Score = 0;
    private float bestScore = 0;
    private bool isStart = false;

    

    private void Awake()
    {
        Init();

        if (PlayerPrefs.HasKey("bestScore"))
        {
            bestScore = PlayerPrefs.GetFloat("bestScore");
            highScoreText.text = bestScore.ToString();
            highScoreText1 = highScoreText;
        }
        curScoreText.text = Score.ToString();
        curScoreText1 = curScoreText;

        PlayerPrefs.GetFloat("bestScore", bestScore);
        PlayerPrefs.Save();
    }

    void Update()
    {
        BlinkTextDestroy();
    }

    private void Init()
    {
        for (int i = 0; i < scoreUI.Length; i++)
        {
            scoreUI[i].SetActive(false);
        }
        for (int i = 0; i < map.Length; i++)
        {
            map[i].SetActive(false);
        }
        inputMouse.enabled = false;
        endGameUI.SetActive(false);
    }

    public void GameStart()
    {
        for (int i = 0; i < scoreUI.Length; i++)
        {
            scoreUI[i].SetActive(true);
        }

        for (int i = 0; i < map.Length; i++)
        {
            map[i].SetActive(true);
        }
        StartGame.SetActive(false);

        for (int i = 0; i < startButtonObject.Length; i++)
        {
            startButtonObject[i].SetActive(false);
        }

        for (int i = 0; i < optionButtonObject.Length; i++)
        {
            optionButtonObject[i].SetActive(false);
        }
        inputMouse.enabled = true;
        isStart = true;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void EndGame()
    {
#if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
#else
            Debug.Log("���� ����");
            Application.Quit();
#endif
    }

    public void ScoreAdd(float score)
    {
        Score += score;
        curScoreText.text = Score.ToString();

        if (Score >= bestScore)
        {
            PlayerPrefs.SetFloat("bestScore", Score);
        }
    }

    public void BlinkTextDestroy()
    {
        if (isStart == true && Input.GetMouseButtonDown(0))
        {
            inputMouse.enabled = false;
        }
    }

    public void RenderEndUI()
    {
        endGameUI.SetActive(true);
    }
}
