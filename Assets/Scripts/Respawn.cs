using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine;

public class Respawn : MonoBehaviour {

    private TMP_Text text;
    private float counter = 3;

    private void Start() {
        text = GetComponent<TMP_Text>();
    }

    private void Update() {
        text.text = "Respawning in: " + Mathf.CeilToInt(counter);
        counter -= Time.deltaTime;

        if (counter < 0) {
            FindObjectOfType<TimeTracker>().attempts++;
            SceneManager.LoadScene(1);
        }
    } 
}
