using System.Collections.Generic;
using cna.poo;
using UnityEngine;

namespace cna.ui {
    public class BattleCanvas : BattleEngine {
        [Header("Shared")]
        [SerializeField] private BattleUndoButton BattleUndoButtonPanel;
        [SerializeField] private MonsterHandPanel MonsterHandPanel;
        [SerializeField] private ProvokeMonsterPanel ProvokeMonsterPanel;
        [SerializeField] private MonsterSelectionPanel MonsterSelectionPanel;
        [SerializeField] private DamageMonsterPanel DamageMonsterPanel;
        [SerializeField] private BattleSummaryPanel BattleSummaryPanel;
        [SerializeField] private BattleContributionPanel BattleContributionPanel;
        [SerializeField] private BattleEffectPanel BattleEffectPanel;
        [SerializeField] private GameObject battleOn;
        [SerializeField] private GameObject battleOff;


        public override void UpdateUI() {
            if (AR != null && L.PlayerTurnPhase == TurnPhase_Enum.Battle) {
                battleOn.SetActive(true);
                battleOff.SetActive(false);
                BattleEffectPanel.UpdateUI();
                battleEngine();
            } else {
                battleOn.SetActive(false);
                battleOff.SetActive(true);
            }
        }

        protected override void battleEngine() {
            base.battleEngine();
            BattleUndoButtonPanel.UpdateUI();
            MonsterHandPanel.UpdateUI();
            MonsterSelectionPanel.UpdateUI();
            BattleContributionPanel.UpdateUI();
        }

        #region PROVOKE
        protected override void battleEngine_SetupProvokeMonsters() {
            base.battleEngine_SetupProvokeMonsters();
            B.BattlePhase = BattlePhase_Enum.Provoke;
            ProvokeMonsterPanel.SetupUI();
            AR.PushForce();
        }

        protected override void battleEngine_ProvokeMonsters() {
            ProvokeMonsterPanel.gameObject.SetActive(true);
            MonsterSelectionPanel.gameObject.SetActive(false);
            DamageMonsterPanel.gameObject.SetActive(false);
            BattleSummaryPanel.gameObject.SetActive(false);
        }
        public void OnClick_ProvokeMonster(int index) {
            ProvokeMonsterPanel.OnClick_ProvokeMonster(index);
            UpdateMonsterDetails();
            AR.PushForce();
        }

        public void OnClick_ProvokeAccept() {
            List<string> monsterNames = ProvokeMonsterPanel.getProvokedMonsters().ConvertAll(m => ((CardMonsterVO)D.Cards[m]).CardTitle);
            if (monsterNames.Count > 0) {
                AR.AddLog("[Provoke] :: " + string.Join(", ", monsterNames));
            }
            B.BattlePhase = BattlePhase_Enum.RangeSiege;
            B.SelectedMonsters.Clear();
            AR.PushForce();
        }
        #endregion

        #region RANGE SIEGE
        protected override void battleEngine_RangeSiege() {
            base.battleEngine_RangeSiege();
            bool resolved = true;
            B.Monsters.Values.ForEach(md => {
                resolved = resolved && (md.Dead);
            });
            if (resolved) {
                OnClick_RangeNext();
            } else {
                ProvokeMonsterPanel.gameObject.SetActive(false);
                MonsterSelectionPanel.gameObject.SetActive(true);
                DamageMonsterPanel.gameObject.SetActive(false);
                BattleSummaryPanel.gameObject.SetActive(false);
            }
        }

        public void OnClick_RangeKillMonster() {
            List<string> monsterNames = B.SelectedMonsters.ConvertAll(m => { B.Monsters[m].Dead = true; return ((CardMonsterVO)D.Cards[m]).CardTitle; });
            if (monsterNames.Count > 0) {
                AR.AddLog("[Range Kill] :: " + string.Join(", ", monsterNames));
            }
            B.Siege.Clear();
            B.Range.Clear();
            B.SelectedMonsters.Clear();
            AR.PushForce();
        }

