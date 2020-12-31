using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject resultMenuUI;
    public GameObject reticle;

	private MouseLook mouseLook;


    void Awake() {
		mouseLook = GameObject.FindObjectOfType<MouseLook> ();
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !resultMenuUI.active){
            if (!GameIsPaused) {
                Pause();
            }
        }
    }

    public void Pause(){
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
		reticle.SetActive(false);
		mouseLook.mouseLook = false;
		Cursor.lockState = CursorLockMode.None;
    }

    public void Resume(){
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
		reticle.SetActive(true);
		mouseLook.mouseLook = true;
		Cursor.lockState = CursorLockMode.Locked;
    }

    public void ReturnMain(){
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
		Cursor.lockState = CursorLockMode.None;
    }
}
