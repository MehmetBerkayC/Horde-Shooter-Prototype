using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    Animator _animator;
    PlayerController _playerController;
    SpriteRenderer _spriteRenderer;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _playerController = GetComponent<PlayerController>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if(_playerController.GetPlayerInputs().x != 0 || _playerController.GetPlayerInputs().y != 0)
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
        if(_playerController.GetPlayerInputs().x < 0)
        {
            _spriteRenderer.flipX = true;
        }   
        else
        {
            _spriteRenderer.flipX = false;
        }
    }
}
