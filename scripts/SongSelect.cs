using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongSelect : MonoBehaviour
{
    public AudioClip[] songs;

    public AudioClip GetSongByIndex(int index){
        return songs[index];
    }
}
