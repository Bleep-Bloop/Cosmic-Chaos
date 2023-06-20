using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{

    [Header("Components")]
    private CharacterBase playerCharacter;
    [SerializeField] private List<WeaponBase> allAvailableWeapons = new List<WeaponBase>(); // All weapon's attached to CharacterBase.
    [SerializeField] private List<WeaponBase> allActiveWeapons = new List<WeaponBase>(); // Attached weapons that are active.

    // Start is called before the first frame update
    void Start()
    {
        playerCharacter = PlayerHealthManager.instance.GetComponent<CharacterBase>();

        // Get attached weapons
        allAvailableWeapons = GetAllAvailableWeapons();
        // Get active attached weapons
        allActiveWeapons = GetAllActiveWeapons();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Returns List<WeaponBase> of all weapons attached to playerCharacter.
    /// Note - Includes Inactive.
    /// </summary>
    /// <returns>List<WeaponBase> - All attached weapons including Inactive.</WeaponBase></returns>
    private List<WeaponBase> GetAllAvailableWeapons()
    {
        List<WeaponBase> availableWeapons = new List<WeaponBase>();
        playerCharacter.GetComponentsInChildren<WeaponBase>(true, availableWeapons);
        return availableWeapons;
    }

    /// <summary>
    /// Returns List<WeaponBase> of all active weapons attached to playerCharacter.
    /// Note - Requires AllAvailableWeapons. ToDo: Separate
    /// </summary>
    /// <returns></returns>
    private List<WeaponBase> GetAllActiveWeapons()
    {
        List<WeaponBase> activeWeapons = new List<WeaponBase>();

        List<WeaponBase> attachedWeapons = GetAllAvailableWeapons();
        foreach (WeaponBase weapon in attachedWeapons)
        {
            if (weapon.isActiveAndEnabled)
            {
                activeWeapons.Add(weapon);
            }
        }

        return activeWeapons;
    }

}
