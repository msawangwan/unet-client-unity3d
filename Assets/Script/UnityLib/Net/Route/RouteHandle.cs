namespace UnityLib.Framework.Net {
    public class RouteHandle : ControllerBehaviour {
        private static bool localAddr = true;

        private static string debug_local_availability = "http://10.0.0.76:80/api/profile/availability";
        private static string debug_remote_availability = "http://tyrant.systems:80/api/profile/availability";

        private static string debug_local_create_profile = "http://10.0.0.76:80/api/profile/new";
        private static string debug_remote_create_profile= "http://tyrant.systems:80/api/profile/new";

        private static string debug_local_store_world_data = "http://10.0.0.76:80/api/profile/world/load";
        private static string debug_remote_store_world_data = "http://tyrant.systems:80/api/profile/world/load";

        private static string session_list_all_active = "http://10.0.0.76:80/api/session/active";
        private static string remote_session_list_all_active = "http://tyrant.systems:80/api/session/active";

        private static string session_check_available = "http://10.0.0.76:80/api/session/availability";
        private static string remote_session_check_available = "http://tyrant.systems:80/api/session/availability";

        private static string session_create_new = "http://10.0.0.76:80/api/session/new";
        private static string remote_session_create_new = "http://tyrant.systems:80/api/session/new";

        private static string session_make_active = "http://10.0.0.76:80/api/session/new/open";
        private static string remote_session_make_active = "http://tyrant.systems:80/api/session/new/open";

        private static string session_join_existing = "http://10.0.0.76:80/api/session/new/join";
        private static string remote_session_join_existing = "http://tyrant.systems:80/api/session/new/join";

        private static string session_establish_conn = "http://10.0.0.76:80/api/session/new/connect";
        private static string remote_session_establish_conn = "http://tyrant.systems:80/api/session/new/connect";

        private static string game_start_update = "http://10.0.0.76:80/api/game/update/start";
        private static string remote_game_start_update = "http://tyrant.systems:80/api/game/update/start";

        private static string game_fetch_frame = "http://10.0.0.76:80/api/game/update/frame";
        private static string remote_game_fetch_frame = "http://tyrant.systems:80/api/game/update/frame";

        private static string game_kill = "http://10.0.0.76:80/api/game/update/kill";

        public static string Debug_Addr_Availability {
            get {
                if (localAddr) {
                    return debug_local_availability;
                } else {
                    return debug_remote_availability;
                }
            }
        }

        public static string Debug_Addr_Create_Profile {
            get {
                if (localAddr) {
                    return debug_local_create_profile;
                } else {
                    return debug_remote_create_profile;
                }
            }
        }

        public static string Debug_Addr_Store_World_Data {
            get {
                if (localAddr) {
                    return debug_local_store_world_data;
                } else {
                    return debug_remote_store_world_data;
                }
            }
        }

        public static string Session_ActiveList {
            get {
                if (localAddr) {
                    return session_list_all_active;
                } else {
                    return remote_session_list_all_active;
                }
            }
        }

        public static string Session_Available {
            get {
                if (localAddr) {
                    return session_check_available;
                } else {
                    return remote_session_check_available;
                }
            }
        }

        public static string Session_CreateNew {
            get {
                if (localAddr) {
                    return session_create_new;
                } else {
                    return remote_session_create_new;
                }
            }
        }

        public static string Session_MakeActive {
            get {
                if (localAddr) {
                    return session_make_active;
                } else {
                    return remote_session_make_active;
                }
            }
        }

        public static string Session_JoinNew {
            get {
                if (localAddr) {
                    return session_join_existing;
                } else {
                    return remote_session_join_existing;
                }
            }
        }

        public static string Session_EstablishConn {
            get {
                if (localAddr){
                    return session_establish_conn;
                } else {
                    return remote_session_establish_conn;
                }
            }
        }

        public static string Game_FetchFrameUpdate {
            get {
                if (localAddr) {
                    return game_fetch_frame;
                } else {
                    return remote_game_fetch_frame;
                }
            }
        }

        public static string Game_StartUpdate {
            get {
                if (localAddr) {
                    return game_start_update;
                } else {
                    return remote_game_start_update;
                }
            }
        }

        protected override bool OnInit() {
            return true;
        }
    }
}