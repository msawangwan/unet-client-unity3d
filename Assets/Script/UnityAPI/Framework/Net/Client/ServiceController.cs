using UnityEngine;
using System;
using System.IO;
using System.Net;
using System.Collections;

namespace UnityAPI.Framework.Net {
    public class ServiceController : ControllerBehaviour {
        [SerializeField] private ClientHandler.Configuration clientConfiguration;
        [SerializeField] private string debug_route1 = "http://10.0.0.76:8000/api/availability";
        [SerializeField] private string debug_route2 = "http://tyrant.systems:80/api/availability";

        // public Func<bool> ValidateProfileNameAvailable(string profileAsJson) {
        //     Func<bool> result = null;
        //     StartCoroutine(ValidationRoutine());
        //     return result;
        // }

        protected override bool OnInit() {
            return true;
        }

        // private IEnumerator ValidationRoutine() {
        //     do {
        //         yield return null;
        //     } while (true);
        // }
    }
}