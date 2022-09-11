using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    // Variables and Items are referenced here
    public float speed = 0;
    public TextMeshProUGUI countText;
    public TextMeshProUGUI livesText;
    public GameObject LoseTextObject;
    public GameObject winTextObject;
    private Rigidbody rb;
    private int count;
    private int lives;
    private float movementX;
    private float movementY;

    // Start is called before the first frame update
    void Start()
    {
        // Initial Point Count Value
        rb = GetComponent<Rigidbody>();
        count = 0;

        // Initial Lives Value
        rb = GetComponent<Rigidbody>();
        lives = 3;

        // Win and Lose text are set to false, not meant to appear yet
        SetCountText();
        winTextObject.SetActive(false);

        SetCountText();
        LoseTextObject.SetActive(false);

    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void SetCountText()
    {
        // If player gets all 28 points, win text appears
        countText.text = "Count: " + count.ToString();
        if (count >= 28)
        {
            winTextObject.SetActive(true);
        }

        // If lives hit 0, Lose text appears and Player Object is destoryed
        livesText.text = "Lives: " + lives.ToString();
        if (lives == 0)
        {
            LoseTextObject.SetActive(true);
            Destroy(gameObject);
        }
    }
    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        rb.AddForce(movement * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        // When collecting pickup item, count value goes up
        if (other.gameObject.CompareTag("Pickup"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;

            SetCountText();
        }

        // when colliding with enemy, lives counted down
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.SetActive(false);
            lives = lives - 1;

            SetCountText();
        }

        // Teleportation to new levels
        if (count == 12)
        {
            transform.position = new Vector3(50f, 0.5f, 50f);
        }

        if (count == 20)
        {
            transform.position = new Vector3(100f, 0.5f, 100f);
        }
    }
}
