using UnityEngine;

public class BackgroundMovement : MonoBehaviour
{
    [SerializeField, Min(0f)]
    // 0 = no parallex
    float speed;

    private float
        startingPosition,
        lengthOfSprite;

    private bool transitioning; 

    // Get starting position & sprite length 
    private void Start()
    { 
        startingPosition = transform.position.x;
        lengthOfSprite = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void Update()
    {
        if (transitioning && !SceneController.instance.pause)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
            if (transform.position.x < startingPosition - lengthOfSprite / 2)
            {
                transform.position =
                    new Vector3(startingPosition + lengthOfSprite / 2,
                    transform.position.y,
                    transform.position.z);
            }
        }
    }

    public void StartTransition()
    {
        transitioning = true; 
    }

    public void EndTransition()
    {
        transitioning = false; 
    }
}