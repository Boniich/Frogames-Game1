using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController sharedInstance;
    public float jumpForce = 5.0f;
    public float runningSpeed = 3.0f;
    private Rigidbody2D rigidbody2D;
    public LayerMask groundLayerMask;
    public Animator animator;
    private Vector3 startPosition;

    void Awake()
    {
        animator.SetBool("isAlive", true);
        sharedInstance = this;
        rigidbody2D = GetComponent<Rigidbody2D>();
        startPosition = this.transform.position;
    }

    // Start is called before the first frame update
   public void StartGame()
    {

        animator.SetBool("isAlive", true);
        this.transform.position = startPosition;
        //reinicimos la velocidad del personaje para que cuando se reinicie, no atraviece el suelo
        rigidbody2D.velocity = new Vector2(0, 0);
        
    }

    // Update is called once per frame
    void Update()
    {

        if (GameManager.sharedInstance.currentGameState == GameState.inTheGame)
        {
            if (Input.GetMouseButtonDown(0) && IsOnTheFloor())
            {
                Debug.Log("Salta");
                Jump();
            }

            animator.SetBool("isGrounded", IsOnTheFloor());
        }


    }

    private void FixedUpdate()
    {

        if (GameManager.sharedInstance.currentGameState == GameState.inTheGame)
        {

            if (rigidbody2D.velocity.x < runningSpeed)
            {
                rigidbody2D.velocity = new Vector2(runningSpeed, rigidbody2D.velocity.y);
            }
        }

    }


    // ForceMode2D.Inpulse, tendra en cuenta la masa del objeto al momento de saltar
    void Jump()
    {
        rigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    bool IsOnTheFloor()
    {
        if(Physics2D.Raycast(this.transform.position, Vector2.down, 1.0f, groundLayerMask.value))
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    public void KillPlayer()
    {
        GameManager.sharedInstance.GameOver();
        animator.SetBool("isAlive", false);

        if (PlayerPrefs.GetFloat("highscore", 0) < this.GetDistance())
        {
            PlayerPrefs.SetFloat("highscore",this.GetDistance());
        }
    }

    public float GetDistance()
    {
        float distanceTravelled = Vector2.Distance(new Vector2(startPosition.x, 0), new Vector2(this.transform.position.x,0));

        return distanceTravelled;
    }
}
