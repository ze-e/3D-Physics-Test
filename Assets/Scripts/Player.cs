using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private Rigidbody rb;
    public Manager manager;

    //jump
    public float jumpForce = 5f;
    public float moveSpeed = 10f;

    //state
    public float YSpeed;
    public bool grounded = true;
    public bool isFalling = false;

    public delegate void EventHandler();
    public event EventHandler DeathEvent;

    //death
    public float DeathCoord = -50;
    public GameObject DeathFX;
    Renderer renderer;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        renderer = GetComponent<Renderer>();
        manager = Manager.GetInstance();
        DeathEvent += DeathHandler;
    }

    private void Update()
    {
        YSpeed = rb.velocity.y;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (grounded)
            {
                Jump(jumpForce);
                grounded = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) && isFalling)
        {
            rb.AddForce(Vector3.left * moveSpeed * Time.deltaTime, ForceMode.Acceleration); 
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) && isFalling)
        {
            rb.AddForce(Vector3.right * moveSpeed * Time.deltaTime, ForceMode.Acceleration); 
        }

        //if (transform.position.y < DeathCoord && Manager.GetInstance().GetProgressByKey(GameProgress.GameOver) == false) StartCoroutine(Death());
        if (transform.position.y < DeathCoord && Manager.GetInstance().GetProgressByKey(GameProgress.GameOver) == false) DeathEvent();

    }

    private void FixedUpdate()
    {
        if(rb.velocity.y < 0)
        {
            isFalling = true;
        }
        else
        {
            isFalling = false;
        }
    }


    void Jump(float force)
    {
        rb.AddForce(Vector3.up * force, ForceMode.Impulse);
    }

    /* platform */


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = true;
        }
    }

    /* boost */
    public void Boost(float boostForce)
    {
        // Apply a vertical force to the player
        rb.AddForce(Vector3.up * boostForce, ForceMode.Impulse);
    }


    /* death */

    void DeathHandler()
    {
        DisablePlayer();
        Instantiate(DeathFX, new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), Quaternion.identity);
    }

    void DisablePlayer()
    {
        renderer.enabled = false;
        rb.isKinematic = true;
    }
}
