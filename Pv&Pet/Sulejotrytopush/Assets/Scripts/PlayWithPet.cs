using UnityEngine;

public class PlayWithPet : MonoBehaviour
{
    private readonly float minSwipe = 20.0f;
    private Vector2 FingerStartPos;
    private Vector2 FingerCurrentPos;
    private bool theyAreTouchingMe;
    private float SwipeTimer = 0.5f;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        if (!theyAreTouchingMe && Input.touchCount > 0)
        {
            Debug.Log("Hello there");
            Touch touch = Input.GetTouch(0);
            FingerStartPos = touch.position;
            theyAreTouchingMe = true;
        }
        else if (theyAreTouchingMe && Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            SwipeTimer -= Time.deltaTime;
            if (SwipeTimer <= 0)
            {
                FingerCurrentPos = touch.position;
                SwipeTimer = 0.5f;
                if (Vector2.Distance(FingerStartPos, FingerCurrentPos) >= minSwipe)
                {
                    Debug.Log("It's a swipe bois");
                }
            }
        }
        else if (theyAreTouchingMe && Input.touchCount == 0)
        {
            Debug.Log("Bye");
            theyAreTouchingMe = false;
            SwipeTimer = 0.5f;
        }
    }
}