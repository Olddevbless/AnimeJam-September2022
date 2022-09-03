﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerMovement : MonoBehaviour
{

    [SerializeField] int speedNormal = 1;
    [SerializeField] int speedPowerUp = 5; 
    [SerializeField] int currentSpeed;
    [SerializeField] Vector2 mousePos;
    [SerializeField] float yOffset; // top/bottom level boundaries
    [SerializeField] float xOffset; // left/right level boundaries
    GameObject enemies;
    public RigidbodyConstraints2D enemiesRBConstraints; // to freeze enemies
    public GameObject kickSprite; // heavyattack sprite
    public GameObject punchSprite; // lightattack sprite
    public int playerHealth; // determines how many hits the player can take, each enemy deals 1 damage
    public int powerupCharge; // how many kills the player needs to execute a powerup
    Slider powerUpSlider;
    [SerializeField] int playerMaxHealth;
    float heavyAttackCD = 2f;
    float heavyAttackCount;
    float lightAttackCD = 0.5f;
    float lightAttackCount;
    public Camera cam;
    Rigidbody2D rb;
    Transform aim;
    Vector2 movement;

    void Start()
    {
        powerUpSlider = FindObjectOfType<Slider>();
        currentSpeed = speedNormal;
        playerHealth = playerMaxHealth;
        rb = this.GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.None;
        aim = gameObject.GetComponentInChildren<Transform>();
        cam = Camera.main;
    }


    void Update()
    {
        powerUpSlider.value = powerupCharge;
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        if (powerupCharge >= 3 && Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine("PowerUp");
            powerupCharge = 0;
        }
        if (transform.position.y> yOffset)
        {
            transform.position = new Vector3 (transform.position.x,yOffset);
        }
        if (transform.position.y< -yOffset)
        {
            transform.position = new Vector3 (transform.position.x,-yOffset);
        }
        if (transform.position.x > xOffset)
        {
            transform.position = new Vector3(xOffset, transform.position.y) ;
        }
        if (transform.position.x < -xOffset)
        {
            transform.position = new Vector3(-xOffset, transform.position.y);
        }

    }
    private void FixedUpdate()
    {
        Movement();
        MouseAim();
        Attack();
    }

    void Movement()
    {
        rb.MovePosition(rb.position + movement * currentSpeed * Time.fixedDeltaTime);

    }

    void MouseAim()
    {

        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;

    }
    void Attack()
    {


        if (Input.GetAxisRaw("Fire2") > 0 && heavyAttackCount <= 0f)
        {
            StartCoroutine("HeavyAttackDuration");
            heavyAttackCount = heavyAttackCD;
        }

        if (heavyAttackCount > 0)
        {

            heavyAttackCount -= Time.deltaTime;
        }
        if (Input.GetAxisRaw("Fire1") > 0 && lightAttackCount <= 0f)
        {
            StartCoroutine("LightAttackDuration");
            lightAttackCount = lightAttackCD;
        }

        if (lightAttackCount > 0)
        {
            lightAttackCount -= Time.deltaTime;
        }
    }
    public void TakeDamage(int damage)
    {
        playerHealth -= damage;
    }
    void Death()
    {
        if (playerHealth <= 0)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }
    IEnumerator HeavyAttackDuration()
    {
        kickSprite.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        kickSprite.SetActive(false);
    }
    IEnumerator LightAttackDuration()
    {
        punchSprite.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        punchSprite.SetActive(false);
    }
    IEnumerator PowerUp()
    {
        enemiesRBConstraints = FindObjectOfType<Enemy>().GetComponent<RigidbodyConstraints2D>();
        enemiesRBConstraints = RigidbodyConstraints2D.FreezeAll;
        currentSpeed =speedPowerUp;
        enemies = FindObjectOfType<Enemy>().gameObject;
        yield return new WaitForSeconds(5f);
        enemiesRBConstraints = RigidbodyConstraints2D.None;
        currentSpeed = speedNormal;
        
        
    }
   
}
