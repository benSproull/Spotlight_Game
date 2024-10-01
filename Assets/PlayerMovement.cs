using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovemnt : MonoBehaviour
{
    public float moveSpeed = 5f;

    public float dashDistance = 5f;
    public float dashCooldown = 1f;
    private float lastDashTime;
    private bool isDashing;

    private Vector2 movement; // stores movement direction
    private Rigidbody2D rb; // Players rigidbody

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();    
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement = movement.normalized;

        if(Input.GetKeyDown(KeyCode.Space) && Time.time >= lastDashTime + dashCooldown)
        {
            StartCoroutine(Dash());
        }
    }

    private IEnumerator Dash()
    {
        isDashing = true;
        lastDashTime = Time.time;

        Vector2 dashDirection = movement;
        rb.MovePosition(rb.position + dashDirection * dashDistance);

        yield return new WaitForSeconds(0.1f);

        isDashing = false;
    }

    void FixedUpdate()
    {
        if (!isDashing)
        {
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        }
        
    }

}
