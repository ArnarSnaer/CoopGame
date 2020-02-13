using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class StartGameTimer : MonoBehaviour
{
    public float currentTime = 0f;
    public float menuTimer = 10f;
    public TextMeshProUGUI timerText;
    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            currentTime = menuTimer;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (currentTime <= 0 && SceneManager.GetActiveScene().name == "MainMenu")
        {
            SceneManager.LoadScene("Level 0");
        }
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            currentTime -= 1 * Time.deltaTime;
            timerText.text = "Game Starts In: " + Mathf.CeilToInt(currentTime).ToString();
        }
    }
}
