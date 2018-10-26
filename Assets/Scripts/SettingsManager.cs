using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    // CONFIGURAÇÕES
    // AUDIO, MUSICA, VOLUMES, CONTROLES - USANDO PLAYERPREFS
    public AudioSource auMusic, auSfx; // AUDIOS PARA MUSICA E EFEITO
    private AudioClip[] auClipMusic; // CONJUNTO DE MUSICAS
    private AudioClip auClipSfx; // EFEITO DO AUDIO
    public Toggle tgMusic, tgSfx, tgControllers;   // MARCADORES PARA MUSICA, AUDIO SFX E CONTROLADORES
    public Slider slVolumeMusic;    // SLIDE PARA VOLUME DA MUSICA
    public Text controller; // TEXTO DOS CONTROLADORES

    #region DEFAULT STATES
    void Start()
    {
        // SALVA ESTADOS DAS CONFIGURAÇÕES
        SaveStates();

        // ASSOCIAR AO COMPONENTE DE MUSICA
        auMusic = GetComponent<AudioSource>();
        auMusic = GameObject.Find("Music").GetComponent<AudioSource>();
        auClipMusic = Resources.LoadAll<AudioClip>("Audios/Musics");

        // ASSOCIAR AO COMPONENTE DE SFX
        auSfx = GetComponent<AudioSource>();
        auSfx = GameObject.Find("Sfx").GetComponent<AudioSource>();
        auClipSfx = Resources.Load<AudioClip>("Audios/Sfxs/click");

        // EXECUTANDO MUSICA DE FORMA ALEATÓRIA
        auMusic.clip = auClipMusic[Random.Range(0, auClipMusic.Length)];
        auMusic.Play();
    }
    #endregion

    public void SettingsStates(int i)
    {
        switch (i)
        {
            // MUSIC
            case 0:
                if (tgMusic.isOn == false)
                {
                    PlayerPrefs.SetInt("MUSIC", 0);
                    auMusic.mute = true;
                    slVolumeMusic.interactable = false;
                }
                else
                {
                    PlayerPrefs.SetInt("MUSIC", 1);
                    auMusic.mute = false;
                    slVolumeMusic.interactable = true;
                }
                break;

            // VOLUME OF MUSIC
            case 1:
                auMusic.volume = slVolumeMusic.value;
                PlayerPrefs.SetFloat("VOLUME", slVolumeMusic.value);
                PlayerPrefs.Save();
                break;

            // AUDIO
            case 2:
                if (tgSfx.isOn == false)
                {
                    PlayerPrefs.SetInt("AUDIO", 0);
                    auSfx.mute = true;
                    auSfx.playOnAwake = false;

                }
                else
                {
                    PlayerPrefs.SetInt("AUDIO", 1);
                    auSfx.mute = false;
                    auSfx.playOnAwake = true;
                    auSfx.PlayOneShot(auClipSfx);
                }
                break;

            // CONTROLLER SELECT
            case 3:
                // MOUSE SELECT
                if (tgControllers.isOn == true)
                {
                    controller.text = "MOUSE";
                    PlayerPrefs.SetInt("CONTROLLER", 0);
                }
                // KEYBOARD SELECT
                else
                {
                    controller.text = "KEYBOARD";
                    PlayerPrefs.SetInt("CONTROLLER", 1);
                }
                break;
        }
    }

    void SaveStates()
    {
        // SAVE SETTINGS VOLUME OF MUSIC
        slVolumeMusic.value = PlayerPrefs.GetFloat("VOLUME");

        // SAVE SETTINGS OF MUSIC
        if (PlayerPrefs.GetInt("MUSIC") == 0)
        {
            tgMusic.isOn = false;
            auMusic.mute = true;
            slVolumeMusic.interactable = false;
            auMusic.volume = slVolumeMusic.value;
        }
        else
        {
            tgMusic.isOn = true;
            auMusic.mute = false;
            slVolumeMusic.interactable = true;
            auMusic.volume = slVolumeMusic.value;
        }

        // SAVE AUDIO SETTINGS
        if (PlayerPrefs.GetInt("AUDIO") == 0)
        {
            tgSfx.isOn = false;
            auSfx.mute = true;
        }
        else
        {
            tgSfx.isOn = true;
            auSfx.mute = false;
        }

        // SAVE CONTROLLER SELECT
        if (PlayerPrefs.GetInt("CONTROLLER") == 0)
        {
            tgControllers.isOn = true;
        }
        else
        {
            tgControllers.isOn = false;
        }
    }
}