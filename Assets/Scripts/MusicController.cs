using UnityEngine.SceneManagement;
using UnityEngine;

// Class that handle background music in the game.
public class MusicController : MonoBehaviour
{
  // Single static instance of MusicPlayer (Singelton pattern).
  private static MusicController instance = null;
  // Audio source ('static' ensure that if new instance of 'MusicPlayer' will run 'OnLevelWasLoaded' it still can use 'audio_source').
  private static AudioSource audio_source;
  // Start clip.
  public AudioClip start_clip;
  // Win clip.
  public AudioClip win_clip;
  // Lose clip.
  public AudioClip lose_clip;
  // Array of audio clips.
  public AudioClip[] levels_clips;

  // Initialization.
  private void Start()
  {
    // Get audio source.
    audio_source=this.GetComponent<AudioSource>();
    // Load music.
    OnLevelWasLoaded(0);
  } // End of Start

  // Function called before 'Start()', just after prefab is instantiated.
  private void Awake()
  {
    // If there is instance of game music and it is not this one.
    if((instance!=null)&&(instance!=this))
    {
      // Destroy game music player (just in case some minor bug will happen).
      Destroy(this.gameObject);
      // Debug error.
      Debug.LogWarning("Duplicate music player self-destructed!");
    }
    // If there is no instance of game music.
    else
    {
      // Save instance.
      instance=this;
      // Make sure that music will not stop after loading another scene.
      GameObject.DontDestroyOnLoad(this.gameObject);      
    }
  } // End of Awake

  // Event - run when level was loaded.
  public void OnLevelWasLoaded(int level)
  {
    // Depending on scene name.
    switch(SceneManager.GetActiveScene().name)
    {
      // Start.
      case "Start":
      // Help.
      case "Help":
      {
        // Load music.
        audio_source.clip=this.start_clip;
        // Break.
        break;
      }
      // Win.
      case "Win":
      {
        // Load music.
        audio_source.clip=this.win_clip;
        // Break.
        break;
      }
      // Lose.
      case "Lose":
      {
        // Load music.
        audio_source.clip=this.lose_clip;
        // Break.
        break;
      }
      // Default.
      default:
      {
        // Load music.
        audio_source.clip=this.levels_clips[level-1];
        // Break.
        break;
      }
    }
    // Play music.
    audio_source.Play();
  } // End of OnLevelWasLoaded

} // End of MusicController