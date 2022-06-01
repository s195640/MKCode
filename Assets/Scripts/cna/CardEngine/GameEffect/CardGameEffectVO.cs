using System;
using System.Collections.Generic;
using cna.poo;
using UnityEngine;

namespace cna {
    [Serializable]
    public class CardGameEffectVO : CardActionVO {
        public CardGameEffectVO(int uniqueId,
                string cardTitle,
                Image_Enum cardImage,
                CardType_Enum cardType,
                GameEffect_Enum gameEffectId,
                GameEffectDuration_Enum gameEffectDurationId,
                Color color,
                string description,
                bool world,
                bool battle,
                bool multi) : base(uniqueId, cardTitle, cardImage, cardType, new List<string>(), new List<List<Crystal_Enum>> { new List<Crystal_Enum> { Crystal_Enum.NA } }, new List<List<TurnPhase_Enum>>()) {
            GameEffectDurationId = gameEffectDurationId;
            GameEffectId = gameEffectId;
            GameEffectColor = color;
            GameEffectDescription = description;
            GameEffectWorld = world;
            GameEffectBattle = battle;
            GameEffectClickable = false;
            GameEffectDisplayMulti = multi;
        }

        public override string ToString() {
            string v = string.Format("{0}, GameEffectId = {1}, GameEffectDescription = {2}", base.ToString(), GameEffectId, GameEffectDescription);
            return v;
        }
    }
}
