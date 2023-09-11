using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class SwordController : MonoBehaviour
{
    [SerializeField] float _stabSpeed = 10f;
    [SerializeField] float _returnSpeed = 10f;

    private Vector3 _initialPosition;
    [SerializeField] bool _isStabbing = false;

    [SerializeField] SwordController sword;

    Transform _enemy;

    private void Start()
    {
        _initialPosition = transform.localPosition;
        _enemy = FindObjectOfType<EnemyAiMovement>().transform;
    }

    private void Update()
    {

        if (Input.GetButtonDown("Fire2")) // Replace "Fire2" with your input for the sword attack
        {
            PerformSwordAttack();
        }

        if (_isStabbing)
        {
            Stab();
        }
        else
        {
            ReturnToInitialPosition();
        }
    }

    public void Stab()
    {
        
        Vector3 targetPosition = _enemy.position;
        Vector3 direction = targetPosition - transform.position;
        direction.Normalize();
 
        transform.Translate(direction * _stabSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            _isStabbing = false;
        }
    }
    private void PerformSwordAttack()
    {
        sword.PerformStab();
    }
    public void PerformStab()
    {
        _isStabbing = true;
    }

    private void ReturnToInitialPosition()
    {
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, _initialPosition, _returnSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.localPosition, _initialPosition) < 0.01f)
        {
            _isStabbing = false;
        }
    }
}
