using UnityEngine;

namespace cna.ui {
    public class TESTConformationCanvas : AppBase {
        //[SerializeField] private MonsterCardSlot prefab;
        //[SerializeField] private Transform content;
        [SerializeField] private GameObject game;

        public override void UpdateUI() {
            game.SetActive(true);
            //add(Image_Enum.CMV_ice_mages_x2);
            //add(Image_Enum.CMW_gunners_x3);
            //add(Image_Enum.CMG_diggers_x2);
            //add(Image_Enum.CMV_illusionists_x2);
            //add(Image_Enum.CMB_crypt_worm_x2);
        }

        //private void add(Image_Enum i) {
        //    int u = D.Cards.Find(c => c.CardImage == i).UniqueId;
        //    MonsterCardSlot c = Instantiate(prefab, Vector3.zero, Quaternion.identity);
        //    c.transform.SetParent(content);
        //    c.transform.localScale = Vector3.one;
        //    c.SetupUI(u);
        //}
    }
}
