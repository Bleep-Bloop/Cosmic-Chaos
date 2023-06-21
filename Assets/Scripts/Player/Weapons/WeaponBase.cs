using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{

    [Header("Weapon Properties")]
    [SerializeField] protected float timeBetweenActivations = 1;
    [SerializeField] protected float minimumAllowedTimeBetweenActivations;

    // Upgrades
    [SerializeField] protected float damage;
    [SerializeField] protected float range;
    [SerializeField] protected Vector3 size; // Transform local scale
    [SerializeField] protected float speed; // Speed of movement in weapon (seperate from activation time).


    protected virtual void Start()
    {
        size = transform.localScale;
        InvokeRepeating("Attack", timeBetweenActivations, timeBetweenActivations);
    }

    protected void ChangeTimeBetweenActivations(float newTimeBetweenActivations)
    {
        CancelInvoke();
        InvokeRepeating("Attack", newTimeBetweenActivations, newTimeBetweenActivations);
    }

    /// <summary>
    /// Uses the weapon.
    /// Invoked every timeBetweenAttacks.
    /// </summary>
    protected abstract void Attack();

    public void Upgrade_Damage(float increaseAmount)
    {
        damage += increaseAmount;
    }

    // Decreases ActivationTime by given amount
    public void Upgrade_ActivationTime(float decreaseAmount)
    {
        float newTimeBetweenActivations = timeBetweenActivations - decreaseAmount;
        if(newTimeBetweenActivations <= minimumAllowedTimeBetweenActivations)
        {
            newTimeBetweenActivations = minimumAllowedTimeBetweenActivations;
        }

        ChangeTimeBetweenActivations(newTimeBetweenActivations);
    }

    public void Upgrade_Range(float increaseAmount)
    {
        range += increaseAmount;
    }

    /// <summary>
    /// Increases the weapon's transform.localScale.
    /// </summary>
    /// <param name="scaleMultipler">Multiplier applied to weapon's transform.LocalScale.</param>
    public void Upgrade_Size(float scaleMultipler)
    {
        size *= scaleMultipler;
    }

    public void Upgrade_Speed(float increaseAmount)
    {
        speed += increaseAmount;
    }

}
