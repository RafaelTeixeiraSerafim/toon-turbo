using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class VehicleController : MonoBehaviour
{
    public float motorTorque = 2000;
    public float brakeTorque = 4000;
    public float maxSpeed = 30;
    public float steeringRange = 20;
    public float steeringRangeAtMaxSpeed = 10;
    public float centreOfGravityOffset = -2f;
    public bool invert = false;
    private int invertMult;
    private float vInput;
    private float hInput;
    private float frictionMult = 1;
    private bool firstDriftIter = true;

    //[Header("Drift Values")]
    public bool drifting;
    public int driftDirection;
    public float driftPower;

    //public float extremumSlip;
    //public float extremumValue;
    //public float asymptoteSlip;
    //public float asymptoteValue;
    //public float stiffness;

    public float rotate;
    public float downForce = 10;
    private float amount;
    private float currentSteerRange;
    private Vector3 forceDir;

    WheelController[] wheels;
    WheelFrictionCurve origForwCurveFront;
    WheelFrictionCurve origSideCurveFront;
    WheelFrictionCurve origForwCurveBack;
    WheelFrictionCurve origSideCurveBack;

    Rigidbody rigidBody;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();

        // Find all child GameObjects that have the WheelController script attached
        wheels = GetComponentsInChildren<WheelController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // Adjust center of mass vertically, to help prevent the car from rolling
        rigidBody.centerOfMass += Vector3.up * centreOfGravityOffset;

        origForwCurveFront = wheels[0].WheelCollider.forwardFriction;
        origSideCurveFront = wheels[0].WheelCollider.sidewaysFriction;

        origForwCurveBack = wheels[2].WheelCollider.forwardFriction;
        origSideCurveBack = wheels[2].WheelCollider.sidewaysFriction;

        if (invert)
            invertMult = -1;
        else
            invertMult = 1;
    }

    // Update is called once per frame
    void Update()
    {

        vInput = Input.GetAxis("Vertical") * invertMult;
        hInput = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump") && !drifting && hInput != 0)
        {
            drifting = true;
            driftDirection = hInput > 0 ? 1 : -1;
            firstDriftIter = true;
        }

        if (Input.GetButtonUp("Jump") && drifting)
        {
            drifting = false;
            frictionMult = 1;
        }


        if (drifting)
        {
            amount = (driftDirection == 1) ? ExtensionMethods.Remap(Input.GetAxis("Horizontal"), -1, 1, 0, 2.2f) : ExtensionMethods.Remap(Input.GetAxis("Horizontal"), -1, 1, 2.2f, 0);
            //float powerControl = (driftDirection == 1) ? ExtensionMethods.Remap(Input.GetAxis("Horizontal"), -1, 1, .2f, 1) : ExtensionMethods.Remap(Input.GetAxis("Horizontal"), -1, 1, 1, .2f);
            //Steer(amount);
            //amount += powerControl;
        }

        Debug.Log(frictionMult);
    }

    private void FixedUpdate()
    {
        // Calculate current speed in relation to the forward direction of the car
        // (this returns a negative number when traveling backwards)
        float forwardSpeed = Vector3.Dot(transform.forward, rigidBody.velocity) * invertMult;
        //Debug.Log(forwardSpeed);

        // Calculate how close the car is to top speed
        // as a number from zero to one
        float speedFactor = Mathf.InverseLerp(0, maxSpeed, forwardSpeed);
        //Debug.Log(speedFactor);

        // Use that to calculate how much torque is available       
        // (zero torque at top speed)
        float currentMotorTorque = Mathf.Lerp(motorTorque, 0, speedFactor);

        // …and to calculate how much to steer 
        // (the car steers more gently at top speed)
        if (!drifting)
        {
            currentSteerRange = Mathf.Lerp(steeringRange, steeringRangeAtMaxSpeed, speedFactor);
        }

        // Check whether the user input is in the same direction 
        // as the car's velocity
        bool isAccelerating = Mathf.Sign(vInput * invertMult) == Mathf.Sign(forwardSpeed);

        rigidBody.AddForce(Vector3.down * downForce, ForceMode.Acceleration);

        foreach (var wheel in wheels)
        {
            if (drifting)
            {
                if (firstDriftIter)
                {
                    frictionMult = Mathf.Lerp(1f, 4f, speedFactor);
                }

                //Debug.Log(frictionMult);
                if (wheel.steerable)
                {
                    SetupDriftValues(wheel, origForwCurveFront, origSideCurveFront, frictionMult);
                }
                else
                {
                    SetupDriftValues(wheel, origForwCurveBack, origSideCurveBack, frictionMult);
                }

            }
            else
            {
                if (wheel.steerable)
                {
                    wheel.WheelCollider.forwardFriction = origForwCurveFront;
                    wheel.WheelCollider.sidewaysFriction = origSideCurveFront;
                }
                else
                {
                    wheel.WheelCollider.forwardFriction = origForwCurveBack;
                    wheel.WheelCollider.sidewaysFriction = origSideCurveBack;
                }

                wheel.WheelCollider.brakeTorque = 0;

            }
            // Apply steering to Wheel colliders that have "Steerable" enabled
            if (wheel.steerable)
            {
                wheel.WheelCollider.steerAngle = hInput * currentSteerRange;
            }

            if (isAccelerating)
            {
                // Apply torque to Wheel colliders that have "Motorized" enabled
                if (wheel.motorized)
                {
                    wheel.WheelCollider.motorTorque = vInput * currentMotorTorque;
                }
                wheel.WheelCollider.brakeTorque = 0;
            }
            else
            {
                // If the user is trying to go in the opposite direction
                // apply brakes to all wheels
                wheel.WheelCollider.brakeTorque = Mathf.Abs(vInput) * brakeTorque;
                wheel.WheelCollider.motorTorque = 0;
            }
        }
        
        if (drifting && firstDriftIter)
        {
            firstDriftIter = false;
        }
    }

    public void Steer(float amount)
    {
        currentSteerRange = steeringRange * amount;
    }

    public void SetupDriftValues(WheelController wheel, WheelFrictionCurve origForwCurve, WheelFrictionCurve origSideCurve, float frictionMult)
    {
        WheelFrictionCurve newCurve = origForwCurve;

        //newCurve.stiffness = 1f * frictionMult;
        //wheel.WheelCollider.forwardFriction = newCurve;

        newCurve = origSideCurve;

        newCurve.extremumSlip = 1;
        newCurve.extremumValue = 1 / frictionMult;
        if (wheel.steerable)
        {
            newCurve.stiffness = 1f * frictionMult;
        }
        else
        {
            newCurve.stiffness = 0.95f * frictionMult;
            wheel.WheelCollider.brakeTorque = 100000;

        }
        wheel.WheelCollider.sidewaysFriction = newCurve;
    }
}