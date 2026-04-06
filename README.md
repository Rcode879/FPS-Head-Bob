# FPS-Head-Bob

## Overview
This script can be used to create a "head bobbing" effect when walking in first person shooter games made using unity game engine.

## How it works 
The movement of the player's head can be modelled using a sine wave, to create a smooth upwards and downwards motion. This is done by changing the y coordinate of the first person camera relative to a sine value. 
```csharp
timer += Time.deltaTime * bobSpeed; 
float sine = Mathf.Sin(timer); 
Vector3 newPos = cam.localPosition;
newPos.y = originalY + Mathf.Sin(timer) * bobIntensity;
cam.localPosition = newPos;
```



Additionally, we consider each transition from positive to negative or vice versa (of the value of sine) to be a single footstep and play a OneShot sound effect of a single footstep. 
```csharp
if(lastSine < 0f && sine >= 0f) 
{
  footstepsSource.PlayOneShot(footstep); 
}
lastSine = sine;
```

When the player is not moving, we want to ensure that the camera returns back to it's original position. For this we use linear interpolation to create a smooth motion.
```csharp
Vector3 newPos = cam.localPosition;
newPos.y = Mathf.Lerp(newPos.y, originalY, Time.deltaTime * bobSpeed); 
cam.localPosition = newPos;
timer = 0f;
lastSine = 0f;

```

## How to implement into game 

1. Add the script as a compnent to the main camera (the FPS cam)
2. Create a new audio source component for the footstep effect and attatch the sound effect
3. Add the transform component of the main camera to the "cam" variable
4. Add the audio source to the variable "Footsteps Source"
5. Add the footstep sound effect to the "footstep" variable
<img width="872" height="514" alt="image" src="https://github.com/user-attachments/assets/858c477f-22a2-4225-a4a4-c068eaa8ca93" />


