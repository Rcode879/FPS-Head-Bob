using UnityEngine;

public class HeadBob : MonoBehaviour
{
    public float bobSpeed = 5f; //speed of head bob
    public float bobIntensity = 0.05f; // max height of head bob 

    private float originalY;
    private float timer = 0f;

    public Transform cam;
    public AudioSource footstepsSource;
    public AudioClip footstep;

    float lastSine = 0f;

    private void Start()
    {
        originalY = cam.localPosition.y; // save initial position of camera's y coordinate
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        if (x != 0 || z != 0) //checks if player is moving (y coordinate not included as we don't want bobbing while jumping)
        {
            timer += Time.deltaTime * bobSpeed; 
            float sine = Mathf.Sin(timer); //use sine wave to model head movement motion
            Vector3 newPos = cam.localPosition;
            newPos.y = originalY + Mathf.Sin(timer) * bobIntensity;
            cam.localPosition = newPos;

            if(lastSine < 0f && sine >= 0f) // each time sine changes from positive to negative = 1 footstep
            {
                footstepsSource.PlayOneShot(footstep); //play single footstep sound
            }
            lastSine = sine;


        }
        else //when not moving
        {
            
            Vector3 newPos = cam.localPosition;
            newPos.y = Mathf.Lerp(newPos.y, originalY, Time.deltaTime * bobSpeed); //Lerp y coordinate of camera to new position, use lerp for smooth movement
            cam.localPosition = newPos;
            timer = 0f;
            lastSine = 0f;
        }
    }
}
