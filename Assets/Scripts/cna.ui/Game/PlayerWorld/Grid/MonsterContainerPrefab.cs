using cna.poo;
using TMPro;
using UnityEngine;

namespace cna.ui {
    public class MonsterContainerPrefab : MonoBehaviour {
        [SerializeField] private AddressableSprite MonsterImage;
        [SerializeField] private GameObject Visable;
        [SerializeField] private GameObject PhysicalAttack;
        [SerializeField] private GameObject ColdAttack;
        [SerializeField] private GameObject FireAttack;
        [SerializeField] private GameObject ColdFireAttack;
        [SerializeField] private GameObject SummonAttack;

        [SerializeField] private TextMeshPro PhysicalAttakText;
        [SerializeField] private TextMeshPro ColdAttakText;
        [SerializeField] private TextMeshPro FireAttakText;
        [SerializeField] private TextMeshPro ColdFireAttakText;
        [SerializeField] private TextMeshPro FameText;
        [SerializeField] private TextMeshPro ArmorText;
        [SerializeField] private GameObject FortificationImage01;
        [SerializeField] private GameObject FortificationImage02;

        [SerializeField] private GameObject FireResistImage;
        [SerializeField] private GameObject IceResistImage;
        [SerializeField] private GameObject PhysicalResistImage;

        [SerializeField] private GameObject ParalyzeImage;
        [SerializeField] private GameObject SwiftImage;
        [SerializeField] private GameObject PoisonImage;
        [SerializeField] private GameObject BrutalImage;



        private CardVO card;

        public void SetupUI(CardVO card, bool visable) {
            this.card = card;
            UpdateUI(visable);
            buildMonsterImage();
        }

        public void UpdateUI(bool visable) {
            MonsterImage.ImageEnum = visable ? card.CardImage : card.MonsterBackCardId;
            Visable.SetActive(visable && card.CardType == CardType_Enum.Monster);
        }

        public void buildMonsterImage() {
            if (card.CardType == CardType_Enum.Monster) {
                bool summonAttack = card.MonsterEffects.Contains(UnitEffect_Enum.Summoner);
                bool fireAttack = card.MonsterEffects.Contains(UnitEffect_Enum.FireAttack);
                bool coldAttack = card.MonsterEffects.Contains(UnitEffect_Enum.ColdAttack);
                bool coldFireAttack = card.MonsterEffects.Contains(UnitEffect_Enum.ColdFireAttack);
                bool physicalAttack = !(fireAttack || coldAttack || coldFireAttack || summonAttack);
                PhysicalAttack.SetActive(physicalAttack);
                ColdAttack.SetActive(coldAttack);
                FireAttack.SetActive(fireAttack);
                ColdFireAttack.SetActive(coldFireAttack);
                SummonAttack.SetActive(summonAttack);
                PhysicalAttakText.text = "" + card.MonsterDamage;
                ColdAttakText.text = "" + card.MonsterDamage;
                FireAttakText.text = "" + card.MonsterDamage;
                ColdFireAttakText.text = "" + card.MonsterDamage;
                FameText.text = "" + card.MonsterFame;
                ArmorText.text = "" + card.MonsterArmor;

                bool fortified = card.MonsterEffects.Contains(UnitEffect_Enum.Fortified);
                bool doubleFortified = card.MonsterEffects.Contains(UnitEffect_Enum.DoubleFortified);

                FortificationImage01.SetActive(fortified);
                FortificationImage02.SetActive(doubleFortified);

                bool fireResistance = card.MonsterEffects.Contains(UnitEffect_Enum.FireResistance);
                bool iceResistance = card.MonsterEffects.Contains(UnitEffect_Enum.IceResistance);
                bool physicalResistance = card.MonsterEffects.Contains(UnitEffect_Enum.PhysicalResistance);
                FireResistImage.SetActive(fireResistance);
                IceResistImage.SetActive(iceResistance);
                PhysicalResistImage.SetActive(physicalResistance);

                bool paralyze = card.MonsterEffects.Contains(UnitEffect_Enum.Paralyze);
                bool swift = card.MonsterEffects.Contains(UnitEffect_Enum.Swiftness);
                bool poison = card.MonsterEffects.Contains(UnitEffect_Enum.Poison);
                bool brutal = card.MonsterEffects.Contains(UnitEffect_Enum.Brutal);
                ParalyzeImage.SetActive(paralyze);
                SwiftImage.SetActive(swift);
                PoisonImage.SetActive(poison);
                BrutalImage.SetActive(brutal);
            }
        }
    }
}
