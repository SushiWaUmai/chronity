using System;
using UnityEngine;

namespace Chronity
{
    public abstract class TimerBase
    {
        public void Pause() => IsPaused = true;

        public void Resume() => IsPaused = false;

        public void Cancel() => IsCanceled = true;

        public virtual bool IsDone => IsCompleted || IsCanceled;

        public bool IsCanceled { get; private set; }
        public bool IsCompleted { get; private set; }

        public float Duration { get; private set; }
        public bool IsLooped { get; private set; }
        public bool IsPaused { get; private set; }

        public float TimePassed => CurrentTime - _startTime;
        public float TimeRemaining => _endTime - CurrentTime;
        public float RatioComplete => TimePassed / Duration;

        protected TimerBase(float duration, Action onComplete, Action<float> onUpdate, bool looped = false)
        {
            Duration = duration;
            _onComplete = onComplete;
            _onUpdate = onUpdate;
            IsLooped = looped;

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
                _onUpdate?.Invoke(TimePassed);
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