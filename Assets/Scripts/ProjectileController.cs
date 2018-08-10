using UnityEngine;

// Class that manage projectile.
public class ProjectileController : MonoBehaviour
{
  // Damage of the projectile.
  private int damage = 100;

  // Return damage of projectile.
  public int DamageGet()
  {
    return this.damage;
  } // End of DamageGet

  // Set damage of projectile.
  public void DamageSet(int damage)
  {
    this.damage=damage;
  } // End of DamageSet

  // Destroy projectile.
  public void ProjectileDestroy()
  {
    // Destroy game object.
    Destroy(this.gameObject);
  } // End of ProjectileDestroy

} // End of ProjectileController