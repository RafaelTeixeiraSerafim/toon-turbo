using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Instância única do GameManager
    public static GameManager Instance { get; private set; }

    [SerializeField] private TrackCheckpoints trackCheckpoints;
    public TrackCheckpoints TrackCheckpoints { get => trackCheckpoints; }

    // Método chamado no início da execução
    private void Awake()
    {
        // Se a instância já existe e não é essa, destrua o GameObject para garantir o Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            // Define a instância única
            Instance = this;
            // Impede que o GameObject seja destruído ao mudar de cena
            DontDestroyOnLoad(gameObject);
        }
    }

    public void PlayerFinishedLap(Enums.PlayerList player)
    {
      
    }
}