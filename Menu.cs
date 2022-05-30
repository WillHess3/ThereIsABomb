using UnityEngine.SceneManagement;
using UnityEngine;

public class Menu : MonoBehaviour {

    private void Start() {
        Cursor.lockState = CursorLockMode.None;
    }

    public void OnPlay() {
        AudioManager.instance.GetSound("menuSelect").Play();
        SceneManager.LoadScene(1);
    }

    public void OnQuit() {
        Application.Quit();
    }

}
