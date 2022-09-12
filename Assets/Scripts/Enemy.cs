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
    [SerializeField] Animator bullyAnimator;
    [SerializeField] float yDistanceMax;
    [SerializeField] float yDisatnceMin;
    [SerializeField] float playerBullyYDistance;
    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        player = FindObjectOfType<PlayerMovement>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        playerBullyYDistance = Mathf.Abs(player.transform.position.y) + Mathf.Abs(transform.position.y);
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

        if (player.transform.position.x< transform.position.x )
        {
            bullyAnimator.SetInteger("BullyRotation", 4);
        }
        if (player.transform.position.x > transform.position.x)
        {
            bullyAnimator.SetInteger("BullyRotation", 2);
        }
        if (player.transform.position.y < transform.position.y)
        {
            bullyAnimator.SetInteger("BullyRotation", 3);
        }
        if (player.transform.position.y > transform.position.y)
        {
            bullyAnimator.SetInteger("BullyRotation", 1);
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
