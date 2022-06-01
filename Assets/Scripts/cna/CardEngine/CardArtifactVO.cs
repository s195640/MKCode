using System.Collections.Generic;
using cna.poo;

namespace cna {
    public abstract class CardArtifactVO : CardActionVO {
        public CardArtifactVO(int uniqueId,
            string artifactTitle,
            Image_Enum cardImage,
            List<string> actions,
            List<List<TurnPhase_Enum>> allowed,
            List<List<BattlePhase_Enum>> battleAllowed)
            : base(uniqueId, artifactTitle, cardImage, CardType_Enum.Artifact, actions, new List<List<Crystal_Enum>>() { new List<Crystal_Enum>() { Crystal_Enum.NA }, new List<Crystal_Enum>() { Crystal_Enum.NA } }, allowed, battleAllowed, CardColor_Enum.NA) {
        }

    }
}
