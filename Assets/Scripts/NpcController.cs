using UnityEngine;
using UnityEngine.Events;

public class NpcController : MonoBehaviour
{
    [SerializeField] bool debug; 

    [SerializeField]
    float spawn, end, target;

    [SerializeField]
    float speed;

    [SerializeField]
    UnityEvent<string> ArriveTargetLocation;
    [SerializeField]
    UnityEvent FinishedTransition; 

    private bool talked, transitioning;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();

        Reset();     
    }

    private void Reset()
    {
        transform.position = new Vector3(spawn, transform.position.y, transform.position.z);
        talked = false;
        transitioning = false;
        animator.SetBool("Running", false);

        if (debug)
            Debug.Log("NPC reset"); 
    }

    private void Update()
    {
        if (transitioning && !SceneController.instance.pause)
        {
            TransitionUpdate();
        }
    }

    public void EnterTransition()
    {
        // Walking animation
        transitioning = true;
        animator.SetBool("Running", true); 

        if (debug)
            Debug.Log("NPC enter transition"); 
    }

    public void ExitTransition()
    {
        // Standing animation
        transitioning = false;
        animator.SetBool("Running", false);
        talked = true;

        if (debug)
            Debug.Log("NPC exit transition"); 
        
    }
    private void TransitionUpdate()
    {
        if (!talked && transform.position.x > target || talked && transform.position.x > end)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
        if (!talked && transform.position.x <= target)
        {
            ArriveTargetLocation?.Invoke("1");
            if (debug)
                Debug.Log("NPC arrived target location"); 
        }
        if (talked && transform.position.x <= end)
        {
            Reset();
            FinishedTransition?.Invoke();
            if (debug)
                Debug.Log("NPC rested to original location");
        }
    }


}
