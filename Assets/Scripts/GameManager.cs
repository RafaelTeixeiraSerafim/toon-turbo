using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Inst�ncia �nica do GameManager
    public static GameManager Instance { get; private set; }

    [SerializeField] private TrackCheckpoints trackCheckpoints;
    public TrackCheckpoints TrackCheckpoints { get => trackCheckpoints; }

    // M�todo chamado no in�cio da execu��o
    private void Awake()
    {
        // Se a inst�ncia j� existe e n�o � essa, destrua o GameObject para garantir o Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            // Define a inst�ncia �nica
            Instance = this;
            // Impede que o GameObject seja destru�do ao mudar de cena
            DontDestroyOnLoad(gameObject);
        }
    }

    public void PlayerFinishedLap(Enums.PlayerList player)
    {
      
    }
}