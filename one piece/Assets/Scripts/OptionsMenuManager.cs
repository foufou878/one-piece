using UnityEngine;
using UnityEngine.UI;

public class OptionsMenuManager : MonoBehaviour
{
    public GameObject optionsPanel; // Référence au OptionsPanel
    public Slider volumeSlider;
    public AudioSource gameMusic; // Référence à la musique du jeu

    private void Start()
    {
        // Charge le volume sauvegardé
        if (PlayerPrefs.HasKey("GameVolume"))
        {
            float savedVolume = PlayerPrefs.GetFloat("GameVolume");
            volumeSlider.value = savedVolume;
            AudioListener.volume = savedVolume;
        }
    }

    public void ShowOptions()
    {
        optionsPanel.SetActive(true); // Affiche le menu des options
    }

    public void CloseOptions()
    {
        optionsPanel.SetActive(false); // Ferme le menu des options
    }

    public void AdjustVolume()
    {
        AudioListener.volume = volumeSlider.value;
        PlayerPrefs.SetFloat("GameVolume", volumeSlider.value);
    }

    public void ToggleMusic()
    {
        if (gameMusic != null)
        {
            gameMusic.mute = !gameMusic.mute;
        }
    }
}
