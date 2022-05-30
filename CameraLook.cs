using UnityEngine;

public class CameraLook : MonoBehaviour {

    private float verticalRotation;
    private float horrizontalRotation;
    private float sensetivity = 200;

    private Transform player;

    private void Start() {
        player = transform.parent;

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update() {
        verticalRotation -= Input.GetAxis("Mouse Y") * sensetivity * Time.deltaTime;
        horrizontalRotation += Input.GetAxis("Mouse X") * sensetivity * Time.deltaTime;

        verticalRotation = Mathf.Clamp(verticalRotation, -90, 90);

        transform.localRotation = Quaternion.Euler(new Vector3(verticalRotation, transform.rotation.y, transform.rotation.z));
        player.rotation = Quaternion.Euler(player.rotation.x, horrizontalRotation, player.rotation.z);
    }

}
