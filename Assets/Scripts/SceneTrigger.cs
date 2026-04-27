using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTrigger : MonoBehaviour {

    [Header("Scene")]
    public string sceneToLoad;

    private void OnTriggerEnter(Collider other) {

        if (other.CompareTag("Player")) {

            Time.timeScale = 1f;
            SceneManager.LoadScene(sceneToLoad);

        }
    }
}