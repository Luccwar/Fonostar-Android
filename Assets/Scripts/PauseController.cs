using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class PauseController : MonoBehaviour
{
    private AudioController AC;
    public Button pauseButton;
    private GameObject pausePanel;
    private GameObject configuracoesPanel;
    public AudioMixer audioMixer;
    public Slider volumeGeralSlider;
    public Slider volumeMusicaSlider;
    public Slider volumeFXSlider;

    private void Awake() {
        AC = FindObjectOfType(typeof(AudioController)) as AudioController;
        pausePanel = GameObject.Find("MenuPause");
        configuracoesPanel = GameObject.Find("MenuConfig");
        pausePanel.SetActive(false);
        configuracoesPanel.SetActive(false);
        volumeGeralSlider.value = PlayerPrefs.GetFloat("VolumeGeral");
        volumeMusicaSlider.value = PlayerPrefs.GetFloat("VolumeMusicaAntes");
        volumeFXSlider.value = PlayerPrefs.GetFloat("VolumeEfeitos");
    }

    public void PauseOff()
    {
        pauseButton.interactable = true;
        pausePanel.SetActive(false);
        configuracoesPanel.SetActive(false);
        GameController.instance.GameSpeed = 1f;
    }

    public void PauseOn()
    {
        GameController.instance.GameSpeed = 0f;
        pauseButton.interactable = false;
        pausePanel.SetActive(true);
        configuracoesPanel.SetActive(false);
    }

    public void Configuracoes()
    {
        pausePanel.SetActive(false);
        configuracoesPanel.SetActive(true);
    }

    public void VoltarMenu()
    {
        AC.TrocarMusica(AC.MusicaTitulo, "MenuPrincipal", true);
    }

    public void SairJogo()
    {
        Application.Quit();
    }

    public void SetVolumeGeral()
    {
        audioMixer.SetFloat("masterVolume", volumeGeralSlider.value);
        if(volumeGeralSlider.value <= -40)
        {
            audioMixer.SetFloat("masterVolume", -80);
        }
        PlayerPrefs.SetFloat("VolumeGeral", volumeGeralSlider.value);
    }

    public void DiminuirVolumeGeral()
    {
        volumeGeralSlider.value--;
        if(volumeGeralSlider.value <= -40)
        {
            audioMixer.SetFloat("masterVolume", -80);
        }
        PlayerPrefs.SetFloat("VolumeGeral", volumeGeralSlider.value);
    }

    public void AumentarVolumeGeral()
    {
        volumeGeralSlider.value++;
        if(volumeGeralSlider.value <= -40)
        {
            audioMixer.SetFloat("masterVolume", -80);
        }
        PlayerPrefs.SetFloat("VolumeGeral", volumeGeralSlider.value);
    }

    public void SetVolumeMusica()
    {
        AC.sourceMusic.volume = volumeMusicaSlider.value;
        PlayerPrefs.SetFloat("VolumeMusica", volumeMusicaSlider.value);
    }

    public void DiminuirVolumeMusica()
    {
        volumeMusicaSlider.value--;
        PlayerPrefs.SetFloat("VolumeMusica", volumeMusicaSlider.value);
    }

    public void AumentarVolumeMusica()
    {
        volumeMusicaSlider.value++;
        PlayerPrefs.SetFloat("VolumeMusica", volumeMusicaSlider.value);
    }

    public void SetVolumeEfeitos()
    {
        AC.sourceFX.volume = volumeFXSlider.value;
        PlayerPrefs.SetFloat("VolumeEfeitos", volumeFXSlider.value);
    }

    public void DiminuirVolumeEfeitos()
    {
        volumeFXSlider.value--;
        PlayerPrefs.SetFloat("VolumeEfeitos", volumeFXSlider.value);
    }

    public void AumentarVolumeEfeitos()
    {
        volumeFXSlider.value++;
        PlayerPrefs.SetFloat("VolumeEfeitos", volumeFXSlider.value);
    }
}
