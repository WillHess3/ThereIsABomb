using UnityEngine;
using UnityEngine.UI;

public class Interact : MonoBehaviour {

    public GameObject computerScreen;
    public GameObject note;
    public GameObject picOfBob;
    public GameObject bombDiffusion;
    public GameObject diffuseInstructions;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.E)) {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, 2f)) {
                if (hit.transform.tag == "Door") {
                    if ((hit.transform.GetComponent<Door>().locked && GetComponentInParent<PlayerData>().HaveKey) || !hit.transform.GetComponent<Door>().locked) {
                        hit.transform.GetComponent<Door>().OpenDoor();
                    }
                } else if (hit.transform.name == "Key") {
                    Destroy(hit.transform.gameObject);
                    GetComponentInParent<PlayerData>().SetHaveKey(true);
                    AudioManager.instance.GetSound("pickup").Play();
            } else if (hit.transform.name == "Screwdriver") {
                    Destroy(hit.transform.gameObject);
                    GetComponentInParent<PlayerData>().SetHaveScrewdriver(true);
                    AudioManager.instance.GetSound("pickup").Play();
                } else if (hit.transform.name == "printer") {
                    if (GetComponentInParent<PlayerData>().HavePrintedPicture) {
                        GetComponentInParent<PlayerData>().SetHavePicture(true);
                        picOfBob.SetActive(false);
                        AudioManager.instance.GetSound("pickup").Play();
                    }
                } else if (hit.transform.tag == "Computer") {
                    GetComponentInParent<PlayerMovement>().enabled = !GetComponentInParent<PlayerMovement>().enabled;
                    GetComponentInParent<Rigidbody>().isKinematic = !GetComponentInParent<Rigidbody>().isKinematic;
                    GetComponent<CameraLook>().enabled = !GetComponent<CameraLook>().enabled;
                    computerScreen.GetComponent<RawImage>().enabled = !computerScreen.GetComponent<RawImage>().enabled;

                    ComputerScreen.Computers type = ComputerScreen.Computers.Broken;
                    switch (hit.transform.name) {
                        case "Bob":
                            type = ComputerScreen.Computers.Bob;
                            AudioManager.instance.GetSound("computerBeep").Play();

                            break;

                        case "Joe":
                            type = ComputerScreen.Computers.Joe;
                            AudioManager.instance.GetSound("computerBeep").Play();

                            break;

                        case "Tom":
                            type = ComputerScreen.Computers.Tom;
                            AudioManager.instance.GetSound("computerBeep").Play();

                            break;

                        case "Broken":
                            type = ComputerScreen.Computers.Broken;
                            break;
                    }

                    computerScreen.GetComponent<ComputerScreen>().OnBootUp(type);

                    Cursor.lockState = Cursor.lockState == CursorLockMode.Locked ? CursorLockMode.None : CursorLockMode.Locked;
                } else if (hit.transform.tag == "VentGrate") {
                    if (GetComponentInParent<PlayerData>().HaveScrewdriver) {
                        hit.transform.GetComponent<Rigidbody>().isKinematic = false;
                        hit.transform.GetComponent<Rigidbody>().useGravity = true;
                        hit.transform.GetComponent<Rigidbody>().AddForce(-20, 0, 0);
                        AudioManager.instance.GetSound("ventRemoved").Play();
                    }
                } else if (hit.transform.name == "note") {
                    GetComponentInParent<PlayerMovement>().enabled = !GetComponentInParent<PlayerMovement>().enabled;
                    GetComponentInParent<Rigidbody>().isKinematic = !GetComponentInParent<Rigidbody>().isKinematic;
                    GetComponent<CameraLook>().enabled = !GetComponent<CameraLook>().enabled;
                    note.GetComponent<RawImage>().enabled = !note.GetComponent<RawImage>().enabled;
                    AudioManager.instance.GetSound("notePickup").Play();
                } else if (hit.transform.name == "faceScanner") {
                    if (GetComponentInParent<PlayerData>().HavePicture) {
                        FindObjectOfType<Elevator>().OpenDoors();
                        AudioManager.instance.GetSound("accessGranted").Play();
                    } else {
                        AudioManager.instance.GetSound("accessDenied").Play();
                    }
                } else if (hit.transform.name == "button") {
                    bombDiffusion.GetComponent<BombDiffusion>().BlowUp();
                } else if (hit.transform.name == "bomb") {
                    bombDiffusion.GetComponent<RawImage>().enabled = true;
                    GetComponentInParent<PlayerMovement>().enabled = false;
                    GetComponentInParent<Rigidbody>().isKinematic = true;
                    GetComponent<CameraLook>().enabled = false;
                    bombDiffusion.GetComponent<BombDiffusion>().StartDiffusionProcess();
                    Cursor.lockState = CursorLockMode.None;
                } else if (hit.transform.name == "chalkboard") {
                    hit.transform.GetComponent<Rigidbody>().isKinematic = false;
                    hit.transform.GetComponent<Rigidbody>().useGravity = true;
                    hit.transform.GetComponent<Rigidbody>().AddForce(0, 0, -60);
                } else if (hit.transform.name == "bombDiffuseInstructions") {
                    GetComponentInParent<PlayerMovement>().enabled = !GetComponentInParent<PlayerMovement>().enabled;
                    GetComponentInParent<Rigidbody>().isKinematic = !GetComponentInParent<Rigidbody>().isKinematic;
                    GetComponent<CameraLook>().enabled = !GetComponent<CameraLook>().enabled;
                    diffuseInstructions.GetComponent<RawImage>().enabled = !diffuseInstructions.GetComponent<RawImage>().enabled;
                    AudioManager.instance.GetSound("notePickup").Play();
                } else {
                    AudioManager.instance.GetSound("interact").Play();
                }
            } else {
                AudioManager.instance.GetSound("interact").Play();
            }
        }
    }

}
