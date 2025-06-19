using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _velocity = 5f;
    [SerializeField] private float _rotationSpeed = 10f;

    private AudioSource audioSource;
    [SerializeField] private AudioClip flap;
    [SerializeField] private AudioClip crash;

    private Rigidbody2D _rb;
    private bool _jump;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.wasPressedThisFrame)// || Mouse.current.leftButton.wasPressedThisFrame)
        {
            _jump = true;
        }
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus)
        {
            InputSystem.ResetHaptics(); // Optional, resets input devices
            InputSystem.Update();       // Force update input system
            Input.ResetInputAxes();     // Reset old inputs
        }
    }

    private void OnApplicationPause(bool pause)
    {
        if (!pause) // App resumed
        {
            InputSystem.Update();
            Input.ResetInputAxes();
        }
    }

    private void FixedUpdate()
    {
        if (_jump)
        {
            audioSource.clip = flap;
            audioSource.Play();

            // Set a fixed vertical velocity
            _rb.linearVelocityY = _velocity; // No scaling based on screen

            _jump = false;
        }

        // Rotate based on velocity (you might not need deltaTime here because it's based on velocity)
        transform.rotation = Quaternion.Euler(0, 0, _rb.linearVelocityY * _rotationSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        audioSource.clip = crash;
        audioSource.Play();
        GameManager.instance.GameOver();
    }
}
