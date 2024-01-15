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
    [SerializeField] float rotationspeed;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        horizontalAxis = Input.GetAxis("Horizontal");
        verticalAxis = Input.GetAxis("Vertical");

        rb.AddForce(new Vector3(horizontalAxis * speed * Time.deltaTime, verticalAxis * rotationspeed * Time.deltaTime), 0);
    }
}
