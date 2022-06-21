using System;
using System.Collections.Generic;
using cna.poo;
using UnityEngine;

namespace cna {
    [Serializable]
    public class CardVO {

        [Header("CardVO")]
        [SerializeField] private int uniqueId;
        [SerializeField] private CardType_Enum cardType;
        [SerializeField] private string cardTitle;
        [SerializeField] private Image_Enum cardImage;

        [Header("CardActionVO")]
        [SerializeField] protected CardColor_Enum cardColor;
        [SerializeField] protected List<string> actions;
        [SerializeField] protected List<List<Crystal_Enum>> costs;
        [SerializeField] protected Image_Enum avatar = Image_Enum.NA;
        protected List<List<TurnPhase_Enum>> allowed;
        protected List<List<BattlePhase_Enum>> battleAllowed;

        [Header("CardMonsterVO")]
        [SerializeField] Image_Enum monsterBackCardId;
        [SerializeField] protected int monsterFame;
        [SerializeField] protected int monsterDamage;
        [SerializeField] protected int monsterArmor;
        [SerializeField] protected List<UnitEffect_Enum> monsterEffects;
        [SerializeField] protected MonsterType_Enum monsterType;

        [Header("CardUnitVO")]
        [SerializeField] private int unitCost;
        [SerializeField] private int unitLevel;
        [SerializeField] private int unitArmor;
        [SerializeField] private List<Image_Enum> unitRecruitLocation;
        [SerializeField] private List<Image_Enum> unitResistance;

        [Header("CardGameEffectVO")]
        [SerializeField] private string gameEffectDescription;
        [SerializeField] private Color gameEffectColor;
        [SerializeField] private GameEffect_Enum gameEffectId;
        [SerializeField] private GameEffectDuration_Enum gameEffectDurationId;
        [SerializeField] private bool gameEffectWorld;
        [SerializeField] private bool gameEffectBattle;
        [SerializeField] private bool gameEffectClickable;
        [SerializeField] private bool gameEffectDisplayMulti;

        [Header("SkillVO")]
        [SerializeField] private Image_Enum skillBackCardId;
        [SerializeField] private bool skillInteractive;
        [SerializeField] private SkillRefresh_Enum skillRefresh;

        [Header("SpellVO")]
        [SerializeField] private string[] spellTitle;

        [Header("Ancient Ruins")]
        [SerializeField] private List<MonsterType_Enum> monsters;
        [SerializeField] private List<Reward_Enum> rewards;

        public CardVO(int uniqueId, string cardTitle, Image_Enum cardImage, CardType_Enum cardType) {
            UniqueId = uniqueId;
            CardTitle = cardTitle;
            CardImage = cardImage;
            CardType = cardType;
        }



        public int UniqueId { get => uniqueId; set => uniqueId = value; }
        public string CardTitle { get => cardTitle; set => cardTitle = value; }
        public virtual Image_Enum CardImage { get => cardImage; set => cardImage = value; }
        public CardType_Enum CardType { get => cardType; set => cardType = value; }
        public CardColor_Enum CardColor { get => cardColor; set => cardColor = value; }
        public virtual List<string> Actions { get => actions; set => actions = value; }
        public List<List<Crystal_Enum>> Costs { get => costs; set => costs = value; }
        public List<List<TurnPhase_Enum>> Allowed { get => allowed; set => allowed = value; }
        public List<List<BattlePhase_Enum>> BattleAllowed { get => battleAllowed; set => battleAllowed = value; }
        public Image_Enum Avatar { get => avatar; set => avatar = value; }
        public Image_Enum MonsterBackCardId { get => monsterBackCardId; set => monsterBackCardId = value; }
        public int MonsterFame { get => monsterFame; set => monsterFame = value; }
        public int MonsterDamage { get => monsterDamage; set => monsterDamage = value; }
        public int MonsterArmor { get => monsterArmor; set => monsterArmor = value; }
        public List<UnitEffect_Enum> MonsterEffects { get => monsterEffects; set => monsterEffects = value; }
        public MonsterType_Enum MonsterType { get => monsterType; set => monsterType = value; }
        public int UnitCost { get => unitCost; set => unitCost = value; }
        public int UnitLevel { get => unitLevel; set => unitLevel = value; }
        public int UnitArmor { get => unitArmor; set => unitArmor = value; }
        public List<Image_Enum> UnitRecruitLocation { get => unitRecruitLocation; set => unitRecruitLocation = value; }
        public List<Image_Enum> UnitResistance { get => unitResistance; set => unitResistance = value; }
        public virtual string GameEffectDescription { get => gameEffectDescription; set => gameEffectDescription = value; }
        public Color GameEffectColor { get => gameEffectColor; set => gameEffectColor = value; }
        public GameEffect_Enum GameEffectId { get => gameEffectId; set => gameEffectId = value; }
        public GameEffectDuration_Enum GameEffectDurationId { get => gameEffectDurationId; set => gameEffectDurationId = value; }
        public Image_Enum SkillBackCardId { get => skillBackCardId; set => skillBackCardId = value; }
        public bool SkillInteractive { get => skillInteractive; set => skillInteractive = value; }
        public SkillRefresh_Enum SkillRefresh { get => skillRefresh; set => skillRefresh = value; }
        public bool GameEffectWorld { get => gameEffectWorld; set => gameEffectWorld = value; }
        public bool GameEffectBattle { get => gameEffectBattle; set => gameEffectBattle = value; }
        public bool GameEffectClickable { get => gameEffectClickable; set => gameEffectClickable = value; }
        public bool GameEffectDisplayMulti { get => gameEffectDisplayMulti; set => gameEffectDisplayMulti = value; }
        public string[] SpellTitle { get => spellTitle; set => spellTitle = value; }
        public List<MonsterType_Enum> Monsters { get => monsters; set => monsters = value; }
        public List<Reward_Enum> Rewards { get => rewards; set => rewards = value; }

        public override bool Equals(object obj) {
            return obj is CardVO data &&
                   uniqueId == data.uniqueId;
        }

        public override int GetHashCode() {
            return HashCode.Combine(uniqueId);
        }

        public virtual void OnClick_ActionBasicButton(GameAPI ar) {
            ar.ErrorMsg = "Action Not Implemented";
            D.Action.ProcessActionResultVO(ar);
        }

        public virtual void OnClick_ActionButton(GameAPI ar) {
            ar.ErrorMsg = "Action Not Implemented";
            D.Action.ProcessActionResultVO(ar);
        }

        #region Updates

        public virtual void ActionFinish_00(GameAPI ar) { }
        public virtual void ActionFinish_01(GameAPI ar) { }
        public virtual void ActionFinish_02(GameAPI ar) { }




        #endregion

        public override string ToString() {
            string v = string.Format("uniqueId = {0}, cardTitle = {1}, cardImage = {2}, cardType = {3}", uniqueId, cardTitle, cardImage.ToString(), cardType.ToString());
            return v;
        }
    }
}
