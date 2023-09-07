using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed = 5f;

    [HideInInspector]
    public Vector2 playerInputs;
    [HideInInspector]
    public float lastHorizontalVector;
    [HideInInspector]
    public float lastVerticalVector;

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        InputManagement();
        
    }

    private void FixedUpdate()
    {
        Movement();
    }

    void InputManagement()
    {
        playerInputs = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if(playerInputs.x != 0 )
        {
            lastHorizontalVector = playerInputs.x;
        }

        if(playerInputs.y != 0)
        {
            lastVerticalVector = playerInputs.y;
        }
    }

    void Movement()
    {
        
        rb.velocity = new Vector2(playerInputs.x * speed, playerInputs.y * speed);

    }
}
