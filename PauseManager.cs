using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour {

    public GameObject pauseMenu;

    public GameObject deadScreen;
    public GameObject compScreen;
    public GameObject winScreen;
    public GameObject diffusionScreen;

    public void OnResume() {
        if (compScreen.GetComponent<RawImage>().enabled || winScreen.activeSelf || deadScreen.activeSelf || diffusionScreen.GetComponent<RawImage>().enabled) {
            Cursor.lockState = CursorLockMode.None;
        } else if (FindObjectOfType<Interact>().note.GetComponent<RawImage>().enabled || FindObjectOfType<Interact>().diffuseInstructions.GetComponent<RawImage>().enabled) {
            Cursor.lockState = CursorLockMode.Locked;
        } else {
            FindObjectOfType<CameraLook>().enabled = true;
            Cursor.lockState = CursorLockMode.Locked;
        }


        AudioManager.instance.GetSound("menuSelect").Play();
        Time.timeScale = 1;
        TimeTracker.instance.paused = false;
        pauseMenu.SetActive(false);
    }

    public void OnMenu() {
        OnResume();
        AudioManager.instance.GetSound("menuSelect").Play();
        SceneManager.LoadScene(0);
    }

    public void OnQuit() {
        Application.Quit();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape) && !pauseMenu.activeSelf) {
            pauseMenu.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
            FindObjectOfType<CameraLook>().enabled = false;
            TimeTracker.instance.paused = true;
        }
    }
}
