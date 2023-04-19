using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class MenuConfiguracoes : MonoBehaviour
{
    public AudioController AC;

    public ButtonListControl BLC;

    public AudioMixer audioMixer;

    private GameObject MenuCanvas, ConfiguracoesCanvas, ConfigAudioCanvas, ConfigNaveCanvas, SelecaoFaseCanvas, TutorialCanvas, CreditosCanvas, SelecaoCanvas, FaseCanvas;
    public Slider volumeGeralSlider;
    public Slider volumeMusicaSlider;
    public Slider volumeFXSlider;


    //Pronuncia
    public GameObject palavraTexto;
    public GameObject faseImage;
    public GameObject botaoOuvir;
    public GameObject botaoIniciarFase;
    public GameObject ouvirTexto;
    public GameObject buttonRetornar;
    public GameObject buttonRetornar2;
    

    private void Start() {
        AC = FindObjectOfType(typeof(AudioController)) as AudioController;
        BLC = FindObjectOfType(typeof(ButtonListControl)) as ButtonListControl;
        MenuCanvas = GameObject.Find("MenuCanvas");
        ConfiguracoesCanvas = GameObject.Find("ConfiguracoesCanvas");
        ConfigAudioCanvas = GameObject.Find("ConfiguracoesAudioCanvas");
        ConfigNaveCanvas = GameObject.Find("ConfiguracoesNaveCanvas");
        SelecaoFaseCanvas = GameObject.Find("SelecaoFaseCanvas");
        TutorialCanvas = GameObject.Find("TutorialCanvas");
        CreditosCanvas = GameObject.Find("CreditosCanvas");
        SelecaoCanvas = GameObject.Find("SelecaoCanvas");
        FaseCanvas = GameObject.Find("FaseCanvas");

        //Pronuncia
        palavraTexto = GameObject.Find("PalavraTexto");
        faseImage = GameObject.Find("FaseImage");
        botaoOuvir = GameObject.Find("BotaoOuvir");
        botaoIniciarFase = GameObject.Find("BotaoIniciarFase");
        ouvirTexto = GameObject.Find("OuvirTexto");
        buttonRetornar = GameObject.Find("ButtonRetornar");
        buttonRetornar.GetComponent<Button>().onClick.AddListener(delegate{RetornarAoMenuPrincipal();});
        buttonRetornar2 = GameObject.Find("BotaoRetornar");
        buttonRetornar2.GetComponent<Button>().onClick.AddListener(delegate{AbrirSelecaoFase();});

        ConfiguracoesCanvas.SetActive(false);
        ConfigAudioCanvas.SetActive(false);
        ConfigNaveCanvas.SetActive(false);
        SelecaoFaseCanvas.SetActive(false);
        TutorialCanvas.SetActive(false);
        CreditosCanvas.SetActive(false);
        SelecaoCanvas.SetActive(false);
        FaseCanvas.SetActive(false);
        volumeGeralSlider.value = PlayerPrefs.GetFloat("VolumeGeral");
        volumeMusicaSlider.value = PlayerPrefs.GetFloat("VolumeMusicaAntes");
        volumeFXSlider.value = PlayerPrefs.GetFloat("VolumeEfeitos");
    }

    public void RetornarAoMenuPrincipal()
    {
        ConfiguracoesCanvas.SetActive(false);
        SelecaoFaseCanvas.SetActive(false);
        TutorialCanvas.SetActive(false);
        CreditosCanvas.SetActive(false);
        SelecaoCanvas.SetActive(false);
        FaseCanvas.SetActive(false);
        MenuCanvas.SetActive(true);
    }

    public void AbrirConfiguracoes()
    {
        MenuCanvas.SetActive(false);
        ConfigAudioCanvas.SetActive(false);
        ConfigNaveCanvas.SetActive(false);
        SelecaoCanvas.SetActive(false);
        FaseCanvas.SetActive(false);
        ConfiguracoesCanvas.SetActive(true);
    }

    public void AbrirConfigAudio()
    {
        ConfiguracoesCanvas.SetActive(false);
        ConfigAudioCanvas.SetActive(true);
    }

    public void AbrirConfigNave()
    {
        ConfiguracoesCanvas.SetActive(false);
        ConfigNaveCanvas.SetActive(true);
    }

    public void AbrirSelecaoFase()
    {
        MenuCanvas.SetActive(false);
        FaseCanvas.SetActive(false);
        SelecaoCanvas.SetActive(true);
        BLC.GenerateList();
        if(volumeMusicaSlider.value == 0.1f)
            StartCoroutine("PronunciaAumentarVolume");
        //volumeMusicaSlider.value = PlayerPrefs.GetFloat("VolumeMusicaAntes");
    }

    public void AbrirPronuncia(Palavra palavra)
    {
        SelecaoCanvas.SetActive(false);
        FaseCanvas.SetActive(true);
        faseImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/" + palavra.imagemPalavra);
        palavraTexto.GetComponent<TextMeshProUGUI>().text = palavra.nome;
        botaoOuvir.GetComponent<Button>().onClick.RemoveAllListeners();
        botaoOuvir.GetComponent<Button>().onClick.AddListener(delegate{AC.tocarFX(Resources.Load<AudioClip>("Audio/" + palavra.somFalado), 1);});
        botaoIniciarFase.GetComponent<Button>().onClick.RemoveAllListeners();
        botaoIniciarFase.GetComponent<Button>().onClick.AddListener(delegate{AC.TrocarMusica(AC.MusicaFase[Random.Range(0, AC.MusicaFase.Length)], palavra.fase, true);});
        botaoIniciarFase.SetActive(false);
        ouvirTexto.GetComponent<TextMeshProUGUI>().text = "";
        if(palavra.palavraContextual != null)
            PlayerPrefs.SetString("PalavraDesejada", palavra.palavraContextual);
        else
            PlayerPrefs.SetString("PalavraDesejada", palavra.nome);
        if(volumeMusicaSlider.value > 0.1f)
        {
            PlayerPrefs.SetFloat("VolumeMusicaAntes", volumeMusicaSlider.value);
            StartCoroutine("PronunciaDiminuirVolume");
            //volumeMusicaSlider.value = 0.1f;
        }
    }

    public void AcertouPronuncia()
    {
        botaoIniciarFase.SetActive(true);
    }

    public void AbrirTelaTutorial()
    {
        MenuCanvas.SetActive(false);
        TutorialCanvas.SetActive(true);
    }

    public void AbrirTelaCreditos()
    {
        MenuCanvas.SetActive(false);
        CreditosCanvas.SetActive(true);
    }

    public void SelecionarFase(string NomeFase)
    {
        AC.TrocarMusica(AC.MusicaFase[Random.Range(0, AC.MusicaFase.Length)], NomeFase, true);
    }

    public void SelecionarFaseSemTrocarMusica(string NomeFase)
    {
        AC.TrocarCena(NomeFase);
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

    IEnumerator PronunciaAumentarVolume()
    {
        for(float volume = 0.1f; volume < PlayerPrefs.GetFloat("VolumeMusicaAntes"); volume += 0.1f)
        {
            yield return new WaitForSeconds(0.1f);
            volumeMusicaSlider.value = volume;
        }
        volumeMusicaSlider.value = PlayerPrefs.GetFloat("VolumeMusicaAntes");
    }

    IEnumerator PronunciaDiminuirVolume()
    {
        for(float volume = volumeMusicaSlider.value; volume > 0.1f; volume -= 0.1f)
        {
            yield return new WaitForSeconds(0.1f);
            volumeMusicaSlider.value = volume;
        }
        volumeMusicaSlider.value = 0.1f;
    }

    public void PianoKey1()
    {
        AC.tocarFX(AC.PianoKey1, PlayerPrefs.GetFloat("VolumeEfeitos"));
    }

    public void PianoKey2()
    {
        AC.tocarFX(AC.PianoKey2, PlayerPrefs.GetFloat("VolumeEfeitos"));
    }

    public void PianoKey3()
    {
        AC.tocarFX(AC.PianoKey3, PlayerPrefs.GetFloat("VolumeEfeitos"));
    }

    public void PianoKey4()
    {
        AC.tocarFX(AC.PianoKey4, PlayerPrefs.GetFloat("VolumeEfeitos"));
    }

    public void PianoKey5()
    {
        AC.tocarFX(AC.PianoKey5, PlayerPrefs.GetFloat("VolumeEfeitos"));
    }

    public void PianoKey6()
    {
        AC.tocarFX(AC.PianoKey6, PlayerPrefs.GetFloat("VolumeEfeitos"));
    }

    public void PianoKey7()
    {
        AC.tocarFX(AC.PianoKey7, PlayerPrefs.GetFloat("VolumeEfeitos"));
    }

    public void PianoKey8()
    {
        AC.tocarFX(AC.PianoKey8, PlayerPrefs.GetFloat("VolumeEfeitos"));
    }

}
