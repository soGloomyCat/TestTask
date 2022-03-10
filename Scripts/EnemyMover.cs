using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] private float _forwardImpulse;
    [SerializeField] private float _lifetime;

    private Rigidbody _rigidbody;
    private bool _isAvailableNextBounce;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        Jump();
    }

    private void Update()
    {
        if (_lifetime <= 0)
            Destroy(gameObject);

        _lifetime -= Time.deltaTime;
    }

    private void Jump()
    {
        _rigidbody.isKinematic = false;
        _rigidbody.AddForce(new Vector3(0, _forwardImpulse, -0.9f), ForceMode.Impulse);
    }

    private void OnCollisionStay(Collision collision)
    {
        _rigidbody.isKinematic = true;
        Invoke(nameof(Jump), 0.5f);
    }
}
