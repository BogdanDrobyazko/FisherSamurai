 
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
 

namespace MinigameBehaviorSystem
{
    public class MinigameReferences : MonoBehaviour
    {
        [Header("Environment")] 
        public GameObject cat;
        public GameObject sky;
        public GameObject pirs;
        public GameObject water;
        public GameObject line;

        [Header("Hitching State Settings")] 
        public SortingGroup hitchingSortingGroup;
        public Canvas hitchingCanvas;
        public GameObject hitchingShadow;
        public GameObject hitchingFishOrbit;
        public GameObject hitchingFish;

        [Header("Pulling State Settings")] 
        public SortingGroup pullingSortingGroup;

        [Header("ProgressBar")] 
        public PullingProgressBar pullingProgressBar;


        [Header("Fish")] 
        public Transform fish;

        [Header("Pulling Wiew")]
        public Transform rodZone;

        public Transform catchPoint;
        public int currentHookTypeIndex;


        [Header("Reward State Settings")] 
        public GameDataSystem gameDataSystem;
        public FishingPrepareSystem fishingPrepareSystem;
        public Canvas catchedFishCanvas;
        public Canvas newCatchedFishCanvas;
        [Space] 
        public GameObject fishesCollection;
        public FishData rewardFish;
        [Space] 
        public GameObject catchedFishField;
        public TextMeshProUGUI rewardRankTMPro;
        public GameObject rewardSprite;
        public GameObject rewardFrame;
        [Space] 
        public GameObject NewCatchedFishField;
        public TextMeshProUGUI newRewardTextMeshPro;
        public GameObject newRewardSprite;
  
    }
}