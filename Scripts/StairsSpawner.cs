using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsSpawner : MonoBehaviour
{
    [SerializeField] private PlayerMover _player;
    [SerializeField] private GameObject _prefab;

    private Transform _lastStepPosition;
    private int _offset;

    private void OnEnable()
    {
        _player.StepTaken += CreateNextStep;
        _lastStepPosition = _prefab.transform;
        _offset = 1;
    }

    private void OnDisable()
    {
        _player.StepTaken -= CreateNextStep;
    }

    private void CreateNextStep()
    {
        GameObject tempStep;

        tempStep = Instantiate(_prefab, transform);
        tempStep.transform.position = new Vector3(0, _lastStepPosition.position.y + _offset, _lastStepPosition.position.z + _offset);
        _lastStepPosition = tempStep.transform;
        Invoke(nameof(DestroyOldStep), .5f);
    }

    private void DestroyOldStep()
    {
        Destroy(transform.GetChild(0).gameObject);
    }
}
