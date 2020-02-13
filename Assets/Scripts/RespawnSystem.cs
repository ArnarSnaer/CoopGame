using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnSystem : MonoBehaviour
{
    public GameObject RedPlayer;
    public GameObject BluePlayer;
    public AudioSource PlayerDeath;
    public AudioSource PlayerRespawn;
    private Vector3 position1;
    private Vector3 position2;

    // Start is called before the first frame update
    void Start()
    {
        position1 = RedPlayer.gameObject.transform.position;
        position2 = BluePlayer.gameObject.transform.position;
    }

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
        PlayerDeath.Play();
        yield return new WaitForSeconds(3);
        RedPlayer.transform.position = position1;
        PlayerRespawn.Play();
    }

    IEnumerator RespawnPlayer2()
    {
        PlayerDeath.Play();
        yield return new WaitForSeconds(3);
        BluePlayer.transform.position = position2;
        PlayerRespawn.Play();
    }
}
