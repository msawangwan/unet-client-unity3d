using UnityEngine;
using System;
using System.IO;
using System.Net;
using System.Collections;

namespace UnityAPI.Framework.Net {
    public class ServiceController : ControllerBehaviour {
        private static bool useLocalAsHost = false;
        private static string debug_local_availability = "http://10.0.0.76:80/api/availability";
        private static string debug_remote_availability = "http://tyrant.systems:80/api/availability";

        private static string debug_local_create_profile = "http://10.0.0.76:80/api/profile/create";
        private static string debug_remote_create_profile= "http://tyrant.systems:80/api/profile/create";

        public static string Debug_Addr_Availability {
            get {
                if (useLocalAsHost) {
                    print("route request: " + debug_local_availability);
                    return debug_local_availability;
                } else {
                    print("route request: " + debug_remote_availability);
                    return debug_remote_availability;
                }
            }
        }

        public static string Debug_Addr_Create_Profile {
            get {
                if (useLocalAsHost) {
                    print("route request: " + debug_local_create_profile);
                    return debug_local_create_profile;
                } else {
                    print("route request: " + debug_remote_create_profile);
                    return debug_remote_create_profile;
                }
            }
        }

        protected override bool OnInit() {
            return true;
        }
    }
}