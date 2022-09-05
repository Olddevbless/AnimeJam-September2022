using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kick : MonoBehaviour
{
    GameObject player;
    GameManager gameManager;
    private void Awake()
    {
        Debug.Log(GameObject.FindObjectOfType<GameManager>().enabled);
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("collision" + other.gameObject.name);
        Debug.Log(other.gameObject.name);
        if (other.CompareTag("Enemy"))
        {
            gameManager.IncreaseScore(other.gameObject.GetComponent<Enemy>().scoreValue);
            Destroy(other.gameObject);
        }

    }


}


