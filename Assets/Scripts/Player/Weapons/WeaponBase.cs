using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{

    [SerializeField] protected float timeBetweenActivations = 1;

    protected virtual void Start()
    {
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

}
