using UnityEngine;

public static class RandomUtil {

	public static Random.State SetSeedState (int seedValue) {
		Random.InitState (seedValue); // seed the pRNG
        return Random.state; // return a state if we want to save for later
    }

	public static Random.State SetSeedStateDefault () {
        Random.InitState(System.Environment.TickCount);
        return Random.state;
    }

	public static void TestRandom() {
		Debug.LogFormat("{0}, {1}, {2}", Random.Range(0,100),Random.Range(0,100), System.Environment.TickCount);
	}
}
