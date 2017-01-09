namespace UnityLib.Net {
    public class RouteHandle : ControllerBehaviour {
        private static bool useLocalAddr = true;

        private static string prefix = "api";

        private static string addr_local = string.Format("{0}/{1}", "http://10.0.0.76:80", prefix);
        private static string addr_remote = string.Format("{0}/{1}", "http://tyrant.systems:80", prefix);

        private static string BuildRoute(string addr, string resource) {
            return string.Format("{0}/{1}", addr, resource);
        }

        private static void another() {
            System.Action pooop = () => {
                another();
            };
        }

        /* DEPRECATED */
        private static string debug_local_availability = "http://10.0.0.76:80/api/profile/availability";
        private static string debug_remote_availability = "http://tyrant.systems:80/api/profile/availability";

        private static string debug_local_create_profile = "http://10.0.0.76:80/api/profile/new";
        private static string debug_remote_create_profile= "http://tyrant.systems:80/api/profile/new";

        private static string debug_local_store_world_data = "http://10.0.0.76:80/api/profile/world/load";
        private static string debug_remote_store_world_data = "http://tyrant.systems:80/api/profile/world/load";

        /* SESSION */
        private static string resource_session_register = "session/register/key";
        private static string resource_session_register_name = "session/register/name";
        private static string resource_session_check_game_name_available = "session/host/name/availability";
        private static string resource_session_hostInstance = "session/host/instance";

        private static string session_list_all_active = "http://10.0.0.76:80/api/session/active";
        private static string remote_session_list_all_active = "http://tyrant.systems:80/api/session/active";

        private static string session_check_available = "http://10.0.0.76:80/api/session/availability";
        private static string remote_session_check_available = "http://tyrant.systems:80/api/session/availability";

        private static string session_create_new = "http://10.0.0.76:80/api/session/new";
        private static string remote_session_create_new = "http://tyrant.systems:80/api/session/new";

        private static string session_make_active = "http://10.0.0.76:80/api/session/new/open";
        private static string remote_session_make_active = "http://tyrant.systems:80/api/session/new/open";

        private static string remote_session_key_from_instance = "http://tyrant.systems:80/api/session/new/instance/key";

        private static string session_join_existing = "http://10.0.0.76:80/api/session/new/join";
        private static string remote_session_join_existing = "http://tyrant.systems:80/api/session/new/join";

        private static string session_establish_conn = "http://10.0.0.76:80/api/session/new/connect";
        private static string remote_session_establish_conn = "http://tyrant.systems:80/api/session/new/connect";


        /* GAME */
        private static string game_start_update = "http://10.0.0.76:80/api/game/update/start";
        private static string remote_game_start_update = "http://tyrant.systems:80/api/game/update/start";

        private static string resourece_game_enterUpdate = "api/game/update/enter";

        private static string game_fetch_frame = "http://10.0.0.76:80/api/game/update/frame";
        private static string remote_game_fetch_frame = "http://tyrant.systems:80/api/game/update/frame";


        private static string game_kill = "http://10.0.0.76:80/api/game/update/kill";

        public static string Debug_Addr_Availability {
            get {
                if (useLocalAddr) {
                    return debug_local_availability;
                } else {
                    return debug_remote_availability;
                }
            }
        }

        public static string Debug_Addr_Create_Profile {
            get {
                if (useLocalAddr) {
                    return debug_local_create_profile;
                } else {
                    return debug_remote_create_profile;
                }
            }
        }

        public static string Debug_Addr_Store_World_Data {
            get {
                if (useLocalAddr) {
                    return debug_local_store_world_data;
                } else {
                    return debug_remote_store_world_data;
                }
            }
        }

        public static string Session_ActiveList {
            get {
                if (useLocalAddr) {
                    return session_list_all_active;
                } else {
                    return remote_session_list_all_active;
                }
            }
        }

        public static string Session_Available {
            get {
                if (useLocalAddr) {
                    return session_check_available;
                } else {
                    return remote_session_check_available;
                }
            }
        }

        public static string Session_CreateNew {
            get {
                if (useLocalAddr) {
                    return session_create_new;
                } else {
                    return remote_session_create_new;
                }
            }
        }

        public static string Session_MakeActive {
            get {
                if (useLocalAddr) {
                    return session_make_active;
                } else {
                    return remote_session_make_active;
                }
            }
        }

        public static string Session_KeyFromInstance {
            get {
                if (useLocalAddr) {
                    return "";
                } else {
                    return remote_session_key_from_instance;
                }
            }
        }

        public static string Session_JoinNew {
            get {
                if (useLocalAddr) {
                    return session_join_existing;
                } else {
                    return remote_session_join_existing;
                }
            }
        }

        public static string Session_EstablishConn {
            get {
                if (useLocalAddr){
                    return session_establish_conn;
                } else {
                    return remote_session_establish_conn;
                }
            }
        }

        public static string Game_FetchFrameUpdate {
            get {
                if (useLocalAddr) {
                    return game_fetch_frame;
                } else {
                    return remote_game_fetch_frame;
                }
            }
        }

        public static string Game_StartUpdate {
            get {
                if (useLocalAddr) {
                    return game_start_update;
                } else {
                    return remote_game_start_update;
                }
            }
        }

        public static string Game_EnterUpdate {
            get {
                if (useLocalAddr) {
                    return BuildRoute(addr_local, resourece_game_enterUpdate);
                } else {
                    return BuildRoute(addr_remote, resourece_game_enterUpdate);
                }
            }
        }

        public static string Session_RegisterSession {
            get {
                if (useLocalAddr) {
                    return BuildRoute(addr_local, resource_session_register);
                } else {
                    return BuildRoute(addr_remote, resource_session_register);
                }
            }
        }

        public static string Session_SetPlayerName {
            get {
                if (useLocalAddr) {
                    return BuildRoute(addr_local, resource_session_register_name);
                } else {
                    return BuildRoute(addr_remote, resource_session_register_name);
                }
            }
        }

        public static string Session_CheckGameNameAvailable {
            get {
                if (useLocalAddr) {
                    return BuildRoute(addr_local, resource_session_check_game_name_available);
                } else {
                    return BuildRoute(addr_remote, resource_session_check_game_name_available);
                }
            }
        }

        public static string Session_HostNewInstance {
            get {
                if (useLocalAddr) {
                    return BuildRoute(addr_local, resource_session_hostInstance);
                } else {
                    return BuildRoute(addr_remote, resource_session_hostInstance);
                }
            }
        }

        protected override bool OnInit() {
            return true;
        }
    }
}