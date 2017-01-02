namespace UnityLib.Framework.Net {
    public class RouteHandle : ControllerBehaviour {
        private static bool useLocalAsHost = true;
        private static string debug_local_availability = "http://10.0.0.76:80/api/profile/availability";
        private static string debug_remote_availability = "http://tyrant.systems:80/api/profile/availability";

        private static string debug_local_create_profile = "http://10.0.0.76:80/api/profile/new";
        private static string debug_remote_create_profile= "http://tyrant.systems:80/api/profile/new";

        private static string debug_local_store_world_data = "http://10.0.0.76:80/api/profile/world/load";
        private static string debug_remote_store_world_data = "http://tyrant.systems:80/api/profile/world/load";

        private static string session_list_all_active = "http://10.0.0.76:80/api/session/active";
        private static string session_check_available = "http://10.0.0.76:80/api/session/availability";
        private static string session_create_new = "http://10.0.0.76:80/api/session/new";
        private static string session_make_active = "http://10.0.0.76:80/api/session/new/open";
        private static string session_join_existing = "http://10.0.0.76:80/api/session/new/join";

        private static string game_fetch_frame = "http://10.0.0.76:80/api/game/frame";

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

        public static string Session_ActiveList {
            get {
                return session_list_all_active;
            }
        }

        public static string Session_Available {
            get {
                return session_check_available;
            }
        }

        public static string Session_CreateNew {
            get {
                return session_create_new;
            }
        }

        public static string Session_MakeActive {
            get {
                return session_make_active;
            }
        }

        public static string Session_JoinNew {
            get {
                return session_join_existing;
            }
        }

        public static string Game_FetchFrame {
            get {
                return game_fetch_frame;
            }
        }

        protected override bool OnInit() {
            return true;
        }
    }
}