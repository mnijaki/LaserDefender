using UnityEngine;

// Class that display tooltip.
public class TooltipDisplayer : MonoBehaviour
{

  // Display tooltip. 
  public void Display(Texture2D textrue)
  {
    TooltipController.Display(textrue,this.transform);
  } // End of Display

} // End of TooltipDisplayer