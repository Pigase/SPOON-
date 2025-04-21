using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerObjects : MonoBehaviour
{
    [SerializeField] private int _poolCount = 12;
    [SerializeField] private bool _autoExpande = true;
    [SerializeField] private Ingredient _prefab;
    [SerializeField] private GameObject _right;

    private float _barrier;

    private PoolMono<Ingredient> _pool;
    private void Start()
    {
        _pool = new PoolMono<Ingredient>(_prefab, _poolCount, transform);
        _pool.autoExpand = _autoExpande;

        _barrier = _right.transform.position.x;

        CreateIngredient();
    }
    private void CreateIngredient()
    {
        float randomRange = Random.Range(-_barrier+1f, _barrier+1f);
        float randomTimeSpawn = Random.Range(1.5f, 2f);
        var ingredient = _pool.GetFreeElement();
        ingredient.transform.position = transform.position;
        ingredient.transform.parent = null;
        ingredient.transform.position = new Vector3(transform.position.x + randomRange, transform.position.y, 0);
        Invoke("CreateIngredient", randomTimeSpawn);
    }
}
