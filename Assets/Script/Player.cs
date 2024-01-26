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
    [SerializeField] float speed;
    [SerializeField] float rotationSpeed;
    [SerializeField] float playerNumber;
    [SerializeField] enum PlayerType{ player1, player2};
    PlayerType myType;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        if(playerNumber == 1)
        {
            myType = PlayerType.player1;
        }else if(playerNumber == 2)
        {
            myType = PlayerType.player2;
        }

        /*if (transform.rotation.y == 0)
            transform.rotation = 0.01;*/
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        horizontalAxis = Input.GetAxis("Horizontal");
        verticalAxis = Input.GetAxis("Vertical");
        Move();
        Rotate();
    }

    private void Move()
    {
        if (myType == PlayerType.player1 && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)))
            rb.AddForce(transform.forward * verticalAxis * speed * Time.deltaTime);
        if (myType == PlayerType.player2 && (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow)))
            rb.AddForce(transform.forward * verticalAxis * speed * Time.deltaTime);
    }

    private void Rotate()
    {
        if (myType == PlayerType.player1 && (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)))
            transform.Rotate(transform.up, horizontalAxis * rotationSpeed * Time.deltaTime);
        if (myType == PlayerType.player2 && (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)))
            transform.Rotate(transform.eulerAngles, horizontalAxis * rotationSpeed * Time.deltaTime);
    }
}

