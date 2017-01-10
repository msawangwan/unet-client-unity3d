namespace UnityLib.Net {
    public class RouteHandle : ControllerBehaviour {
        private static bool useLocalAddr = false;

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
     
        /* SESSION */
        private static string resource_session_register                  = "session/register/key";
        private static string resource_session_register_name             = "session/register/name";
        private static string resource_session_check_game_name_available = "session/host/name/availability";
        private static string resource_session_hostInstance              = "session/host/simulation";
        private static string resource_session_joinShowList              = "session/join/lobby/list";

        /* GAME */
        private static string game_start_update = "http://10.0.0.76:80/api/game/update/start";
        private static string remote_game_start_update = "http://tyrant.systems:80/api/game/update/start";

        private static string resourece_game_enterUpdate = "api/game/update/enter";

        private static string game_fetch_frame = "http://10.0.0.76:80/api/game/update/frame";
        private static string remote_game_fetch_frame = "http://tyrant.systems:80/api/game/update/frame";


        private static string game_kill = "http://10.0.0.76:80/api/game/update/kill";

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

        public static string Session_CheckHostNameAvailable {
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

        public static string Session_FetchLobbyList {
            get {
                if (useLocalAddr) {
                    return BuildRoute(addr_local, resource_session_joinShowList);
                } else {
                    return BuildRoute(addr_remote, resource_session_joinShowList);
                }
            }
        }

        protected override bool OnInit() {
            return true;
        }
    }
}