using UnityEngine;
using System;
using System.IO;
using System.Net;
using System.Collections;

namespace UnityAPI.Framework.Net {
    public class ServiceController : ControllerBehaviour {
        private static bool useLocalAsHost = false;
        private static string debug_route1 = "http://10.0.0.76:80/api/availability";
        private static string debug_route2 = "http://tyrant.systems:80/api/availability";

        public static string DebugAddr {
            get {
                if (useLocalAsHost) {
                    return debug_route1;
                } else {
                    return debug_route2;
                }
            }
        }

        protected override bool OnInit() {
            return true;
        }
    }
}