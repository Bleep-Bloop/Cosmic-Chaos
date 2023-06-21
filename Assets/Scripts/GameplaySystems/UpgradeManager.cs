using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

enum WeaponUpgradeType
{
    UnlockWeapon,
    ActivationTime,
    Damage,
    Range,
    Size,
    Speed,
}

public class UpgradeManager : MonoBehaviour
{

    public static UpgradeManager instance;

    /// Components ///
    [Header("Components")]
    private CharacterBase playerCharacter;
    [SerializeField] private List<WeaponBase> allAvailableWeapons = new List<WeaponBase>(); // All weapon's attached to CharacterBase.
    private FloatingJoystick floatingJoystick;

    /// UI ///
    [Header("Upgrade UI")]
    [SerializeField] private Canvas UpgradeMenuCanvas;
    [Header("Buttons")]
    [SerializeField] private Button upgrade1Button;
    [SerializeField] private Button upgrade2Button;
    [SerializeField] private Button upgrade3Button;
    [Header("Text Boxes")]
    [SerializeField] private TextMeshProUGUI upgrade1TextBox;
    [SerializeField] private TextMeshProUGUI upgrade2TextBox;
    [SerializeField] private TextMeshProUGUI upgrade3TextBox;

    /// Upgrade Choices ///
    [Header("Upgrade Runtime")]
    [Header("Picked Weapons")]
    [SerializeField] private WeaponBase upgrade1Weapon;
    [SerializeField] private WeaponBase upgrade2Weapon;
    [SerializeField] private WeaponBase upgrade3Weapon;

    [SerializeField] private WeaponUpgradeType upgrade1Type;
    [SerializeField] private WeaponUpgradeType upgrade2Type;
    [SerializeField] private WeaponUpgradeType upgrade3Type;

    private float upgrade1Amount;
    private float upgrade2Amount;
    private float upgrade3Amount;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerCharacter = PlayerHealthManager.instance.GetComponent<CharacterBase>();
        floatingJoystick = GameObject.FindGameObjectWithTag("Joystick").GetComponent<FloatingJoystick>();

        // Get all weapons attached to playerCharacter.
        allAvailableWeapons = GetAllAvailableWeapons();

