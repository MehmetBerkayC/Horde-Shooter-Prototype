using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] Transform _target;

    private void Start()
    {
        
    }

    void Update()
    {
        // transform.position = _target.position + _offset;

        transform.position = new Vector3(Mathf.Clamp(_target.position.x, -4.5f, 4.5f), Mathf.Clamp(_target.position.y, -10.5f, 10.5f) ,transform.position.z);
    }
}
