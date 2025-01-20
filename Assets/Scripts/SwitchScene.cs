using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic; 

public class SwitchScene : MonoBehaviour
{
    [SerializeField]
    string nextScene;

    [SerializeField]
    Animator transition;

    public float transitionTime = 1f; 

    public void ToNextScene()
    {
        StartCoroutine(LoadLevel()); 
    }

    IEnumerator LoadLevel()
    {
        transition.SetTrigger("Enter"); 

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(nextScene);
    }

}
