using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles first-person movement and controls.
/// </summary>
[RequireComponent(typeof(CameraController))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Stats))]
public class FirstPersonPlayerController : MonoBehaviour
{
    #region Variables - Camera
    [Header("Camera Attributes")]
    [Tooltip("When enabled, camera movement will be smoothed (mouse acceleration).")]
    [SerializeField] bool enableCamSmoothing = true;
    [Tooltip("How fast the camera moves.")]
    [SerializeField] float lookSpeed = 2.0f;
    [Tooltip("How responsive the camera is when smoothing is enabled.")]
    [Range(1f, 20f)] [SerializeField] float camResponsiveness = 10.0f;
    #endregion

    #region Variables - Player
    [Header("Player Attributes")]
    [Tooltip("The player's default speed.")]
    [System.Obsolete("This attribute has been deprecated. Use Stats instead.")] [HideInInspector] [SerializeField] float defaultSpeed = 10.0f;
    [Tooltip("The percentage increase in player speed when sprinting.")]
    [SerializeField] float sprintMultiplier = 0.5f;
    [Tooltip("The percentage decrease in player speed when strafing.")]
    [SerializeField] float strafeMultiplier = 0.2f;
    private float movementV;
    private float movementH;
    private float speed;

    bool sprint = false;
    bool godModeEnabled = false;
    float originalMoveSpeed;
    float originalMoveSpeedGM;

    float timePassed = 0;
    #endregion

    #region Variables - Physics
    [Header("Physics Attributes")]
    [Tooltip("The amount of vertical force applied to the player when the player jumps.")]
    [System.Obsolete("This attribute has been deprecated. Use Stats instead.")] [HideInInspector] [SerializeField] float jumpForce = 10.0f;
    [Tooltip("How fast the player will fall back down after jumping.")]
    [SerializeField] float fallMultiplier = 2.5f;
    [SerializeField] float jumpMultiplier = 2f;
    #endregion

    Rigidbody rb;
    Stats stats;

    // Start is called before the first frame update
    void Start()
    {
        stats = GetComponent<Stats>();
        rb = GetComponent<Rigidbody>();
        stats.currentMoveSpeed = stats.defaultMoveSpeed;
        originalMoveSpeed = stats.currentMoveSpeed;

        //Locks and hides the mouse cursor.
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        //Sets movement speed for each axis (should be the same)
        if (!godModeEnabled)
        {
            movementV = Input.GetAxisRaw("Vertical") * stats.currentMoveSpeed; //Forward/backward
            movementH = Input.GetAxisRaw("Horizontal") * (stats.currentMoveSpeed - stats.currentMoveSpeed * strafeMultiplier); //Side to side, or strafing
        }
        else
        {
            movementV = Input.GetAxisRaw("Vertical") * stats.godModeSpeed; //Forward/backward
            movementH = Input.GetAxisRaw("Horizontal") * (stats.godModeSpeed - stats.godModeSpeed * strafeMultiplier); //Side to side, or strafing
        }

        //Stops ability to move when game is paused
        movementV *= Time.deltaTime;
        movementH *= Time.deltaTime;


        if (movementV != 0 || movementH != 0)
        {
            if (1f + timePassed <= Time.time)
            {
                GetComponent<SoundManager>().PlayWalkSound();
                timePassed = Time.time;
                Debug.Log("Playing walking sounds.");
            }
        }

        //Jump function
        if (Input.GetKeyDown("space") && CanJump())
        {
            if (!godModeEnabled)
            {
                rb.AddForce(0, stats.defaultJumpForce, 0, ForceMode.Impulse);
                rb.velocity += Physics.gravity * (fallMultiplier - 1) * Time.deltaTime;
            }
        }

        //Sprint function
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (!sprint)
                originalMoveSpeed = stats.currentMoveSpeed;

            sprint = true;
            stats.currentMoveSpeed = originalMoveSpeed + originalMoveSpeed * sprintMultiplier;
        }
        else
        {
            stats.currentMoveSpeed = originalMoveSpeed;
            sprint = false;
        }

        //Flying function (during godmode)
        if (Input.GetKey("space") && godModeEnabled)
        {
            transform.Translate(0, 1, 0);
        }
        else if (Input.GetKey(KeyCode.LeftControl) && godModeEnabled)
        {
            transform.Translate(0, -1, 0);
        }

        //Enables or disables godmode (noclip). Purely for testing purposes.
        if (Input.GetKeyDown(KeyCode.RightBracket))
        {
            GodMode();
        }
    }

    private void FixedUpdate()
    {
        //Moves player
        transform.Translate(0, 0, movementV);
        transform.Translate(movementH, 0, 0);
    }

    /// <summary>
    /// Checks if player is/isn't in the air, if player is in the air then player cannot jump.
    /// </summary>
    /// <returns></returns>
    bool CanJump()
    {
        Ray ray = new Ray(transform.position, transform.up * -1);
        RaycastHit hit;

        if (godModeEnabled)
        {
            return true;
        }

        if (Physics.Raycast(ray, out hit, transform.localScale.y))
            return true;
        else
            return false;
    }

    /// <summary>
    /// Enables or disables godmode (noclip).
    /// </summary>
    void GodMode()
    {
        //If godmode is enabled, turn it off.
        if (godModeEnabled)
        {
            Debug.Log("Disabled godmode.");
            GetComponent<Rigidbody>().isKinematic = false;
            godModeEnabled = false;

            stats.currentMoveSpeed = originalMoveSpeedGM;
        }
        else
        {
            Debug.Log("Enabled godmode.");
            GetComponent<Rigidbody>().isKinematic = true;
            godModeEnabled = true;

            originalMoveSpeedGM = stats.currentMoveSpeed;
            stats.currentMoveSpeed = 20;
        }
    }

    #region Variable Return Functions
    /// <summary>
    /// Returns lookSpeed.
    /// </summary>
    /// <returns></returns>
    public float GetLookSpeed()
    {
        return lookSpeed;
    }

    /// <summary>
    /// Returns camResponsiveness.
    /// </summary>
    /// <returns></returns>
    public float GetCamResponsiveness()
    {
        return lookSpeed;
    }

    /// <summary>
    /// Returns whether camera smoothing is turned on or off.
    /// </summary>
    /// <returns></returns>
    public bool GetCamSmoothingEnabled()
    {
        return enableCamSmoothing;
    }
    #endregion




}
