using Unity.VisualScripting;
using UnityEngine;

public class QuitGame : MonoBehaviour
{
    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Application is ended"); 
    }
}
