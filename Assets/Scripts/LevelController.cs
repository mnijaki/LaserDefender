using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

// Class that manage levels.
public class LevelController : MonoBehaviour
{
  // Initialization.
  private void Start()
  {
    // If there is game object named "Level".
    if(GameObject.Find("Level")!=null)
    {
      // Actualize level text.
      GameObject.Find("Level").GetComponent<Text>().text=SceneManager.GetActiveScene().buildIndex.ToString();
    }
  } // End of Start

  // Quit.
  public void Quit()
  {
    Application.Quit();
  } // End of Quit

  // Load level by name.
  public void LoadLevelByName(string name)
  {
    // Load scene.
    SceneManager.LoadScene(name);
  } // End of LoadLevelByName

  // Load level by name with delay.
  public void LoadLevelByNameWithDelay(string name,float time)
  {
    StartCoroutine(LoadLevelByNameWithDelay2(name,time));
  } // End of LoadLevelByNameWithDelay

  // Load level by name with delay.
  private IEnumerator LoadLevelByNameWithDelay2(string name,float time)
  {
    // Wait for 'time' seconds.
    yield return new WaitForSeconds(time);
    // Load next level.
    this.LoadLevelByName(name);
  } // End of LoadLevelByNameWithDelay2

  // Load next level.
  public void LoadNextLevel()
  {
    // Load next level.
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);    
  } // End of LoadNextLevel

  // Load next level with delay.
  public void LoadNextLevelWithDelay(float time)
  {
    StartCoroutine(LoadNextLevelWithDelay2(time));
  } // End of LoadNextLevelWithDelay

  // Load next level with delay.
  private IEnumerator LoadNextLevelWithDelay2(float time)
  {
    // Wait for 'time' seconds.
    yield return new WaitForSeconds(time);
    // Load next level.
    this.LoadNextLevel();
  } // End of LoadNextLevelWithDelay2

} // End of LevelController