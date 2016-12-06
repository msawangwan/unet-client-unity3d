using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuButtonController : MonoBehaviour {
    public static MenuButtonController StaticInstace = null;

    public Button BtnNewGame = null;
	public Button BtnLoadGame = null;
	public Button BtnSaveGame = null;

	private void MapButtons () {
		BtnNewGame.onClick.RemoveAllListeners ();
        BtnNewGame.onClick.AddListener ( () => {
            Debug.LogFormat(gameObject, "selected {0}", BtnNewGame.name);
            Game game = new Game(null);
            StarMap.State savedMap = game.Setup(true, null);
            StateSerializer.WriteSave(savedMap, StringConstant.SaveFile.StarMap);
            MenuButtonController.StaticInstace.gameObject.SetActive(false);
        });

		BtnLoadGame.onClick.RemoveAllListeners ();
		BtnLoadGame.onClick.AddListener ( () => {
            Debug.LogFormat(gameObject, "selected {0}", BtnLoadGame.name);
            StarMap.State load = StateSerializer.LoadFromSave<StarMap.State>(StringConstant.SaveFile.StarMap);
            Game game = new Game(null);
            load = game.Setup(false, load);
            game.Setup(false, load);
            StateSerializer.WriteSave(load, StringConstant.SaveFile.StarMap);
            MenuButtonController.StaticInstace.gameObject.SetActive(false);
        });
    }

	private void Awake () {
        StaticInstace = this;
        MapButtons();
        Debug.LogFormat(gameObject, "menu ready: {0}",gameObject.name);
    }
}
