---
title: Features
---

## Features :sparkles:

**Make a timer repeat by setting `isLooped` to true.**
```c#
// Log "Hello World" every 10 seconds.

Timer.Register(10f, () => Debug.Log("Hello World"), isLooped: true);
```

**Cancel a timer after calling it.**
```c#
private Timer timer;

private void Start()
{
    timer = Timer.Register(2f, () => Debug.Log("You won't see this text if you press X."));
}

private void Update()
{
    if (Input.GetKeyDown(KeyCode.X)) 
    {
        Timer.Cancel(timer);
    }
}
```


**Attach the timer to a MonoBehaviour so that the timer is destroyed when the MonoBehaviour is.**

Very often, a timer called from a MonoBehaviour will manipulate that behaviour's state. Thus, it is common practice to cancel the timer in the OnDestroy method of the MonoBehaviour. We've added a convenient extension method that attaches a Timer to a MonoBehaviour such that it will automatically cancel the timer when the MonoBehaviour is detected as null.

```c#
public class CoolMonoBehaviour : MonoBehaviour
{
    private void Start() 
    {
        // Use the AttachTimer extension method to create a timer that is destroyed when this
        // object is destroyed.
        this.AttachTimer(5f, () => {
      
            // If this code runs after the object is destroyed, a null reference will be thrown,
            // which could corrupt game state.
            this.gameObject.transform.position = Vector3.zero;
        });
    }
   
    private void Update() 
    {
        // This code could destroy the object at any time!
        if (Input.GetKeyDown(KeyCode.X)) 
        {
            GameObject.Destroy(this.gameObject);
        }
    }
}
```

**Update a value gradually over time using the `onUpdate` callback.**

```c#
// Change a color from white to red over the course of five seconds.
Color color = Color.white;
float transitionDuration = 5f;

Timer.Register(transitionDuration,
   onUpdate: secondsElapsed => color.r = 255 * (secondsElapsed / transitionDuration),
   onComplete: () => Debug.Log("Color is now red"));
```

**Make a timer presist through scene changes using the `cancelOnSceneChange` parameter.**
```c#
// Make a timer that will persist through scene changes.
Timer.Register(5f, () => Debug.Log("Hello World"), cancelOnSceneChange: false);

// Change scene from another script 

// Logs "Hello World" after 5 seconds.
```

**Make a timer run in the editor by using the `EditorTimer` class.**
```c#
// Logs "Hello World" after 5 seconds in the editor

EditorTimer.Register(5, () => Debug.Log("Hello World"));
```
