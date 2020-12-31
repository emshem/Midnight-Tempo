using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour {
	// target
	[SerializeField]
	private GameObject target;

	// results panel
	[SerializeField]
	private GameObject resultsPanel;

	// reticle
	[SerializeField]
	private GameObject reticle;

	[SerializeField]
	private Text scoreText;
	public TMP_Text inGameScore;

	public static int score;

	private Vector2 targetRandomPosition;

	[SerializeField]
	private SongSelect songSelect;

	AudioSource _audioSource;

	private MouseLook mouseLook;

	public Camera fpsCam;

	// audio
	private float[] samples = new float[1024];
	public float spawnThreshold = 0.00025f;
	public int frequency = 650;
	public FFTWindow fftWindow;
	static public bool paused = false;

	public PauseMenu pauseMenu;

	public HealthBar healthBar;

	public static float healthValue;

    public GameObject loadingScreen;
    public Slider loadingProgress;

	// Start is called before the first frame update
	void Start() {
		score = 0;
		healthValue = 0.5f;
		resultsPanel.SetActive(false);
		_audioSource = GetComponent<AudioSource>();
		_audioSource.clip = songSelect.GetSongByIndex(SongIndex.index);
		StartCoroutine("StartGame");
	}

	// Update is called once per frame
	void Update() {
		if (Input.GetButtonDown("Fire1") && !paused) {
			Shoot();
		}
		if (healthValue == 0) {
			_audioSource.Stop();
			healthBar.slider.value = 0;
		}
		// update score and health
		if (resultsPanel.activeSelf == false){
			inGameScore.text = score.ToString();
			healthBar.slider.value = healthValue;
		}

		if (PauseMenu.GameIsPaused){
			_audioSource.Pause();
			paused = true;
		}
		else if (!PauseMenu.GameIsPaused && paused && !_audioSource.isPlaying){
			paused = false;
			_audioSource.Play();
		}
	}

	void Awake() {
		mouseLook = GameObject.FindObjectOfType<MouseLook> ();
	}

	void Shoot() {
		RaycastHit hit;
		LayerMask mask = LayerMask.GetMask("Target");
		if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, Mathf.Infinity, mask)){
			Hit();
		}
		else {
			Miss();
		}
	}

	void Hit() {
		score += 10;
		healthBar.Hit();
	}
	
	public void Miss() {
		score -= 5;
		healthBar.Missed();
	}

	public void TargetGone(){
		healthBar.Missed();
	}

	public void ReturnMain(){
		if (paused) {
			pauseMenu.ReturnMain();
			paused = false;
		}
		StartCoroutine(LoadAsynchronously("main-menu"));
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

	public void RestartGame() {
		if (paused) {
       		pauseMenu.Resume();
			_audioSource.Stop();
			paused = false;
			StopCoroutine("StartGame");
			DestroyAllTargets();
		}
		else{
			resultsPanel.SetActive(false);
			reticle.SetActive(true);
			mouseLook.mouseLook = true;
			Cursor.lockState = CursorLockMode.Locked;
		}
		healthBar.slider.value = 0.5f;
		healthValue = 0.5f;
		StartCoroutine("StartGame");
	}

	public void DestroyAllTargets() {
    	GameObject[] targets = GameObject.FindGameObjectsWithTag ("Target");
		if (targets != null){
			for(var i = 0 ; i < targets.Length ; i ++) {
				Destroy(targets[i]);
			}
		}
 	}

	public IEnumerator StartGame() {
		score = 0;
		_audioSource.Play();
		while (_audioSource.isPlaying || paused){
			// spawn based on audio
			StartCoroutine("AudioSpawn");
			if (healthBar.slider.value < 0.1f) {
				_audioSource.Stop();
				healthBar.slider.value = 0;
			}
			yield return new WaitForSeconds(0.4f);
		}
		resultsPanel.SetActive(true);
		reticle.SetActive(false);
		mouseLook.mouseLook = false;
		float mouseX = 0;
		float mouseY = 0;
		Cursor.lockState = CursorLockMode.None;
		scoreText.text = "Score: " + score;
	}

	public IEnumerator AudioSpawn() {
		AudioListener.GetSpectrumData(samples, 0, fftWindow);

		if (samples[frequency] > spawnThreshold) {
			targetRandomPosition = new Vector2(Random.Range(-6f, 4f), Random.Range(1f, 3f));
			Instantiate(target, targetRandomPosition, Quaternion.identity);
		}
		yield return null;
	}

	

}
