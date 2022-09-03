using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    GameObject player;
    public int scoreValue;
    public int enemySpeed;
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, enemySpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerMovement>().kickSprite|| collision.gameObject.GetComponent<PlayerMovement>().punchSprite)
        {
            GameManager.FindObjectOfType<GameManager>().IncreaseScore(scoreValue);
            player.GetComponent<PlayerMovement>().powerupCharge++;
            Destroy(this);
        }
        player.GetComponent<PlayerMovement>().TakeDamage(1);
        GameManager.FindObjectOfType<GameManager>().IncreaseScore(scoreValue);
        Destroy(this);
    }
}
