using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EndScreen : MonoBehaviour
{
	public TextMeshProUGUI timerText;
	GameObject timer;
	bool winState;
	// Start is called before the first frame update
	void Start()
	{
		timer = GameObject.Find("Timer");
		winState = timer.GetComponent<StartGameTimer>().winState;
		if (winState)
		{
			timerText.text = "You Won!";
		}
		else
		{
			timerText.text = "Better Luck Next Time!";
		}
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.Space))
		{
			winState = false;
			timer.GetComponent<StartGameTimer>().currentTime = timer.GetComponent<StartGameTimer>().gameTimer;
			SceneManager.LoadScene("Level 0");
		}
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			StartCoroutine(WaitThenQuit());
		}
	}
	IEnumerator WaitThenQuit()
	{
		// Quit Game After Timer to allow for Fade Out Effect
		yield return new WaitForSeconds(0.5f);
		Application.Quit();
	}
}