        public void OnClick_RangeNext() {
            B.BattlePhase = BattlePhase_Enum.Block;
            B.SelectedMonsters.Clear();
            List<MonsterMetaData> summonedMonsters = new List<MonsterMetaData>();
            List<string> monsterNames = new List<string>();
            B.Monsters.Values.ForEach(md => {
                CardMonsterVO c = (CardMonsterVO)D.Cards[md.Uniqueid];
                if (!md.Dead && c.MonsterEffects.Contains(UnitEffect_Enum.Summoner)) {
                    int monsterId = D.Scenario.DrawMonster(MonsterType_Enum.Brown);
                    monsterNames.Add(D.Cards[monsterId].CardTitle);
                    MonsterMetaData summonedMonster = new MonsterMetaData(monsterId, md.Location, md.Structure);
                    summonedMonster.Summoned = true;
                    summonedMonster.Summoner = md.Uniqueid;
                    summonedMonsters.Add(summonedMonster);
                    L.VisableMonsters.Add(monsterId);
                }
            });
            if (monsterNames.Count > 0) {
                AR.AddLog("[Summoned] :: " + string.Join(", ", monsterNames));
            }
            summonedMonsters.ForEach(md => {
                B.Monsters.Add(md.Uniqueid, md);
                L.GameEffects.Keys.ForEach(ge => {
                    if (L.GameEffects[ge].Contains(0) && L.GameEffects[ge].Contains(md.Summoner)) {
                        L.AddGameEffect(ge, md.Uniqueid);
                    }
                });
            });
            UpdateMonsterDetails();
            AR.PushForce();
        }
        #endregion

        #region BLOCK
        protected override void battleEngine_Block() {
            base.battleEngine_Block();
            bool resolved = true;
            B.Monsters.Values.ForEach(md => {
                resolved = resolved && (md.Blocked || md.Dead || ((CardMonsterVO)D.Cards[md.Uniqueid]).isSummoner);
            });
            if (resolved) {
                OnClick_BlockNext();
            } else {
                ProvokeMonsterPanel.gameObject.SetActive(false);
                MonsterSelectionPanel.gameObject.SetActive(true);
                DamageMonsterPanel.gameObject.SetActive(false);
                BattleSummaryPanel.gameObject.SetActive(false);
            }
        }

        public void OnClick_BlockMonster() {
            AR.AddLog("[Block] :: " + ((CardMonsterVO)D.Cards[B.SelectedMonsters[0]]).CardTitle);
            L.GameEffects.Keys.ForEach(ge => {
                int count = L.GameEffects[ge].Count;
                switch (ge) {
                    case GameEffect_Enum.CS_BurningShield: {
                        B.Attack.Fire += (4 * count);
                        break;
                    }
                    case GameEffect_Enum.CS_ExplodingShield: {
                        B.Monsters[B.SelectedMonsters[0]].Dead = true;
                        break;
                    }
                    case GameEffect_Enum.AC_IceShield: {
                        for (int i = 0; i < count; i++) {
                            if (L.GameEffects[GameEffect_Enum.AC_IceShield].Values[i] == 0) {
                                L.GameEffects[GameEffect_Enum.AC_IceShield].Values[i] = B.SelectedMonsters[0];
                            }
                        }
                        break;
                    }
                }
            });
            B.Monsters[B.SelectedMonsters[0]].Blocked = true;
            B.Shield.Clear();
            B.SelectedMonsters.Clear();
            L.RemoveGameEffect(GameEffect_Enum.ColdToughness);
            L.RemoveGameEffect(GameEffect_Enum.UtemGuardsmen);
            L.RemoveGameEffect(GameEffect_Enum.CUE_AltemGuardians01);
            L.RemoveGameEffect(GameEffect_Enum.CS_BurningShield);
            L.RemoveGameEffect(GameEffect_Enum.CS_ExplodingShield);
            AR.PushForce();
        }

        public void OnClick_BlockNext() {
            B.BattlePhase = BattlePhase_Enum.AssignDamage;
            B.SelectedMonsters.Clear();
            AR.PushForce();
        }

        #endregion

        #region ASSIGN DAMAGE
        protected override void battleEngine_AssignDamage() {
            base.battleEngine_AssignDamage();
            bool resolved = true;
            B.Monsters.Values.ForEach(md => {
                resolved = resolved && (md.Assigned || md.Blocked || md.Dead || ((CardMonsterVO)D.Cards[md.Uniqueid]).isSummoner);
            });
            if (resolved) {
                B.BattlePhase = BattlePhase_Enum.Attack;
                List<MonsterMetaData> l = B.Monsters.Values.FindAll(md => md.Summoned);
                l.ForEach(md => B.Monsters.Remove(md.Uniqueid));
                UpdateMonsterDetails();
                AR.PushForce();
            } else {
                ProvokeMonsterPanel.gameObject.SetActive(false);
                MonsterSelectionPanel.gameObject.SetActive(true);
                DamageMonsterPanel.gameObject.SetActive(true);
                BattleSummaryPanel.gameObject.SetActive(false);
                DamageMonsterPanel.UpdateUI(AR);
            }
        }

