using System.Collections;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    public float forwardSpeed = 25f, strafeSpeed = 7.5f, hoverSpeed = 5f;
    private float activeForwardSpeed, activeStrafeSpeed, activeHoverSpeed;
    private float forwardAcceleration = 2.5f, strafeAcceleration = 2f, hoverAcceleration = 2f;

    public float lookRateSpeed = 90f;
    private Vector2 lookInput, screenCenter, mouseDistance;

    private float rollInput;
    public float rollSpeed = 90f, rollAcceleration = 3.5f;

    private Rigidbody rb;

    public float bounceForce = 10f; // Adjust the value as needed

    public float boostSpeedMultiplier = 1.5f; // Speed multiplier when boosting
    public float boostFuelConsumptionRate = 1f; // Fuel consumption rate per second
    public float boostFuelRefillTime = 10f; // Time to refill the boost fuel in seconds
    private bool isBoosting = false;
    private float currentBoostFuel;

    private bool isSpinning = false; // Track if the player is spinning
    private Quaternion initialRotation; // Initial rotation of the player

    // Start is called before the first frame update
    void Start()
    {
        screenCenter.x = Screen.width * .5f;
        screenCenter.y = Screen.height * .5f;

        Cursor.lockState = CursorLockMode.Confined;

        rb = GetComponent<Rigidbody>();
        rb.useGravity = false; // Disable gravity for better control

        currentBoostFuel = boostFuelRefillTime; // Initialize boost fuel to full

        initialRotation = transform.rotation; // Store the initial rotation
    }

    // Update is called once per frame
    void Update()
    {
        lookInput.x = Input.mousePosition.x;
        lookInput.y = Input.mousePosition.y;

        mouseDistance.x = (lookInput.x - screenCenter.x) / screenCenter.y;
        mouseDistance.y = (lookInput.y - screenCenter.y) / screenCenter.y;

        mouseDistance = Vector2.ClampMagnitude(mouseDistance, 1f);

        rollInput = Mathf.Lerp(rollInput, Input.GetAxisRaw("Roll"), rollAcceleration * Time.deltaTime);

        //transform.Rotate(-mouseDistance.y * lookRateSpeed * Time.deltaTime, mouseDistance.x * lookRateSpeed * Time.deltaTime, 0f, Space.Self);

        transform.Rotate(-mouseDistance.y * lookRateSpeed * Time.deltaTime, mouseDistance.x * lookRateSpeed * Time.deltaTime, rollInput * rollSpeed * Time.deltaTime, Space.Self);

        float boostMultiplier = isBoosting ? boostSpeedMultiplier : 1f; // Apply boost speed multiplier if currently boosting

        activeForwardSpeed = Mathf.Lerp(activeForwardSpeed, Input.GetAxisRaw("Vertical") * forwardSpeed * boostMultiplier, forwardAcceleration * Time.deltaTime);
        activeStrafeSpeed = Mathf.Lerp(activeStrafeSpeed, Input.GetAxisRaw("Horizontal") * strafeSpeed * boostMultiplier, strafeAcceleration * Time.deltaTime);
        activeHoverSpeed = Mathf.Lerp(activeHoverSpeed, Input.GetAxisRaw("Hover") * hoverSpeed * boostMultiplier, hoverAcceleration * Time.deltaTime);

        transform.position += transform.forward * activeForwardSpeed * Time.deltaTime;
        transform.position += (transform.right * activeStrafeSpeed * Time.deltaTime) + (transform.up * activeHoverSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            StartBoost();
        }
        else
        {
            EndBoost();
        }

        if (isBoosting)
        {
            currentBoostFuel -= boostFuelConsumptionRate * Time.deltaTime; // Consume boost fuel over time

            if (currentBoostFuel <= 0f)
            {
                currentBoostFuel = 0f;
                EndBoost(); // Stop boosting when fuel is depleted
            }
        }
        else
        {
            currentBoostFuel += Time.deltaTime / boostFuelRefillTime; // Refill boost fuel over time

            if (currentBoostFuel >= 1f)
            {
                currentBoostFuel = 1f;
            }
        }

        if (isSpinning)
        {
            // Add your spinning logic here
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            StopSpinning();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Barrier"))
        {
            // Calculate the force direction opposite to the collision normal
            Vector3 forceDirection = -collision.contacts[0].normal;

            // Apply the force to the player's Rigidbody
            rb.AddForce(forceDirection * bounceForce, ForceMode.Impulse);

            isSpinning = true; // Start spinning when hitting the barrier
        }
    }

    public void StartBoost()
    {
        if (currentBoostFuel > 0f)
        {
            isBoosting = true;
        }
    }

    public void EndBoost()
    {
        isBoosting = false;
    }

    public void StopSpinning()
    {
        isSpinning = false; // Stop spinning

        // Reset rotation and angular velocity without changing position
        transform.rotation = initialRotation;
        rb.angularVelocity = Vector3.zero;
    }
}