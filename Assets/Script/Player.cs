using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    float horizontalAxis;
    float verticalAxis;
    Rigidbody rb;
    [SerializeField] public StaminaWheel staminaWheel;
    [SerializeField] public GameManager gameManager;
    [SerializeField] private GameObject ball;
    [SerializeField] private GameObject Camera;
    [SerializeField] public float speed;
    [SerializeField] public float maxSpeed;
    [SerializeField] float rotationSpeed;
    [SerializeField] public float playerNumber;
    [SerializeField] private float baseSpeed;
    [SerializeField] public float speedDeceleration = 3;

    [SerializeField] private float maxStamina = 100f;
    [SerializeField] private float maxFOV= 60f;
    [SerializeField] private float minFOV= 60f;
    [SerializeField] public float speedAcceleration = 1f;
    [SerializeField] public enum PlayerType{ player1, player2};

    private float stamina;
    public PlayerType myType;
    [SerializeField] public bool isTouchingBall = true;
    float kickForce = 1000f;
    private Rigidbody ballRigidbody;
    public float detectionRadius = 100f;
    private bool ballDetected;

    public void Awake()
    {
        ballRigidbody = ball.GetComponent<Rigidbody>();
        //Debug.Log(ballRigidbody);
        if (ballRigidbody != null)
        {
            // Ball Rigidbody
            ballRigidbody.AddForce(Vector3.up * 500f);
        }
        else
        {
            Debug.LogWarning("Le Rigidbody de la balle n'a pas été trouvé.");
        }
        rb = GetComponent<Rigidbody>();
        if(playerNumber == 1)
        {
            myType = PlayerType.player1;
        }else if(playerNumber == 2)
        {
            myType = PlayerType.player2;
        }
        stamina = maxStamina;
        baseSpeed = speed;
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        horizontalAxis = Input.GetAxis("Horizontal");
        verticalAxis = Input.GetAxis("Vertical");
        Debug.Log(ballRigidbody);
        Move();
        Rotate();
        useStamina();

        bool isTouchingBall = Physics.CheckSphere(transform.position, detectionRadius, LayerMask.GetMask("Ball"));

        // Mettez à jour touchingBall en conséquence
        isTouchingBall = ballDetected;
    }

    private void Move()
    {
        if (myType == PlayerType.player1 && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)))
            rb.AddForce(transform.forward * verticalAxis * speed * Time.deltaTime);
        if (myType == PlayerType.player2 && (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow)))
            rb.AddForce(transform.forward * verticalAxis * speed * Time.deltaTime);
        // Vérifiez la touche spécifique pour le coup de pied
        //Debug.Log(ballRigidbody);
        if ((myType == PlayerType.player1 && Input.GetKeyDown(KeyCode.E)) ||
            (myType == PlayerType.player2 && Input.GetKeyDown(KeyCode.Return)))
        {
            if (isTouchingBall)
            {
                ballRigidbody.AddForce(transform.forward * kickForce);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            isTouchingBall = true;
            ballRigidbody = collision.gameObject.GetComponent<Rigidbody>();
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            isTouchingBall = false;
            ballRigidbody = null;
        }
    }

    private void Rotate()
    {
        if (myType == PlayerType.player1 && (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)))
            transform.Rotate(transform.eulerAngles, horizontalAxis * rotationSpeed * Time.deltaTime);
        if (myType == PlayerType.player2 && (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)))
            transform.Rotate(transform.eulerAngles, horizontalAxis * rotationSpeed * Time.deltaTime);
    }

    private void useStamina()
    {
        // Player 1
        if (myType == PlayerType.player1)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                if (stamina > 0)
                {
                    speed += speedAcceleration * Time.deltaTime;
                    stamina -= 30 * Time.deltaTime;
                    // Limitez la vitesse à maxSpeed
                    speed = Mathf.Clamp(speed, 0f, maxSpeed);
                    if(Camera.GetComponent<Camera>().fieldOfView >= minFOV)
                    {
                        Camera.GetComponent<Camera>().fieldOfView -= 0.25f;
                    }
                }
            }
            else
            {
                // Décrémentez la vitesse lorsque la touche n'est pas enfoncée
                speed = Mathf.MoveTowards(speed, baseSpeed, speedDeceleration * Time.deltaTime);

                if (stamina < maxStamina)
                {
                    stamina += 30 * Time.deltaTime;
                }

                if (Camera.GetComponent<Camera>().fieldOfView <= maxFOV)
                {
                    Camera.GetComponent<Camera>().fieldOfView += 0.25f;
                }
            }
        }

        // Player 2
        else if (myType == PlayerType.player2)
        {
            if (Input.GetKey(KeyCode.Keypad0))
            {
                if (stamina > 0)
                {
                    speed += speedAcceleration * Time.deltaTime;
                    stamina -= 30 * Time.deltaTime;
                    // Limitez la vitesse à maxSpeed
                    speed = Mathf.Clamp(speed, 0f, maxSpeed);
                }
            }
            else
            {
                if(speed >= maxSpeed)
                    speed = maxSpeed;
                // Décrémentez la vitesse lorsque la touche n'est pas enfoncée
                speed = Mathf.MoveTowards(speed, baseSpeed, speedDeceleration * Time.deltaTime);

                if (stamina < maxStamina)
                {
                    stamina += 30 * Time.deltaTime;
                }
            }
        }

        staminaWheel.SetRedWheelFillAmount(stamina / maxStamina + 0.07f);
        staminaWheel.SetGreenWheelFillAmount(stamina / maxStamina);
        Debug.Log(stamina);
    }
}

