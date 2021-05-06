using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] int health;
    [SerializeField] int score;
    // Start is called before the first frame update
    
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Debug.Log(hit.transform.gameObject.layer);
        if (hit.transform.gameObject.layer == 9)
        {
            hit.transform.gameObject.GetComponent<IHitable>().OnHit(1000);
            score += hit.transform.gameObject.GetComponent<IPoolValuesInitializer>().GetScore();
            health -= hit.transform.gameObject.GetComponent<IPoolValuesInitializer>().GetAttackDamage();
        }
    }
    public void SetHealth(int _damage)
    {
        health -= _damage;
    }
    public float GetHealth()
    {
        if(health<0)
        {
            health = 0;
        }
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
