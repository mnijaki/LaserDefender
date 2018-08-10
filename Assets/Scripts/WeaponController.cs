using UnityEngine;
using UnityEngine.UI;

// Class that manage weapon handling.
public class WeaponController : MonoBehaviour
{
  // Weapon text.
  private Text weapon_txt;
  // Array of weapon types.
  public WeaponType[] weapon_types;
  // Array of avaible weapon types.
  private bool[] aviable_weapon_types;
  // Active weapon type.
  private WeaponType active_weapon_type;
  // Padding of the ship.
  public float ship_padding = 0.5F;
  // Instance of 'WeaponController'.
  private static WeaponController _instance;
  public static WeaponController Instance
  {
    get
    {
      return WeaponController._instance;
    }
  }

  // Initialization.
  private void Start()
  {
    // Initialize 'WeaponController' class.
    WeaponController._instance=this;
    // Get text component.
    this.weapon_txt=GameObject.Find("Weapon").GetComponent<Text>();
    // Set active weapon.
    ActiveWeaponSet(0);
    // Set all weapons to unaviable(except laser).
    this.aviable_weapon_types=new bool[this.weapon_types.Length];
    for(int i=1; i<this.weapon_types.Length; i++)
    {
      this.aviable_weapon_types[i]=false;
    }
    this.aviable_weapon_types[0]=true;
  } // End of Start

  // Manage weapon types.
  public void WeaponTypesManage()
  {
    // Loop over weapon types.
    for(int i=0; i<this.weapon_types.Length; i++)
    {
      // If user pressed number [0-9].
      if(Input.GetKeyDown(""+i))
      {
        // Set active weapon.
        ActiveWeaponSet(i);
      }
    }
  } // End of WeaponTypesManage

  // Set active weapon.
  private void ActiveWeaponSet(int idx)
  {
    // Set active weapon.
    this.active_weapon_type=this.weapon_types[idx];
    // Set text of active weapon.
    this.weapon_txt.text=this.active_weapon_type.weapon_name;
  } // End of ActiveWeaponSet

  // Launch projectile.
  public void ProjectileLaunch()
  {
    // If space pressed.
    if(Input.GetKeyDown(KeyCode.Space))
    {
      // 0.0000001F - time before the first invokation. Essentially that should be 0.0F, but
      // sometimes it generate some bugs, 0.0000001F eliminate it.
      InvokeRepeating("ProjectileLaunch2",0.001F,this.active_weapon_type.fire_rate);
    }
    // If space not pressed.
    else
    {
      // If space unpressed.
      if(Input.GetKeyUp(KeyCode.Space))
      {
        CancelInvoke("ProjectileLaunch2");
      }
    }
  } // End of ProjectileLaunch

  // Launch projectile.
  public void ProjectileLaunch2()
  {
    // Create projectlie.
    GameObject projectile = Instantiate(this.active_weapon_type.projectile_prefab,this.transform.position+new Vector3(0,this.ship_padding,0),Quaternion.identity);
    // Set damage of projectile.
    projectile.GetComponent<ProjectileController>().DamageSet(this.active_weapon_type.damage);
    // Add velocity to projectile.
    projectile.GetComponent<Rigidbody2D>().velocity=new Vector2(0.0F,this.active_weapon_type.projectile_speed);
    // Play sound.
    AudioSource.PlayClipAtPoint(this.active_weapon_type.projectile_sound,this.transform.position);
  } // End of ProjectileLaunch2

} // End of WeaponController