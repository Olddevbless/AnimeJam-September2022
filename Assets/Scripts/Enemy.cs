using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    GameObject player;
    public int scoreValue;
    public int enemySpeed;
    GameManager gameManager;
    [SerializeField] float yOffset;
    [SerializeField] float xOffset;
    public bool playerIsPoweredUp;
    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        player = FindObjectOfType<PlayerMovement>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerIsPoweredUp)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, enemySpeed * Time.deltaTime);
        }
        if (transform.position.y > yOffset)
        {
            transform.position = new Vector3(transform.position.x, yOffset);
        }
        if (transform.position.y < -yOffset)
        {
            transform.position = new Vector3(transform.position.x, -yOffset);
        }
        if (transform.position.x > xOffset)
        {
            transform.position = new Vector3(xOffset, transform.position.y);
        }
        if (transform.position.x < -xOffset)
        {
            transform.position = new Vector3(-xOffset, transform.position.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.collider.gameObject.name);
        if (collision.gameObject.CompareTag("Player"))
        {
            player.GetComponent<PlayerMovement>().TakeDamage(1);
            GameManager.FindObjectOfType<GameManager>().IncreaseScore(scoreValue);
            Destroy(this);

        }


    }
}
