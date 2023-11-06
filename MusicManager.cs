using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public float volumeLevel = 1f;
    private static MusicManager instance = null;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject); 
        }
    }

    void Start()
    {
        float volumeLevel = PlayerPrefs.GetFloat("GameVolume", 1f); // 1f is the default value if "GameVolume" isn't set
        GetComponent<AudioSource>().volume = volumeLevel;
    }
    void Update()
    {
        volumeLevel = PlayerPrefs.GetFloat("GameVolume", 1f); // 1f is the default value if "GameVolume" isn't set
        GetComponent<AudioSource>().volume = volumeLevel;
    }


}