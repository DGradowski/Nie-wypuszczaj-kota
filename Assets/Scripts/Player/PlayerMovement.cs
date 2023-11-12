using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{

    public float MoveSpeed;
    public Rigidbody2D rb;
    private Vector2 PlayerInput;
   
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        PlayerInput.x = Input.GetAxisRaw("Horizontal");
        PlayerInput.y = Input.GetAxisRaw("Vertical");

        PlayerInput.Normalize(); // Sprawia ze poruszanie sie po skosie nie jest szybsze jak poruszanie sie w inne kierunki

    }

    void FixedUpdate()
    {
        rb.velocity = PlayerInput * MoveSpeed;
    }
}
