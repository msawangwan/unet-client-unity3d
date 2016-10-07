using UnityEngine;
// a game needs:
// a game board aka the star map
// players
public class Game {
    public Player[] Players { get; private set; }
    public StarMap StarMapLocalInstance { get; private set; }

    public Game ( Player[] players ) {
        Players = players;
    }

    public void Setup () {

    }

    private void InitialiseStarMap ( StarMap.SaveData starMapSaveData ) {
        if (starMapSaveData.IsNew == true) {
            StarMapLocalInstance.InitialiseNewMapWithRandomParameters();
        } else {
            StarMapLocalInstance.GeneratorOptions.UseCustomSeedValue = true;
            StarMapLocalInstance.GeneratorOptions.CustomSeedValue = starMapSaveData.SeedValue;
            StarMapLocalInstance.InitialiseSavedMapWithLoadedParameters();
        }
    }

    private void InitialisePlayer ( Player player ) {
        if (player != null) {
            if (player.State.StarMapInstance == null) {
                
            }
        }
    }
}
