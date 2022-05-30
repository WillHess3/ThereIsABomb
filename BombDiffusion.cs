using System.Collections.Generic;
using UnityEngine;

public class BombDiffusion : MonoBehaviour {

    public GameObject blueBreak, yellowBreak, pinkBreak, greenBreak, redBreak;

    public List<GameObject> buttons;

    private string[] sequence;
    private int sequencePlace;

    public GameObject player;
    public GameObject rubble;
    public GameObject blood;

    private float timer = 0;

    public GameObject winner;
    private bool win;
    private bool dead;

    private void Start() {
        sequence = new string[5];

        sequence[0] = "green";
        sequence[1] = "pink";
        sequence[2] = "red";
        sequence[3] = "yellow";
        sequence[4] = "blue";

        sequencePlace = 0;
    }

    public void StartDiffusionProcess() {
        foreach (GameObject button in buttons) {
            button.SetActive(true);
        }
    }

    public void OnBlueClicked() {
        if (!blueBreak.activeSelf) {
            if (sequence[sequencePlace] == "blue") {
                sequencePlace++;
                winner.SetActive(true);
                win = true;
                AudioManager.instance.GetSound("wireSnip").Play();
            } else {
                BlowUp();
            }
        }

        blueBreak.SetActive(true);
    }

    public void OnYellowClicked() {
        if (!yellowBreak.activeSelf) {
            if (sequence[sequencePlace] == "yellow") {
                sequencePlace++;
                AudioManager.instance.GetSound("wireSnip").Play();
            } else {
                BlowUp();
            }
        }

        yellowBreak.SetActive(true);
    }

    public void OnPinkClicked() {
        if (!pinkBreak.activeSelf) {
            if (sequence[sequencePlace] == "pink") {
                sequencePlace++;
                AudioManager.instance.GetSound("wireSnip").Play();
            } else {
                BlowUp();
            }
        }

        pinkBreak.SetActive(true);
    }

    public void OnGreenClicked() {
        if (!greenBreak.activeSelf) {
            if (sequence[sequencePlace] == "green") {
                sequencePlace++;
                AudioManager.instance.GetSound("wireSnip").Play();
            } else {
                BlowUp();
            }
        }

        greenBreak.SetActive(true);
    }

    public void OnRedClicked() {
        if (!redBreak.activeSelf) {
            if (sequence[sequencePlace] == "red") {
                sequencePlace++;
                AudioManager.instance.GetSound("wireSnip").Play();
            } else {
                BlowUp();
            }
        }

        redBreak.SetActive(true);
    }

    public void BlowUp() {
        AudioManager.instance.StopAllSounds();
        AudioManager.instance.GetSound("bomb").Play();
        player.GetComponent<PlayerMovement>().enabled = false;
        player.transform.GetComponentInChildren<CameraLook>().enabled = false;
        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        player.GetComponent<Rigidbody>().AddForceAtPosition(new Vector3(Random.Range(-10, 10), Random.Range(0, 10), Random.Range(-10, 10)), new Vector3(player.transform.position.x, player.transform.position.y - 1, player.transform.position.z), ForceMode.VelocityChange);
        rubble.SetActive(true);
        blood.SetActive(true);
        dead = true;
    }

    private void Update() {
        if (timer > 90 && !win && !dead) {
            BlowUp();
        } else {
            timer += Time.deltaTime;
        }
    }
}
