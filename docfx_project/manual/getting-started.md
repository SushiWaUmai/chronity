---
title: Getting Started 
---

# Getting Started :rocket: 

## Installation

### Install with Git

Please follow the instructions in the manual about [Installing a package from a Git URL](https://docs.unity3d.com/Manual/upm-ui-giturl.html). 


Use the following URL to install the latest version of the package:
https://github.com/SushiWaUmai/Chronity.git?path=/com.sushiwaumai.chronity

### Install with OpenUPM

This needs [OpenUPM](https://openupm.com/) to be installed to your machine.

Chronity can also be installed using OpenUPM.

Run the following command on the project root folder:
```
openupm add com.sushiwaumai.chronity
```

---

## Quick Start :mortar_board:

**This is how to call a function after a delay in Chronity.**

```c#
// Log "Hello World" after five seconds.

Timer.Register(5f, () => Debug.Log("Hello World"));
```