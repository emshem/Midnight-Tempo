using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewSong : MonoBehaviour
{
    public AudioClip mainMenuSong;
    public SongSelect songSelect;
    AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
		_audioSource = GetComponent<AudioSource>();
        playMainMenuSong();
    }

    public void playMainMenuSong() {
        _audioSource.clip = mainMenuSong;
        _audioSource.Play();
    }

    public void playSelectedSong(int index)
    {
        _audioSource.clip = songSelect.GetSongByIndex(index);
        _audioSource.Play();
    }
}
