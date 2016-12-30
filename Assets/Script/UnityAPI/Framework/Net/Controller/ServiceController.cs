namespace UnityAPI.Framework.Net {
    public class ServiceController : ControllerBehaviour {
        private static bool useLocalAsHost = true;
        private static string debug_local_availability = "http://10.0.0.76:80/api/profile/availability";
        private static string debug_remote_availability = "http://tyrant.systems:80/api/profile/availability";

        private static string debug_local_create_profile = "http://10.0.0.76:80/api/profile/new";
        private static string debug_remote_create_profile= "http://tyrant.systems:80/api/profile/new";

        private static string debug_local_store_world_data = "http://10.0.0.76:80/api/profile/world/load";
        private static string debug_remote_store_world_data = "http://tyrant.systems:80/api/profile/world/load";

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

        public static string Debug_Addr_Store_World_Data {
            get {
                if (useLocalAsHost) {
                    print("route request: " + debug_local_store_world_data);
                    return debug_local_store_world_data;
                } else {
                    print("route request: " + debug_remote_store_world_data);
                    return debug_remote_store_world_data;
                }
            }
        }

        protected override bool OnInit() {
            return true;
        }
    }
}