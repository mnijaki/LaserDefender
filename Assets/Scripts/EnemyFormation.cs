
using UnityEngine;

// Class that manage spawning enemies(creating formation).
public class EnemyFormation : MonoBehaviour
{
  // Enemy prefab.
  public GameObject enemy_prefab;
  // Enemy spawn delay.
  public float enemy_spawn_delay = 1.0F;
  // Number of enemies to spawn.
  public int enemies_to_spawn = 9;
  // Movement speed of the enemy formation.
  public float formation_speed = 1.0F;
  // Width and height of enemy formation.
  public float width = 10.0F;
  public float height = 10.0F;
  // Boundries of the game.
  private float min_x;
  private float max_x;

  // Initialization.
  private void Start()
  {
    // Set random position of enemy formation.
    this.transform.position+=Vector3.left*Random.Range((int)-this.width,(int)this.width);
    // Get boundries.
    BoundriesGet();
    // Initialization of all enemies at once.
    EnemiesSpawnAll();
  } // End of Start
	
	// Update, called once per frame.
	private void Update()
  {
    // Move formation.
    FormationMove();
    // Manage of enemies.
    EnemiesManage();
  } // End of Update

  // Get boundries.
  private void BoundriesGet()
  {
    // Calculate distance between camera and game object.
    float distance=this.transform.position.z-Camera.main.transform.position.z;
    // Set min x.
    this.min_x=Camera.main.ViewportToWorldPoint(new Vector3(0,0,distance)).x+(this.width/2);
    // Set max x.
    this.max_x=Camera.main.ViewportToWorldPoint(new Vector3(1,1,distance)).x-(this.width/2);
  } // End of BoundriesGet

  // Draw gizmos of enemy formation, even if game object is not selected.
  private void OnDrawGizmos()
  {
    Gizmos.DrawWireCube(this.transform.position,new Vector3(this.width,this.height,0));
  } // End of OnDrawGizmos

  // Manage of enemies.
  private void EnemiesManage()
  {
    // If all enemies are destroyed.
    if(IsAllEnemiesDestroyed())
    {
      // If no more enemies to spawn.
      if(this.enemies_to_spawn<1)
      {
        // Load next level with delay.
        GameObject.FindObjectOfType<LevelController>().LoadNextLevelWithDelay(3.0F);
      }
      // If there are enemies to spawn.
      else
      {
        // Initialization of enemies.
        EnemiesSpawnUntilFull();
      }
    }
  } // End of EnemiesManage

  // Initialization of all enemies at once.
  private void EnemiesSpawnAll()
  {
    // Loop over children of the enemy formation in hierarchy (loop over positions).
    foreach(Transform child in this.transform)
    {
      // If no more enemies to spawn then break from loop.
      if(this.enemies_to_spawn<1)
      {
        return;
      }
      // Instantiate enemy.
      Instantiate(this.enemy_prefab,child.transform.position,Quaternion.identity,child.transform);
      // Actualize number of enemies to spawn.
      this.enemies_to_spawn--;
    }
  } // End of EnemiesSpawnAll

  // Initialization of enemies until all position are taken.
  private void EnemiesSpawnUntilFull()
  {
    // If no more enemies to spawn then break from loop.
    if(this.enemies_to_spawn<1)
    {
      return;
    }
    // Get free position.
    Transform free=FormationFreePosGet();
    // If there is free position.
    if(free!=null)
    {
      // Instantiate enemy.
      Instantiate(this.enemy_prefab,free.position,Quaternion.identity,free);
      // Actualize number of enemies to spawn.
      this.enemies_to_spawn--;
    }
    // If there is still free position.
    if(FormationFreePosGet()!=null)
    {
      // Initialization of enemies until all position are taken.
      Invoke("EnemiesSpawnUntilFull",this.enemy_spawn_delay);
    }
  } // End of EnemiesSpawnUntilFull

  // Return information if all enemies in formation are destroyed.
  private bool IsAllEnemiesDestroyed()
  {
    // Loop over children of the formation in hierarchy.
    foreach(Transform child in this.transform)
    {
      // If child(position) have childs(enemy ships) then return FALSE.
      if(child.childCount>0)
      {
        return false;
      }
    }
    // No enemy ships so return TRUE.
    return true;
  } // End of IsAllEnemiesDestroyed

  // Returns next free position in formation.
  Transform FormationFreePosGet()
  {
    // Loop over children of the formation in hierarchy.
    foreach(Transform child in this.transform)
    {
      // If child(position) dont have childs(enemy ships) then return transform.
      if(child.childCount==0)
      {
        return child.transform;
      }
    }
    // No free positions so return NULL.
    return null;
  } // End of FormationFreePosGet

  // Move formation.
  private void FormationMove()
  {
    // Update position of enemy formation.
    this.transform.position+=Vector3.left*this.formation_speed*Time.deltaTime;
    // If enemy spawner out of boundries.
    if((this.transform.position.x<this.min_x)||(this.transform.position.x>this.max_x))
    {
      // Clamp 'x' value to game boundries.
      this.transform.position=new Vector3(Mathf.Clamp(this.transform.position.x,this.min_x,this.max_x),this.transform.position.y,this.transform.position.z);
      // Change direction of formation.
      this.formation_speed=-this.formation_speed;
    }
  } // End of FormationMove
  
} // End of EnemyFormation