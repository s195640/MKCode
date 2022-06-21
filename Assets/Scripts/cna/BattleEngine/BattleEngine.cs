﻿using System.Collections.Generic;
using cna.poo;
using UnityEngine;

namespace cna {
    public abstract class BattleEngine : MonoBehaviour {
        private GameAPI ar;
        public PlayerData L { get => AR.P; }
        public BattleData B { get => L.Battle; }
        public Dictionary<int, MonsterDetailsVO> MonsterDetails { get => monsterDetails; set => monsterDetails = value; }
        public GameAPI AR { get => ar; set => ar = value; }

        [SerializeField] internal PlayerData gdProvoke;
        [SerializeField] internal PlayerData gdRange;
        [SerializeField] internal PlayerData gdBlock;
        [SerializeField] internal PlayerData gdDamage;
        [SerializeField] internal PlayerData gdAttack;

        private Dictionary<int, MonsterDetailsVO> monsterDetails = new Dictionary<int, MonsterDetailsVO>();

        public void SetupUI(GameAPI ar) {
            this.ar = ar;
        }

        public abstract void UpdateUI();

        protected virtual void battleEngine() {
            switch (B.BattlePhase) {
                case BattlePhase_Enum.StartOfBattle: {
                    battleEngine_StartOfBattle();
                    break;
                }
                case BattlePhase_Enum.SetupProvoke: {
                    battleEngine_SetupProvokeMonsters();
                    break;
                }
                case BattlePhase_Enum.Provoke: {
                    battleEngine_ProvokeMonsters();
                    break;
                }
                case BattlePhase_Enum.RangeSiege: {
                    battleEngine_RangeSiege();
                    break;
                }
                case BattlePhase_Enum.Block: {
                    battleEngine_Block();
                    break;
                }
                case BattlePhase_Enum.AssignDamage: {
                    battleEngine_AssignDamage();
                    break;
                }
                case BattlePhase_Enum.Attack: {
                    battleEngine_Attack();
                    break;
                }
                case BattlePhase_Enum.EndOfBattle: {
                    battleEngine_EndOfBattle();
                    break;
                }
            }
        }

        #region START BATTLE
        protected void battleEngine_StartOfBattle() {
            UpdateMonsterDetails();
            List<string> monsterNames = B.Monsters.Keys.ConvertAll(m => ((CardMonsterVO)D.Cards[m]).CardTitle);
            AR.AddLog("[Battle Starts] :: " + string.Join(", ", monsterNames));
            B.BattlePhase = BattlePhase_Enum.SetupProvoke;
            AR.PushForce();
        }

        public void UpdateMonsterDetails() {
            MonsterDetails.Clear();
            B.Monsters.Keys.ForEach(m => {
                CardVO monsterCard = D.Cards[m];
                MonsterDetails.Add(monsterCard.UniqueId, new MonsterDetailsVO(monsterCard, L.GameEffects));
            });
        }
        #endregion

        #region PROVOKE
        protected virtual void battleEngine_SetupProvokeMonsters() {
            CloneGameData(ref gdProvoke);
        }
        protected abstract void battleEngine_ProvokeMonsters();
        #endregion

        #region RANGE SIEGE
        protected virtual void battleEngine_RangeSiege() {
            CloneGameData(ref gdRange);
        }
        #endregion

        #region BLOCK
        protected virtual void battleEngine_Block() {
            CloneGameData(ref gdBlock);
        }

        #endregion

        #region ASSIGN DAMAGE
        protected virtual void battleEngine_AssignDamage() {
            CloneGameData(ref gdDamage);
        }
        #endregion

        #region ATTACK
        protected virtual void battleEngine_Attack() {
            CloneGameData(ref gdAttack);
        }
        #endregion

        #region END BATTLE
        protected abstract void battleEngine_EndOfBattle();
        #endregion

        #region Battle Undo
        protected void CloneGameData(ref PlayerData clone) {
            clone = AR.P.Clone();
        }
        protected virtual void OnClick_UndoProvoke() {
            AR.AddLog("[Undo - Provoke]");
            AR.P.UpdateData(gdProvoke);
            D.Action.Clear();
            UpdateMonsterDetails();
            AR.PushForce();
        }
        protected virtual void OnClick_UndoRange() {
            AR.AddLog("[Undo - Range]");
            AR.P.UpdateData(gdRange);
            D.Action.Clear();
            UpdateMonsterDetails();
            AR.PushForce();
        }
        protected virtual void OnClick_UndoBlock() {
            AR.AddLog("[Undo - Block]");
            AR.P.UpdateData(gdBlock);
            D.Action.Clear();
            UpdateMonsterDetails();
            AR.PushForce();
        }
        protected virtual void OnClick_UndoDamage() {
            AR.AddLog("[Undo - Damage]");
            AR.P.UpdateData(gdDamage);
            D.Action.Clear();
            UpdateMonsterDetails();
            AR.PushForce();
        }
        protected virtual void OnClick_UndoAttack() {
            AR.AddLog("[Undo - Attack]");
            AR.P.UpdateData(gdAttack);
            D.Action.Clear();
            UpdateMonsterDetails();
            AR.PushForce();
        }
        #endregion
    }
}
