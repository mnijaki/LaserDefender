using UnityEngine;
using UnityEngine.UI;

// Class that manage score.
public class ScoreController : MonoBehaviour
{
  // Score.
  private static int score = 0;
  // Text component.
  private static Text txt;

  // Initialization.
  private void Start()
  {
    // Get text component.
    txt=this.GetComponent<Text>();
    // Reset score.
    ScoreReset();
    // Actualize score text.
    txt.text=score.ToString()+" PTS";
  } // End of Start

  // Actualize score.
  public static void ScoreAct(int points)
  {
    // Actualize score.
    score+=points;
    // Actualize score text.
    txt.text=score.ToString()+" PTS";
  } // End ScoreAct

  // Get score.
  public static int ScoreGet()
  {
    return score;
  } // End of ScoreGet

  // Reset score.
  public static void ScoreReset()
  {
    // Actualize score.
    score=0;
  } // End of ScoreReset

} // End of ScoreController