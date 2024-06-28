using UnityEngine;

public class WheelController : MonoBehaviour
{
    public Transform wheelModel;

    [HideInInspector] public WheelCollider WheelCollider;

    // Create properties for the CarControl script
    // (You should enable/disable these via the 
    // Editor Inspector window)
    public bool steerable;
    public bool motorized;
    public float yRotation;
    public float zRotation;

    Vector3 position;
    Quaternion rotation;

    // Start is called before the first frame update
    private void Awake()
    {
        WheelCollider = GetComponent<WheelCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get the Wheel collider's world pose values and
        // use them to set the wheel model's position and rotation
        WheelCollider.GetWorldPose(out position, out rotation);
        wheelModel.transform.position = position;
        wheelModel.transform.rotation = rotation;
        wheelModel.transform.Rotate(0, yRotation, zRotation);
    }
}