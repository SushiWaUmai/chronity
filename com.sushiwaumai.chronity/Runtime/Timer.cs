using System;
using UnityEngine;

namespace Chronity
{
    /// <summary>
    /// Allows you to run events <b>during runtime</b> on a delay without the use of <see cref="Coroutine"/>
    /// or <see cref="MonoBehaviour"/>.
    ///
    /// To create and start a Timer, use the <see cref="Register"/> method.
    /// </summary>
    public partial class Timer : TimerBase
    {
        /// <summary>
        /// Register a new timer that should fire an event after a certain amount of time
        /// has elapsed.
        ///
        /// Registered timers are destroyed when the scene changes.
        /// </summary>
        /// <param name="duration">The time to wait before the timer should fire, in seconds.</param>
        /// <param name="onComplete">An action to fire when the timer completes.</param>
        /// <param name="onUpdate">An action that should fire each time the timer is updated. Takes the amount
        /// of time passed in seconds since the start of the timer's current loop.</param>
        /// <param name="useRealTime">Whether the timer uses real-time(i.e. not affected by pauses,
        /// slow/fast motion) or game-time(will be affected by pauses and slow/fast-motion).</param>
        /// <param name="isLooped">Whether the timer should repeat after executing.</param>
        /// <param name="cancelOnSceneChange">Whether the timer should cancel when the scene changes</param>
        /// <param name="attachedBehavior">An object to attach this timer to. After the object is destroyed,
        /// the timer will expire and not execute. This allows you to avoid annoying <see cref="NullReferenceException"/>s
        /// by preventing the timer from running and accessessing its parents' components
        /// after the parent has been destroyed.</param>
        /// <returns>A timer object that allows you to examine stats and stop/resume progress.</returns>
        public static Timer Register(float duration, Action onComplete, Action<float> onUpdate = null, bool useRealTime = false, bool isLooped = false, bool cancelOnSceneChange = true, MonoBehaviour attachedBehavior = null)
        {
            Timer result = new Timer(duration, onComplete, onUpdate, useRealTime, isLooped, cancelOnSceneChange, attachedBehavior);
            TimerManager.RegisterTimer(result);
            return result;
        }

        /// <summary>
        /// Pauses all created timers.
        /// </summary>
        public static void PauseAllTimers() => TimerManager.PauseAllTimers();

        /// <summary>
        /// Resumes all created timers.
        /// </summary>
        public static void ResumeAllTimers() => TimerManager.ResumeAllTimers();

        /// <summary>
        /// Cancels all created timers.
        /// </summary>
        public static void CancelAllTimers() => TimerManager.CancelAllTimers();

        /// <summary>
        /// Whether the timer uses real-time or game-time. Real time is unaffected by changes to <see cref="Time.timeScale"/>
        /// of the game (e.g. pausing, slow-mo), while game time is affected.
        /// </summary>
        public bool UsesRealTime { get; private set; }

        /// <summary>
        /// Whether the timer should be destroyed when scene changes.
        /// </summary>
        public bool CancelOnSceneChange { get; private set; }

        /// <summary>
        /// The <see cref="MonoBehaviour"/> the timer is attached to.
        /// </summary>
        public MonoBehaviour AttachedBehavior { get; private set; }

        public override bool IsDone => base.IsDone || AttachedBehaviorDestroyed;

        protected Timer(float duration, Action onComplete, Action<float> onUpdate, bool usesRealTime = false, bool isLooped = false, bool cancelOnSceneChange = true, MonoBehaviour attachedBehavior = null)
           : base(duration, onComplete, onUpdate, isLooped)
        {
            CancelOnSceneChange = cancelOnSceneChange;
            UsesRealTime = usesRealTime;

            AttachedBehavior = attachedBehavior;

            if (attachedBehavior)
                _hasAttachedBehavior = true;

        }

        protected override float CurrentTime => UsesRealTime ? Time.realtimeSinceStartup : Time.time;

        private readonly bool _hasAttachedBehavior;
        private bool AttachedBehaviorDestroyed => _hasAttachedBehavior && AttachedBehavior == null;
    }
}
