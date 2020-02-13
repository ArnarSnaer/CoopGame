using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnSystem : MonoBehaviour
{
    public GameObject RedPlayer;
    public GameObject BluePlayer;
    public Vector3 position1;
    public Vector3 position2;

    // Start is called before the first frame update

    void OnTriggerEnter2D(Collider2D badItem)
    {
        if ((badItem.tag == "RedPlayer"))
        {
            StartCoroutine(RespawnPlayer1());
        }
        if ((badItem.tag == "BluePlayer"))
        {
            StartCoroutine(RespawnPlayer2());
        }
    }

    IEnumerator RespawnPlayer1()
    {
        RedPlayer.gameObject.SetActive(false);
        yield return new WaitForSeconds(3);
        RedPlayer.transform.position = position1;
        RedPlayer.gameObject.SetActive(true);
    }

    IEnumerator RespawnPlayer2()
    {
        BluePlayer.gameObject.SetActive(false);
        yield return new WaitForSeconds(3);
        BluePlayer.transform.position = position2;
        BluePlayer.gameObject.SetActive(true);
    }
}
