using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public float jumpForce = 0;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public GameObject loseTextObject;

    private Rigidbody rb;
    private int count;
    private float movementX;
    private float movementY;
    private Vector3 scaleChange;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;

        SetCountText();
        winTextObject.SetActive(false);
        loseTextObject.SetActive(false);
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void OnJump()
    {
        rb.AddForce(0, jumpForce * speed, 0, ForceMode.Impulse);
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 12)
        {
            GetComponent<Renderer>().material.color = Color.green;
            winTextObject.SetActive(true);
            PauseGame();
        }
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        rb.AddForce(movement * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            GetComponent<Renderer>().material.color = Color.red;
            other.gameObject.SetActive(false);
            count = count + 1;
            scaleChange = new Vector3(0.05f, 0.05f, 0.05f);
            rb.transform.localScale -= scaleChange;

            SetCountText();
        }

        if (other.gameObject.CompareTag("MegaPickUp"))
        {
            GetComponent<Renderer>().material.color = Color.cyan;
            other.gameObject.SetActive(false);
            scaleChange = new Vector3(0.25f, 0.25f, 0.25f);
            rb.transform.localScale += scaleChange;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            GetComponent<Renderer>().material.color = Color.grey;
            loseTextObject.SetActive(true);
            PauseGame();
        }
    }

    void PauseGame()
    {
        Time.timeScale = 0;
    }

}
