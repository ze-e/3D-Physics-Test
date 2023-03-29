using UnityEngine;
using Settings;

public class Player : MonoBehaviour
{
    #region var
    private Rigidbody rb;

    //jump
    public float jumpForce = 5f;
    public float moveSpeed = 10f;

    //state
    public bool grounded = true;
    public bool isFalling = false;

    public delegate void EventHandler();
    public event EventHandler DeathEvent;

    //death
    public float DeathCoord = -50;
    public GameObject DeathFX;
    Renderer renderer;

    #endregion var

    #region lifecycle methods
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        renderer = GetComponent<Renderer>();
        DeathEvent += DeathHandler;
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
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) && isFalling)
        {
            rb.AddForce(Vector3.left * moveSpeed * Time.deltaTime, ForceMode.Acceleration); 
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) && isFalling)
        {
            rb.AddForce(Vector3.right * moveSpeed * Time.deltaTime, ForceMode.Acceleration); 
        }

        if (transform.position.y < DeathCoord && Manager.Instance.GetProgressByKey(GameProgress.GameOver) == false) DeathEvent();

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

    #endregion lifecycle methods

    #region controller

    void Jump(float force)
    {
        rb.AddForce(Vector3.up * force, ForceMode.Impulse);
    }

    public void Boost(float boostForce)
    {
        // Apply a vertical force to the player
        rb.AddForce(Vector3.up * boostForce, ForceMode.Impulse);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = true;
        }
    }

    #endregion controller

    #region lifescyle

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

    #endregion lifecycle
}
