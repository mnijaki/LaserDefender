using UnityEngine;
using UnityEngine.UI;

// Player controller.
public class PlayerController : MonoBehaviour
{
  // Shield time.
  public float shield = 0.0F;
  // Shield text.
  private Text shield_txt;
  // Number of lifes.
  public int lifes = 3;
  // Life text.
  private Text lifes_txt;
  // Health.
  public int health = 300;
  // Health text.
  private Text health_txt;
  // Movement speed of the ship.
  public float speed = 5.0F;
  // Ship explosion.
  public GameObject ship_explosion;
  // Destroy sound.
  public AudioClip destroy_sound;
  // Padding of player ship.
  public float padding = 0.5F;
  // Boundries of the game.
  private float min_x;
  private float max_x;
  private float min_y;
  private float max_y;

  // Initialize.
  private void Start()
  {
    // Get text components.
    this.lifes_txt=GameObject.Find("Shield").GetComponent<Text>();
    this.lifes_txt=GameObject.Find("Lifes").GetComponent<Text>();
    this.health_txt=GameObject.Find("Health").GetComponent<Text>();
    // Get boundries.
    BoundriesGet();
  } // End of Start

  // Update (called once per frame).
  private void Update()
  {
    // Move player ship.
    PlayerMove();
    // Manage weapon types.
    WeaponTypesManage();
    // Manage firing.
    FireManage();
  } // End of Update

  // Get boundries.
  private void BoundriesGet()
  {
    // Calculate distance between camera and game object.
    float distance=this.transform.position.z-Camera.main.transform.position.z;
    // Get left upper corner.
    Vector3 corner=Camera.main.ViewportToWorldPoint(new Vector3(0,0,distance));
    // Set min 'x' and 'y'.
    this.min_x=corner.x+this.padding;
    this.min_y=corner.y+this.padding;
    // Get right down corner.
    corner=Camera.main.ViewportToWorldPoint(new Vector3(1,1,distance));
    // Set max 'x' and 'y'.
    this.max_x=corner.x-this.padding;
    this.max_y=corner.y-this.padding;
  } // End of BoundriesGet

  // Movement.
  private void PlayerMove()
  {
    // Movment vector.
    Vector3 move=this.transform.position;
    // Actualize position.
    if(Input.GetKey(KeyCode.LeftArrow))
    {
      move+=Vector3.left*this.speed*Time.deltaTime;
    }
    if(Input.GetKey(KeyCode.RightArrow))
    {
      move+=Vector3.right*this.speed*Time.deltaTime;
    }
    if(Input.GetKey(KeyCode.DownArrow))
    {
      move+=Vector3.down*this.speed*Time.deltaTime;
    }
    if(Input.GetKey(KeyCode.UpArrow))
    {
      move+=Vector3.up*this.speed*Time.deltaTime;
    }
    // Clamp 'x' and 'y' position to game boundries.
    move=new Vector3(Mathf.Clamp(move.x,this.min_x,this.max_x),Mathf.Clamp(move.y,this.min_y,this.max_y),move.z);
    // Move player.
    this.transform.position=move;
  } // End of PlayerMove

  // Manage weapon types.
  private void WeaponTypesManage()
  {
    WeaponController.Instance.WeaponTypesManage();
  } // End of WeaponTypesManage

  // Fire manage.
  private void FireManage()
  {
    WeaponController.Instance.ProjectileLaunch();
  } // End of FireManage

  // On collision.
  private void OnTriggerEnter2D(Collider2D collision)
  {
    // Get projectile.
    ProjectileController projectile=collision.gameObject.GetComponent<ProjectileController>();
    // If collision with projectile.
    if(projectile!=null)
    {
      // Decrease health.
      HealthDown(projectile.DamageGet());
      // Destroy projectile.
      projectile.ProjectileDestroy();
    }
  } // End of OnTriggerEnter2D

  // Set lifes.
  private void LifesSet(int lifes)
  {
    // Actualize lifes.
    this.lifes=lifes;
    // Actualize lifes text.
    this.lifes_txt.text=this.lifes.ToString();
  } // End of LifesSet

  // Increase lifes.
  private void LifesUp(int val)
  {
    // Actualize lifes.
    this.lifes+=val;
    // Actualize lifes text.
    this.lifes_txt.text=this.lifes.ToString();
  } // End of LifesUp

  // Decrease lifes.
  private void LifesDown(int val)
  {
    // Actualize lifes.
    this.lifes-=val;
    // Actualize lifes text.
    this.lifes_txt.text=this.lifes.ToString();
    // If lifes above 0.
    if(this.lifes>0)
    {
      // Set health.
      HealthSet(300);
    }
    // If lifes below 0.
    else
    {
      // Set health.
      HealthSet(0);
    }
    // Destroy player.
    PlayerDestroy();    
  } // End of LifesDown

  // Set health.
  private void HealthSet(int health)
  {
    // Actualize health.
    this.health=health;
    // Actualize health text.
    this.health_txt.text=this.health.ToString();
  } // End of HealthSet

  // Increase health.
  private void HealthUp(int val)
  {
    // Actualize health.
    this.health+=val;
    // Actualize health text.
    this.health_txt.text=this.health.ToString();
  } // End of HealthUp

  // Decrease health.
  private void HealthDown(int val)
  {
    // If animation is playing don't decrease health.
    if(this.GetComponent<Animator>().enabled)
    {
      return;
    }
    // Actualize health.
    this.health-=val;
    // Actualize health text.
    this.health_txt.text=this.health.ToString();
    // If health below 1.
    if(this.health<1)
    {
      // Decrease lifes.
      LifesDown(1);
    }
  } // End of HealthDown

  // Destroy player.
  private void PlayerDestroy()
  {
    // Play sound.
    AudioSource.PlayClipAtPoint(this.destroy_sound,this.transform.position);
    // Instantiate explosion.
    Instantiate(this.ship_explosion,this.transform.position,this.transform.rotation);
    // Move ship to starting position.
    this.transform.position=new Vector3(0,this.min_y,this.transform.position.z);
    // Enable animation.
    this.GetComponent<Animator>().enabled=true;
    // If no more lifes.
    if(this.lifes<1)
    {
      // Load 'Lose' screen.
      GameObject.FindObjectOfType<LevelController>().LoadLevelByNameWithDelay("Lose",2.0F);
      // Destroy game object.
      Destroy(this.gameObject);
    }
  } // End of PlayerDestroy

  // Disable animation.
  private void AnimDisable()
  {
    // Disable animation.
    this.GetComponent<Animator>().enabled=false;
    // Reset animation to start.
    this.GetComponent<Animator>().Play("MoveToStartPos",-1,0.0F);
  } // End of AnimDisable

} // End of PlayerController