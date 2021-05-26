using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;                                            
    public float moveSpeed, gravityModifier, jumpPower, sprintSpeed = 16f;
    public CharacterController charCon;
    private Vector3 moveInput;
    public Transform camTrans;
    public float mouseSensitivity;
    public bool invertX, invertY;
    private bool canIJump = false, canIDoubleJump = false;
    public Transform groundCheckPoint;
    public LayerMask whatIsGround;
    public Animator anim;
    public Transform firePoint;
    public Gun activeGun;
    public List<Gun> allGuns = new List<Gun>();                                           // Public list of guns
    public int currentGun;
    public float maxViewAngle = 60f;
    public AudioSource footstepFast, footstepSlow;

    // Start before Start() | when unity starts
    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentGun--;                                                                   // Incrementarea de la SwitchGun()
        SwitchGun();    
    }

    // Update is called once per frame
    void Update()
    {
        if (!UIController.instance.pauseScreen.activeInHierarchy && !GameManager.instance.levelEnding)                  // As long as the pause menu isn t active in our scene
        {
            // Storing velocity - save moveInput.y from the last frame
            float yStore = moveInput.y;

            Vector3 vertMove = transform.forward * Input.GetAxis("Vertical");                        // Vertical keywords - moves forward & backwars
            Vector3 horiMove = transform.right * Input.GetAxis("Horizontal");                        // Horizontal keywords - moves left & right
            moveInput = vertMove + horiMove;
            moveInput.Normalize();                                                      // Moving won t be more that should be. Diagonal distance fix

            // move || sprint
            if (Input.GetKey(KeyCode.LeftShift))                                        // If LSHIFT is hold down
            {
                moveInput *= sprintSpeed;                                               // Move speed = sprint speed
            }
            else
            {
                moveInput *= moveSpeed;                                                 // Move speed = defalut speed 
            }
            moveInput.y = yStore;                                        // Reapply to the moveInput
            moveInput.y += Physics.gravity.y * gravityModifier * Time.deltaTime;                // Adding gravity acceleration
 
            if(charCon.isGrounded)                                                  // If player is on the ground
            {
                moveInput.y = Physics.gravity.y * gravityModifier * Time.deltaTime;                           // Adding gravity acceleration
            }
            canIJump = Physics.OverlapSphere(groundCheckPoint.position, 0.25f, whatIsGround).Length > 0;       // Creates an imaginary sphere, it will check for objects within that sphere,
                                                                                                                                                //  in our case if it s ground
            // preset double jump
            if(canIJump == true)
            {
                canIDoubleJump = false;
            }

            // Handle Jumping
            if (Input.GetKeyDown(KeyCode.Space) && canIJump == true)                // When SPACE is pushed 
            {
                moveInput.y = jumpPower;                            // Player jumps
                canIDoubleJump = true;                          // Presets double jump
                AudioManager.instance.PlaySFX(4);                   // Play a specific sound effect
            }
            //Handle doubleJump
            else if(canIDoubleJump == true && Input.GetKeyDown(KeyCode.Space))                  // When SPACE is pushed again after jump
            {
                moveInput.y = jumpPower;                                            // Player jumps
                canIDoubleJump = false;                                         // Make sure you can jump the third time 
                AudioManager.instance.PlaySFX(4);                                   // Plays a specific sound
            }

            charCon.Move(moveInput * Time.deltaTime);                                 // Fps movement fix

            //Control camera rotation with the mouse + changing mouse sensitivity / sensitivity set by player
            Vector2 mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")) * mouseSensitivity;  

            //Invert mouse axis
            if (invertX == true)            
            {
                mouseInput.x = -mouseInput.x;
            }
            if (invertY == true)
            {
                mouseInput.y = -mouseInput.y;
            }

            // Converts quad values to basic trio (doesn t let the player rotate vertically with the hole body)
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + mouseInput.x, transform.rotation.eulerAngles.z); 

            // Converts quad values to basic trio
            camTrans.rotation = Quaternion.Euler(camTrans.rotation.eulerAngles + new Vector3(-mouseInput.y, 0f, 0f));

            // Fixes the bug with rotative camera by not letting the player look down & up at certain coordinates
            if (camTrans.rotation.eulerAngles.x > maxViewAngle && camTrans.rotation.eulerAngles.x < 180f)
            {
                camTrans.rotation = Quaternion.Euler(maxViewAngle, camTrans.rotation.eulerAngles.y, camTrans.rotation.eulerAngles.z);
            } 
            else if (camTrans.rotation.eulerAngles.x > 180f && camTrans.rotation.eulerAngles.x < 360f - maxViewAngle)
            {
                camTrans.rotation = Quaternion.Euler(360 - maxViewAngle, camTrans.rotation.eulerAngles.y, camTrans.rotation.eulerAngles.z);
            }

            // Handle Shooting Single rounds
            if (Input.GetMouseButtonDown(0))                            // If the mouse button 0 is pushed
            {
                RaycastHit hit;                                     
                if (Physics.Raycast(camTrans.position, camTrans.forward, out hit, 50f))             //  If we go from the center from the camera, forward, store it on hit, cheeck for 50
                {
                    if (Vector3.Distance(camTrans.position, hit.point) > 7f)                           // makes bullets not hit direct in the crosshair if player is too close to objects
                    {
                        firePoint.LookAt(hit.point);                                                        // Ray cast hit / aim for the crosshair
                    }
                } else
                {
                    firePoint.LookAt(camTrans.position + (camTrans.forward * 30f));                                      // Fix aiming at crosshair
                }
                FireShot();                                                                                 // Fires a shot by creating an bullet element and playing a specific music
            }

            // Handle Shooting Fullauto
            if (Input.GetMouseButton(0) && activeGun.canAutoFire)                                    // If the mouse button 0 is hold down & autoFire is true
            {
                if(activeGun.fireCounter <= 0)
                {
                    FireShot();                                                                         // Fires again & again if
                }
            }         
            if (Input.GetKeyDown(KeyCode.Tab))                                          // Switch gun on TAB press
            {
                SwitchGun();
            }
            if (Input.GetMouseButtonDown(1))
            {
                CameraController.instance.ZoomIn(activeGun.zoomAmount);                             // Zooms in with a specific amount
            }
            if (Input.GetMouseButtonUp(1))
            {
                CameraController.instance.ZoomOut();                                        // Zooms out to normal view
            }

            // Player is moving at a certain amount / applying animations
            anim.SetFloat("moveSpeed", moveInput.magnitude);
            anim.SetBool("onGround", canIJump);
        }
    }

    public void FireShot()
    {
        if(activeGun.currentAmmo > 0)                                                               // If the player has ammo
        {
            activeGun.currentAmmo--;                                                                            // Decreasing ammo
            Instantiate(activeGun.bullet, firePoint.position, firePoint.rotation);                                       // Creating the bullet
            activeGun.fireCounter = activeGun.fireRate;                                                             // Set the fire counter
            UIController.instance.ammoText.text = "AMMO: " + activeGun.currentAmmo;                              // Recalculate the ammo and display it on the HUD
        }
    }

    public void SwitchGun()
    {
        activeGun.gameObject.SetActive(false);                                  // Deactivate the active gun
        currentGun ++;
        if (currentGun >= allGuns.Count)
        {
            currentGun = 0;
        }
        activeGun = allGuns[currentGun];                                                // Get the current gun 
        activeGun.gameObject.SetActive(true);                                           // Set the current gun
        UIController.instance.ammoText.text = "AMMO: " + activeGun.currentAmmo;             // Update the ammo on HUD for the each gun after switching
        firePoint.position = activeGun.firepoint.position;                                  // Fixes the firepoint bug
    }
}
