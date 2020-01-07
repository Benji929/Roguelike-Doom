using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float moveSpeed;
    private float moveVector_x;
    private float moveVector_y;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //movement inputs
        moveVector_x = Input.GetAxis("Horizontal");
        moveVector_y = Input.GetAxis("Vertical");
        
    }

    private void FixedUpdate()
    {
        //movement
        rb.velocity = new Vector2(moveVector_x, moveVector_y) * moveSpeed * Time.deltaTime;
    }
}
