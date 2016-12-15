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

        private static string debug_route11 = "http://10.0.0.76:80/api/profile/create";
        private static string debug_route22 = "http://tyrant.systems:80/api/profile/create";

        public static string DebugAddr0 {
            get {
                if (useLocalAsHost) {
                    return debug_route1;
                } else {
                    return debug_route2;
                }
            }
        }

        public static string DebugAddr1 {
            get {
                if (useLocalAsHost) {
                    return debug_route11;
                } else {
                    return debug_route22;
                }
            }
        }

        protected override bool OnInit() {
            return true;
        }
    }
}