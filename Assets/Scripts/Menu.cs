using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject PainelMenuP;
    [SerializeField] private GameObject PainelOpções;
    [SerializeField] public GameObject Ambulance;
    [SerializeField] private GameObject Titulo;

    private void Start()
    {
        PainelOpções.SetActive(false);  
    }
    public void Jogar()
    {
        SceneManager.LoadScene(1);
    }

    // Update is called once per frame  
    public void AbrirOpcoes()
    {
        PainelMenuP.SetActive(false);
        PainelOpções.SetActive(true);
        Ambulance.SetActive(false);
        Titulo.SetActive(false);
    }

    public void FecharOpcoes()
    {
        PainelMenuP.SetActive(true);
        PainelOpções.SetActive(false);
        Ambulance.SetActive(true);
        Titulo.SetActive(true);
    }

    public void SairJogo()
    {
        Debug.Log("Sair do Jogo");
        Application.Quit();
    }
    

}
