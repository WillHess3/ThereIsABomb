using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour {

    public GameObject leftDoor;
    public GameObject rightDoor;

    private bool openDoors;
    private float doorTimer = 1;
    private int openCloseMultiplier = 1;

    private bool beginAscent;
    private float liftTimer = 5;

    private bool playDoorOpen;

    private void Start() {
        AudioManager.instance.GetSound("elevatorDoors").loop = true;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.GetComponent<PlayerMovement>()) {
            openCloseMultiplier = -1;
            openDoors = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.GetComponent<PlayerMovement>()) {
            openDoors = false;
        }
    }

    public void OpenDoors() {
        openDoors = true;
    }

    private void Update() {
        if (openDoors) {
            if (doorTimer >= 0 && doorTimer <= 1) {
                doorTimer -= Time.deltaTime * openCloseMultiplier;

                playDoorOpen = true;
            } else if (doorTimer < 0) {
                doorTimer = 0;
                playDoorOpen = false;
                openDoors = false;
            } else if (doorTimer > 1) {
                doorTimer = 1;
                if (openCloseMultiplier == -1) {
                    openDoors = false;
                    beginAscent = true;
                    playDoorOpen = false;
                }
            }

            leftDoor.transform.localPosition = new Vector3(leftDoor.transform.localPosition.x, leftDoor.transform.localPosition.y, Mathf.Lerp(-0.238f, 0.479583f, doorTimer));
            rightDoor.transform.localPosition = new Vector3(rightDoor.transform.localPosition.x, rightDoor.transform.localPosition.y, Mathf.Lerp(2.209f, 1.479583f, doorTimer));
        } 

        if (beginAscent) {
            if (liftTimer > 0) {
                liftTimer -= Time.deltaTime;
                if (!AudioManager.instance.GetSound("elevatorElevate").isPlaying) {
                    AudioManager.instance.GetSound("elevatorElevate").Play();
                }
            } else {
                openDoors = true;
                openCloseMultiplier = 1;
                beginAscent = false;
            }

            transform.localPosition = new Vector3(transform.localPosition.x, Mathf.Lerp(6.77f, 1.02715f, liftTimer / 5f), transform.localPosition.z);
        } else {
            if (AudioManager.instance.GetSound("elevatorElevate").isPlaying) {
                AudioManager.instance.GetSound("elevatorElevate").Stop();
            }
        }

        if (playDoorOpen) {
            if (!AudioManager.instance.GetSound("elevatorDoors").isPlaying) {
                AudioManager.instance.GetSound("elevatorDoors").Play();
            }
        } else {
            if (AudioManager.instance.GetSound("elevatorDoors").isPlaying) {
                AudioManager.instance.GetSound("elevatorDoors").Stop();
            }
        }
    }

}
