using UnityEngine;

// Class that draw gizmos.
public class Position : MonoBehaviour
{
  // Radius.
  public float radius=0.5F;

  // Draw gizmos of game object, even if game object is not selected.
  private void OnDrawGizmos()
  {
    Gizmos.DrawWireSphere(this.transform.position,this.radius);
  } // End of OnDrawGizmos

} // End of Position