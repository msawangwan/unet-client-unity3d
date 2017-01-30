using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityLib {
    public class GameUpdate : MonoBehaviour {
        private Queue<IEnumerator> blockingRequestBuffer = new Queue<IEnumerator>();
        private Queue<IEnumerator> nonblockingRequestBuffer = new Queue<IEnumerator>();

        private IEnumerator routine = null;
        private IEnumerator currentNonblocking = null;
        private IEnumerator currentBlocking = null;

        private IEnumerator nextBlockingRequest {
            get {
                return blockingRequestBuffer.Dequeue();
            }
        }

        private IEnumerator nextNonblocking {
            get {
                return nonblockingRequestBuffer.Dequeue();
            }
        }

        private bool blocking {
            get {
                return blockingRequestBuffer.Count > 0;
            }
        }

        private bool nonblocking {
            get {
                return nonblockingRequestBuffer.Count > 0;
            }
        }

        public void StartExecution() {
            if (routine == null) {
                routine = Run();
                StartCoroutine(routine);
            } else {
                Debug.LogError("[+] already executing Run() ...");
            }
        }

        public void AddBlocking(IEnumerator e) {
            blockingRequestBuffer.Enqueue(e);
        } 

        public void AddNonblocking(IEnumerator e) {
            nonblockingRequestBuffer.Enqueue(e);
        }

        public static GameUpdate New() {
            GameUpdate gu = new GameObject("game_update").AddComponent<GameUpdate>();
            return gu;
        }

        private IEnumerator Run() {
            do {
                printExecutionStatus();
                if (blocking) {
                    if (currentBlocking == null) {
                        currentBlocking = nextBlockingRequest;
                        Debug.LogFormat("[+] blocking now ... [{0}]", Time.time);
                        yield return currentBlocking;
                        Debug.LogFormat("[+] done blocking [{0}]", Time.time);
                        currentBlocking = null;
                    }
                    continue;
                }
                if (nonblocking) {
                    currentNonblocking = nextNonblocking;
                    Debug.LogFormat("[+] started a reqest [{0}]", Time.time);
                    StartCoroutine(currentNonblocking);
                }
                yield return Wait.ForEndOfFrame;
            } while (true);
        }

        private void printExecutionStatus() {
            Debug.LogFormat(
                "[+] status\n \t number of blocking routines [{0}]\n \t number of non-blocking routines [{1}]\n \t is blocking [{2}]\n",
                blockingRequestBuffer.Count, nonblockingRequestBuffer.Count, blocking
            );
        }
    }
}