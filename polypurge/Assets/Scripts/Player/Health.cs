using UnityEngine;

public abstract class Health : MonoBehaviour
{
    [SerializeField] protected float maxhitpoints = 100f;
    [SerializeField] protected float hitpoints;
    
    void Awake()
    {
        //start the scene with your max hitpoints
        hitpoints = maxhitpoints;    
    }

    public virtual void TakeDamage(float Damage)
    {
        //take the damage from the hitpoints
        hitpoints -= Damage;
    }

    public virtual void HealDamage(float heal)
    {
        // checking if the heal will exceed the max hitpoints
        if (hitpoints + heal > maxhitpoints)
        {
            //if it does, set the hitpoints to the max hitpoints
            hitpoints = maxhitpoints;
            return;
        }
        //if it doesn't, add the heal to the hitpoints
        hitpoints += heal;
        
    }
}


