using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformController : MonoBehaviour
{
    [Range(0.01f, 20.0f)] [SerializeField] private float moveSpeed = 0.1f;



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
                
            }
        }
    }

    void MoveRight()
    {
        transform.Translate(moveSpeed * Time.deltaTime, 0.0f, 0.0f, Space.World);
        isMovingRight = true;
       
    }

    void MoveLeft()
    {

        transform.Translate(-moveSpeed * Time.deltaTime, 0.0f, 0.0f, Space.World);
        isMovingRight = false;

    }

    void Awake()
    {
        startPositionX = this.transform.position.x;
        
    }
}
