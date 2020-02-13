using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EndScreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.Space))
        {
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
