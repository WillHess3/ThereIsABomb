using UnityEngine;

public class Door : MonoBehaviour {

    private bool startOpeningProcess;

    private float openingTimer = 1f;

    public Transform hinge;

    private bool open;

    public bool locked;

    public GameObject lockObj;

    public void OpenDoor() {
        if (!startOpeningProcess) {
            startOpeningProcess = true;

            if (locked) {
                lockObj.SetActive(false);
            }
        }
    }

    private void Update() {
        if (startOpeningProcess) {
            float rotAmt = Mathf.Lerp(0, 90, 1 - openingTimer);
            
            if (openingTimer > 0) {
                openingTimer -= Time.deltaTime;
            } else {
                openingTimer = 1;
                startOpeningProcess = false;

                open = !open;
                return;
            }

            transform.RotateAround(hinge.position, Vector3.up, (Mathf.Lerp(0, 90, 1 - openingTimer) - rotAmt) * (open ? -1 : 1));
        }
    }

}
