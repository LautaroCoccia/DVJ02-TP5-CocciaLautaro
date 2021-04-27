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
    public void OnHit(float damage)
    {
        Debug.Log("Health: "+ health);
        health -= damage;
        Debug.Log("Health: " + health);

        if (health <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
