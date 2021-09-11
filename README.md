# Chronity

A library for running functions after a delay in Unity.

This package is heavily inspired by the [UnityTimer](https://github.com/akbiggs/UnityTimer) made by [akbiggs](https://github.com/akbiggs).

## Getting Started

### Installation
Please follow the instructions in the manual about [Installing a package from a Git URL](https://docs.unity3d.com/Manual/upm-ui-giturl.html). 


Use the following URL to install the latest version of the package:
https://github.com/SushiWaUmai/Chronity?path=/com.sushiwaumai.chronity


### Quick Start
This is how to call a function after a delay in Chronity.

```cs
// Log "Hello World" after five seconds.

Timer.Register(5f, () => Debug.Log("Hello World"));
```
