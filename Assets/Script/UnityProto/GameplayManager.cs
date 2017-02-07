using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityProto {
    public class GameplayManager : MonoBehaviour {
        public static GameplayManager Instance;

        private Queue<IEnumerator> blockingRoutines = new Queue<IEnumerator>();
        private Queue<IEnumerator> nonblockingRoutines = new Queue<IEnumerator>();

        private IEnumerator currentBlockingRoutine = null;

        public void EnqueueRoutine(IEnumerator r, bool blocking) {
            if (blocking) {
                blockingRoutines.Enqueue(r);
            } else {
                nonblockingRoutines.Enqueue(r);
            }
        }

        private void Awake() {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private IEnumerator Run() {
            do {
                if (blockingRoutines.Count > 0) {
                    if (currentBlockingRoutine == null) {
                        currentBlockingRoutine = blockingRoutines.Dequeue();
                        yield return currentBlockingRoutine;
                        currentBlockingRoutine = null;
                    }
                    continue;
                }
                if (nonblockingRoutines.Count > 0) {
                    StartCoroutine(nonblockingRoutines.Dequeue());
                }
                yield return UnityLib.Wait.ForEndOfFrame;
            } while (true);
        }

        private void Start() {
            StartCoroutine(Run());
        }
    }
}
