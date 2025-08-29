using UnityEngine;


//abstract class cannot be instantiated, but can be inherited from.
[RequireComponent(typeof(SpriteRenderer), typeof(Animator))]
public abstract class Enemy : MonoBehaviour
{

    protected SpriteRenderer sr;
    protected Animator anim;
    protected int health;

    [SerializeField] private int maxHealth = 5;

    // Start is called once before the first execution of Update after the MonoBehaviour is created.

    protected virtual void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        if (maxHealth <= 0)
        {
            Debug.LogError("Max health must be greater than 0. Setting to default value of 3.");
            maxHealth = 3;
        }

        health = maxHealth;
    }

    public virtual void TakeDamage(int damageValue, DamageType damageType = DamageType.Default)
    {
        health -= damageValue;

        if (health <= 0)
        {
            anim.SetTrigger("death");

            // Destroy the enemy after the death animation is complete
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject, 0.5f); // Adjust the delay as needed for the animation
            }
            else
            {
                Destroy(gameObject, 0.5f); // If no parent, destroy this game object directly
            }
        }
    }
}

public enum DamageType
{
    Default,
}
