using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
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
        rb = GetComponent<Rigidbody>();
        count = 0;

        rb = GetComponent<Rigidbody>();
        lives = 3;

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
        countText.text = "Count: " + count.ToString();
        if (count >= 28)
        {
            winTextObject.SetActive(true);
        }

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
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;

            SetCountText();
        }

        else if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.SetActive(false);
            count = count - 1;
            SetCountText();
        }

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