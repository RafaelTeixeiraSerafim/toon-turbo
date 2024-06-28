using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pausar : MonoBehaviour
{
    [SerializeField] private GameObject PainelMenuP;
    [SerializeField] private GameObject PainelOpcoes;

    private void Start()
    {
        PainelOpcoes.SetActive(false);
        PainelMenuP.SetActive(false);
    }
    public void Voltar()
    {
        PainelMenuP.SetActive(false);
        Time.timeScale = 1;
    }

    // Update is called once per frame  
    public void AbrirOpcoesPause()
    {
        PainelMenuP.SetActive(false);
        PainelOpcoes.SetActive(true);
    }

    public void FecharOpcoesPause()
    {
        PainelMenuP.SetActive(true);
        PainelOpcoes.SetActive(false);
    }

    public void VoltarMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void OpenPause()
    {
        PainelMenuP.SetActive(true);
        Time.timeScale = 0;
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            OpenPause();
        }
    }


}
