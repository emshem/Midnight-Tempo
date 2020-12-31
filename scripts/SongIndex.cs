using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SongIndex : MonoBehaviour
{
    static public int index = 0;

    public GameObject loadingScreen;
    public Slider slider;

    private AsyncOperation operation;
    private bool load = false;

    void Start(){
        DontDestroyOnLoad (transform.gameObject);
    }

    void Update() {
        if (load){
            loadingScreen.SetActive(true);
            float progress = Mathf.Clamp01(operation.progress/.9f);
            slider.value = progress;
        }

    }

    public void SetIndex(int temp){
        // set index
        index = temp;
    }

    public void PlaySong() {
        operation = SceneManager.LoadSceneAsync("1st-person-rhythm-game");
        load = true;  
    }
}
