using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private bool isGrounded;
    private int jumpCount;

    // Variable to keep track of collected "PickUp" objects.
    private int count;

    // Movement along X and Y axes.
    private float movementX;
    private float movementY;

    // Speed at which the player moves.
    public float speed = 0;

    // Jump force
    public float jumpForce = 2.0f;

    // UI text component to display count of "PickUp" objects collected.
    public TextMeshProUGUI countText;

    // UI object to display winning text.
    public GameObject winTextObject;
    public GameObject buttonObject;
    public GameObject button1Object;
    public GameObject button2Object;
    public GameObject button3Object;

    private bool isMenuVisible = false;

    // Start is called before the first frame update.
    void Start()
    {
        // Get and store the Rigidbody component attached to the player.
        rb = GetComponent<Rigidbody>();
        isGrounded = true; 
        jumpCount = 0;
        // Initialize count to zero.
        count = 0;

        // Update the count display.
        SetCountText();
        winTextObject.SetActive(false);
        buttonObject.SetActive(false);
        button1Object.SetActive(false);
        button2Object.SetActive(false);
        button3Object.SetActive(false);
    }

    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            ShowMenu();
        }
    }

    // This function is called when a move input is detected.
    void OnMove(InputValue movementValue)
    {
        // Convert the input value into a Vector2 for movement.
        Vector2 movementVector = movementValue.Get<Vector2>();

        // Store the X and Y components of the movement.
        movementX = movementVector.x;
        movementY = movementVector.y; // Invert the Y direction.
    }

    // This function is called when a jump input is detected.
    void OnJump(InputValue jumpValue)
    {
        // Check if the jump input is pressed and the player is grounded.
        if (jumpValue.isPressed && isGrounded)
        {
            // Apply an upward force to make the player jump.
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false; 
            jumpCount++; 
        }
    }

    // This function is called when the player collides with something.
    void OnCollisionEnter(Collision collision)
    {
        // Check if the player has collided with the ground.
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            jumpCount = 0; 
        }
    }

    // FixedUpdate is called once per fixed frame-rate frame.
   void FixedUpdate()
    {
        // Create a 3D movement vector using the X and Y inputs.
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        // Convert the movement vector to the world space based on the camera's rotation.
        movement = Camera.main.transform.TransformDirection(movement);

        // Apply force to the Rigidbody to move the player.
        rb.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the object the player collided with has the "PickUp" tag.
        if (other.gameObject.CompareTag("Pickup"))
        {
            // Deactivate the collided object (making it disappear).
            other.gameObject.SetActive(false);
            // Increment the count of "PickUp" objects collected.
            count = count + 1;

            // Update the count display.
            SetCountText();
        }
    }

    // Function to update the displayed count of "PickUp" objects collected.
    void SetCountText()
    {
        // Update the count text with the current count.
        countText.text = "Count: " + count.ToString() + "/8";

        // Check if the count has reached or exceeded the win condition.
        if (count >= 8)
        {
            // Display the win text.
            winTextObject.SetActive(true);
        }
    }

    void ShowMenu()
    {
        isMenuVisible = !isMenuVisible;

        buttonObject.SetActive(isMenuVisible);
        button1Object.SetActive(isMenuVisible);
        button2Object.SetActive(isMenuVisible);
        button3Object.SetActive(isMenuVisible);
    }
}
