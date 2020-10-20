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
    public Slider volumeSlider;

    private void Awake() {
        AC = FindObjectOfType(typeof(AudioController)) as AudioController;
        pausePanel = GameObject.Find("MenuPause");
        configuracoesPanel = GameObject.Find("MenuConfig");
        pausePanel.SetActive(false);
        configuracoesPanel.SetActive(false);
        volumeSlider.value = PlayerPrefs.GetFloat("Volume");
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

    public void SetVolume (float volume)
    {
        audioMixer.SetFloat("masterVolume", volume);
        if(volumeSlider.value <= -40)
        {
            audioMixer.SetFloat("masterVolume", -80);
        }
        PlayerPrefs.SetFloat("Volume", volumeSlider.value);
    }

    public void DiminuirVolume()
    {
        volumeSlider.value--;
        if(volumeSlider.value <= -40)
        {
            audioMixer.SetFloat("masterVolume", -80);
        }
        PlayerPrefs.SetFloat("Volume", volumeSlider.value);
    }

    public void AumentarVolume()
    {
        volumeSlider.value++;
        if(volumeSlider.value <= -40)
        {
            audioMixer.SetFloat("masterVolume", -80);
        }
        PlayerPrefs.SetFloat("Volume", volumeSlider.value);
    }
}
