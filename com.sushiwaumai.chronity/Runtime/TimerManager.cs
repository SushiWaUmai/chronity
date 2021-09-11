using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Chronity
{
    public partial class Timer : TimerBase
    {
        private class TimerManager : MonoBehaviour
        {
            public static TimerManager Singleton
            {
                get
                {
                    if (!_singleton)
                    {
                        GameObject go = new GameObject("Chronity");
                        _singleton = go.AddComponent<TimerManager>();
                    }

                    return _singleton;
                }
            }

            public static void RegisterTimer(Timer timer) => Singleton._timers.Add(timer);

            public static void PauseAllTimers()
            {
                for (int i = 0; i < Singleton._timers.Count; i++)
                    Singleton._timers[i].Pause();
            }

            public static void ResumeAllTimers()
            {
                for (int i = 0; i < Singleton._timers.Count; i++)
                    Singleton._timers[i].Resume();
            }

            public static void CancelAllTimers()
            {
                for (int i = 0; i < Singleton._timers.Count; i++)
                    Singleton._timers[i].Cancel();
            }

            private static void UpdateAllTimers()
            {
                for (int i = 0; i < Singleton._timers.Count; i++)
                    Singleton._timers[i].Update();
            }

            private static void RemoveAbundantTimers()
            {
                Singleton._timers.RemoveAll(x => x.IsDone);
            }

            private void HandleSceneChange(Scene from, Scene to)
            {
                for (int i = 0; i < _timers.Count; i++)
                {
                    if (_timers[i].CancelOnSceneChange)
                    {
                        _timers[i].Cancel();
                    }
                }

                RemoveAbundantTimers();
            }

            private void Awake()
            {
                DontDestroyOnLoad(gameObject);
            }

            private void Start()
            {
                SceneManager.activeSceneChanged += HandleSceneChange;
            }

            private void OnDestroy()
            {
                SceneManager.activeSceneChanged -= HandleSceneChange;
            }

            private void Update()
            {
                UpdateAllTimers();
                RemoveAbundantTimers();
            }

            private static TimerManager _singleton;

            private List<Timer> _timers = new List<Timer>();
        }
    }
}