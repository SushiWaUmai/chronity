using System;
using UnityEngine;

namespace Chronity
{
    /// <summary>
    /// Base of the Timer Class,
    /// it allows you to run events on a delay without the use of <see cref="Coroutine"/>
    /// or <see cref="MonoBehaviour"/>
    /// </summary>
    public abstract class TimerBase
    {
        /// <summary>
        /// Pause a running timer. A paused timer can be resumed from the same point it was paused.
        /// </summary>
        public void Pause() => IsPaused = true;

        /// <summary>
        /// Continue a paused timer. Does nothing if the timer has not been paused.
        /// </summary>
        public void Resume() => IsPaused = false;

        /// <summary>
        /// Cancels the timer. The timer's onComplete callback will not be called.
        /// </summary>
        public void Cancel() => IsCanceled = true;

        /// <summary>
        /// Whether the timer has finished for any reason.
        /// </summary>
        public virtual bool IsDone => IsCompleted || IsCanceled;

        /// <summary>
        /// Whether the timer has canceled running.
        /// </summary>
        public bool IsCanceled { get; private set; }

        /// <summary>
        /// Whether the timer has completed running. This is false when timer was canceled.
        /// </summary>
        public bool IsCompleted { get; private set; }

        /// <summary>
        /// How long it takes to complete from start of finish.
        /// </summary>
        public float Duration { get; private set; }

        /// <summary>
        /// Whether the timer will run again upon completion.
        /// </summary>
        public bool IsLooped { get; private set; }

        /// <summary>
        /// Whether the timer is paused
        /// </summary>
        public bool IsPaused { get; private set; }

        /// <summary>
        /// How many seconds have elapsed since the start of the timer.
        /// </summary>
        public float TimeElapsed => CurrentTime - _startTime;

        /// <summary>
        /// How many seconds there are left until completion.
        /// </summary>
        public float TimeRemaining => _endTime - CurrentTime;

        /// <summary>
        /// How much progress the timer made from start to finish as a ratio
        /// </summary>
        public float RatioComplete => TimeElapsed / Duration;

        /// <summary>
        /// How much progress the timer has left as a ratio
        /// </summary>
        public float RatioRemaining => TimeRemaining / Duration;

        protected TimerBase(float duration, Action onComplete, Action<float> onUpdate, bool isLooped = false)
        {
            Duration = duration;
            _onComplete = onComplete;
            _onUpdate = onUpdate;
            IsLooped = isLooped;

            _startTime = CurrentTime;
            _lastTime = _startTime;
        }

        protected virtual void Update()
        {
            if (IsDone)
                return;

            HandlePause();
            CheckTime();
            HandleUpdate();

            _lastTime = CurrentTime;
        }

        private void HandlePause()
        {
            if (IsPaused)
                _startTime += TimeDelta;
        }

        private void CheckTime()
        {
            if (_endTime < CurrentTime)
            {
                _onComplete?.Invoke();

                if (IsLooped)
                    _startTime = CurrentTime;
                else
                    IsCompleted = true;
            }
        }

        private void HandleUpdate()
        {
            if (!IsPaused && !IsDone)
            {
                _onUpdate?.Invoke(TimeElapsed);
            }
        }

        protected abstract float CurrentTime { get; }

        private float TimeDelta => CurrentTime - _lastTime;

        private readonly Action _onComplete;
        private readonly Action<float> _onUpdate;

        private float _startTime;
        private float _lastTime;

        private float _endTime => _startTime + Duration;
    }
}
