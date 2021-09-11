using System;
using UnityEngine;

namespace Chronity
{
    public partial class Timer
    {
        public void Pause() => IsPaused = true;

        public void Resume() => IsPaused = false;

        public void Cancel() => IsCanceled = true;

        public bool IsDone => IsCompleted || IsCanceled || AttachedBehaviorDestroyed;

        public bool IsCanceled { get; private set; }
        public bool IsCompleted { get; private set; }

        public float Duration { get; private set; }
        public bool UsesRealTime { get; private set; }
        public bool IsLooped { get; private set; }
        public bool IsPaused { get; private set; }
        public bool CancelOnSceneChange { get; private set; }
        public MonoBehaviour AttachedBehavior { get; private set; }

        public float TimePassed => CurrentTime - _startTime;
        public float TimeRemaining => _endTime - CurrentTime;
        public float RatioComplete => TimePassed / Duration;

        private Timer(float duration, Action onComplete, Action<float> onUpdate, bool usesRealTime = false, bool looped = false, bool cancelOnSceneChange = true, MonoBehaviour attachedBehavior = null)
        {
            Duration = duration;
            _onComplete = onComplete;
            _onUpdate = onUpdate;
            UsesRealTime = usesRealTime;
            IsLooped = looped;
            CancelOnSceneChange = cancelOnSceneChange;

            AttachedBehavior = attachedBehavior;

            if (attachedBehavior)
                _hasAttachedBehavior = true;

            _startTime = CurrentTime;
            _lastTime = _startTime;
        }

        private void Update()
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
            if(!IsPaused && !IsDone)
            {
                _onUpdate?.Invoke(TimePassed);
            }
        }

        private float CurrentTime => UsesRealTime ? Time.realtimeSinceStartup : Time.time;
        private float TimeDelta => CurrentTime - _lastTime;
        private bool AttachedBehaviorDestroyed => _hasAttachedBehavior && AttachedBehavior == null;

        private readonly Action _onComplete;
        private readonly Action<float> _onUpdate;
        private readonly bool _hasAttachedBehavior;

        private float _startTime;
        private float _lastTime;

        private float _endTime => _startTime + Duration;
    }
}