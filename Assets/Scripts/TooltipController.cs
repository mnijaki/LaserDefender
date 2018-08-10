using UnityEngine;

// Class that manage tooltip.
public class TooltipController : MonoBehaviour
{
  // Sprite renderer.
  private static SpriteRenderer sprite_renderer;
  // Transform.
  private static Transform trans;

  // Initialization.
  private void Awake()
  {
    // Get sprite renderer.
    sprite_renderer=this.GetComponent<SpriteRenderer>();
    // Get transform. 
    trans=this.transform;
  } // End of Awake

  // Display tooltip.
  public static void Display(Texture2D textrue, Transform trans_new)
  {
    // Create sprite.
    Sprite mySprite = Sprite.Create(textrue,new Rect(0.0f,0.0f,textrue.width,textrue.height),new Vector2(0.5f,0.5f),300.0f);
    // Render sprite.
    sprite_renderer.sprite=mySprite;
    // Actualize transform.
    trans.position=trans_new.position+new Vector3(2,0,0);
  } // End of Display

} // End of TooltipController