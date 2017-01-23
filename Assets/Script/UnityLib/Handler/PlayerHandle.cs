using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityLib {
    public class PlayerHandle : MonoBehaviour {
        public Player PlayerInstance { get; private set; }

        public void OnPlayerJoinedGame(Player.Parameters playerparameters) {
            PlayerInstance.ID = playerparameters.id;
            PlayerInstance.Index = playerparameters.index;
        }

        public static PlayerHandle New(string name) {
            PlayerHandle ph = new GameObject(name).AddComponent<PlayerHandle>();
            ph.PlayerInstance = Player.New(name);
            return ph;
        }
    }
}
