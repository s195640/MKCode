using System.Collections.Generic;
using cna.poo;
using UnityEngine;

namespace cna {
    public abstract class BattleEngine : MonoBehaviour {
        public PlayerData L { get => D.LocalPlayer; }
        public BattleData B { get => L.Battle; }
        public Dictionary<int, MonsterDetailsVO> MonsterDetails { get => monsterDetails; set => monsterDetails = value; }

        [SerializeField] internal GameData gdProvoke;
        [SerializeField] internal GameData gdRange;
        [SerializeField] internal GameData gdBlock;
        [SerializeField] internal GameData gdDamage;
        [SerializeField] internal GameData gdAttack;

        private Dictionary<int, MonsterDetailsVO> monsterDetails = new Dictionary<int, MonsterDetailsVO>();

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
        protected virtual void battleEngine_StartOfBattle() {
            UpdateMonsterDetails();
            List<string> monsterNames = B.Monsters.Keys.ConvertAll(m => ((CardMonsterVO)D.Cards[m]).CardTitle);
            D.C.LogMessage("[Battle Starts] :: " + string.Join(", ", monsterNames));
            gdProvoke.GameStatus = Game_Enum.NA;
            gdRange.GameStatus = Game_Enum.NA;
            gdBlock.GameStatus = Game_Enum.NA;
            gdDamage.GameStatus = Game_Enum.NA;
            gdAttack.GameStatus = Game_Enum.NA;
            B.BattlePhase = BattlePhase_Enum.SetupProvoke;
            D.C.Send_GameData();
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
        protected void CloneGameData(ref GameData clone) {
            if (clone.GameStatus == Game_Enum.NA) {
                clone = D.G.Clone();
            }
        }
        protected virtual void OnClick_UndoProvoke() {
            D.C.LogMessage("[Undo - Provoke]");
            D.G = gdProvoke.Clone();
            D.Action.Clear();
            UpdateMonsterDetails();
            gdRange.GameStatus = Game_Enum.NA;
            gdBlock.GameStatus = Game_Enum.NA;
            gdDamage.GameStatus = Game_Enum.NA;
            gdAttack.GameStatus = Game_Enum.NA;
            D.C.Send_GameData();
        }
        protected virtual void OnClick_UndoRange() {
            D.C.LogMessage("[Undo - Range]");
            D.G = gdRange.Clone();
            D.Action.Clear();
            UpdateMonsterDetails();
            gdBlock.GameStatus = Game_Enum.NA;
            gdDamage.GameStatus = Game_Enum.NA;
            gdAttack.GameStatus = Game_Enum.NA;
            D.C.Send_GameData();
        }
        protected virtual void OnClick_UndoBlock() {
            D.C.LogMessage("[Undo - Block]");
            D.G = gdBlock.Clone();
            D.Action.Clear();
            UpdateMonsterDetails();
            gdDamage.GameStatus = Game_Enum.NA;
            gdAttack.GameStatus = Game_Enum.NA;
            D.C.Send_GameData();
        }
        protected virtual void OnClick_UndoDamage() {
            D.C.LogMessage("[Undo - Damage]");
            D.G = gdDamage.Clone();
            D.Action.Clear();
            UpdateMonsterDetails();
            gdAttack.GameStatus = Game_Enum.NA;
            D.C.Send_GameData();
        }
        protected virtual void OnClick_UndoAttack() {
            D.C.LogMessage("[Undo - Attack]");
            D.G = gdAttack.Clone();
            D.Action.Clear();
            UpdateMonsterDetails();
            D.C.Send_GameData();
        }
        #endregion
    }
}
