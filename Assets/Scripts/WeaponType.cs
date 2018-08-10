using UnityEngine;

// Class that describe weapon type.
public class WeaponType : MonoBehaviour
{
  // Name of weapon type.
  public string weapon_name = "Laser";
  // Rate of fire of the weapon.
  public float fire_rate = 0.5F;
  // Damage of the weapon.
  public int damage = 100;
  // Projectile speed.
  public float projectile_speed = 5.0F;
  // Projectile sound.
  public AudioClip projectile_sound;
  // Projectile prefab.
  public GameObject projectile_prefab;
} // End of WeaponType