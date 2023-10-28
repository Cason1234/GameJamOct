using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform StartPos;
    public Transform EndPos;
    public GameObject body;

    Rigidbody2D rb2d;

    public float moveSpeed;

    int moveDirection;

    bool isDead;
    public Transform deathCheck;
    public LayerMask PlayerLayer;

    bool playerDead;
    public Transform playerCheck;
    bool stopChecking = false;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        moveDirection = 1;
        
    }

    // Update is called once per frame
    void Update()
    {  
        CheckStatus();
        
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        Flip();
        rb2d.velocity = new Vector2(moveDirection * moveSpeed, 0);
    }

    public void Flip()
    {
        if (transform.position.x >= EndPos.position.x)
        {
            moveDirection = -1;
        }
        if (transform.position.x <= StartPos.position.x)
        {
            moveDirection = 1;
        }
    }

    public int CheckStatus()
    {
        isDead = Physics2D.OverlapCircle(deathCheck.position, deathCheck.GetComponent<CircleCollider2D>().radius, PlayerLayer);
        playerDead = Physics2D.OverlapCircle(playerCheck.position, playerCheck.GetComponent<CircleCollider2D>().radius, PlayerLayer);
        if(isDead && !stopChecking){
            Destroy(body);
            stopChecking = true;
            return 0;
        }
        if(playerDead && !stopChecking){
            Debug.Log("GameOver");
            stopChecking = true;
            return 0;
        }
        return 0;
    }
}
