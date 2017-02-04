using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityLib {
    public class UpdateHandler : MonoBehaviour {
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

        public static UpdateHandler New() {
            UpdateHandler gu = new GameObject("game_update").AddComponent<UpdateHandler>();
            return gu;
        }

        private IEnumerator Run() {
            do {
                printExecutionStatus();
                if (blocking) {
                    if (currentBlocking == null) {
                        currentBlocking = nextBlockingRequest;
                        yield return currentBlocking;
                        currentBlocking = null;
                    }
                    continue;
                }
                if (nonblocking) {
                    currentNonblocking = nextNonblocking;
                    StartCoroutine(currentNonblocking);
                }
                yield return Wait.ForEndOfFrame;
                // if (!blocking && !nonblocking) {
                //     if ()
                // }
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