using UnityEngine;

public abstract class CharacterHealth : MonoBehaviour
{
    private int _health;
    private int _maxHealth;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateHealth()
    {
        if (_health == 0) Die();
    }

    protected abstract void Die();
}
