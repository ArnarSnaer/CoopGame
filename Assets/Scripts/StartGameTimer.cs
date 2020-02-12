using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class StartGameTimer : MonoBehaviour
{
	public float currentTime = 0f;
	public float menuTimer = 10f;
	public float gameTimer = 120f;
	public TextMeshProUGUI timerText;
	public bool winState;
	public bool inGame = false;
	private static StartGameTimer instance = null;
	public static StartGameTimer Instance
	{
		get { return instance; }
	}
	// Start is called before the first frame update
	void Start()
	{
		if (SceneManager.GetActiveScene().name == "MainMenu")
		{
			currentTime = menuTimer;
		}
		else
		{
			currentTime = gameTimer;
		}
	}
	// Update is called once per frame
	void Update()
	{
		if (currentTime <= 0 && SceneManager.GetActiveScene().name == "MainMenu")
		{
			SceneManager.LoadScene("Level 0");
			currentTime = gameTimer;
			inGame = true;
		}
		if (SceneManager.GetActiveScene().name == "MainMenu" && inGame == false)
		{
			currentTime -= 1 * Time.deltaTime;
			timerText.text = "Game Starts In: " + Mathf.CeilToInt(currentTime).ToString();
		}
		if (inGame && SceneManager.GetActiveScene().name != "Main Menu" && SceneManager.GetActiveScene().name != "EndScreen")
		{
			timerText = GameObject.Find("Canvas").GetComponentInChildren<TextMeshProUGUI>();
			currentTime -= 1 * Time.deltaTime;
			timerText.text = Mathf.CeilToInt(currentTime).ToString();
		}
		if (currentTime <= 0 && SceneManager.GetActiveScene().name != "MainMenu" && SceneManager.GetActiveScene().name != "EndScreen")
		{
			SceneManager.LoadScene("EndScreen");
		}
	}
	void Awake()
	{
		if (instance != null && instance != this)
		{
			Destroy(this.gameObject);
			return;
		}
		else
		{
			instance = this;
		}
		DontDestroyOnLoad(this.gameObject);
	}
}