        public void OnClick_SelectAvatar() {
            B.SelectedUnit = 0;
            UpdateUI();
        }

        public void OnClick_AssignDamage() {
            string msg = DamageMonsterPanel.isAllowedToAssignDamage(AR);
            if (msg.Equals("")) {
                AR.AddLog(DamageMonsterPanel.AssignDamage(AR));
                AR.PushForce();
            } else {
                D.Msg(msg);
            }
        }
        #endregion

        #region ATTACK
        protected override void battleEngine_Attack() {
            base.battleEngine_Attack();
            bool resolved = true;
            B.Monsters.Values.ForEach(md => {
                resolved = resolved && md.Dead;
            });
            if (resolved) {
                OnClick_EndBattle();
            } else {
                ProvokeMonsterPanel.gameObject.SetActive(false);
                MonsterSelectionPanel.gameObject.SetActive(true);
                DamageMonsterPanel.gameObject.SetActive(false);
                BattleSummaryPanel.gameObject.SetActive(false);
            }
        }

        public void OnClick_AttackKillMonster() {
            List<string> monsterNames = B.SelectedMonsters.ConvertAll(m => { B.Monsters[m].Dead = true; return ((CardMonsterVO)D.Cards[m]).CardTitle; });
            if (monsterNames.Count > 0) {
                AR.AddLog("[Attack Kill] :: " + string.Join(", ", monsterNames));
            }
            B.Siege.Clear();
            B.Range.Clear();
            B.Attack.Clear();
            B.SelectedMonsters.Clear();
            AR.PushForce();
        }

        public void OnClick_EndBattle() {
            B.BattlePhase = BattlePhase_Enum.EndOfBattle;
            AR.PushForce();
        }
        #endregion

        #region END BATTLE
        protected override void battleEngine_EndOfBattle() {
            ProvokeMonsterPanel.gameObject.SetActive(false);
            MonsterSelectionPanel.gameObject.SetActive(false);
            DamageMonsterPanel.gameObject.SetActive(false);
            BattleSummaryPanel.gameObject.SetActive(true);
            BattleSummaryPanel.UpdateUI();
        }

        public void OnClick_ExitBattle() {
            B.BattlePhase = BattlePhase_Enum.NA;
            AR.TurnPhase(TurnPhase_Enum.AfterBattle);
            BattleSummaryPanel.ExitBattle(AR);
            D.ScreenState = ScreenState_Enum.Map;
            AR.PushForce();
        }
        #endregion

        public void OnClick_MonsterCard(MonsterCardSlot monsterCardSlot) {
            int mid = monsterCardSlot.MonsterDetails.UniqueId;
            bool summoner = monsterCardSlot.MonsterDetails.Summoner;
            MonsterMetaData md = B.Monsters[mid];
            switch (B.BattlePhase) {
                case BattlePhase_Enum.Provoke: { break; }
                case BattlePhase_Enum.RangeSiege: {
                    if (!md.Dead) {
                        if (B.SelectedMonsters.Contains(mid)) {
                            B.SelectedMonsters.Remove(mid);
                        } else {
                            B.SelectedMonsters.Add(mid);
                        }
                        battleEngine();
                    }
                    break;
                }
                case BattlePhase_Enum.Block: {
                    if (!(md.Dead || md.Blocked || summoner)) {
                        if (B.SelectedMonsters.Contains(mid)) {
                            B.SelectedMonsters.Remove(mid);
                        } else {
                            B.SelectedMonsters.Clear();
                            B.SelectedMonsters.Add(mid);
                        }
                        battleEngine();
                    }
                    break;
                }
                case BattlePhase_Enum.AssignDamage: {
                    if (!(md.Dead || md.Blocked || md.Assigned || summoner)) {
                        if (B.SelectedMonsters.Contains(mid)) {
                            B.SelectedMonsters.Remove(mid);
                        } else {
                            B.SelectedMonsters.Clear();
                            B.SelectedMonsters.Add(mid);
                        }
                        battleEngine();
                    }
                    break;
                }
                case BattlePhase_Enum.Attack: {
                    if (!md.Dead) {
                        if (B.SelectedMonsters.Contains(mid)) {
                            B.SelectedMonsters.Remove(mid);
                        } else {
                            B.SelectedMonsters.Add(mid);
                        }
                        battleEngine();
                    }
                    break;
                }
            }
        }

    }
}
