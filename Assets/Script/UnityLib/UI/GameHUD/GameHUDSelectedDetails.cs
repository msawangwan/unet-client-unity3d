using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UnityLib {
    public class GameHUDSelectedDetails : MonoBehaviour {
        [SerializeField] private GameObject detailsContainerPanel;
        [SerializeField] private GameObject nodeDetailsControllerPanel;
        [SerializeField] private GameObject unitDetailsControllerPanel;

        [SerializeField] private Text nodeTitleText;
        [SerializeField] private Text unitTitleText;
        [SerializeField] private Text defText;
        [SerializeField] private Text capText;
        [SerializeField] private Text hpText;
        [SerializeField] private Text atkText;
        [SerializeField] private Text mvText;
    }
}
