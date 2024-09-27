using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Inst�ncia �nica do GameManager
    public static GameManager Instance { get; private set; }

    [SerializeField] private TrackCheckpoints trackCheckpoints;
    public TrackCheckpoints TrackCheckpoints { get => trackCheckpoints; }
    public bool IsRaceFinished { get; private set; } = false;

    // M�todo chamado no in�cio da execu��o
    private void Awake()
    {
        // Se a inst�ncia j� existe e n�o � essa, destrua o GameObject para garantir o Singleton
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);

        }
        //else if (Instance != this)
        //{
        //    Destroy(Instance);
        //}
        //else
        //{
            // Define a inst�ncia �nica
            Instance = this;
            // Impede que o GameObject seja destru�do ao mudar de cena
        //}
    }

    public void PlayerFinishedLap(Enums.PlayerList player)
    {
      
    }

    public void SetIsRaceFinished(bool isRaceFinished)
    {
        this.IsRaceFinished = isRaceFinished;
    }
}