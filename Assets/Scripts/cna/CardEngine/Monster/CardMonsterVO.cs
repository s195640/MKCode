using System;
using System.Collections.Generic;
using cna.poo;

namespace cna {
    [Serializable]
    public class CardMonsterVO : CardVO {
        public CardMonsterVO(int uniqueId, string monsterName, Image_Enum monster, int fame, int damage, int armor, List<UnitEffect_Enum> monsterEffects) : base(
            uniqueId,
            monsterName,
            monster,
            CardType_Enum.Monster
            ) {
            MonsterFame = fame;
            MonsterDamage = damage;
            MonsterArmor = armor;
            MonsterEffects = monsterEffects;
            if ((int)monster > 1000 && (int)monster < 1050) {
                MonsterBackCardId = Image_Enum.CMG_back;
                MonsterType = MonsterType_Enum.Green;
            } else if ((int)monster > 1050 && (int)monster < 1100) {
                MonsterBackCardId = Image_Enum.CMY_back;
                MonsterType = MonsterType_Enum.Grey;
            } else if ((int)monster > 1100 && (int)monster < 1150) {
                MonsterBackCardId = Image_Enum.CMB_back;
                MonsterType = MonsterType_Enum.Brown;
            } else if ((int)monster > 1150 && (int)monster < 1200) {
                MonsterBackCardId = Image_Enum.CMV_back;
                MonsterType = MonsterType_Enum.Violet;
            } else if ((int)monster > 1200 && (int)monster < 1250) {
                MonsterBackCardId = Image_Enum.CMW_back;
                MonsterType = MonsterType_Enum.White;
            } else if ((int)monster > 1250 && (int)monster < 1300) {
                MonsterBackCardId = Image_Enum.CMR_back;
                MonsterType = MonsterType_Enum.Red;
            }
        }


        public bool isNormalAttack { get => !MonsterEffects.Contains(UnitEffect_Enum.FireAttack) && !MonsterEffects.Contains(UnitEffect_Enum.ColdAttack) && !MonsterEffects.Contains(UnitEffect_Enum.ColdFireAttack); }
        public UnitEffect_Enum getAttackType {
            get {
                if (MonsterEffects.Contains(UnitEffect_Enum.FireAttack)) {
                    return UnitEffect_Enum.FireAttack;
                } else if (MonsterEffects.Contains(UnitEffect_Enum.ColdAttack)) {
                    return UnitEffect_Enum.ColdAttack;
                } else if (MonsterEffects.Contains(UnitEffect_Enum.ColdFireAttack)) {
                    return UnitEffect_Enum.ColdFireAttack;
                } else {
                    return UnitEffect_Enum.None;
                }
            }
        }
        public bool isSummoner { get => MonsterEffects.Contains(UnitEffect_Enum.Summoner); }
    }
}
