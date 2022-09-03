using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    [SerializeField] int speed = 1;
    [SerializeField] Vector2 mousePos;
    [SerializeField] GameObject kickSprite;
    [SerializeField] GameObject punchSprite;
    public int playerHealth;
    [SerializeField] int playerMaxHealth;
    float heavyAttackCD = 2f;
    float heavyAttackCount;
    float lightAttackCD = 0.5f;
    float lightAttackCount;
    public Camera cam;
    Rigidbody2D rb;
    Transform aim;
    Vector2 movement;
    // Start is called before the first frame update
    void Start()
    {
        playerHealth = playerMaxHealth;
        rb = this.GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.None;
        aim = gameObject.GetComponentInChildren<Transform>();
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }
    private void FixedUpdate()
    {
        Movement();
        MouseAim();
        Attack();
    }

    void Movement()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);

    }

    void MouseAim()
    {
       
        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg -90f ;
        rb.rotation = angle;
        
    }
    void Attack()
    {
        
        
        if (Input.GetAxisRaw("Fire2")>0 && heavyAttackCount<=0f)
        {
            StartCoroutine("HeavyAttackDuration");
            heavyAttackCount = heavyAttackCD;
        }
        //else
        //{
        //    kickSprite.SetActive(false);
        //}
        if (heavyAttackCount>0 )
        {
            
            heavyAttackCount -= Time.deltaTime;
        }
        if (Input.GetAxisRaw("Fire1") > 0 && lightAttackCount <= 0f)
        {
            StartCoroutine("LightAttackDuration");
            lightAttackCount = lightAttackCD;
        }
        //else
        //{
        //    punchSprite.SetActive(false);
        //}
        if (lightAttackCount>0)
        {
            lightAttackCount -= Time.deltaTime;
        }
    }
    void TakeDamage(int damage)
    {
        playerHealth -= damage;
    }
    void Death()
    {
        if (playerHealth <=0)
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
}
