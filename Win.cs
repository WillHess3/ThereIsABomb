using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour {

    private TimeTracker timeTracker;

    public TMP_Text timeAttempts;

    private void Start() {
        timeTracker = FindObjectOfType<TimeTracker>();
        timeTracker.win = true;

        timeAttempts.text = "time: " + Mathf.RoundToInt(timeTracker.time) + "seconds \n attempts: " + timeTracker.attempts;
    }

    public void QuitToMainMenu() {
        SceneManager.LoadScene(0);
        AudioManager.instance.GetSound("menuSelect");
    }

}
