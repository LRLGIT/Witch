using UnityEngine;

public class MusicStart : MonoBehaviour
{
    public GameObject FuwenPanel;
    public float beatTempo;
    private Vector3 originalPosition;
    public bool hasStart = false;
    public bool hasEnd = false;
    public AudioClip musicClip;
    private AudioSource musicSource;
    private bool isMusicPlaying = false;

    void Start()
    {
        originalPosition = transform.position;
        musicSource = gameObject.GetComponent<AudioSource>();
        if (musicSource == null)
        {
            musicSource = gameObject.AddComponent<AudioSource>();

        }
        musicSource.clip = musicClip;

    }

    void Update()
    {
        if (hasStart)
        {
            FuwenPanel.SetActive(false);
            transform.position -= new Vector3(beatTempo *Time.deltaTime, 0f, 0f);
            if (!isMusicPlaying)
            {
                PlayMusic();
                isMusicPlaying = true;
            }
        }
        else if(hasEnd)
        {
            FuwenPanel.SetActive(true);
            transform.position = originalPosition;
            if (isMusicPlaying)
            {
                StopMusic();
                isMusicPlaying = false;
            }
        }
    }

    void PlayMusic()
    {
        if (!musicSource.isPlaying)
        {
            musicSource.Play();
        }
    }

    void StopMusic()
    {
        if (musicSource.isPlaying)
        {
            musicSource.Stop();
        }
    }
}
