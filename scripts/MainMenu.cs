using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public struct SongInfo {
    public string title;
    public string link;
    public SongInfo(string title, string link) {
        this.title = title;
        this.link = link;
    }
}

public static class songInfo {
    public static SongInfo[] infoList = {
        new SongInfo(
            "Level Up - DR VOX",
            "https://www.youtube.com/c/ArgofoxCC"
        ),
        new SongInfo(
            "Mean Machine - jimmysquare",
            "https://soundcloud.com/jimmysquare"
        ),
        new SongInfo(
            "Conundrum - Meizong",
            "https://www.youtube.com/c/ArgofoxCC"
        ),
        new SongInfo(
            "Cloud 9 - Valesco",
            "https://www.youtube.com/c/ArgofoxCC"
        ),
        new SongInfo(
            "All I Need - Valesco",
            "https://www.youtube.com/c/ArgofoxCC"
        ),
        new SongInfo(
            "Mellow Acid - CyberSDF",
            "https://soundcloud.com/cybersdf"
        ),
        new SongInfo(
            "Helios - Romos",
            "https://soundcloud.com/romixyx"
        ),
        new SongInfo(
            "You And Me - Aether",
            "https://soundcloud.com/aether"
        ),
    };
}

public class MainMenu : MonoBehaviour
{
    public TMP_Text songTitle;
    public string songLink;
    public GameObject loadingScreen;
    public Slider loadingProgress;

    void Start(){
        this.songTitle.text = "";
    }

    public void setSongInfo(int index) {
        this.songTitle.text = songInfo.infoList[index].title;
        this.songLink = string.Copy(songInfo.infoList[index].link);
    }

    public void PlayGame() {
        StartCoroutine(LoadAsynchronously("1st-person-rhythm-game"));
    }

    IEnumerator LoadAsynchronously(string scene) {
        AsyncOperation operation = SceneManager.LoadSceneAsync(scene);
        loadingScreen.SetActive(true);
        while(!operation.isDone){
            float progress = Mathf.Clamp01(operation.progress / .9f);
            loadingProgress.value = progress;
            yield return null;
        }
    }

    static public void QuitGame() {
        Application.Quit();
    }

    public void openURL() {
        Application.OpenURL(this.songLink);
    }
}
