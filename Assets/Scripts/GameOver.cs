using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{

    private AudioController AC;
    // Start is called before the first frame update
    void Start()
    {
        AC = FindObjectOfType(typeof(AudioController)) as AudioController;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TentarNovamente(string NomeFase, int MusicaNumero)
    {
        AC.TrocarMusica(AC.MusicaFase[MusicaNumero], NomeFase, true);
    }

    public void VoltarAoMenu()
    {
        AC.TrocarMusica(AC.MusicaTitulo, "MenuPrincipal", true);
    }
}
