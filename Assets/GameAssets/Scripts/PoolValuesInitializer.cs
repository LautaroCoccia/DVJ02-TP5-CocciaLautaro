using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PoolValuesInitializer : MonoBehaviour,IHitable, IPoolValuesInitializer
{
    [SerializeField] float health;
    [SerializeField] int score;
    [SerializeField] int attackDamage;
    public void SetHealth(float _health)
    {
        health = _health;
    }
    public float GetHealth()
    {
        return health;
    }
    public void SetScore(int _score)
    {
        score = _score;
    }
    public int GetScore()
    {
        return score;
    }
    public void SetAttackDamage(int _attackDamage)
    {
        attackDamage = _attackDamage;
    }
    public int GetAttackDamage()
    {
        return attackDamage;
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

