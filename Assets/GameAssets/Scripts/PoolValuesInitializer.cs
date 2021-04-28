using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolValuesInitializer : MonoBehaviour,IHitable, IPoolValuesInitializer
{
    [SerializeField] float health;
    public void SetHealth(float _health)
    {
        health = _health;
    }
    public float GetHealth()
    {
        return health;
    }
    public void OnHit(float _damage)
    {
        health -= _damage;
        if (health <= 0)
        {
            gameObject.SetActive(false);
            ObjectPooler.Get().OnDie(gameObject);
        }
    }
}
