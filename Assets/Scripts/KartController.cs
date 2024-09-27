using UnityEngine;
using TMPro;

public class KartController : MonoBehaviour
{
    public Transform kartModel;
    public Rigidbody sphereRB;
    public Transform groundRay;

    float speedInput, currentSpeed;
    private float turnInput;
    private bool grounded;
    private float groundRoughness;
    private bool onRamp;
    private int coins = 0;
    private int tokenLap = 0;
    private float gravity = 10f;
    public int allowDrive = 1;
    private int actualLap = 1;
    private int checkpointsCollected = 0;
    public bool hasFinishedRace { get; private set; } = false;

    [Header("Parameters")]
    public string verticalInput = "Vertical";   // Nome do eixo para o movimento vertical
    public string horizontalInput = "Horizontal"; // Nome do eixo para o movimento horizontal
    public float maxAcceleration;
    public float steering;
    public float drag;
    public float maxWheelTurn;
    public float coinBoostDivider;
    public TextMeshProUGUI coinText;
    public LayerMask layerMask;
    public Transform[] wheels;

    [SerializeField] private Enums.PlayerList actualPlayer;
    [SerializeField] private LapFinishUI lapFinishUI;
    [SerializeField] private GameObject WrongWayP1;
    [SerializeField] private GameObject WrongWayP2;
    [SerializeField] private Timer timer;
    public Enums.PlayerList ActualPlayer { get => actualPlayer; }
    public int CheckpointsCollected { get => checkpointsCollected; }

    void Start()
    {
        sphereRB.transform.parent = null;
        InvokeRepeating("SubtractCoin", 0f, 5f);
        lapFinishUI.UpdateLapText(actualLap);
    }

    void Update()
    {
        //Follow Collider
        transform.position = sphereRB.transform.position;

        //Accelerate
        speedInput = Input.GetAxis(verticalInput) * (maxAcceleration * (1f + coins / coinBoostDivider)) * allowDrive;
        if (Input.GetAxis(verticalInput) < 0)
        {
            speedInput /= 2;
        }

        turnInput = Input.GetAxis(horizontalInput);

        currentSpeed = Mathf.SmoothStep(currentSpeed, speedInput, Time.deltaTime * 2f); speedInput = 0f;

        //Steering
        if (grounded)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, turnInput * steering * Time.deltaTime * Input.GetAxis(verticalInput), 0f));
        }

        coinText.text = coins.ToString();

        //Animations

        //a) Kart
        kartModel.localRotation = Quaternion.Euler(kartModel.rotation.x, transform.rotation.y, kartModel.rotation.z);

        //b) Wheels
        foreach (Transform wheel in wheels)
        {
            WheelParams wheelParams = wheel.GetComponent<WheelParams>();
            if (wheelParams.steerable && turnInput != 0)
            {
                //Debug.Log(turnInput * maxWheelTurn);
                //wheel.Rotate(Vector3.up, turnInput * maxWheelTurn, Space.Self);
                wheel.localRotation = Quaternion.Euler(wheel.localRotation.eulerAngles.x, turnInput * maxWheelTurn, wheel.localRotation.eulerAngles.z);
                //wheel.SetLocalPositionAndRotation(Vector3.zero, Quaternion.Euler(wheel.localRotation.eulerAngles.x, turnInput * maxWheelTurn, wheel.localRotation.eulerAngles.z));
            }

            if (wheel.localRotation.eulerAngles.x > -90.2f && wheel.localRotation.eulerAngles.x < 89.8f)
            {
                wheel.Rotate(Vector3.right, currentSpeed * Time.deltaTime * 20, Space.Self);
            }
            else
            {
                Transform wheelParent = wheel.GetComponentInParent<Transform>();
                wheelParent.localRotation = wheel.localRotation;
                wheel.localRotation = Quaternion.Euler(89.8f, wheel.localRotation.eulerAngles.y, wheel.localRotation.eulerAngles.z);
            }
        }
    }

    private void FixedUpdate()
    {
        grounded = false;

        if (Physics.Raycast(groundRay.position, -transform.up, out RaycastHit hit, 1f, layerMask))
        {
            groundRoughness = 1f;
            grounded = true;
            sphereRB.drag = drag;

            if (!hit.collider.CompareTag("Road"))
                groundRoughness = 0.75f;

            if (hit.collider.CompareTag("Ramp"))
            {
                if (!onRamp)
                {
                    onRamp = true;
                    currentSpeed *= 1.5f;
                }
            }
            else
            {
                onRamp = false;
            }

            Vector3 normal = Vector3.Lerp(transform.up, hit.normal, Time.deltaTime * 5.0f);
            transform.rotation = Quaternion.FromToRotation(transform.up, normal) * transform.rotation;
        }
        else
        {
            sphereRB.drag = 0;

            Vector3 normal = Vector3.Lerp(transform.up, Vector3.up, Time.deltaTime * 1f);
            transform.rotation = Quaternion.FromToRotation(transform.up, normal) * transform.rotation;
        }

        if (grounded)
        {
            //Forward Acceleration
            sphereRB.AddForce(transform.forward * currentSpeed / 2 * groundRoughness, ForceMode.Acceleration);
        }

        //Gravity
        sphereRB.AddForce(Vector3.down * gravity, ForceMode.Acceleration);
    }

    public void CoinBoost()
    {
        coins = Mathf.Min(coins + 1, 10);
    }

    private void SubtractCoin()
    {
        coins = Mathf.Max(coins - 1, 0);
    }


    public void CollectCheckpoint()
    {
        switch (ActualPlayer)
        {
            case Enums.PlayerList.PlayerOne:
                WrongWayP1.gameObject.SetActive(false);
                break;
            case Enums.PlayerList.PlayerTwo:
                WrongWayP2.gameObject.SetActive(false);
                break;
        }
        checkpointsCollected++;
        if (CheckpointsCollected == 12)
        {
            GameManager.Instance.TrackCheckpoints.ActivateFinalCheckpoint(ActualPlayer);
        }
        else if (CheckpointsCollected == 13)
        {
            NextLap();
        }
    }

    private void NextLap()
    {
        GameManager.Instance.TrackCheckpoints.ReactivateCheckpoints(ActualPlayer);
        checkpointsCollected = 0;
        actualLap++;
        if (actualLap == 3)
        {
            Debug.Log(actualLap);
            if (actualPlayer == Enums.PlayerList.PlayerOne)
            {
                if (!GameManager.Instance.IsRaceFinished)
                    lapFinishUI.ShowVictoryText(1);
                else
                    lapFinishUI.ShowDefeatText(1);
            }
            else if (actualPlayer == Enums.PlayerList.PlayerTwo)
            {
                if (!GameManager.Instance.IsRaceFinished)
                    lapFinishUI.ShowVictoryText(2);
                else
                    lapFinishUI.ShowDefeatText(2);
            }
            timer.StopTimer();
            hasFinishedRace = true;
            GameManager.Instance.SetIsRaceFinished(true);
            return;
        }
        if (!hasFinishedRace)
            lapFinishUI.UpdateLapText(actualLap);
    }
}
