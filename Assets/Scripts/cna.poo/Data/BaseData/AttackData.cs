using System;
using UnityEngine;

namespace cna.poo {
    [Serializable]
    public class AttackData : BaseData {
        public AttackData() {
            Clear();
        }

        public AttackData(int physical) {
            Clear();
            this.physical = physical;
        }

        [SerializeField] private int physical;
        [SerializeField] private int fire;
        [SerializeField] private int cold;
        [SerializeField] private int coldFire;

        public int Physical { get => physical; set => physical = value; }
        public int Fire { get => fire; set => fire = value; }
        public int Cold { get => cold; set => cold = value; }
        public int ColdFire { get => coldFire; set => coldFire = value; }

        public void Clear() {
            physical = 0;
            fire = 0;
            cold = 0;
            coldFire = 0;
        }

        public void Add(AttackData i) {
            Physical += i.Physical;
            Fire += i.Fire;
            Cold += i.Cold;
            ColdFire += i.ColdFire;
        }
        public int getTotal() {
            return Physical + Fire + Cold + ColdFire;
        }
        public int getBlock(UnitEffect_Enum attackType, AttackData mod) {
            int block = 0;
            switch (attackType) {
                case UnitEffect_Enum.Summoner: { break; }
                case UnitEffect_Enum.ColdFireAttack: {
                    block = ColdFire + (mod.ColdFire);
                    break;
                }
                case UnitEffect_Enum.ColdAttack: {
                    block = ColdFire + Cold + (mod.ColdFire + mod.Cold);
                    break;
                }
                case UnitEffect_Enum.FireAttack: {
                    block = ColdFire + Fire + (mod.ColdFire + mod.Fire);
                    break;
                }
                case UnitEffect_Enum.None: {
                    block = ColdFire + Cold + Fire + Physical + (mod.ColdFire + mod.Cold + mod.Fire + mod.Physical);
                    break;
                }
            }
            int otherBlock = (getTotal() + mod.getTotal()) - block;
            return block + otherBlock / 2;
        }

        public V2IntVO getDamage(bool resistFire, bool resistIce, bool resistPhysical) {
            V2IntVO damage = V2IntVO.zero; //   X = Efficient, Y = Inefficient
            if (resistFire) {
                damage.Y += Fire;
            } else {
                damage.X += Fire;
            }
            if (resistIce) {
                damage.Y += Cold;
            } else {
                damage.X += Cold;
            }
            if (resistPhysical) {
                damage.Y += Physical;
            } else {
                damage.X += Physical;
            }
            if (resistFire && resistIce) {
                damage.Y += ColdFire;
            } else {
                damage.X += ColdFire;
            }
            return damage;
        }

        public void UpdateData(AttackData a) {
            physical = a.physical;
            fire = a.fire;
            cold = a.cold;
            coldFire = a.coldFire;
        }

        public override string Serialize() {
            string data = physical + ":"
                + fire + ":"
                + cold + ":"
                + coldFire + ":";
            return data;
        }

        public override void Deserialize(string data) {
            string[] d = data.Split(":");
            CNASerialize.Dz(d[0], out physical);
            CNASerialize.Dz(d[1], out fire);
            CNASerialize.Dz(d[2], out cold);
            CNASerialize.Dz(d[3], out coldFire);
        }
    }
}
