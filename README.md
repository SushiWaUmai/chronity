[![# Chronity](./.github/images/Slide.png)](https://sushiwaumai.github.io/Chronity)

[![Release](https://img.shields.io/github/v/release/SushiWaUmai/Chronity?include_prereleases&style=flat-square)](https://github.com/SushiWaUmai/Chronity/releases)
[![OpenUPM](https://img.shields.io/npm/v/com.sushiwaumai.chronity?label=openupm&registry_uri=https://package.openupm.com&style=flat-square)](https://openupm.com/packages/com.sushiwaumai.chronity/)
[![LISENCE](https://img.shields.io/github/license/SushiWaUmai/Chronity?style=flat-square)](https://github.com/SushiWaUmai/Chronity/blob/main/LICENSE)
[![Compatibility](https://img.shields.io/badge/-2020.3+-11191F?logo=Unity&style=flat-square)](https://unity3d.com/get-unity/download/archive)
[![GitHub Repo stars](https://img.shields.io/github/stars/SushiWaUmai/Chronity?color=%23dca&label=%E2%AD%90&style=flat-square)](https://github.com/SushiWaUmai/Chronity/stargazers)

:hourglass: A library for running functions after a delay in Unity.

This package is a fork of the [UnityTimer](https://github.com/akbiggs/UnityTimer) made by [akbiggs](https://github.com/akbiggs).

To get started, read the [docs](https://sushiwaumai.github.io/Chronity) or follow this [README](README.md) file.

## Table of Contents
- [Getting Started](https://github.com/SushiWaUmai/Chronity#getting-started-rocket)
  - [Installation](https://github.com/SushiWaUmai/Chronity#installation)
  - [Quick Start](https://github.com/SushiWaUmai/Chronity#quick-start-mortar_board)
- [Features](https://github.com/SushiWaUmai/Chronity#features-sparkles)
- [License](https://github.com/SushiWaUmai/Chronity#license-scroll)

## Getting Started :rocket:

### Installation
Please follow the instructions in the manual about [Installing a package from a Git URL](https://docs.unity3d.com/Manual/upm-ui-giturl.html). 


Use the following URL to install the latest version of the package:
https://github.com/SushiWaUmai/Chronity.git?path=/com.sushiwaumai.chronity


### Quick Start :mortar_board:
**This is how to call a function after a delay in Chronity.**

```c#
// Log "Hello World" after five seconds.

Timer.Register(5f, () => Debug.Log("Hello World"));
```

## Features :sparkles:

**Make a timer repeat by setting `isLooped` to true.**
```c#
// Log "Hello World" every 10 seconds.

Timer.Register(10f, () => Debug.Log("Hello World"), isLooped: true);
```

**Cancel a timer after calling it.**
```c#
Timer timer;

private void Start()
{
    timer = Timer.Register(2f, () => Debug.Log("You won't see this text if you press X."));
}

private void Update()
{
    if (Input.GetKeyDown(KeyCode.X)) 
    {
        timer.Cancel();
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

**A number of other useful features are included!**

- time.Pause()
- timer.Resume()
- timer.TimePassed
- timer.TimeRemaining
- timer.RatioComplete
- timer.IsDone


## License :scroll:

Code released under [the MIT License](https://github.com/SushiWaUmai/Chronity/blob/main/LICENSE).
