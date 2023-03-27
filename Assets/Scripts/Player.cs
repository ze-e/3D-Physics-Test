using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private Rigidbody rb;

    //jump
    public float jumpForce = 5f;
    public float moveSpeed = 10f;

    //state
    public float YSpeed;
    public bool grounded = true;
    public bool isFalling = false;
    //death
    public float DeathCoord = -50;
    public GameObject DeathFX;
    Renderer renderer;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        renderer = GetComponent<Renderer>();
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

        if (transform.position.y < DeathCoord && Manager.GetInstance().GetProgressByKey(GameProgress.GameOver) == false) StartCoroutine(RestartGame());

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
    IEnumerator RestartGame()
    {
        var _manager = Manager.GetInstance();
        _manager.SetProgressByKey(GameProgress.GameOver, true);
        RunDeathFX();
        // Wait for 3 seconds
        yield return new WaitForSeconds(3f);
        _manager.SetProgressByKey(GameProgress.GameOver, false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }

    void DisablePlayer() {
        renderer.enabled = false;
        rb.isKinematic = true;
    }

    void RunDeathFX()
    {
        DisablePlayer();
        Instantiate(DeathFX, new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), Quaternion.identity);
    }
}
