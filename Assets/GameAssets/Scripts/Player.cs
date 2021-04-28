using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float health = 100;
    public int score;
    public void SetHealth(float _damage)
    {
        health -= _damage;
    }
    public float GetHealth()
    {
        return health;
    }
    public void SetScore(int _score)
    {
        score += _score;
    }
    public int GetScore()
    {
        return score;
    }
}
