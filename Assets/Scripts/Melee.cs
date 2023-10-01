using UnityEngine;

public class Melee : MonoBehaviour
{
    [SerializeField] float _stabSpeed = 20f;
    [SerializeField] float _returnSpeed = 10f;

    [SerializeField] bool _isStabbing = false;

    Transform _enemy;
    Vector3 _initialPosition;

    private void Start()
    {
        _initialPosition = transform.localPosition;
        _enemy = FindObjectOfType<Enemy>().transform;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire2")) // Replace "Fire2" with your input for the sword attack
        {
            Stab();
        }
    }

    public void Stab()
    {
        
        if(_isStabbing == false)
        {
            _isStabbing = true;

            Vector3 targetPosition = _enemy.position;
            Vector3 direction = targetPosition - transform.position;
            direction.Normalize();
 
            transform.Translate(direction * _stabSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                ReturnToInitialPosition();
            }
        }
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
