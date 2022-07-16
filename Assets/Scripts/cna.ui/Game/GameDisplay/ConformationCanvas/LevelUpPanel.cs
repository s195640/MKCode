using System;
using System.Collections.Generic;
using cna.poo;
using UnityEngine;
using UnityEngine.UI;

namespace cna.ui {
    public class LevelUpPanel : BasePanel {

        private GameAPI ar;
        private Action<GameAPI> callback;
        private List<int> actionOffering;
        private List<int> skillOffering;
        private List<int> skills;

        [SerializeField] private CNA_Button acceptButton;

        [SerializeField] private SkillCardSlot selectedSkill;
        [SerializeField] private NormalCardSlot selectedAction;

        [SerializeField] private NormalCardSlot[] actionSlotOffering;
        [SerializeField] private SkillCardSlot skillSlot_Prefab;
        [SerializeField] private Transform content;
        private List<SkillCardSlot> skillSlotOffering = new List<SkillCardSlot>();
        [SerializeField] private SkillCardSlot[] levelUpSkillSlots;

        private int actionOfferingIndex;
        private int skillOfferingIndex;
        private int skillIndex;

        public void SetupUI(GameAPI ar, Action<GameAPI> callback, List<int> actionOffering, List<int> skillOffering, List<int> skills) {
            this.ar = ar;
            this.callback = callback;
            this.actionOffering = actionOffering;
            this.skillOffering = skillOffering;
            this.skills = skills;
            skillSlotOffering.ForEach(c => Destroy(c.gameObject));
            skillSlotOffering.Clear();
            foreach (NormalCardSlot n in actionSlotOffering) n.gameObject.SetActive(false);
            for (int i = 0; i < actionOffering.Count; i++) {
                actionSlotOffering[i].gameObject.SetActive(true);
                actionSlotOffering[i].SetupUI(ar.P, actionOffering[i], CardHolder_Enum.LevelUp);
            }
            foreach (SkillCardSlot n in levelUpSkillSlots) n.gameObject.SetActive(false);
            for (int i = 0; i < skills.Count; i++) {
                levelUpSkillSlots[i].gameObject.SetActive(true);
                levelUpSkillSlots[i].SetupUI(ar.P, skills[i], CardHolder_Enum.LevelUp);
            }

            this.skillOffering.ForEach(c => {
                SkillCardSlot s = Instantiate(skillSlot_Prefab, Vector3.zero, Quaternion.identity);
                s.transform.SetParent(content);
                s.transform.localScale = Vector3.one;
                s.SetupUI(ar.P, c, CardHolder_Enum.LevelUp);
                s.GetComponent<Button>().onClick.AddListener(() => {
                    OnClick_SelectSkillOfferingCard(c);
                });
                skillSlotOffering.Add(s);
            });
            Clear_data();
            gameObject.SetActive(true);
        }

        public void OnClick_Clear() {
            Clear_data();
        }

        public void OnClick_Accept() {
            if (actionOfferingIndex >= 0 && (skillOfferingIndex >= 0 || skillIndex >= 0)) {
                ar.UniqueCardId = actionOffering[actionOfferingIndex];
                if (skillOfferingIndex >= 0) {
                    ar.SelectedUniqueCardId = skillOffering[skillOfferingIndex];
                } else {
                    ar.SelectedUniqueCardId = skills[skillIndex];
                }
                gameObject.SetActive(false);
                callback(ar);
            } else {
                D.Msg("You must select a Skill and an Action Card!");
                acceptButton.ShakeButton();
            }
        }

        public void OnClick_SelectActionOfferingCard(int index) {
            actionOfferingIndex = index;
            selectedAction.SetupUI(ar.P, actionOffering[actionOfferingIndex], CardHolder_Enum.LevelUp);
            if (index != 0 && skillOfferingIndex >= 0) {
                skillOfferingIndex = -1;
                selectedSkill.SetupUI(ar.P, 0, CardHolder_Enum.LevelUp);
            }
        }

        public void OnClick_SelectSkillCard(int index) {
            skillOfferingIndex = -1;
            skillIndex = index;
            selectedSkill.SetupUI(ar.P, skills[skillIndex], CardHolder_Enum.LevelUp);
        }

        public void OnClick_SelectSkillOfferingCard(int uniqueCardId) {
            skillIndex = -1;
            skillOfferingIndex = skillOffering.FindIndex(c => c == uniqueCardId);
            selectedSkill.SetupUI(ar.P, skillOffering[skillOfferingIndex], CardHolder_Enum.LevelUp);
            if (actionOfferingIndex != 0) {
                actionOfferingIndex = 0;
                selectedAction.SetupUI(ar.P, actionOffering[actionOfferingIndex], CardHolder_Enum.LevelUp);
            }
        }

        public void Clear_data() {
            actionOfferingIndex = -1;
            skillOfferingIndex = -1;
            skillIndex = -1;
            selectedSkill.SetupUI(ar.P, 0, CardHolder_Enum.LevelUp);
            selectedAction.SetupUI(ar.P, 0, CardHolder_Enum.LevelUp);
        }
    }
}
