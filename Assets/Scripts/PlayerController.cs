using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void EnterTransition()
    {
        animator.SetBool("Running", true); 
    }

    public void ExitTransition()
    {
        animator.SetBool("Running", false); 
    }
}
