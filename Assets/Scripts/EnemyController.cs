using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float wallCheckDistance = 1.2f;
    [SerializeField]
    private float attackRange = 10;
    [SerializeField]
    private LayerMask whatIsWall;
    [SerializeField]
    private LayerMask whatIsPlayer;
    [SerializeField]
    private float damage = 10;
    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private float timeShoot = 2f;
    [SerializeField]
    private float health = 10;

    [SerializeField]
    private Transform checkWallUp, checkWallLeft, checkWallRight, checkWallDown;

    private Vector2 facingDirection;

    private bool isWallUp, isWallDown, isWallRight, isWallLeft, isDead, isAttack;

    private Animator anim;
    private Rigidbody2D rb;
    private float timeChange = 1f;
    private float m_timeChange;
    private float m_timeShoot;
    private float timeRandomShoot;
    private float m_timeRandomShoot;
    private BulletController bulletController;
    private AudioSource audioSoure;

    

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        bulletController = bullet.GetComponent<BulletController>();
        audioSoure = GetComponent<AudioSource>();
        m_timeChange = timeChange;
        
        m_timeShoot = 0;

        switch (Random.Range(1, 5))
        {
            case 1:
                facingDirection.Set(0, 1);
                timeRandomShoot = 1;
                break;
            case 2:
                facingDirection.Set(-1, 0);
                timeRandomShoot = 2;
                break;
            case 3:
                facingDirection.Set(0, -1);
                timeRandomShoot = 3;
                break;
            case 4:
                facingDirection.Set(1, 0);
                timeRandomShoot = 4;
                break;
        }

        m_timeRandomShoot = timeRandomShoot;

    }

    private void Update()
    {
        Move();
        Animation();
        Attack();
        Die();
    }

    private void FixedUpdate()
    {
        CheckWall();
        ChangeDirection();
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    private void PlaySound()
    {
        audioSoure.PlayOneShot(audioSoure.clip);
    }

    //-----MOVE STATE-----
    private void Move()
    {
        if (isDead)
        {
            rb.velocity = Vector2.zero;
            return;
        }
        rb.velocity = new Vector2(facingDirection.x * moveSpeed, facingDirection.y * moveSpeed);
        
    }

    private void ChangeDirection()
    {
        if (isWallUp && isWallLeft && isWallRight)
        {
            facingDirection.Set(0, -1);  
        }else if(isWallUp && isWallRight && isWallDown)
        {
            facingDirection.Set(-1, 0);
        }else if(isWallRight && isWallDown&& isWallLeft)
        {
            facingDirection.Set(0, 1);
        }else if(isWallDown && isWallLeft&& isWallUp)
        {
            facingDirection.Set(1, 0);
        }else if(isWallUp && isWallRight)
        {
            switch(Random.Range(1, 3))
            {
                case 1:
                    facingDirection.Set(-1, 0);
                    break;
                case 2:
                    facingDirection.Set(0, -1);
                    break;
            }
        }else if(isWallRight && isWallDown)
        {
            switch (Random.Range(1, 3))
            {
                case 1:
                    facingDirection.Set(-1, 0);
                    break;
                case 2:
                    facingDirection.Set(0, 1);
                    break;
            }
        }
        else if (isWallLeft && isWallUp)
        {
            switch (Random.Range(1, 3))
            {
                case 1:
                    facingDirection.Set(1, 0);
                    break;
                case 2:
                    facingDirection.Set(0, -1);
                    break;
            }
        }else if (isWallUp && facingDirection == Vector2.up)
        {
            switch (Random.Range(1, 4))
            {
                case 1:
                    facingDirection.Set(1, 0);
                    break;
                case 2:
                    facingDirection.Set(-1, 0);
                    break;
                case 3:
                    facingDirection.Set(0, -1);
                    break;
            }
        }
        else if (isWallRight && facingDirection == Vector2.right)
        {
            switch (Random.Range(1, 4))
            {
                case 1:
                    facingDirection.Set(0, 1);
                    break;
                case 2:
                    facingDirection.Set(-1, 0);
                    break;
                case 3:
                    facingDirection.Set(0, -1);
                    break;
            }
        }
        else if (isWallDown && facingDirection == Vector2.down)
        {
            switch (Random.Range(1, 4))
            {
                case 1:
                    facingDirection.Set(1, 0);
                    break;
                case 2:
                    facingDirection.Set(-1, 0);
                    break;
                case 3:
                    facingDirection.Set(0, 1);
                    break;
            }
        }
        else if (isWallLeft && facingDirection == Vector2.left)
        {
            switch (Random.Range(1, 4))
            {
                case 1:
                    facingDirection.Set(1, 0);
                    break;
                case 2:
                    facingDirection.Set(0, 1);
                    break;
                case 3:
                    facingDirection.Set(0, -1);
                    break;
            }
        }
        else if(!isWallDown && !isWallUp && !isWallLeft && !isWallRight)
        {
            m_timeChange -= Time.fixedDeltaTime;
            if (m_timeChange <= 0)
            {
                m_timeChange = timeChange;
                switch (Random.Range(1, 5))
                    {
                        case 1:
                            facingDirection.Set(0, 1);
                            break;
                        case 2:
                            facingDirection.Set(-1, 0);
                            break;
                        case 3:
                            facingDirection.Set(0, -1);
                            break;
                        case 4:
                            facingDirection.Set(1, 0);
                            break;
                    }
            }
            
        }
    }

    private void Animation()
    {
        if (isDead) return;
        if(facingDirection == Vector2.up)
        {
            anim.SetBool("isUp", true);
            anim.SetBool("isDown", false);
            anim.SetBool("isLeft", false);
            anim.SetBool("isRight", false);
        }else if (facingDirection == Vector2.down)
        {
            anim.SetBool("isUp", false);
            anim.SetBool("isDown", true);
            anim.SetBool("isLeft", false);
            anim.SetBool("isRight", false);
        }else if (facingDirection == Vector2.left)
        {
            anim.SetBool("isUp", false);
            anim.SetBool("isDown", false);
            anim.SetBool("isLeft", true);
            anim.SetBool("isRight", false);
        }else if (facingDirection == Vector2.right)
        {
            anim.SetBool("isUp", false);
            anim.SetBool("isDown", false);
            anim.SetBool("isLeft", false);
            anim.SetBool("isRight", true);
        }
    }

    //------ATTACK STATE------------
    private void Attack()
    {
        if (isDead) return;
        isAttack = Physics2D.Raycast(checkWallUp.position, facingDirection, attackRange, whatIsPlayer);
        
        if (isAttack)
        {
            
            if(m_timeShoot <= 0)
            {
                m_timeShoot = timeShoot;
                bulletController.direction = new Vector3(facingDirection.x, facingDirection.y, 0);
                Instantiate(bullet, new Vector3(checkWallUp.position.x, checkWallUp.position.y, 0), Quaternion.identity);
            }
            else
            {
                m_timeShoot -= Time.deltaTime;
            }
        }
        else
        {
            m_timeRandomShoot -= Time.deltaTime;
            if (m_timeRandomShoot <= 0)
            {
                switch(Random.Range(1, 5))
                {
                    case 1:
                            timeRandomShoot = 1;
                            break;
                    case 2:
                            timeRandomShoot = 2;
                            break;
                    case 3:
                            timeRandomShoot = 3;
                            break;
                    case 4:
                            timeRandomShoot = 4;
                            break;
                }
                m_timeRandomShoot = timeRandomShoot;
                bulletController.direction = new Vector3(facingDirection.x, facingDirection.y, 0);
                Instantiate(bullet, new Vector3(checkWallUp.position.x, checkWallUp.position.y, 0), Quaternion.identity);
            }
        }
    }

    //------DIE STATE------------
    private void Die()
    {
        if (health <= 0)
        {
            isDead = true;
            anim.SetBool("isDestroy", true);
            anim.SetBool("isUp", false);
            anim.SetBool("isDown", false);
            anim.SetBool("isLeft", false);
            anim.SetBool("isRight", false);
        }
    }

    private void Damage(float damage)
    {
        health -= damage;
    }

    public void CheckWall()
    {
        isWallUp = Physics2D.Raycast(checkWallUp.position, Vector2.up, wallCheckDistance, whatIsWall);
        isWallDown = Physics2D.Raycast(checkWallDown.position, Vector2.down, wallCheckDistance, whatIsWall);
        isWallLeft = Physics2D.Raycast(checkWallLeft.position, Vector2.left, wallCheckDistance, whatIsWall);
        isWallRight = Physics2D.Raycast(checkWallRight.position, Vector2.right, wallCheckDistance, whatIsWall);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(checkWallUp.position, checkWallUp.position + (Vector3)(Vector2.up * wallCheckDistance));
        Gizmos.DrawLine(checkWallDown.position, checkWallDown.position + (Vector3)(Vector2.down * wallCheckDistance));
        Gizmos.DrawLine(checkWallLeft.position, checkWallLeft.position + (Vector3)(Vector2.left * wallCheckDistance));
        Gizmos.DrawLine(checkWallRight.position, checkWallRight.position + (Vector3)(Vector2.right * wallCheckDistance));
        Gizmos.DrawLine(checkWallUp.position, checkWallUp.position + (Vector3)(facingDirection * attackRange));

    }
}
