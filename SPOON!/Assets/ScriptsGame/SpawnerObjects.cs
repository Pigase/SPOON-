using System.Collections;
using UnityEngine;

public class SpawnerObjects : MonoBehaviour
{
    [SerializeField] private int _poolCount = 12;
    [SerializeField] private float _pauseTime = 5f;
    [SerializeField] private bool _autoExpande = true;
    [SerializeField] private Ingredient _prefab;
    [SerializeField] private GameObject _right;

    private float _barrier;
    private PoolMono<Ingredient> _pool;
    private bool _isSpawningPaused = false;
    private Coroutine _spawnCoroutine;

    private void Start()
    {
        _pool = new PoolMono<Ingredient>(_prefab, _poolCount, transform);
        _pool.autoExpand = _autoExpande;
        _barrier = _right.transform.position.x;

        StartSpawning();
    }

    private void OnEnable()
    {
        GameManipulator.SecondWave += StopSpawn;
    }

    private void OnDisable()
    {
        GameManipulator.SecondWave -= StopSpawn;
    }

    private void StartSpawning()
    {
        if (_spawnCoroutine != null)
            StopCoroutine(_spawnCoroutine);
        _spawnCoroutine = StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            if (!_isSpawningPaused)
            {
                float randomRange = Random.Range(-_barrier + 1f, _barrier + 1f);
                var ingredient = _pool.GetFreeElement();
                ingredient.transform.position = new Vector3(
                    transform.position.x + randomRange,
                    transform.position.y,
                    0
                );
                ingredient.transform.parent = null;
            }

            float randomTimeSpawn = Random.Range(1.5f, 2f);
            yield return new WaitForSeconds(randomTimeSpawn);
        }
    }

    private void StopSpawn()
    {
        StartCoroutine(PauseSpawning(_pauseTime));
    }

    private IEnumerator PauseSpawning(float pauseTime)
    {
        _isSpawningPaused = true;
        yield return new WaitForSeconds(pauseTime);
        _isSpawningPaused = false;
    }
}