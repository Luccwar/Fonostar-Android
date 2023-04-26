using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour
{
    public AudioMixer audioMixer;
    public AudioSource sourceMusic;
    public AudioSource sourceFX;

    [Header("Musicas")]
    public AudioClip MusicaTitulo;
    public AudioClip[] MusicaFase;

    [Header("FX")]

    public AudioClip PianoKey1;
    public AudioClip PianoKey2, PianoKey3, PianoKey4, PianoKey5, PianoKey6, PianoKey7, PianoKey8;


    //Configurações dos audios
    public float volumeMaximoMusica;
    public float volumeMaximoFX;

    //Configurações da troca de música
    private AudioClip novaMusica;
    private string novaCena;
    private bool trocarCena;
    public bool Dialogo;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        // Carrega as configurações de audio do aparelho
        if(!PlayerPrefs.HasKey("VolumeGeral"))
        {
            PlayerPrefs.SetFloat("VolumeGeral", -10);
        }
        if(!PlayerPrefs.HasKey("VolumeMusica"))
        {
            PlayerPrefs.SetFloat("VolumeMusica", 0.3f);
            PlayerPrefs.SetFloat("VolumeMusicaAntes", 0.3f);
        }
        if(!PlayerPrefs.HasKey("VolumeEfeitos"))
        {
            PlayerPrefs.SetFloat("VolumeEfeitos", 1f);
        }
        audioMixer.SetFloat("masterVolume", PlayerPrefs.GetFloat("VolumeGeral"));
        volumeMaximoMusica = PlayerPrefs.GetFloat("VolumeMusicaAntes");
        volumeMaximoFX = PlayerPrefs.GetFloat("VolumeEfeitos");

        PlayerPrefs.SetString("CenaAnterior", "");
        TrocarMusicaInicio(MusicaTitulo, "MenuPrincipal", true);
    }

    public void TrocarMusica(AudioClip clip, string nomeCena, bool mudarCena)
    {
        novaMusica = clip;
        novaCena = nomeCena;
        trocarCena = mudarCena;

        StartCoroutine("ChangeMusic");
    }

    public void TrocarMusicaInicio(AudioClip clip, string nomeCena, bool mudarCena)
    {
        novaMusica = clip;
        novaCena = nomeCena;
        trocarCena = mudarCena;

        StartCoroutine("Inicio");
    }

    public void TrocarCena(string nomeCena)
    {
        PlayerPrefs.SetString("CenaAnterior", SceneManager.GetActiveScene().name);
        SceneManager.LoadScene(nomeCena);
    }

    IEnumerator ChangeMusic()
    {
        if(SceneManager.GetActiveScene().name == "MenuPrincipal")
        {
            for(float volume = PlayerPrefs.GetFloat("VolumeMusica"); volume>= 0; volume -= 0.1f)
            {
                yield return new WaitForSeconds(0.1f);
                sourceMusic.volume = volume;
            }
            sourceMusic.volume = 0;
            sourceMusic.clip = novaMusica;
            sourceMusic.Play();
        }
        else
        {
            for(float volume = PlayerPrefs.GetFloat("VolumeMusicaAntes"); volume>= 0; volume -= 0.1f)
            {
                yield return new WaitForSeconds(0.1f);
                sourceMusic.volume = volume;
            }
            sourceMusic.volume = 0;
            sourceMusic.clip = novaMusica;
            sourceMusic.Play();
        }

        if(trocarCena)
        {
            SceneManager.LoadScene(novaCena);
        }

        for(float volume = 0; volume < PlayerPrefs.GetFloat("VolumeMusicaAntes"); volume += 0.1f)
        {
            yield return new WaitForSeconds(0.1f);
            sourceMusic.volume = volume;
        }
        sourceMusic.volume = PlayerPrefs.GetFloat("VolumeMusicaAntes");
    }

    IEnumerator Inicio()
    {
        for(float volume = 0; volume>= 0; volume -= 0.1f)
        {
            yield return new WaitForSeconds(0.1f);
            sourceMusic.volume = volume;
        }

        if(trocarCena)
        {
            SceneManager.LoadScene(novaCena);
        }
        
        sourceMusic.volume = 0;
        sourceMusic.clip = novaMusica;
        sourceMusic.Play();

        for(float volume = 0; volume < PlayerPrefs.GetFloat("VolumeMusicaAntes"); volume += 0.1f)
        {
            yield return new WaitForSeconds(0.1f);
            sourceMusic.volume = volume;
        }
        sourceMusic.volume = PlayerPrefs.GetFloat("VolumeMusicaAntes");
    }

    public void tocarFX(AudioClip FX, float Volume)
    {
        float tempVolume = Volume;
        if(Volume >= volumeMaximoFX)
        {
            tempVolume = volumeMaximoFX;
        }
        sourceFX.volume = tempVolume;
        sourceFX.PlayOneShot(FX);
    }

}
