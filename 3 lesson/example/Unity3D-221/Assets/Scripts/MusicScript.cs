using UnityEngine;

public class MusicScript : MonoBehaviour
{
    public static MusicScript Instance;

    private AudioSource music;



    void Start()
    {

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        music = GetComponent<AudioSource>();
        GameState.AddListener(OnGameStateChanged);


        music.volume = GameState.musicVolume;
    }

    private void OnGameStateChanged(string fieldName)
    {

        music.volume = GameState.musicVolume;
    }

    private void OnDestroy()
    {

        if (Instance == this)
        {
            GameState.RemoveListener(OnGameStateChanged);
        }
    }
}
