using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class HitIndicatorFactory
{
    private readonly ObjectPool<GameObject> _indicatorsPool;
    private readonly List<Transform> _activeIndicators = new List<Transform>();

    private const string HIT_INDICATOR_TEMPLATE_PATH = "Prefabs/HitIndicator";

    public HitIndicatorFactory(int initialCapacity)
    {
        GameObject hitIndicatorTemplate = Resources.Load<GameObject>(HIT_INDICATOR_TEMPLATE_PATH);

        _indicatorsPool = new ObjectPool<GameObject>(
            () => Object.Instantiate(hitIndicatorTemplate),
            indicator => indicator.SetActive(true),
            indicator => indicator.SetActive(false),
            Object.Destroy,
            collectionCheck: true,
            defaultCapacity: initialCapacity);
    }

    public Transform GetIndicator()
    {
        Transform pooledObject = _indicatorsPool.Get().transform;
        _activeIndicators.Add(pooledObject);
        return pooledObject;
    }

    public void ReleaseIndicator(GameObject indicator)
    {
        _indicatorsPool.Release(indicator);
    }

    public void ReleaseAllIndicators()
    {
        foreach (Transform indicator in _activeIndicators)
        {
            ReleaseIndicator(indicator.gameObject);
        }
        
        _activeIndicators.Clear();
    }
}
