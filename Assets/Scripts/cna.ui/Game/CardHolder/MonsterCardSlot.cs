using cna.poo;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace cna.ui {
    public class MonsterCardSlot : MonoBehaviour {

        private BattleCanvas _battleCanvas;
        public BattleCanvas BattleCanvas { get { if (_battleCanvas == null) { _battleCanvas = GameObject.Find("BattleCanvas").GetComponent<BattleCanvas>(); } return _battleCanvas; } }
        public MonsterDetailsVO MonsterDetails { get => monsterDetails; set => monsterDetails = value; }

        [SerializeField] private Button button;
        [SerializeField] private TextMeshProUGUI cardTitle;
        [SerializeField] private AddressableImage monsterImage;
        [SerializeField] private TextMeshProUGUI armor;
        [SerializeField] private TextMeshProUGUI damage;
        [SerializeField] private TextMeshProUGUI fame;
        [SerializeField] private GameObject attackPhysical;
        [SerializeField] private GameObject attackFire;
        [SerializeField] private GameObject attackCold;
        [SerializeField] private GameObject attackColdFire;
        [SerializeField] private GameObject attackSummoner;
        [SerializeField] private GameObject fortification01;
        [SerializeField] private GameObject fortification02;
        [SerializeField] public GameObject Selected;
        [SerializeField] public GameObject deadImage;
        [SerializeField] public GameObject blockImage;
        [SerializeField] public GameObject summonImage;
        [SerializeField] public GameObject assignImage;
        [SerializeField] public GameObject fireResistImage;
        [SerializeField] public GameObject iceResistImage;
        [SerializeField] public GameObject physicalResistImage;

        [SerializeField] public GameObject buffSwiftImage;
        [SerializeField] public GameObject buffPoisonImage;
        [SerializeField] public GameObject buffParalyzeImage;
        [SerializeField] public GameObject buffBrutalImage;

        [SerializeField] public GameObject MonsterStats;
        [SerializeField] public GameObject MonsterName;

        private MonsterDetailsVO monsterDetails;

        public void SetupUI(int uniqueId) {
            SetupUI(new MonsterDetailsVO(D.Cards[uniqueId], new CNAMap<GameEffect_Enum, WrapList<int>>()));
        }

        public void SetupUI(MonsterDetailsVO monsterDetails) {
            UpdateUI(monsterDetails);
        }

        public void UpdateUI(MonsterDetailsVO monsterDetails) {
            MonsterDetails = monsterDetails;
            bool monsterVisable = D.LocalPlayer.VisableMonsters.Contains(monsterDetails.UniqueId);
            if (monsterVisable) {
                if (MonsterDetails.monsterCard.CardType == CardType_Enum.Monster) {
                    MonsterStats.SetActive(true);
                    MonsterName.SetActive(true);
                    cardTitle.text = MonsterDetails.CardTitle;
                    monsterImage.ImageEnum = MonsterDetails.MonsterImage;
                    armor.text = "" + MonsterDetails.Armor;
                    fame.text = "" + MonsterDetails.Fame;
                    if (MonsterDetails.Summoner) {
                        attackSummoner.SetActive(true);
                        damage.gameObject.SetActive(false);
                    } else {
                        attackSummoner.SetActive(false);
                        damage.gameObject.SetActive(true);
                        damage.text = "" + MonsterDetails.Damage;
                        if (MonsterDetails.FireAttack) {
                            attackPhysical.SetActive(false);
                            attackFire.SetActive(true);
                            attackCold.SetActive(false);
                            attackColdFire.SetActive(false);
                        } else if (MonsterDetails.ColdAttack) {
                            attackPhysical.SetActive(false);
                            attackFire.SetActive(false);
                            attackCold.SetActive(true);
                            attackColdFire.SetActive(false);
                        } else if (MonsterDetails.ColdFireAttack) {
                            attackPhysical.SetActive(false);
                            attackFire.SetActive(false);
                            attackCold.SetActive(false);
                            attackColdFire.SetActive(true);
                        } else {
                            attackPhysical.SetActive(true);
                            attackFire.SetActive(false);
                            attackCold.SetActive(false);
                            attackColdFire.SetActive(false);
                        }
                    }
                    fortification01.SetActive(MonsterDetails.Fortified);
                    fortification02.SetActive(MonsterDetails.DoubleFortified);

                    fireResistImage.SetActive(MonsterDetails.FireResistance);
                    iceResistImage.SetActive(MonsterDetails.IceResistance);
                    physicalResistImage.SetActive(MonsterDetails.PhysicalResistance);

                    buffSwiftImage.SetActive(MonsterDetails.Swiftness);
                    buffPoisonImage.SetActive(MonsterDetails.Poison);
                    buffParalyzeImage.SetActive(MonsterDetails.Paralyze);
                    buffBrutalImage.SetActive(MonsterDetails.Brutal);
                } else {
                    monsterImage.ImageEnum = MonsterDetails.MonsterImage;
                    MonsterStats.SetActive(false);
                    MonsterName.SetActive(false);
                }
            } else {
                monsterImage.ImageEnum = MonsterDetails.MonsterImageBack;
                MonsterStats.SetActive(false);
                MonsterName.SetActive(false);
            }

            Selected.SetActive(D.LocalPlayer.Battle.SelectedMonsters.Contains(MonsterDetails.UniqueId));
            deadImage.SetActive(false);
            blockImage.SetActive(false);
            summonImage.SetActive(false);
            assignImage.SetActive(false);
            if (D.LocalPlayer.Battle.BattlePhase > BattlePhase_Enum.Provoke && D.LocalPlayer.Battle.Monsters.ContainsKey(MonsterDetails.UniqueId)) {
                MonsterMetaData monsterMetaData = D.LocalPlayer.Battle.Monsters[MonsterDetails.UniqueId];
                deadImage.SetActive(monsterMetaData.Dead);
                switch (D.LocalPlayer.Battle.BattlePhase) {
                    case BattlePhase_Enum.RangeSiege: {
                        blockImage.SetActive(false);
                        summonImage.SetActive(false);
                        assignImage.SetActive(false);
                        break;
                    }
                    case BattlePhase_Enum.Block: {
                        blockImage.SetActive(monsterMetaData.Blocked);
                        summonImage.SetActive(MonsterDetails.Summoner);
                        assignImage.SetActive(false);
                        break;
                    }
                    case BattlePhase_Enum.AssignDamage: {
                        blockImage.SetActive(monsterMetaData.Blocked);
                        summonImage.SetActive(MonsterDetails.Summoner);
                        assignImage.SetActive(monsterMetaData.Assigned);
                        break;
                    }
                    case BattlePhase_Enum.Attack: {
                        blockImage.SetActive(false);
                        summonImage.SetActive(false);
                        assignImage.SetActive(false);
                        break;
                    }
                }
            }
        }

        public void OnClick_Monster() {
            BattleCanvas.OnClick_MonsterCard(this);
        }

        public void IsButtonInteractable(bool val) {
            button.interactable = val;
        }
    }
}
