using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform _pool;
    [SerializeField] private EnemyMover _enemy;
    [SerializeField] private float _maxCooldown;
    [SerializeField] private int _leftmostPosition;
    [SerializeField] private int _rightmostPosition;

    private Coroutine _coroutine;

    private void Start()
    {
        _coroutine = StartCoroutine(SpawnEnemy());
    }

    private void OnDisable()
    {
        StopCoroutine(_coroutine);
    }

    private IEnumerator SpawnEnemy()
    {
        Vector3 spawnPosition;

        while (true)
        {
            spawnPosition = new Vector3(Random.Range(_leftmostPosition, _rightmostPosition), transform.position.y, transform.position.z);
            Instantiate(_enemy, spawnPosition, Quaternion.identity, _pool);
            yield return new WaitForSeconds(Random.Range(1, _maxCooldown));
        }
    }
}
