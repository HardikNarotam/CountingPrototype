using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class BallShooter : MonoBehaviour
{
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private Slider slider;
    private InputSystem_Actions input;
    private float rotateInput;
    [SerializeField] private float powerStrength;
    [SerializeField] private float rotateSpeed;

    public int availableBalls = 3;
    public Text availableBallsText;
    public Button shootButton;
    public Text gameOverText;

    public GameObject counter;
    private Counter counterScript;

    private void Awake()
    {
        input = new InputSystem_Actions();
    }

    private void Start()
    {
        counterScript = counter.GetComponent<Counter>();
    }

    private void OnEnable()
    {
        input.Player.Rotate.performed += OnRotate;
        input.Player.Rotate.canceled += OnRotate;
        input.Player.Rotate.Enable();
    }

    private void OnDisable()
    {
        input.Player.Rotate.performed -= OnRotate;
        input.Player.Rotate.canceled -= OnRotate;
        input.Player.Rotate.Disable();
    }

    private void OnRotate(InputAction.CallbackContext ctx)
    {
        rotateInput = ctx.ReadValue<float>();
    }

    private void Update()
    {
        float yaw = rotateInput * rotateSpeed * Time.deltaTime;
        transform.Rotate(0f, yaw, 0f);

        availableBallsText.text = "Balls Left: " + availableBalls;
    }

    private void ShootBall()
    {
        if (availableBalls <= 0)
            return;

        counterScript.scored = false;

        Vector3 spawnPos = transform.position + transform.forward * 0.5f;
        GameObject ball = Instantiate(ballPrefab, spawnPos, transform.rotation);

        Rigidbody rb = ball.GetComponent<Rigidbody>();
        Vector3 shootDir = Quaternion.Euler(0f, 0f, -45f) * transform.forward;
        rb.AddForce(shootDir * slider.value * powerStrength, ForceMode.Impulse);

        availableBalls--;

        if (availableBalls <= 0)
        {
            Invoke(nameof(CheckGameOver), 3f);
        }
    }

    private void CheckGameOver()
    {
        if (!counterScript.scored)
        {
            EndGame();
        }
    }
    public void EndGame()
    {
        Debug.Log("Game Over! No more balls left.");
        shootButton.interactable = false;
        gameOverText.gameObject.SetActive(true);
    }
}
