using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    
    [Space]
    [Header("Move Properties")]
    [SerializeField] private float speed;
    [SerializeField] private float airMultiplayer;
    [SerializeField] private float jumpHeight;
    [SerializeField] float fallMultiplier = 2.5f;
    [SerializeField] float lowJumpMultiplier = 2f;


    [Header("Collision")] [SerializeField] private Vector2 bottomOffset;
    [SerializeField] private Vector2 collisionSize;
    [SerializeField] private LayerMask groundLayer;
    private bool _onGround;


    private void Update()
    {
        // Ground Check
        _onGround = Physics2D.OverlapBox((Vector2)transform.position + bottomOffset, collisionSize, 360, groundLayer);

        Move();
        BetterJump();
    }

    private void Move()
    {
        Vector2 input = InputManager.singleton.getMoveDir();

        float y = rb.velocity.y;//_onGround ? -2f : 0;
        if (InputManager.singleton.getSpace() && _onGround)
        {
            y = jumpHeight;
            Debug.Log("Jumped");
        }

        rb.velocity = new Vector2(input.x * speed * (_onGround ? 1 : airMultiplayer), y);
    }

    void BetterJump()
    {
        if(rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }else if(rb.velocity.y > 0 && !InputManager.singleton.getSpace())
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube((Vector2)transform.position + bottomOffset, collisionSize);
    }
}
