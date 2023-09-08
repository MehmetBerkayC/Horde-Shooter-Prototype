using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    Animator _animator;
    PlayerMovement _playerMovement;
    SpriteRenderer _spriteRenderer;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _playerMovement = GetComponent<PlayerMovement>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if(_playerMovement.GetPlayerInputs().x != 0 || _playerMovement.GetPlayerInputs().y != 0)
        {
            _animator.SetBool("Move", true);

            SpriteDirectionChecker();
        }
        else
        {
            _animator.SetBool("Move", false);
        }
    }

    public void Flip(float value)
    {
        if (value < 0f)
        {
            _spriteRenderer.flipX = true;
        }
        else
        {
            _spriteRenderer.flipX = false;
        }
    }

    void SpriteDirectionChecker()
    {
        if(_playerMovement.GetPlayerInputs().x < 0)
        {
            _spriteRenderer.flipX = true;
        }   
        else
        {
            _spriteRenderer.flipX = false;
        }
    }
}
