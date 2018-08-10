using UnityEngine;
using UnityEngine.UI;

// Class that manage displaying score.
public class ScoreDisplayer : MonoBehaviour
{

	// Initialization.
	void Start()
  {
    // Display score.
    this.GetComponent<Text>().text+=ScoreController.ScoreGet()+" POINTS";
    // Reset score.
    ScoreController.ScoreReset();
  } // End of Start

} // End of ScoreDisplayer