using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Jugar()
    {
        Debug.Log("Botón Jugar presionado");
        SceneManager.LoadScene("Main");
    }

    public void Salir()
    {
        Application.Quit();
    }

    public void VolverAlMenu()
    {
        SceneManager.LoadScene("Menu");
    }

}