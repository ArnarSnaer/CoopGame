using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public string nextLevel;
    void Update()
    {
        if (Input.GetAxisRaw("Cancel") > 0)
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

    public void ChangeLevel()
    {
        SceneManager.LoadScene(nextLevel);
    }
}
