using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    // Référence au RulesPanel que tu as créé (à assigner dans l'Inspector)
    public GameObject rulesPanel;

    // Fonction appelée lorsque le bouton "Start" est cliqué
    public void StartGame()
    {
        // Charge la scène du jeu ; ici "Example Scene" est le nom de ta scène de jeu
        SceneManager.LoadScene("Example Scene");
    }

    // Fonction appelée lorsque le bouton "Rules" est cliqué
    public void ShowRules()
    {
        if (rulesPanel != null)
        {
            rulesPanel.SetActive(true); // Affiche le panneau des règles
        }
        else
        {
            Debug.LogWarning("Rules Panel n'est pas assigné dans MainMenuManager !");
        }
    }

    // Fonction appelée lorsque le bouton "Fermer" du RulesPanel est cliqué
    public void CloseRules()
    {
        if (rulesPanel != null)
        {
            rulesPanel.SetActive(false); // Cache le panneau des règles
        }
        else
        {
            Debug.LogWarning("Rules Panel n'est pas assigné dans MainMenuManager !");
        }
    }
}
