using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(AudioSource))]
public class FPSPlayer : MonoBehaviour {
    [Header("Movimiento")]
    public float speed = 5f;

    [Header("Mouse")]
    public float mouseSensitivity = 2f;
    private float yRotation = 0f;
    private float yVelocity = 0f;
    public float gravity = -9.81f;

    [Header("Audio")]
    public AudioClip footstepClip;

    private AudioSource audioSource;
    private CharacterController controller;
    private Camera playerCamera;

    [Header("Pause Menu")]
    public GameObject pauseMenuPrefab;
    private GameObject pauseMenuInstance;

    public bool isPaused = false;

    void Start() {

        // CREAR CÁMARA
        GameObject camObj = new GameObject("PlayerCamera");
        camObj.transform.parent = this.transform;
        camObj.transform.localPosition = new Vector3(0, 1.6f, 0);

        playerCamera = camObj.AddComponent<Camera>();
        playerCamera.nearClipPlane = 0.01f;

        // INSTANCIAR MENU DE PAUSA
        pauseMenuInstance = Instantiate(pauseMenuPrefab);

        Canvas canvas = pauseMenuInstance.GetComponentInChildren<Canvas>();

        if (canvas != null) {
            canvas.renderMode = RenderMode.ScreenSpaceCamera;
            canvas.worldCamera = playerCamera;
            canvas.planeDistance = 0.4f;
            canvas.sortingOrder = 100;
        }

        //CONECTAR MENU CON PLAYER
        PauseMenuManager pauseManager = pauseMenuInstance.GetComponent<PauseMenuManager>();

        if (pauseManager != null) {
            pauseManager.player = this;
        }

        // AUDIO
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = footstepClip;
        audioSource.loop = true;

        // CONTROLLER
        controller = GetComponent<CharacterController>();

        // CURSOR INICIAL (FPS)
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update() {

        // BLOQUEAR TODO SI ESTÁ EN PAUSA
        if (isPaused) return;

        HandleMouseLook();
        HandleMovement();
        HandleFootsteps();

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        yVelocity += gravity * Time.deltaTime;
        move.y = yVelocity;

        controller.Move(move * Time.deltaTime);
    }

    void HandleMovement() {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);
    }

    void HandleMouseLook() {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * 100f * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * 100f * Time.deltaTime;

        yRotation -= mouseY;
        yRotation = Mathf.Clamp(yRotation, -90f, 90f);

        playerCamera.transform.localRotation = Quaternion.Euler(yRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    void HandleFootsteps() {
        bool isMoving = controller.velocity.magnitude > 0.1f && controller.isGrounded;

        if (isMoving) {
            if (!audioSource.isPlaying && footstepClip != null)
                audioSource.Play();
        } else {
            if (audioSource.isPlaying)
                audioSource.Stop();
        }
    }
}