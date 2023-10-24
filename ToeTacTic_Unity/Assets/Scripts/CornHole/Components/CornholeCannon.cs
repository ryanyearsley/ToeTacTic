using UnityEngine;
using UnityEngine.UI;

public class CornholeCannon : MonoBehaviour
{
    public Transform bagSpawnTransform;
    public Transform horizontalPivotTransform;
    public Transform verticalPivotTransform;
    public GameObject cornholeBagPrefab;
    public Image forceIndicatorFill;
    public Image angleIndicatorFill;
    public float minCannonForce = 500.0f;
    public float maxCannonForce = 1000.0f; 
    public float forceIncreaseRate = 500.0f; // Rate at which force increases per second

    public float rotateSpeed = 50f;

    public float maxHorizontalAngle = 45.0f;
    public float minHorizontalAngle = -45.0f;
    public float maxVerticalAngle = 45.0f;
    public float minVerticalAngle = -5.0f;

    private bool isCharging; 
    private float currentCannonForce = 500.0f; // Initial cannon force
    private float xRotation = 0f;
    private float yRotation = 0f;
    void Update()
    {
        RotateCannon();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCharging();
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            ShootCannon();
        }

        if (isCharging)
        {
            ChargeCannon();
        }
    }

    void RotateCannon()
    {
        //Horizontal
        float horizontalInput = Input.GetAxis("Horizontal") * rotateSpeed * Time.deltaTime;
        //horizontalPivotTransform.Rotate(Vector3.up * horizontalInput);
        yRotation += horizontalInput;
        yRotation = Mathf.Clamp(yRotation, minHorizontalAngle, maxHorizontalAngle);
        horizontalPivotTransform.localRotation = Quaternion.Euler(0, yRotation, 0);
        //Vertical
        float verticalInput = Input.GetAxis("Vertical") * rotateSpeed * Time.deltaTime;
        xRotation -= verticalInput;
        xRotation = Mathf.Clamp(xRotation, minVerticalAngle, maxVerticalAngle);
        verticalPivotTransform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        float angleFillAmount = 1 - ((xRotation - minVerticalAngle) / (maxVerticalAngle - minVerticalAngle));
        angleIndicatorFill.fillAmount = angleFillAmount;
    }
    void StartCharging()
    {
        isCharging = true;
        currentCannonForce = minCannonForce;
    }

    void ChargeCannon()
    {
        currentCannonForce += forceIncreaseRate * Time.deltaTime;
        currentCannonForce = Mathf.Clamp(currentCannonForce, minCannonForce, maxCannonForce); 
        // Calculate the fill amount based on the current cannon force
        float chargeFillAmount = (currentCannonForce - minCannonForce) / (maxCannonForce - minCannonForce);

        // Update the UI Image's fill amount to represent the charge level
        forceIndicatorFill.fillAmount = chargeFillAmount;
    }

    void ShootCannon()
    {
        isCharging = false;
        // Create a new instance of the projectile prefab
        GameObject newProjectile = Instantiate(cornholeBagPrefab, bagSpawnTransform.position, bagSpawnTransform.rotation);

        // Get the Rigidbody component of the projectile
        Rigidbody projectileRigidbody = newProjectile.GetComponent<Rigidbody>();

        if (projectileRigidbody != null)
        {
            // Apply force to the projectile to shoot it
            projectileRigidbody.AddForce(bagSpawnTransform.forward * currentCannonForce);
        }
    }

}
