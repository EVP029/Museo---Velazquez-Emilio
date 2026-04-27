using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PauseMenuManager : MonoBehaviour {

    [Header("UI")]
    public GameObject pauseMenuUI;
    public Canvas canvasGroup;

    [Header("Settings")]
    public float fadeDuration = 0.3f;
    public string mainMenuSceneName = "MainMenu";

    [Header("Player Reference")]
    public FPSPlayer player;

    private bool isPaused = false;
    private Coroutine currentCoroutine;

    void Start() {
        pauseMenuUI.SetActive(false);
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    // PAUSAR
    public void PauseGame() {
        if (currentCoroutine != null)
            StopCoroutine(currentCoroutine);

        if (player != null)
            player.isPaused = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        currentCoroutine = StartCoroutine(FadeInPause());
    }

    IEnumerator FadeInPause() {
        pauseMenuUI.SetActive(true);

        float t = 0f;

        Time.timeScale = 0f;

        while (t < fadeDuration) {
            t += Time.unscaledDeltaTime;
            yield return null;
        }

        isPaused = true;
    }

    // REANUDAR
    public void ResumeGame() {
        if (currentCoroutine != null)
            StopCoroutine(currentCoroutine);

        if (player != null)
            player.isPaused = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        currentCoroutine = StartCoroutine(FadeOutPause());
    }

    IEnumerator FadeOutPause() {
        float t = 0f;

        while (t < fadeDuration) {
            t += Time.unscaledDeltaTime;
            yield return null;
        }

        pauseMenuUI.SetActive(false);

        Time.timeScale = 1f;
        isPaused = false;
    }

    // REINICIAR ESCENA
    public void RestartScene() {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // IR AL MENU PRINCIPAL
    public void LoadMainMenu() {
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuSceneName);
    }

    public void CloseGame() {

        Application.Quit();
        Debug.Log("Salio del juego");

    }
}