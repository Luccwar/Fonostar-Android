using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioController : MonoBehaviour
{

    public AudioSource sourceMusic;
    public AudioSource sourceFx;

    [Header("Musicas")]
    public AudioClip MusicaTitulo;
    public AudioClip MusicaFase1;

    [Header("FX")]
    public AudioClip FxClick;

    //Configurações dos audios
    private float volumeMaximoMusica;
    private float volumeMaximoFx;

    //Configurações da troca de música
    private AudioClip novaMusica;
    private string novaCena;
    private bool trocarCena;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        // Carrega as configurações de audio do aparelho
        volumeMaximoMusica = 1;
        volumeMaximoFx = 1;

        TrocarMusica(MusicaTitulo, "MenuPrincipal", true);
    }

    public void TrocarMusica(AudioClip clip, string nomeCena, bool mudarCena)
    {
        novaMusica = clip;
        novaCena = nomeCena;
        trocarCena = mudarCena;

        StartCoroutine("ChangeMusic");
    }

    IEnumerator ChangeMusic()
    {
        for(float volume = volumeMaximoMusica; volume>= 0; volume -= 0.1f)
        {
            yield return new WaitForSeconds(0.1f);
            sourceMusic.volume = volume;
        }
        sourceMusic.volume = 0;
        sourceMusic.clip = novaMusica;
        sourceMusic.Play();

        for(float volume = 0; volume < 0; volume += 0.1f)
        {
            yield return new WaitForSeconds(0.1f);
            sourceMusic.volume = volume;
        }
        sourceMusic.volume = volumeMaximoMusica;

        if(trocarCena)
        {
            SceneManager.LoadScene(novaCena);
        }
    }

}
