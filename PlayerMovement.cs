using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    private Rigidbody rb;

    private float forwardsMovement;
    private float strafeMovement;

    private float speed = 1, strafeSpeed = 1;
    private const float MAX_SPEED = 100;
    private float acceleration = 8f;

    private float lastForwardMovementDir;
    private float lastStrafeMovementDir;

    private bool crouching;

    private bool grounded;
    private bool aired;
    private float airedTimer;
    private bool spacePressed;
    public Transform groundCheck;


    private void Start() {
        rb = GetComponent<Rigidbody>();
        AudioManager.instance.GetSound("footsteps").loop = true;
        AudioManager.instance.GetSound("ventWalk").loop = true;
    }

    private void Update() { 
        if (Input.GetKeyDown(KeyCode.LeftControl)) {
            crouching = !crouching;

            transform.localScale = new Vector3(1, crouching ? .5f : 1, 1);
        }

        spacePressed = Input.GetKey(KeyCode.Space);
       
        if (aired) {
            airedTimer += Time.deltaTime;
            if (airedTimer > .1f) {
                aired = false;
                airedTimer = 0;
            }
        }
    }

    private void FixedUpdate() {
        forwardsMovement = (Input.GetAxisRaw("Vertical") + lastForwardMovementDir) * speed * Time.fixedDeltaTime;
        strafeMovement = (Input.GetAxisRaw("Horizontal") + lastStrafeMovementDir) * strafeSpeed * 0.5f * Time.fixedDeltaTime;

        if (Mathf.Abs(Input.GetAxisRaw("Vertical")) > 0) {
            lastForwardMovementDir = Mathf.Sign(Input.GetAxisRaw("Vertical"));

            if (speed < MAX_SPEED) {
                speed += acceleration;
            }

            if (transform.position.x < 3.98f) {
                if (!AudioManager.instance.GetSound("footsteps").isPlaying) {
                    AudioManager.instance.GetSound("footsteps").Play();
                }
            } else {
                if (!AudioManager.instance.GetSound("ventWalk").isPlaying) {
                    AudioManager.instance.GetSound("ventWalk").Play();
                }
            }
        } else if (Input.GetAxisRaw("Vertical") == 0) {
            if (speed > 1) {
                speed -= acceleration * 3;
            } else {
                speed = 1;
                lastForwardMovementDir = 0;
            }

            if (AudioManager.instance.GetSound("footsteps").isPlaying) {
                AudioManager.instance.GetSound("footsteps").Stop();
            }

            if (AudioManager.instance.GetSound("ventWalk").isPlaying) {
                AudioManager.instance.GetSound("ventWalk").Stop();
            }
        }

        if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0) {
            lastStrafeMovementDir = Mathf.Sign(strafeMovement);

            if (strafeSpeed < MAX_SPEED) {
                strafeSpeed += acceleration;
            }
        } else if (Input.GetAxisRaw("Horizontal") == 0) {
            if (strafeSpeed > 1) {
                strafeSpeed -= acceleration * 3;
            } else {
                strafeSpeed = 1;
                lastStrafeMovementDir = 0;
            }
        }

        rb.velocity = (forwardsMovement * transform.forward + strafeMovement * transform.right) * (crouching ? .7f : 1) + new Vector3(0, rb.velocity.y, 0);

        grounded = Physics.Raycast(groundCheck.position, -transform.up, 0.01f);


        if (spacePressed && grounded && !aired) {
            rb.AddForce(new Vector3(0, 3f, 0), ForceMode.VelocityChange);
            aired = true;
        }

    }

}
