using UnityEngine;

// Class that shread object that collide with shreder(e.g. projectiles).
public class Shredder : MonoBehaviour
{

  // On collision.
  public void OnTriggerEnter2D(Collider2D collision)
  {
    // Destory game object.
    Destroy(collision.gameObject);
  } // End of OnTriggerEnter2D

} // End of Shredder