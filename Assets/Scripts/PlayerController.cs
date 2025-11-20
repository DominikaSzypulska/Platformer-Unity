using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour

{
    [Header("Movement parameters")] [Range(0.01f, 20.0f)] [SerializeField]
    private float moveSpeed = 0.1f;

    [Range(0.01f, 20.0f)] [SerializeField] private float jumpForce = 6.0f;
    [Space(10)] private Rigidbody2D rigidBody;
    private bool isFacingRight = true;
    private Animator animator;
    private bool isWalking = false;
    public LayerMask groundLayer;
    private float rayLength = 1.5f;
    private int score = 0;
    public int lives = 3;
    public int keyFound = 0;
    private static int keysNumber = 3;
    private Vector2 startPosition;
    public GameObject gameObject;
    [SerializeField] private AudioClip CoinSound;
    [SerializeField] private AudioClip DeadSound;
    [SerializeField] private AudioClip HpSound;
    [SerializeField] private AudioClip FightSound;
    [SerializeField] private AudioClip KeySound;
    
    public GameManager gameManager;
    private AudioSource source;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       
        isWalking = false;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
           
            
                if (gameManager.isInGame && gameManager.currentGameState == GameManager.GameState.GAME)
                {
                    gameManager.PauseMenu();
                }
                else if (!gameManager.isInGame && gameManager.currentGameState == GameManager.GameState.PAUSE_MENU)
                {
                    gameManager.InGame(); 
                }
            
        }

        if (gameManager.currentGameState == GameManager.GameState.GAME)
        {
            
            if (Input.GetKey(KeyCode.RightArrow) == true || Input.GetKey(KeyCode.D) == true)
            {
                transform.Translate(moveSpeed * Time.deltaTime, 0.0f, 0.0f, Space.World);
                isWalking = true;
                if (isFacingRight == false)
                {
                    Flip();
                }

            }
        

            if (Input.GetKey(KeyCode.LeftArrow) == true || Input.GetKey(KeyCode.A) == true)
            {
                transform.Translate(-moveSpeed * Time.deltaTime, 0.0f, 0.0f, Space.World);
                 isWalking = true;
                if (isFacingRight == true)
                {
                    Flip();
                }
            }

            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }

            //Debug.DrawRay(transform.position,rayLength*Vector3.down, Color.white,1,false);
            animator.SetBool("isGrounded", IsGrounded());
            animator.SetBool("isWalking", isWalking);
    }
}

bool IsGrounded()
    {
        return Physics2D.Raycast(this.transform.position, Vector2.down, rayLength, groundLayer.value);
    }
    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        startPosition = transform.position;
        gameManager = FindObjectOfType<GameManager>();
        source = GetComponent<AudioSource>();

    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x = -theScale.x;
        transform.localScale = theScale;
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bonus") == true)
        {
            gameManager.AddPoints(1);
            score += 1;
            Debug.Log("Score: " +score);
            other.gameObject.SetActive(false);
            source.PlayOneShot(CoinSound, AudioListener.volume);
        }

        if (other.CompareTag("Meta"))
        {
            if(keyFound==keysNumber)
            {  Debug.Log("You finished the game!!! You  found all keys");
                //gameObject.SetActive(true);
                score += 100 * lives;
                gameManager.LevelComleted();
            }
            else
                Debug.Log("You had to collect " +(keysNumber-keyFound)+" more keys to complet level");
        }

        if (other.CompareTag("FallLevel"))
        {
            Death();
        }
        if (other.CompareTag("Heal"))
        {
            source.PlayOneShot(HpSound, AudioListener.volume);
            lives += 1;
            gameManager.AddLives(1);
            other.gameObject.SetActive(false);
            Debug.Log("You collect heart! Now you have "+lives +" lives");
        }
        if (other.CompareTag("Key"))
        {
            gameManager.AddKeys(1);
            keyFound += 1;
            other.gameObject.SetActive(false);
            Debug.Log("Key is collected");
            source.PlayOneShot(KeySound, AudioListener.volume);
        }

        if (other.CompareTag("MovingPlatform"))
        {
            transform.SetParent(other.transform);
        }
        if (other.CompareTag("Enemy"))
        {   source.PlayOneShot(FightSound, AudioListener.volume);
            if(transform.position.y > other.gameObject.transform.position.y)
            {
                gameManager.killEnemies(1);
                Debug.Log("Killed an enemy");
                
            }
            else
            {
                lives -= 1;
                gameManager.OddLives(1);
                if (lives == 0)
                {
                    Debug.Log("You lost");
                    transform.position = startPosition;
                }

                else
                {
                    Debug.Log("You have "+lives+ " lives");
                    transform.position = startPosition;
                }
            }

            
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("MovingPlatform"))
        {
            transform.SetParent(null);
        }
    }
    void Death()
    {
        lives -= 1;
        if (lives == 0)
        {
            Debug.Log("You lost");
            transform.position = startPosition;
        }

        else
        {
            Debug.Log("You have "+lives+ " lives");
            transform.position = startPosition;
        }
        source.PlayOneShot(DeadSound, AudioListener.volume);
    }
    void Jump()
    {
        if (IsGrounded() == true)
        {
            rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            Debug.Log("Jumping");
        }
        
    }
}
