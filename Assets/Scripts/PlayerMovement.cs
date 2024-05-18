using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 8f;
    [SerializeField] private float _jumpPower = 16f;

    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _groundLayer;

    [SerializeField] private SpriteRenderer _playerSpriteRenderer;
    [SerializeField] private Animator _playerAnimator;

    private float _horizontal;
    private bool _isFacingRight = true;

    // Update is called once per frame
    void Update()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");

        if(Input.GetButtonDown("Jump") && IsGrounded())
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _jumpPower);
        }

        if(Input.GetButtonUp("Jump") && _rb.velocity.y > 0f)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _rb.velocity.y * 0.5f);
        }

        PlayerAnimatorController();

        Flip();
    }

    private void FixedUpdate()
    {
        _rb.velocity = new Vector2(_horizontal * _speed, _rb.velocity.y);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(_groundCheck.position, 0.2f, _groundLayer);
    }

    private void Flip()
    {
        if (_horizontal < 0f)
        {
            _isFacingRight = false;

        }
        else if (_horizontal > 0f)
        {
            _isFacingRight = true;
        }

        if (_isFacingRight)
        {
            _playerSpriteRenderer.flipX = false;
        } else
        {
            _playerSpriteRenderer.flipX = true;
        }
    }

    private void PlayerAnimatorController()
    {
        if (!IsGrounded())
        {
            _playerAnimator.SetBool("Jump", true);
        } else
        {
            _playerAnimator.SetBool("Jump", false);
        }

        if (_horizontal != 0)
        {
            _playerAnimator.SetBool("Running", true);
        } else
        {
            _playerAnimator.SetBool("Running", false);
        }
    }
}
