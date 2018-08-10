using UnityEngine;

// Enemy controller.
public class EnemyController : MonoBehaviour
{
  // Health.
  public int health = 100;
  // Rate of fire.
  public float fire_rate = 0.1F;
  // Points for destroying.
  public int points = 10;
  // Destroy sound.
  public AudioClip destroy_sound;
  // Padding of enemy ship.
  public float padding = 0.5F;
  // Projectile prefab.
  public GameObject projectile_prefab;
  // Projectile speed.
  public float projectile_speed = 1.0F;
  // Projectile speed.
  public int projectile_damage = 100;
  // Projectile sound.
  public AudioClip projectile_sound;
  // Ship explosion.
  public GameObject ship_explosion;

  // Update (called once per frame).
  private void Update()
  {
    // Manage projectiles.
    ProjectilesManage();
  } // End of Update

  // Manage projectiles.
  private void ProjectilesManage()
  {
    // Time.deltaTime guarantee that when FPS will drop down, propability of launching projectile will go up.
    if(Random.value<Time.deltaTime*this.fire_rate)
    {
      // Launch projectile.
      ProjectileLaunch();
    }
  } // End of ProjectilesManage

  // Launch projectile.
  private void ProjectileLaunch()
  {
    // Create projectlie.
    GameObject projectile = Instantiate(this.projectile_prefab,this.transform.position-new Vector3(0,this.padding,0),Quaternion.identity);
    // Set damage of projectile.
    projectile.GetComponent<ProjectileController>().DamageSet(this.projectile_damage);
    // Add velocity to projectile.
    projectile.GetComponent<Rigidbody2D>().velocity=new Vector2(0.0F,-this.projectile_speed);
    // Play sound.
    AudioSource.PlayClipAtPoint(this.projectile_sound,this.transform.position);
  } // End of ProjectileLaunch

  // On collision.
  private void OnTriggerEnter2D(Collider2D collision)
  {
    // Get projectile.
    ProjectileController projectile = collision.gameObject.GetComponent<ProjectileController>();
    // If collision with projectile.
    if(projectile!=null)
    {
      // Destroy projectile.
      projectile.ProjectileDestroy();
      // Actualize health.
      this.health-=projectile.DamageGet();
      // If health below 1.
      if(this.health<1)
      {
        // Destroy enemy.
        EnemyDestroy();
      }     
    }
  } // End of OnTriggerEnter2D

  // Destroy enemy.
  private void EnemyDestroy()
  {
    // Play sound.
    AudioSource.PlayClipAtPoint(this.destroy_sound,this.transform.position);
    // Instantiate explosion.
    Instantiate(this.ship_explosion,this.transform.position,this.transform.rotation);
    // Actualize score.
    ScoreController.ScoreAct(this.points);
    // Destroy game object.
    Destroy(this.gameObject);
  } // End of EnemyDestroy

} // End of EnemyController