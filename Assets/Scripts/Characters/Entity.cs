using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    [SerializeField] int health;

    public int Health 
    { 
        get => health; 
        set
        {
            if (value <= 0)
            {
                value = 0;
                Death();
            }

            health = value;
        }
    }

    public virtual void Death()
    {
        Destroy(gameObject);
    }
}
