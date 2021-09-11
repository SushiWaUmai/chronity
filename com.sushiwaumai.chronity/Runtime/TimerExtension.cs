using System;
using UnityEngine;

namespace Chronity
{
    public static class TimerExtension
    {
        public static Timer RegisterTimer(this MonoBehaviour behaviour, float duration, Action onComplete, Action<float> onUpdate = null, bool useRealTime = false, bool loop = false, bool cancelOnSceneChange = true)
        {
            Timer result = Timer.Register(duration, onComplete, onUpdate, useRealTime, loop, cancelOnSceneChange, behaviour);
            return result;
        }
    }
}