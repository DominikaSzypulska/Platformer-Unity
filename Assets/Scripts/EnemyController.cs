using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{ 
    [Range( 0.01f, 20.0f )] [SerializeField] private float moveSpeed = 0.1f; 
    
    
    private bool isFacingRight=false;
    private Animator animator;
    public float moveRange = 1.0f;
    private bool isMovingRight = true;
    
    private float startPositionX;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isMovingRight == true)
        {
            if (this.transform.position.x <= startPositionX + moveRange)
            {
                
                MoveRight();
                
            }
            else
            {
              
                MoveLeft();
                
                
            }
            
        }
        else
        {
            if (this.transform.position.x >= startPositionX - moveRange)
            {
                MoveLeft();
                
            }
            else
            {
                
                MoveRight();
                Flip();
            }
        }
    }

    void MoveRight()
    {
        transform.Translate( moveSpeed * Time.deltaTime, 0.0f, 0.0f, Space.World );
        isMovingRight = true;
        if (isFacingRight == false)  
        {
            Flip(); 
        }
    }

    void MoveLeft()
    {
        
        transform.Translate(-moveSpeed * Time.deltaTime, 0.0f, 0.0f, Space.World);
        isMovingRight = false;
        if (isFacingRight == true)
        {
            Flip();
        }
    }
    void Awake()
    {
        startPositionX = this.transform.position.x;
             animator = GetComponent<Animator>();
    }

    private IEnumerator KillOnAnimationEnd()
    {
        yield return new WaitForSeconds(0.1f);
        gameObject.SetActive(false);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") == true)
        {
            if (other.gameObject.transform.position.y > transform.position.y)
            {
                animator.SetBool("isDead",true);
                StartCoroutine(KillOnAnimationEnd());
            }
        }
    }
    
    
    private void Flip()
    {
         
        isFacingRight = !isFacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x = -theScale.x;
        transform.localScale = theScale;
        
    }
}
