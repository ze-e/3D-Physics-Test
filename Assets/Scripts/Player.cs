using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    //jump
    public float jumpForce = 4f;
    private Rigidbody rb;
    public int maxJumps = 2;
    private int jumps = 0;
    private bool grounded = true;

    //death
    public GameObject DeathFX;
    Renderer renderer;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        renderer = GetComponent<Renderer>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (grounded)
            {
                Jump(jumpForce);
                grounded = false;
            }
            else if (jumps < maxJumps)
            {
                Jump((jumpForce /2));
                jumps++;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Death"))
        {
            StartCoroutine(RestartGame());
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = true;
            jumps = 0;
        }
    }

    void Jump(float force)
    {
        rb.AddForce(Vector3.up * force, ForceMode.Impulse);
    }

      IEnumerator RestartGame()
    {
        RunDeathFX();
        // Wait for 3 seconds
        yield return new WaitForSeconds(3f);
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