        // Bind UpgradeButtons
        upgrade1Button.onClick.AddListener(Upgrade1ButtonPressed);
        upgrade2Button.onClick.AddListener(Upgrade2ButtonPressed);
        upgrade3Button.onClick.AddListener(Upgrade3ButtonPressed);
    }

    /// <summary>
    /// Creates the three upgrade choices the player gets in the UpgradeManager UI.
    /// These are used in the 'Upgrade1/2/3ButtonPressed' functions.
    /// </summary>
    private void PrepareUpgrades()
    {
        upgrade1Weapon = allAvailableWeapons[Random.Range(0, allAvailableWeapons.Count)];
        
        // Ensure the player does not get multiple unlock UpgradeType on the same weapon.
        do
        {
            upgrade2Weapon = allAvailableWeapons[Random.Range(0, allAvailableWeapons.Count)];
        } while (upgrade2Weapon == upgrade1Weapon && !upgrade2Weapon.isActiveAndEnabled);
        
        do
        {
            upgrade3Weapon = allAvailableWeapons[Random.Range(0, allAvailableWeapons.Count)];
        } while (upgrade3Weapon == upgrade2Weapon && !upgrade3Weapon.isActiveAndEnabled ||
                    upgrade3Weapon == upgrade1Weapon && !upgrade3Weapon.isActiveAndEnabled);

        // Set Upgrade 1 
        if(!upgrade1Weapon.isActiveAndEnabled)
            upgrade1Type = WeaponUpgradeType.UnlockWeapon;
        else
            upgrade1Type = (WeaponUpgradeType)Random.Range(1, WeaponUpgradeType.GetNames(typeof(WeaponUpgradeType)).Length);

        // Set Upgrade 2
        if(!upgrade2Weapon.isActiveAndEnabled)
            upgrade2Type = WeaponUpgradeType.UnlockWeapon;
        else
            upgrade2Type = (WeaponUpgradeType)Random.Range(1, WeaponUpgradeType.GetNames(typeof(WeaponUpgradeType)).Length);

        // Set Upgrade 3
        if(!upgrade3Weapon.isActiveAndEnabled)
            upgrade3Type = WeaponUpgradeType.UnlockWeapon;
        else
            upgrade3Type = (WeaponUpgradeType)Random.Range(1, WeaponUpgradeType.GetNames(typeof(WeaponUpgradeType)).Length);

    }

    /// <summary>
    /// Performs Upgrade/Unlock set in PrepareButtons().
    /// </summary>
    private void Upgrade1ButtonPressed()
    {
        switch (upgrade1Type)
        {
            case WeaponUpgradeType.UnlockWeapon:
                UnlockWeapon(upgrade1Weapon);
                break;
            case WeaponUpgradeType.ActivationTime:
                upgrade1Weapon.Upgrade_ActivationTime(upgrade1Amount);
                break;
            case WeaponUpgradeType.Damage:
                upgrade1Weapon.Upgrade_Damage(upgrade1Amount);
                break;
            case WeaponUpgradeType.Range:
                upgrade1Weapon.Upgrade_Range(upgrade1Amount);
                break;
            case WeaponUpgradeType.Size:
                upgrade1Weapon.Upgrade_Size(upgrade1Amount);
                break;
            case WeaponUpgradeType.Speed:
                upgrade1Weapon.Upgrade_Speed(upgrade1Amount);
                break;
            default:
                break;
        }
        CloseUpgradeMenu();
    }

    private void Upgrade2ButtonPressed()
    {
        switch (upgrade2Type)
        {
            case WeaponUpgradeType.UnlockWeapon:
                UnlockWeapon(upgrade2Weapon);
                break;
            case WeaponUpgradeType.ActivationTime:
                upgrade2Weapon.Upgrade_ActivationTime(upgrade2Amount);
                break;
            case WeaponUpgradeType.Damage:
                upgrade2Weapon.Upgrade_Damage(upgrade2Amount);
                break;
            case WeaponUpgradeType.Range:
                upgrade2Weapon.Upgrade_Range(upgrade2Amount);
                break;
            case WeaponUpgradeType.Size:
                upgrade2Weapon.Upgrade_Size(upgrade2Amount);
                break;
            case WeaponUpgradeType.Speed:
                upgrade2Weapon.Upgrade_Speed(upgrade2Amount);
                break;
            default:
                break;
        }
        CloseUpgradeMenu();
    }

    private void Upgrade3ButtonPressed()
    {
        switch (upgrade3Type)
        {
            case WeaponUpgradeType.UnlockWeapon:
                UnlockWeapon(upgrade3Weapon);
                break;
            case WeaponUpgradeType.ActivationTime:
                upgrade3Weapon.Upgrade_ActivationTime(upgrade3Amount);
                break;
            case WeaponUpgradeType.Damage:
                upgrade3Weapon.Upgrade_Damage(upgrade3Amount);
                break;
            case WeaponUpgradeType.Range:
                upgrade3Weapon.Upgrade_Range(upgrade3Amount);
                break;
            case WeaponUpgradeType.Size:
                upgrade3Weapon.Upgrade_Size(upgrade3Amount);
                break;
            case WeaponUpgradeType.Speed:
                upgrade3Weapon.Upgrade_Speed(upgrade3Amount);
                break;
            default:
                break;
        }
        CloseUpgradeMenu();
    }

    public void OpenUpgradeMenu()
    {
        UpgradeMenuCanvas.gameObject.SetActive(true);
        floatingJoystick.gameObject.SetActive(false);
        PrepareUpgrades();
    }

    public void CloseUpgradeMenu()
    {
        floatingJoystick.gameObject.SetActive(true);
        UpgradeMenuCanvas.gameObject.SetActive(false);
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
    /// Takes in a weapon and sets it Active for.
    /// </summary>
    /// <param name="weapon">Inactive WeaponBase to be made Active</param>
    private void UnlockWeapon(WeaponBase weapon)
    {
        weapon.gameObject.SetActive(true);
    }

}
