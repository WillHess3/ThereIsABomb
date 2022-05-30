using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeTracker : MonoBehaviour {

    public static TimeTracker instance;

    public float time;
    public int attempts;

    public bool win;

    public bool paused;

    private void Awake() {
        if (instance != null) {
            Destroy(gameObject);
        } else {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
    }

    private void Update() {
        if (SceneManager.GetActiveScene().buildIndex == 1 && !win && !paused) {
            time += Time.deltaTime;
        }
    }

}
