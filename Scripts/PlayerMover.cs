using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _forwardImpulse;
    [SerializeField] private float _lateralImpulse;

    private Rigidbody _rigidbody;
    private Vector3 _startPosition;
    private bool _isFall;

    public bool IsAvailableNextClick { get; private set; }

    public event UnityAction StepTaken;
    public event UnityAction ScoreChanged;
    public event UnityAction IsDead;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.isKinematic = true;
        _startPosition = transform.position;
        _isFall = false;
        IsAvailableNextClick = true;
    }

    private void Update()
    {
        if (IsAvailableNextClick)
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
                Move(1);
        }

        if (transform.position.x > 3.5f && !_isFall || transform.position.x < -3.5f && !_isFall)
        {
            FreezeWatcher();
            Invoke(nameof(ActivateEvent), 1f);
        }

    }

    public void Move(int caseIndex)
    {
        IsAvailableNextClick = false;
        _rigidbody.isKinematic = false;

        switch (caseIndex)
        {
            case 1:
                _rigidbody.AddForce(new Vector3(0, _forwardImpulse, 0.9f), ForceMode.Impulse);
                StepTaken?.Invoke();
                break;
            case 2:
                _rigidbody.AddForce(new Vector3(-_lateralImpulse, 3, 0), ForceMode.Impulse);
                break;
            case 3:
                _rigidbody.AddForce(new Vector3(_lateralImpulse, 3, 0), ForceMode.Impulse);
                break;
        }
    }

    private void CorrectPosition()
    {
        transform.position = new Vector3(transform.position.x, _startPosition.y + 1, _startPosition.z + 1);
        _startPosition = transform.position;
    }

    private void FreezeWatcher()
    {
        _isFall = true;
        transform.GetChild(0).gameObject.transform.SetParent(null);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.TryGetComponent<EnemyMover>(out EnemyMover enemyMover))
        {
            ActivateEvent();
            FreezeWatcher();
            Destroy(gameObject);
        }

        _rigidbody.isKinematic = true;
        IsAvailableNextClick = true;

        if (transform.position.z != _startPosition.z)
        {
            CorrectPosition();
            ScoreChanged?.Invoke();
        }
    }

    private void ActivateEvent()
    {
        IsDead?.Invoke();
    }
}
