using System;
using System.Collections.Generic;
using cna.poo;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.Tilemaps;

namespace cna {
    public class AppEngineHelper : MonoBehaviour {

        public void Awake() {
            foreach (Transform child in transform) {
                child.gameObject.SetActive(false);
            }
            UpdateAvatarMap();
            UpdateSpriteMap();
            UpdateCardMetaData();
            UpdateCardMetaDataGameEffects();
            UpdateLocationsGameEffects();
        }

        void Update() {
            if (isLoaded()) {
                gameObject.GetComponent<AppBase>().UpdateUI();
                Destroy(this);
            }
        }

        public static void UpdateSpriteMap() {
            D.SpriteMap = new Dictionary<Image_Enum, Sprite>();
            D.TerrainMap = new Dictionary<Image_Enum, Tile>();
            D.StructureMap = new Dictionary<Image_Enum, Tile>();
            foreach (int imageId in Enum.GetValues(typeof(Image_Enum))) {
                Image_Enum imageEnum = (Image_Enum)imageId;
                if (!D.SpriteMap.ContainsKey(imageEnum)) {
                    string imageName = imageEnum.ToString();
                    if (imageName.StartsWith("TH_")) {
                        imageName = string.Format("Assets/Images/terrain/{0}.png", imageName);
                    } else if (imageName.StartsWith("SH_")) {
                        imageName = string.Format("Assets/Images/structure/{0}.png", imageName.Substring(3));
                    } else if (imageName.StartsWith("I_")) {
                        imageName = string.Format("Assets/Images/Icon/{0}.png", imageName.Substring(2));
                    } else if (imageName.StartsWith("C_")) {
                        imageName = string.Format("Assets/Images/card/{0}.jpg", imageName.Substring(2));
                    } else if (imageName.StartsWith("CB_")) {
                        imageName = string.Format("Assets/Images/card/basic/deed_basic_{0}.jpg", imageName.Substring(3));
                    } else if (imageName.StartsWith("CA_")) {
                        imageName = string.Format("Assets/Images/card/advanced/adv_action_{0}.jpg", imageName.Substring(3));
                    } else if (imageName.StartsWith("CT_")) {
                        imageName = string.Format("Assets/Images/card/artifact/artifact_{0}.jpg", imageName.Substring(3));
                    } else if (imageName.StartsWith("CUR_")) {
                        imageName = string.Format("Assets/Images/card/unit/regular_{0}.png", imageName.Substring(4));
                    } else if (imageName.StartsWith("CUE_")) {
                        imageName = string.Format("Assets/Images/card/unit/elite_{0}.png", imageName.Substring(4));
                    } else if (imageName.StartsWith("CMG_")) {
                        imageName = string.Format("Assets/Images/card/monster/enemy_marauding_orcs_{0}.png", imageName.Substring(4));
                    } else if (imageName.StartsWith("CMY_")) {
                        imageName = string.Format("Assets/Images/card/monster/enemy_keeps_{0}.png", imageName.Substring(4));
                    } else if (imageName.StartsWith("CMB_")) {
                        imageName = string.Format("Assets/Images/card/monster/enemy_dungeon_{0}.png", imageName.Substring(4));
                    } else if (imageName.StartsWith("CMV_")) {
                        imageName = string.Format("Assets/Images/card/monster/enemy_mage_towers_{0}.png", imageName.Substring(4));
                    } else if (imageName.StartsWith("CMW_")) {
                        imageName = string.Format("Assets/Images/card/monster/enemy_cities_{0}.png", imageName.Substring(4));
                    } else if (imageName.StartsWith("CMR_")) {
                        imageName = string.Format("Assets/Images/card/monster/enemy_draconum_{0}.png", imageName.Substring(4));
                    } else if (imageName.StartsWith("SKG_")) {
                        imageName = string.Format("Assets/Images/card/skill/Green - Goldyx/skill_goldyx_{0}.png", imageName.Substring(4));
                    } else if (imageName.StartsWith("SKB_")) {
                        imageName = string.Format("Assets/Images/card/skill/Blue - Tovak/skill_tovak_{0}.png", imageName.Substring(4));
                    } else if (imageName.StartsWith("SKW_")) {
                        imageName = string.Format("Assets/Images/card/skill/White - Norowas/skill_norowas_{0}.png", imageName.Substring(4));
                    } else if (imageName.StartsWith("SKR_")) {
                        imageName = string.Format("Assets/Images/card/skill/Red - Arythea/skill_arythea_{0}.png", imageName.Substring(4));
                    } else if (imageName.StartsWith("CS_")) {
                        D.SpriteMap.Add(imageEnum, null);
                        continue;
                    } else if (imageName.StartsWith("R_")) {
                        imageName = string.Format("Assets/Images/card/ruin/ruins_{0}.png", imageName.Substring(2));
                    } else if (imageName.StartsWith("T_")) {
                        imageName = string.Format("Assets/Images/card/tactics/tactics_{0}.jpg", imageName.Substring(2));
                    } else if (imageName.Equals("A_MEEPLE_GREEN")) {
                        imageName = "Assets/Images/avatar/meeple/meeple_goldyx.png";
                    } else if (imageName.Equals("A_MEEPLE_RED")) {
                        imageName = "Assets/Images/avatar/meeple/meeple_arythea.png";
                    } else if (imageName.Equals("A_MEEPLE_BLUE")) {
                        imageName = "Assets/Images/avatar/meeple/meeple_tovak.png";
                    } else if (imageName.Equals("A_MEEPLE_WHITE")) {
                        imageName = "Assets/Images/avatar/meeple/meeple_norowas.png";
                    } else if (imageName.Equals("A_SHIELD_GREEN")) {
                        imageName = "Assets/Images/avatar/shield/shield_green_goldyx.png";
                    } else if (imageName.Equals("A_SHIELD_RED")) {
                        imageName = "Assets/Images/avatar/shield/shield_red_arythea.png";
                    } else if (imageName.Equals("A_SHIELD_BLUE")) {
                        imageName = "Assets/Images/avatar/shield/shield_blue_tovak.png";
                    } else if (imageName.Equals("A_SHIELD_WHITE")) {
                        imageName = "Assets/Images/avatar/shield/shield_white_norowas.png";
                    }
                    Addressables.LoadAssetAsync<Sprite>(imageName).Completed += ((obj => {
                        if (obj.Status == AsyncOperationStatus.Succeeded) {
                            if (imageEnum.ToString().StartsWith("TH_")) {
                                CNATile t = ScriptableObject.CreateInstance(typeof(CNATile)) as CNATile;
                                t.TileId = imageEnum;
                                t.sprite = obj.Result;
                                D.TerrainMap.Add(imageEnum, t);
                            } else if (imageEnum.ToString().StartsWith("SH_")) {
                                CNATile t = ScriptableObject.CreateInstance(typeof(CNATile)) as CNATile;
                                t.TileId = imageEnum;
                                t.sprite = obj.Result;
                                D.StructureMap.Add(imageEnum, t);
                            }
                            D.SpriteMap.Add(imageEnum, obj.Result);
                        }
                    }));
                }
            }
        }

        public static bool isLoaded() {
            return D.SpriteMap != null && Enum.GetValues(typeof(Image_Enum)).Length == D.SpriteMap.Count;
        }

        public void UpdateAvatarMap() {
            D.AvatarMetaDataMap = new Dictionary<Image_Enum, AvatarMetaData>();
            D.AvatarMetaDataMap.Add(Image_Enum.A_MEEPLE_RANDOM, Resources.Load<AvatarMetaData>("Avatars/Random"));
            D.AvatarMetaDataMap.Add(Image_Enum.A_MEEPLE_BLUE, Resources.Load<AvatarMetaData>("Avatars/Blue"));
            D.AvatarMetaDataMap.Add(Image_Enum.A_MEEPLE_GREEN, Resources.Load<AvatarMetaData>("Avatars/Green"));
            D.AvatarMetaDataMap.Add(Image_Enum.A_MEEPLE_RED, Resources.Load<AvatarMetaData>("Avatars/Red"));
            D.AvatarMetaDataMap.Add(Image_Enum.A_MEEPLE_WHITE, Resources.Load<AvatarMetaData>("Avatars/White"));
        }

        public void UpdateCardMetaData() {
            D.Cards = new List<CardVO>();
            //  Reserve i = 0 for NA
            D.Cards.Add(new CardVO(D.Cards.Count, "NA", Image_Enum.NA, CardType_Enum.NA));
            //  Reserve i = 1 for Basic Action
            D.Cards.Add(new BasicVO(D.Cards.Count));
            //  Wounds
            for (int i = 0; i < 100; i++) {
                D.Cards.Add(new CardVO(D.Cards.Count, "Wound", Image_Enum.C_wound, CardType_Enum.Wound));
            }
            Image_Enum[] avatarList = new Image_Enum[] { Image_Enum.A_MEEPLE_GREEN, Image_Enum.A_MEEPLE_RED, Image_Enum.A_MEEPLE_BLUE, Image_Enum.A_MEEPLE_WHITE };
            foreach (Image_Enum avatar in avatarList) {
                D.Cards.Add(new TranquilityVO(D.Cards.Count, avatar));
                D.Cards.Add(new ThreatenVO(D.Cards.Count, avatar));
                D.Cards.Add(new SwiftnessVO(D.Cards.Count, avatar));
                D.Cards.Add(new SwiftnessVO(D.Cards.Count, avatar));
                D.Cards.Add(new RageVO(D.Cards.Count, avatar));
                D.Cards.Add(new StaminaVO(D.Cards.Count, avatar));
                D.Cards.Add(new StaminaVO(D.Cards.Count, avatar));
                D.Cards.Add(new MarchVO(D.Cards.Count, avatar));
                D.Cards.Add(new MarchVO(D.Cards.Count, avatar));
                D.Cards.Add(new ManaDrawVO(D.Cards.Count, avatar));
                D.Cards.Add(new ImprovisationVO(D.Cards.Count, avatar));
                D.Cards.Add(new CrystallizeVO(D.Cards.Count, avatar));
                switch (avatar) {
                    case Image_Enum.A_MEEPLE_BLUE: {
                        D.Cards.Add(new ConcentrationVO(D.Cards.Count, avatar));
                        D.Cards.Add(new PromiseVO(D.Cards.Count, avatar));
                        D.Cards.Add(new ColdToughnessVO(D.Cards.Count, avatar));
                        D.Cards.Add(new RageVO(D.Cards.Count, avatar));
                        break;
                    }
                    case Image_Enum.A_MEEPLE_GREEN: {
                        D.Cards.Add(new WillFocusVO(D.Cards.Count, avatar));
                        D.Cards.Add(new PromiseVO(D.Cards.Count, avatar));
                        D.Cards.Add(new DeterminationVO(D.Cards.Count, avatar));
                        D.Cards.Add(new RageVO(D.Cards.Count, avatar));
                        break;
                    }
                    case Image_Enum.A_MEEPLE_RED: {
                        D.Cards.Add(new ConcentrationVO(D.Cards.Count, avatar));
                        D.Cards.Add(new PromiseVO(D.Cards.Count, avatar));
                        D.Cards.Add(new DeterminationVO(D.Cards.Count, avatar));
                        D.Cards.Add(new BattleVersatilityVO(D.Cards.Count, avatar));
                        break;
                    }
                    case Image_Enum.A_MEEPLE_WHITE: {
                        D.Cards.Add(new ConcentrationVO(D.Cards.Count, avatar));
                        D.Cards.Add(new NobleMannersVO(D.Cards.Count, avatar));
                        D.Cards.Add(new DeterminationVO(D.Cards.Count, avatar));
                        D.Cards.Add(new RageVO(D.Cards.Count, avatar));
                        break;
                    }
                }
            }

            //  GREEN
            D.Cards.Add(new CardMonsterVO(D.Cards.Count, "Cursed Hags", Image_Enum.CMG_cursed_hags_x2, 3, 3, 5, new List<UnitEffect_Enum>() { UnitEffect_Enum.Poison }));
            D.Cards.Add(new CardMonsterVO(D.Cards.Count, "Wolf Riders", Image_Enum.CMG_wolf_riders_x2, 3, 3, 4, new List<UnitEffect_Enum>() { UnitEffect_Enum.Swiftness }));
            D.Cards.Add(new CardMonsterVO(D.Cards.Count, "Diggers", Image_Enum.CMG_diggers_x2, 2, 3, 3, new List<UnitEffect_Enum>() { UnitEffect_Enum.Fortified }));
            D.Cards.Add(new CardMonsterVO(D.Cards.Count, "IronClads", Image_Enum.CMG_ironclads_x2, 4, 4, 3, new List<UnitEffect_Enum>() { UnitEffect_Enum.PhysicalResistance, UnitEffect_Enum.Brutal }));
            D.Cards.Add(new CardMonsterVO(D.Cards.Count, "Orc Summoners", Image_Enum.CMG_orc_summoners_x2, 4, 0, 4, new List<UnitEffect_Enum>() { UnitEffect_Enum.Summoner }));
            D.Cards.Add(new CardMonsterVO(D.Cards.Count, "Prowlers", Image_Enum.CMG_prowlers_x2, 2, 4, 3, new List<UnitEffect_Enum>() { }));
            D.Cards.Add(new CardMonsterVO(D.Cards.Count, "Cursed Hags", Image_Enum.CMG_cursed_hags_x2, 3, 3, 5, new List<UnitEffect_Enum>() { UnitEffect_Enum.Poison }));
            D.Cards.Add(new CardMonsterVO(D.Cards.Count, "Wolf Riders", Image_Enum.CMG_wolf_riders_x2, 3, 3, 4, new List<UnitEffect_Enum>() { UnitEffect_Enum.Swiftness }));
            D.Cards.Add(new CardMonsterVO(D.Cards.Count, "Diggers", Image_Enum.CMG_diggers_x2, 2, 3, 3, new List<UnitEffect_Enum>() { UnitEffect_Enum.Fortified }));
            D.Cards.Add(new CardMonsterVO(D.Cards.Count, "IronClads", Image_Enum.CMG_ironclads_x2, 4, 4, 3, new List<UnitEffect_Enum>() { UnitEffect_Enum.PhysicalResistance, UnitEffect_Enum.Brutal }));
            D.Cards.Add(new CardMonsterVO(D.Cards.Count, "Orc Summoners", Image_Enum.CMG_orc_summoners_x2, 4, 0, 4, new List<UnitEffect_Enum>() { UnitEffect_Enum.Summoner }));
            D.Cards.Add(new CardMonsterVO(D.Cards.Count, "Prowlers", Image_Enum.CMG_prowlers_x2, 2, 4, 3, new List<UnitEffect_Enum>() { }));

            D.Cards.Add(new CardMonsterVO(D.Cards.Count, "Cursed Hags", Image_Enum.CMG_cursed_hags_x2, 3, 3, 5, new List<UnitEffect_Enum>() { UnitEffect_Enum.Poison }));
            D.Cards.Add(new CardMonsterVO(D.Cards.Count, "Wolf Riders", Image_Enum.CMG_wolf_riders_x2, 3, 3, 4, new List<UnitEffect_Enum>() { UnitEffect_Enum.Swiftness }));
            D.Cards.Add(new CardMonsterVO(D.Cards.Count, "Diggers", Image_Enum.CMG_diggers_x2, 2, 3, 3, new List<UnitEffect_Enum>() { UnitEffect_Enum.Fortified }));
            D.Cards.Add(new CardMonsterVO(D.Cards.Count, "IronClads", Image_Enum.CMG_ironclads_x2, 4, 4, 3, new List<UnitEffect_Enum>() { UnitEffect_Enum.PhysicalResistance, UnitEffect_Enum.Brutal }));
            D.Cards.Add(new CardMonsterVO(D.Cards.Count, "Orc Summoners", Image_Enum.CMG_orc_summoners_x2, 4, 0, 4, new List<UnitEffect_Enum>() { UnitEffect_Enum.Summoner }));
            D.Cards.Add(new CardMonsterVO(D.Cards.Count, "Prowlers", Image_Enum.CMG_prowlers_x2, 2, 4, 3, new List<UnitEffect_Enum>() { }));
            D.Cards.Add(new CardMonsterVO(D.Cards.Count, "Cursed Hags", Image_Enum.CMG_cursed_hags_x2, 3, 3, 5, new List<UnitEffect_Enum>() { UnitEffect_Enum.Poison }));
            D.Cards.Add(new CardMonsterVO(D.Cards.Count, "Wolf Riders", Image_Enum.CMG_wolf_riders_x2, 3, 3, 4, new List<UnitEffect_Enum>() { UnitEffect_Enum.Swiftness }));
            D.Cards.Add(new CardMonsterVO(D.Cards.Count, "Diggers", Image_Enum.CMG_diggers_x2, 2, 3, 3, new List<UnitEffect_Enum>() { UnitEffect_Enum.Fortified }));
            D.Cards.Add(new CardMonsterVO(D.Cards.Count, "IronClads", Image_Enum.CMG_ironclads_x2, 4, 4, 3, new List<UnitEffect_Enum>() { UnitEffect_Enum.PhysicalResistance, UnitEffect_Enum.Brutal }));
            D.Cards.Add(new CardMonsterVO(D.Cards.Count, "Orc Summoners", Image_Enum.CMG_orc_summoners_x2, 4, 0, 4, new List<UnitEffect_Enum>() { UnitEffect_Enum.Summoner }));
            D.Cards.Add(new CardMonsterVO(D.Cards.Count, "Prowlers", Image_Enum.CMG_prowlers_x2, 2, 4, 3, new List<UnitEffect_Enum>() { }));

            //  GREY
            D.Cards.Add(new CardMonsterVO(D.Cards.Count, "Crossbowmen", Image_Enum.CMY_crossbowmen_x3, 3, 4, 4, new List<UnitEffect_Enum>() { UnitEffect_Enum.Swiftness }));
            D.Cards.Add(new CardMonsterVO(D.Cards.Count, "Golems", Image_Enum.CMY_golems_x2, 4, 2, 5, new List<UnitEffect_Enum>() { UnitEffect_Enum.PhysicalResistance }));
            D.Cards.Add(new CardMonsterVO(D.Cards.Count, "Guardsmen", Image_Enum.CMY_guardsmen_x3, 3, 3, 7, new List<UnitEffect_Enum>() { UnitEffect_Enum.Fortified }));
            D.Cards.Add(new CardMonsterVO(D.Cards.Count, "Swordsmen", Image_Enum.CMY_swordsmen_x2, 4, 6, 5, new List<UnitEffect_Enum>() { }));
            D.Cards.Add(new CardMonsterVO(D.Cards.Count, "Crossbowmen", Image_Enum.CMY_crossbowmen_x3, 3, 4, 4, new List<UnitEffect_Enum>() { UnitEffect_Enum.Swiftness }));
            D.Cards.Add(new CardMonsterVO(D.Cards.Count, "Golems", Image_Enum.CMY_golems_x2, 4, 2, 5, new List<UnitEffect_Enum>() { UnitEffect_Enum.PhysicalResistance }));
            D.Cards.Add(new CardMonsterVO(D.Cards.Count, "Guardsmen", Image_Enum.CMY_guardsmen_x3, 3, 3, 7, new List<UnitEffect_Enum>() { UnitEffect_Enum.Fortified }));
            D.Cards.Add(new CardMonsterVO(D.Cards.Count, "Swordsmen", Image_Enum.CMY_swordsmen_x2, 4, 6, 5, new List<UnitEffect_Enum>() { }));
            D.Cards.Add(new CardMonsterVO(D.Cards.Count, "Crossbowmen", Image_Enum.CMY_crossbowmen_x3, 3, 4, 4, new List<UnitEffect_Enum>() { UnitEffect_Enum.Swiftness }));
            D.Cards.Add(new CardMonsterVO(D.Cards.Count, "Guardsmen", Image_Enum.CMY_guardsmen_x3, 3, 3, 7, new List<UnitEffect_Enum>() { UnitEffect_Enum.Fortified }));

            D.Cards.Add(new CardMonsterVO(D.Cards.Count, "Crossbowmen", Image_Enum.CMY_crossbowmen_x3, 3, 4, 4, new List<UnitEffect_Enum>() { UnitEffect_Enum.Swiftness }));
            D.Cards.Add(new CardMonsterVO(D.Cards.Count, "Golems", Image_Enum.CMY_golems_x2, 4, 2, 5, new List<UnitEffect_Enum>() { UnitEffect_Enum.PhysicalResistance }));
            D.Cards.Add(new CardMonsterVO(D.Cards.Count, "Guardsmen", Image_Enum.CMY_guardsmen_x3, 3, 3, 7, new List<UnitEffect_Enum>() { UnitEffect_Enum.Fortified }));
            D.Cards.Add(new CardMonsterVO(D.Cards.Count, "Swordsmen", Image_Enum.CMY_swordsmen_x2, 4, 6, 5, new List<UnitEffect_Enum>() { }));
            D.Cards.Add(new CardMonsterVO(D.Cards.Count, "Crossbowmen", Image_Enum.CMY_crossbowmen_x3, 3, 4, 4, new List<UnitEffect_Enum>() { UnitEffect_Enum.Swiftness }));
            D.Cards.Add(new CardMonsterVO(D.Cards.Count, "Golems", Image_Enum.CMY_golems_x2, 4, 2, 5, new List<UnitEffect_Enum>() { UnitEffect_Enum.PhysicalResistance }));
            D.Cards.Add(new CardMonsterVO(D.Cards.Count, "Guardsmen", Image_Enum.CMY_guardsmen_x3, 3, 3, 7, new List<UnitEffect_Enum>() { UnitEffect_Enum.Fortified }));
            D.Cards.Add(new CardMonsterVO(D.Cards.Count, "Swordsmen", Image_Enum.CMY_swordsmen_x2, 4, 6, 5, new List<UnitEffect_Enum>() { }));
            D.Cards.Add(new CardMonsterVO(D.Cards.Count, "Crossbowmen", Image_Enum.CMY_crossbowmen_x3, 3, 4, 4, new List<UnitEffect_Enum>() { UnitEffect_Enum.Swiftness }));
            D.Cards.Add(new CardMonsterVO(D.Cards.Count, "Guardsmen", Image_Enum.CMY_guardsmen_x3, 3, 3, 7, new List<UnitEffect_Enum>() { UnitEffect_Enum.Fortified }));

            //  BROWN
            D.Cards.Add(new CardMonsterVO(D.Cards.Count, "Crypt Worm", Image_Enum.CMB_crypt_worm_x2, 5, 6, 6, new List<UnitEffect_Enum>() { UnitEffect_Enum.Fortified }));
            D.Cards.Add(new CardMonsterVO(D.Cards.Count, "Gargoyle", Image_Enum.CMB_gargoyle_x2, 4, 5, 4, new List<UnitEffect_Enum>() { UnitEffect_Enum.PhysicalResistance }));
            D.Cards.Add(new CardMonsterVO(D.Cards.Count, "Medusa", Image_Enum.CMB_medusa_x2, 5, 6, 4, new List<UnitEffect_Enum>() { UnitEffect_Enum.Paralyze }));
            D.Cards.Add(new CardMonsterVO(D.Cards.Count, "Minotaur", Image_Enum.CMB_minotaur_x2, 4, 5, 5, new List<UnitEffect_Enum>() { UnitEffect_Enum.Brutal }));
            D.Cards.Add(new CardMonsterVO(D.Cards.Count, "Werewolf", Image_Enum.CMB_werewolf_x2, 5, 7, 5, new List<UnitEffect_Enum>() { UnitEffect_Enum.Swiftness }));
            D.Cards.Add(new CardMonsterVO(D.Cards.Count, "Crypt Worm", Image_Enum.CMB_crypt_worm_x2, 5, 6, 6, new List<UnitEffect_Enum>() { UnitEffect_Enum.Fortified }));
            D.Cards.Add(new CardMonsterVO(D.Cards.Count, "Gargoyle", Image_Enum.CMB_gargoyle_x2, 4, 5, 4, new List<UnitEffect_Enum>() { UnitEffect_Enum.PhysicalResistance }));
            D.Cards.Add(new CardMonsterVO(D.Cards.Count, "Medusa", Image_Enum.CMB_medusa_x2, 5, 6, 4, new List<UnitEffect_Enum>() { UnitEffect_Enum.Paralyze }));
            D.Cards.Add(new CardMonsterVO(D.Cards.Count, "Minotaur", Image_Enum.CMB_minotaur_x2, 4, 5, 5, new List<UnitEffect_Enum>() { UnitEffect_Enum.Brutal }));
            D.Cards.Add(new CardMonsterVO(D.Cards.Count, "Werewolf", Image_Enum.CMB_werewolf_x2, 5, 7, 5, new List<UnitEffect_Enum>() { UnitEffect_Enum.Swiftness }));

            D.Cards.Add(new CardMonsterVO(D.Cards.Count, "Crypt Worm", Image_Enum.CMB_crypt_worm_x2, 5, 6, 6, new List<UnitEffect_Enum>() { UnitEffect_Enum.Fortified }));
            D.Cards.Add(new CardMonsterVO(D.Cards.Count, "Gargoyle", Image_Enum.CMB_gargoyle_x2, 4, 5, 4, new List<UnitEffect_Enum>() { UnitEffect_Enum.PhysicalResistance }));
            D.Cards.Add(new CardMonsterVO(D.Cards.Count, "Medusa", Image_Enum.CMB_medusa_x2, 5, 6, 4, new List<UnitEffect_Enum>() { UnitEffect_Enum.Paralyze }));
            D.Cards.Add(new CardMonsterVO(D.Cards.Count, "Minotaur", Image_Enum.CMB_minotaur_x2, 4, 5, 5, new List<UnitEffect_Enum>() { UnitEffect_Enum.Brutal }));
            D.Cards.Add(new CardMonsterVO(D.Cards.Count, "Werewolf", Image_Enum.CMB_werewolf_x2, 5, 7, 5, new List<UnitEffect_Enum>() { UnitEffect_Enum.Swiftness }));
            D.Cards.Add(new CardMonsterVO(D.Cards.Count, "Crypt Worm", Image_Enum.CMB_crypt_worm_x2, 5, 6, 6, new List<UnitEffect_Enum>() { UnitEffect_Enum.Fortified }));
            D.Cards.Add(new CardMonsterVO(D.Cards.Count, "Gargoyle", Image_Enum.CMB_gargoyle_x2, 4, 5, 4, new List<UnitEffect_Enum>() { UnitEffect_Enum.PhysicalResistance }));
            D.Cards.Add(new CardMonsterVO(D.Cards.Count, "Medusa", Image_Enum.CMB_medusa_x2, 5, 6, 4, new List<UnitEffect_Enum>() { UnitEffect_Enum.Paralyze }));
            D.Cards.Add(new CardMonsterVO(D.Cards.Count, "Minotaur", Image_Enum.CMB_minotaur_x2, 4, 5, 5, new List<UnitEffect_Enum>() { UnitEffect_Enum.Brutal }));
            D.Cards.Add(new CardMonsterVO(D.Cards.Count, "Werewolf", Image_Enum.CMB_werewolf_x2, 5, 7, 5, new List<UnitEffect_Enum>() { UnitEffect_Enum.Swiftness }));

            //  VIOLET
            D.Cards.Add(new CardMonsterVO(D.Cards.Count, "Fire Golems", Image_Enum.CMV_fire_golems_x1, 5, 3, 4, new List<UnitEffect_Enum>() { UnitEffect_Enum.FireAttack, UnitEffect_Enum.FireResistance, UnitEffect_Enum.PhysicalResistance, UnitEffect_Enum.Brutal }));
            D.Cards.Add(new CardMonsterVO(D.Cards.Count, "Fire Mages", Image_Enum.CMV_fire_mages_x2, 5, 6, 5, new List<UnitEffect_Enum>() { UnitEffect_Enum.FireAttack, UnitEffect_Enum.FireResistance }));
            D.Cards.Add(new CardMonsterVO(D.Cards.Count, "Ice Golems", Image_Enum.CMV_ice_golems_x1, 5, 2, 4, new List<UnitEffect_Enum>() { UnitEffect_Enum.ColdAttack, UnitEffect_Enum.IceResistance, UnitEffect_Enum.PhysicalResistance, UnitEffect_Enum.Paralyze }));
            D.Cards.Add(new CardMonsterVO(D.Cards.Count, "Ice Mages", Image_Enum.CMV_ice_mages_x2, 5, 5, 6, new List<UnitEffect_Enum>() { UnitEffect_Enum.ColdAttack, UnitEffect_Enum.IceResistance }));
            D.Cards.Add(new CardMonsterVO(D.Cards.Count, "Illusionists", Image_Enum.CMV_illusionists_x2, 4, 0, 3, new List<UnitEffect_Enum>() { UnitEffect_Enum.Summoner, UnitEffect_Enum.PhysicalResistance }));
            D.Cards.Add(new CardMonsterVO(D.Cards.Count, "Monks", Image_Enum.CMV_monks_x2, 4, 5, 5, new List<UnitEffect_Enum>() { UnitEffect_Enum.Poison }));
            D.Cards.Add(new CardMonsterVO(D.Cards.Count, "Fire Mages", Image_Enum.CMV_fire_mages_x2, 5, 6, 5, new List<UnitEffect_Enum>() { UnitEffect_Enum.FireAttack, UnitEffect_Enum.FireResistance }));
            D.Cards.Add(new CardMonsterVO(D.Cards.Count, "Ice Mages", Image_Enum.CMV_ice_mages_x2, 5, 5, 6, new List<UnitEffect_Enum>() { UnitEffect_Enum.ColdAttack, UnitEffect_Enum.IceResistance }));
            D.Cards.Add(new CardMonsterVO(D.Cards.Count, "Illusionists", Image_Enum.CMV_illusionists_x2, 4, 0, 3, new List<UnitEffect_Enum>() { UnitEffect_Enum.Summoner, UnitEffect_Enum.PhysicalResistance }));
            D.Cards.Add(new CardMonsterVO(D.Cards.Count, "Monks", Image_Enum.CMV_monks_x2, 4, 5, 5, new List<UnitEffect_Enum>() { UnitEffect_Enum.Poison }));

            D.Cards.Add(new CardMonsterVO(D.Cards.Count, "Fire Golems", Image_Enum.CMV_fire_golems_x1, 5, 3, 4, new List<UnitEffect_Enum>() { UnitEffect_Enum.FireAttack, UnitEffect_Enum.FireResistance, UnitEffect_Enum.PhysicalResistance, UnitEffect_Enum.Brutal }));
            D.Cards.Add(new CardMonsterVO(D.Cards.Count, "Fire Mages", Image_Enum.CMV_fire_mages_x2, 5, 6, 5, new List<UnitEffect_Enum>() { UnitEffect_Enum.FireAttack, UnitEffect_Enum.FireResistance }));
            D.Cards.Add(new CardMonsterVO(D.Cards.Count, "Ice Golems", Image_Enum.CMV_ice_golems_x1, 5, 2, 4, new List<UnitEffect_Enum>() { UnitEffect_Enum.ColdAttack, UnitEffect_Enum.IceResistance, UnitEffect_Enum.PhysicalResistance, UnitEffect_Enum.Paralyze }));
            D.Cards.Add(new CardMonsterVO(D.Cards.Count, "Ice Mages", Image_Enum.CMV_ice_mages_x2, 5, 5, 6, new List<UnitEffect_Enum>() { UnitEffect_Enum.ColdAttack, UnitEffect_Enum.IceResistance }));
            D.Cards.Add(new CardMonsterVO(D.Cards.Count, "Illusionists", Image_Enum.CMV_illusionists_x2, 4, 0, 3, new List<UnitEffect_Enum>() { UnitEffect_Enum.Summoner, UnitEffect_Enum.PhysicalResistance }));
            D.Cards.Add(new CardMonsterVO(D.Cards.Count, "Monks", Image_Enum.CMV_monks_x2, 4, 5, 5, new List<UnitEffect_Enum>() { UnitEffect_Enum.Poison }));
            D.Cards.Add(new CardMonsterVO(D.Cards.Count, "Fire Mages", Image_Enum.CMV_fire_mages_x2, 5, 6, 5, new List<UnitEffect_Enum>() { UnitEffect_Enum.FireAttack, UnitEffect_Enum.FireResistance }));
            D.Cards.Add(new CardMonsterVO(D.Cards.Count, "Ice Mages", Image_Enum.CMV_ice_mages_x2, 5, 5, 6, new List<UnitEffect_Enum>() { UnitEffect_Enum.ColdAttack, UnitEffect_Enum.IceResistance }));
            D.Cards.Add(new CardMonsterVO(D.Cards.Count, "Illusionists", Image_Enum.CMV_illusionists_x2, 4, 0, 3, new List<UnitEffect_Enum>() { UnitEffect_Enum.Summoner, UnitEffect_Enum.PhysicalResistance }));
            D.Cards.Add(new CardMonsterVO(D.Cards.Count, "Monks", Image_Enum.CMV_monks_x2, 4, 5, 5, new List<UnitEffect_Enum>() { UnitEffect_Enum.Poison }));

            //  WHITE
            D.Cards.Add(new CardMonsterVO(D.Cards.Count, "Altem Guardians", Image_Enum.CMW_altem_guardians_x2, 8, 5, 7, new List<UnitEffect_Enum>() { UnitEffect_Enum.Fortified, UnitEffect_Enum.FireResistance, UnitEffect_Enum.IceResistance, UnitEffect_Enum.PhysicalResistance }));
            D.Cards.Add(new CardMonsterVO(D.Cards.Count, "Altem Guardsmen", Image_Enum.CMW_altem_guardsmen_x2, 8, 6, 7, new List<UnitEffect_Enum>() { UnitEffect_Enum.Fortified, UnitEffect_Enum.FireResistance, UnitEffect_Enum.IceResistance, UnitEffect_Enum.PhysicalResistance }));
            D.Cards.Add(new CardMonsterVO(D.Cards.Count, "Altem Mages", Image_Enum.CMW_altem_mages_x2, 8, 4, 8, new List<UnitEffect_Enum>() { UnitEffect_Enum.ColdFireAttack, UnitEffect_Enum.Fortified, UnitEffect_Enum.PhysicalResistance, UnitEffect_Enum.Poison, UnitEffect_Enum.Brutal }));
            D.Cards.Add(new CardMonsterVO(D.Cards.Count, "Freezers", Image_Enum.CMW_freezers_x3, 7, 3, 7, new List<UnitEffect_Enum>() { UnitEffect_Enum.ColdAttack, UnitEffect_Enum.FireResistance, UnitEffect_Enum.Paralyze, UnitEffect_Enum.Swiftness }));
            D.Cards.Add(new CardMonsterVO(D.Cards.Count, "Gunners", Image_Enum.CMW_gunners_x3, 7, 6, 6, new List<UnitEffect_Enum>() { UnitEffect_Enum.FireAttack, UnitEffect_Enum.IceResistance, UnitEffect_Enum.Brutal }));
            D.Cards.Add(new CardMonsterVO(D.Cards.Count, "Altem Guardians", Image_Enum.CMW_altem_guardians_x2, 8, 5, 7, new List<UnitEffect_Enum>() { UnitEffect_Enum.Fortified, UnitEffect_Enum.FireResistance, UnitEffect_Enum.IceResistance, UnitEffect_Enum.PhysicalResistance }));
            D.Cards.Add(new CardMonsterVO(D.Cards.Count, "Altem Guardsmen", Image_Enum.CMW_altem_guardsmen_x2, 8, 6, 7, new List<UnitEffect_Enum>() { UnitEffect_Enum.Fortified, UnitEffect_Enum.FireResistance, UnitEffect_Enum.IceResistance, UnitEffect_Enum.PhysicalResistance }));
            D.Cards.Add(new CardMonsterVO(D.Cards.Count, "Altem Mages", Image_Enum.CMW_altem_mages_x2, 8, 4, 8, new List<UnitEffect_Enum>() { UnitEffect_Enum.ColdFireAttack, UnitEffect_Enum.Fortified, UnitEffect_Enum.PhysicalResistance, UnitEffect_Enum.Poison, UnitEffect_Enum.Brutal }));
            D.Cards.Add(new CardMonsterVO(D.Cards.Count, "Freezers", Image_Enum.CMW_freezers_x3, 7, 3, 7, new List<UnitEffect_Enum>() { UnitEffect_Enum.ColdAttack, UnitEffect_Enum.FireResistance, UnitEffect_Enum.Paralyze, UnitEffect_Enum.Swiftness }));
            D.Cards.Add(new CardMonsterVO(D.Cards.Count, "Gunners", Image_Enum.CMW_gunners_x3, 7, 6, 6, new List<UnitEffect_Enum>() { UnitEffect_Enum.FireAttack, UnitEffect_Enum.IceResistance, UnitEffect_Enum.Brutal }));
            D.Cards.Add(new CardMonsterVO(D.Cards.Count, "Freezers", Image_Enum.CMW_freezers_x3, 7, 3, 7, new List<UnitEffect_Enum>() { UnitEffect_Enum.ColdAttack, UnitEffect_Enum.FireResistance, UnitEffect_Enum.Paralyze, UnitEffect_Enum.Swiftness }));
            D.Cards.Add(new CardMonsterVO(D.Cards.Count, "Gunners", Image_Enum.CMW_gunners_x3, 7, 6, 6, new List<UnitEffect_Enum>() { UnitEffect_Enum.FireAttack, UnitEffect_Enum.IceResistance, UnitEffect_Enum.Brutal }));

            D.Cards.Add(new CardMonsterVO(D.Cards.Count, "Altem Guardians", Image_Enum.CMW_altem_guardians_x2, 8, 5, 7, new List<UnitEffect_Enum>() { UnitEffect_Enum.Fortified, UnitEffect_Enum.FireResistance, UnitEffect_Enum.IceResistance, UnitEffect_Enum.PhysicalResistance }));
            D.Cards.Add(new CardMonsterVO(D.Cards.Count, "Altem Guardsmen", Image_Enum.CMW_altem_guardsmen_x2, 8, 6, 7, new List<UnitEffect_Enum>() { UnitEffect_Enum.Fortified, UnitEffect_Enum.FireResistance, UnitEffect_Enum.IceResistance, UnitEffect_Enum.PhysicalResistance }));
            D.Cards.Add(new CardMonsterVO(D.Cards.Count, "Altem Mages", Image_Enum.CMW_altem_mages_x2, 8, 4, 8, new List<UnitEffect_Enum>() { UnitEffect_Enum.ColdFireAttack, UnitEffect_Enum.Fortified, UnitEffect_Enum.PhysicalResistance, UnitEffect_Enum.Poison, UnitEffect_Enum.Brutal }));
            D.Cards.Add(new CardMonsterVO(D.Cards.Count, "Freezers", Image_Enum.CMW_freezers_x3, 7, 3, 7, new List<UnitEffect_Enum>() { UnitEffect_Enum.ColdAttack, UnitEffect_Enum.FireResistance, UnitEffect_Enum.Paralyze, UnitEffect_Enum.Swiftness }));
            D.Cards.Add(new CardMonsterVO(D.Cards.Count, "Gunners", Image_Enum.CMW_gunners_x3, 7, 6, 6, new List<UnitEffect_Enum>() { UnitEffect_Enum.FireAttack, UnitEffect_Enum.IceResistance, UnitEffect_Enum.Brutal }));
            D.Cards.Add(new CardMonsterVO(D.Cards.Count, "Altem Guardians", Image_Enum.CMW_altem_guardians_x2, 8, 5, 7, new List<UnitEffect_Enum>() { UnitEffect_Enum.Fortified, UnitEffect_Enum.FireResistance, UnitEffect_Enum.IceResistance, UnitEffect_Enum.PhysicalResistance }));
            D.Cards.Add(new CardMonsterVO(D.Cards.Count, "Altem Guardsmen", Image_Enum.CMW_altem_guardsmen_x2, 8, 6, 7, new List<UnitEffect_Enum>() { UnitEffect_Enum.Fortified, UnitEffect_Enum.FireResistance, UnitEffect_Enum.IceResistance, UnitEffect_Enum.PhysicalResistance }));
            D.Cards.Add(new CardMonsterVO(D.Cards.Count, "Altem Mages", Image_Enum.CMW_altem_mages_x2, 8, 4, 8, new List<UnitEffect_Enum>() { UnitEffect_Enum.ColdFireAttack, UnitEffect_Enum.Fortified, UnitEffect_Enum.PhysicalResistance, UnitEffect_Enum.Poison, UnitEffect_Enum.Brutal }));
            D.Cards.Add(new CardMonsterVO(D.Cards.Count, "Freezers", Image_Enum.CMW_freezers_x3, 7, 3, 7, new List<UnitEffect_Enum>() { UnitEffect_Enum.ColdAttack, UnitEffect_Enum.FireResistance, UnitEffect_Enum.Paralyze, UnitEffect_Enum.Swiftness }));
            D.Cards.Add(new CardMonsterVO(D.Cards.Count, "Gunners", Image_Enum.CMW_gunners_x3, 7, 6, 6, new List<UnitEffect_Enum>() { UnitEffect_Enum.FireAttack, UnitEffect_Enum.IceResistance, UnitEffect_Enum.Brutal }));
            D.Cards.Add(new CardMonsterVO(D.Cards.Count, "Freezers", Image_Enum.CMW_freezers_x3, 7, 3, 7, new List<UnitEffect_Enum>() { UnitEffect_Enum.ColdAttack, UnitEffect_Enum.FireResistance, UnitEffect_Enum.Paralyze, UnitEffect_Enum.Swiftness }));
            D.Cards.Add(new CardMonsterVO(D.Cards.Count, "Gunners", Image_Enum.CMW_gunners_x3, 7, 6, 6, new List<UnitEffect_Enum>() { UnitEffect_Enum.FireAttack, UnitEffect_Enum.IceResistance, UnitEffect_Enum.Brutal }));

            //  RED
            D.Cards.Add(new CardMonsterVO(D.Cards.Count, "Fire Dragon", Image_Enum.CMR_fire_dragon_x2, 8, 9, 7, new List<UnitEffect_Enum>() { UnitEffect_Enum.FireAttack, UnitEffect_Enum.PhysicalResistance, UnitEffect_Enum.FireResistance }));
            D.Cards.Add(new CardMonsterVO(D.Cards.Count, "High Dragon", Image_Enum.CMR_high_dragon_x2, 9, 6, 9, new List<UnitEffect_Enum>() { UnitEffect_Enum.ColdFireAttack, UnitEffect_Enum.FireResistance, UnitEffect_Enum.IceResistance, UnitEffect_Enum.Brutal }));
            D.Cards.Add(new CardMonsterVO(D.Cards.Count, "Ice Dragon", Image_Enum.CMR_ice_dragon_x2, 8, 6, 7, new List<UnitEffect_Enum>() { UnitEffect_Enum.ColdAttack, UnitEffect_Enum.IceResistance, UnitEffect_Enum.PhysicalResistance, UnitEffect_Enum.Paralyze }));
            D.Cards.Add(new CardMonsterVO(D.Cards.Count, "Swamp Dragon", Image_Enum.CMR_swamp_dragon_x2, 7, 5, 9, new List<UnitEffect_Enum>() { UnitEffect_Enum.Poison, UnitEffect_Enum.Swiftness }));
            D.Cards.Add(new CardMonsterVO(D.Cards.Count, "Fire Dragon", Image_Enum.CMR_fire_dragon_x2, 8, 9, 7, new List<UnitEffect_Enum>() { UnitEffect_Enum.FireAttack, UnitEffect_Enum.PhysicalResistance, UnitEffect_Enum.FireResistance }));
            D.Cards.Add(new CardMonsterVO(D.Cards.Count, "High Dragon", Image_Enum.CMR_high_dragon_x2, 9, 6, 9, new List<UnitEffect_Enum>() { UnitEffect_Enum.ColdFireAttack, UnitEffect_Enum.FireResistance, UnitEffect_Enum.IceResistance, UnitEffect_Enum.Brutal }));
            D.Cards.Add(new CardMonsterVO(D.Cards.Count, "Ice Dragon", Image_Enum.CMR_ice_dragon_x2, 8, 6, 7, new List<UnitEffect_Enum>() { UnitEffect_Enum.ColdAttack, UnitEffect_Enum.IceResistance, UnitEffect_Enum.PhysicalResistance, UnitEffect_Enum.Paralyze }));
            D.Cards.Add(new CardMonsterVO(D.Cards.Count, "Swamp Dragon", Image_Enum.CMR_swamp_dragon_x2, 7, 5, 9, new List<UnitEffect_Enum>() { UnitEffect_Enum.Poison, UnitEffect_Enum.Swiftness }));

            D.Cards.Add(new CardMonsterVO(D.Cards.Count, "Fire Dragon", Image_Enum.CMR_fire_dragon_x2, 8, 9, 7, new List<UnitEffect_Enum>() { UnitEffect_Enum.FireAttack, UnitEffect_Enum.PhysicalResistance, UnitEffect_Enum.FireResistance }));
            D.Cards.Add(new CardMonsterVO(D.Cards.Count, "High Dragon", Image_Enum.CMR_high_dragon_x2, 9, 6, 9, new List<UnitEffect_Enum>() { UnitEffect_Enum.ColdFireAttack, UnitEffect_Enum.FireResistance, UnitEffect_Enum.IceResistance, UnitEffect_Enum.Brutal }));
            D.Cards.Add(new CardMonsterVO(D.Cards.Count, "Ice Dragon", Image_Enum.CMR_ice_dragon_x2, 8, 6, 7, new List<UnitEffect_Enum>() { UnitEffect_Enum.ColdAttack, UnitEffect_Enum.IceResistance, UnitEffect_Enum.PhysicalResistance, UnitEffect_Enum.Paralyze }));
            D.Cards.Add(new CardMonsterVO(D.Cards.Count, "Swamp Dragon", Image_Enum.CMR_swamp_dragon_x2, 7, 5, 9, new List<UnitEffect_Enum>() { UnitEffect_Enum.Poison, UnitEffect_Enum.Swiftness }));
            D.Cards.Add(new CardMonsterVO(D.Cards.Count, "Fire Dragon", Image_Enum.CMR_fire_dragon_x2, 8, 9, 7, new List<UnitEffect_Enum>() { UnitEffect_Enum.FireAttack, UnitEffect_Enum.PhysicalResistance, UnitEffect_Enum.FireResistance }));
            D.Cards.Add(new CardMonsterVO(D.Cards.Count, "High Dragon", Image_Enum.CMR_high_dragon_x2, 9, 6, 9, new List<UnitEffect_Enum>() { UnitEffect_Enum.ColdFireAttack, UnitEffect_Enum.FireResistance, UnitEffect_Enum.IceResistance, UnitEffect_Enum.Brutal }));
            D.Cards.Add(new CardMonsterVO(D.Cards.Count, "Ice Dragon", Image_Enum.CMR_ice_dragon_x2, 8, 6, 7, new List<UnitEffect_Enum>() { UnitEffect_Enum.ColdAttack, UnitEffect_Enum.IceResistance, UnitEffect_Enum.PhysicalResistance, UnitEffect_Enum.Paralyze }));
            D.Cards.Add(new CardMonsterVO(D.Cards.Count, "Swamp Dragon", Image_Enum.CMR_swamp_dragon_x2, 7, 5, 9, new List<UnitEffect_Enum>() { UnitEffect_Enum.Poison, UnitEffect_Enum.Swiftness }));

            //  YELLOW (Ruins)
            D.Cards.Add(new CardRuinVO(D.Cards.Count, Image_Enum.R_altars02, CardType_Enum.AncientRuins_Alter, Crystal_Enum.Blue));
            D.Cards.Add(new CardRuinVO(D.Cards.Count, Image_Enum.R_altars04, CardType_Enum.AncientRuins_Alter, Crystal_Enum.Red));
            D.Cards.Add(new CardRuinVO(D.Cards.Count, Image_Enum.R_altars01, CardType_Enum.AncientRuins_Alter, Crystal_Enum.Green));
            D.Cards.Add(new CardRuinVO(D.Cards.Count, Image_Enum.R_altars03, CardType_Enum.AncientRuins_Alter, Crystal_Enum.White));

            D.Cards.Add(new CardRuinVO(D.Cards.Count, Image_Enum.R_enemies01, CardType_Enum.AncientRuins_Monster, Crystal_Enum.NA, new List<MonsterType_Enum>() { MonsterType_Enum.Brown, MonsterType_Enum.Red }, new List<Reward_Enum>() { Reward_Enum.Artifact, Reward_Enum.Artifact }));
            D.Cards.Add(new CardRuinVO(D.Cards.Count, Image_Enum.R_enemies02, CardType_Enum.AncientRuins_Monster, Crystal_Enum.NA, new List<MonsterType_Enum>() { MonsterType_Enum.Green, MonsterType_Enum.Red }, new List<Reward_Enum>() { Reward_Enum.Artifact, Reward_Enum.Action }));
            D.Cards.Add(new CardRuinVO(D.Cards.Count, Image_Enum.R_enemies03, CardType_Enum.AncientRuins_Monster, Crystal_Enum.NA, new List<MonsterType_Enum>() { MonsterType_Enum.Grey, MonsterType_Enum.White }, new List<Reward_Enum>() { Reward_Enum.Artifact, Reward_Enum.Spell }));
            D.Cards.Add(new CardRuinVO(D.Cards.Count, Image_Enum.R_enemies04, CardType_Enum.AncientRuins_Monster, Crystal_Enum.NA, new List<MonsterType_Enum>() { MonsterType_Enum.Grey, MonsterType_Enum.Brown }, new List<Reward_Enum>() { Reward_Enum.Artifact }));
            D.Cards.Add(new CardRuinVO(D.Cards.Count, Image_Enum.R_enemies05, CardType_Enum.AncientRuins_Monster, Crystal_Enum.NA, new List<MonsterType_Enum>() { MonsterType_Enum.Grey, MonsterType_Enum.Violet }, new List<Reward_Enum>() { Reward_Enum.Unit }));
            D.Cards.Add(new CardRuinVO(D.Cards.Count, Image_Enum.R_enemies06, CardType_Enum.AncientRuins_Monster, Crystal_Enum.NA, new List<MonsterType_Enum>() { MonsterType_Enum.Green, MonsterType_Enum.Brown }, new List<Reward_Enum>() { Reward_Enum.Artifact }));
            D.Cards.Add(new CardRuinVO(D.Cards.Count, Image_Enum.R_enemies07, CardType_Enum.AncientRuins_Monster, Crystal_Enum.NA, new List<MonsterType_Enum>() { MonsterType_Enum.Green, MonsterType_Enum.Green }, new List<Reward_Enum>() { Reward_Enum.Blue, Reward_Enum.Red, Reward_Enum.Green, Reward_Enum.White }));
            D.Cards.Add(new CardRuinVO(D.Cards.Count, Image_Enum.R_enemies08, CardType_Enum.AncientRuins_Monster, Crystal_Enum.NA, new List<MonsterType_Enum>() { MonsterType_Enum.Brown, MonsterType_Enum.Violet }, new List<Reward_Enum>() { Reward_Enum.Spell, Reward_Enum.Blue, Reward_Enum.Red, Reward_Enum.Green, Reward_Enum.White }));


            //  Units NORMAL
            D.Cards.Add(new ForestersVO(D.Cards.Count));
            D.Cards.Add(new GuardianGolemsVO(D.Cards.Count));
            D.Cards.Add(new HerbalistsVO(D.Cards.Count));
            D.Cards.Add(new IllusionistsVO(D.Cards.Count));
            D.Cards.Add(new NorthernMonksVO(D.Cards.Count));
            D.Cards.Add(new PeasantsVO(D.Cards.Count));
            D.Cards.Add(new RedCapeMonksVO(D.Cards.Count));
            D.Cards.Add(new SavageMonksVO(D.Cards.Count));
            D.Cards.Add(new UtemCrossbowmenVO(D.Cards.Count));
            D.Cards.Add(new UtemGuardsmenVO(D.Cards.Count));
            D.Cards.Add(new UtemSwordsmenVO(D.Cards.Count));

            D.Cards.Add(new ForestersVO(D.Cards.Count));
            D.Cards.Add(new GuardianGolemsVO(D.Cards.Count));
            D.Cards.Add(new HerbalistsVO(D.Cards.Count));
            D.Cards.Add(new IllusionistsVO(D.Cards.Count));
            D.Cards.Add(new NorthernMonksVO(D.Cards.Count));
            D.Cards.Add(new PeasantsVO(D.Cards.Count));
            D.Cards.Add(new RedCapeMonksVO(D.Cards.Count));
            D.Cards.Add(new SavageMonksVO(D.Cards.Count));
            D.Cards.Add(new UtemCrossbowmenVO(D.Cards.Count));
            D.Cards.Add(new UtemGuardsmenVO(D.Cards.Count));
            D.Cards.Add(new UtemSwordsmenVO(D.Cards.Count));

            D.Cards.Add(new ForestersVO(D.Cards.Count));
            D.Cards.Add(new GuardianGolemsVO(D.Cards.Count));
            D.Cards.Add(new HerbalistsVO(D.Cards.Count));
            D.Cards.Add(new IllusionistsVO(D.Cards.Count));
            D.Cards.Add(new NorthernMonksVO(D.Cards.Count));
            D.Cards.Add(new PeasantsVO(D.Cards.Count));
            D.Cards.Add(new RedCapeMonksVO(D.Cards.Count));
            D.Cards.Add(new SavageMonksVO(D.Cards.Count));
            D.Cards.Add(new UtemCrossbowmenVO(D.Cards.Count));
            D.Cards.Add(new UtemGuardsmenVO(D.Cards.Count));
            D.Cards.Add(new UtemSwordsmenVO(D.Cards.Count));

            D.Cards.Add(new ForestersVO(D.Cards.Count));
            D.Cards.Add(new GuardianGolemsVO(D.Cards.Count));
            D.Cards.Add(new HerbalistsVO(D.Cards.Count));
            D.Cards.Add(new IllusionistsVO(D.Cards.Count));
            D.Cards.Add(new NorthernMonksVO(D.Cards.Count));
            D.Cards.Add(new PeasantsVO(D.Cards.Count));
            D.Cards.Add(new RedCapeMonksVO(D.Cards.Count));
            D.Cards.Add(new SavageMonksVO(D.Cards.Count));
            D.Cards.Add(new UtemCrossbowmenVO(D.Cards.Count));
            D.Cards.Add(new UtemGuardsmenVO(D.Cards.Count));
            D.Cards.Add(new UtemSwordsmenVO(D.Cards.Count));

            //  Units ELITE
            D.Cards.Add(new FireGolemsVO(D.Cards.Count));
            D.Cards.Add(new IceGolemsVO(D.Cards.Count));
            D.Cards.Add(new FireMagesVO(D.Cards.Count));
            D.Cards.Add(new IceMagesVO(D.Cards.Count));
            D.Cards.Add(new AmotepGunnersVO(D.Cards.Count));
            D.Cards.Add(new AmotepFreezersVO(D.Cards.Count));
            D.Cards.Add(new CatapultsVO(D.Cards.Count));
            D.Cards.Add(new AltemMagesVO(D.Cards.Count));
            D.Cards.Add(new AltemGuardiansVO(D.Cards.Count));

            D.Cards.Add(new FireGolemsVO(D.Cards.Count));
            D.Cards.Add(new IceGolemsVO(D.Cards.Count));
            D.Cards.Add(new FireMagesVO(D.Cards.Count));
            D.Cards.Add(new IceMagesVO(D.Cards.Count));
            D.Cards.Add(new AmotepGunnersVO(D.Cards.Count));
            D.Cards.Add(new AmotepFreezersVO(D.Cards.Count));
            D.Cards.Add(new CatapultsVO(D.Cards.Count));
            D.Cards.Add(new AltemMagesVO(D.Cards.Count));
            D.Cards.Add(new AltemGuardiansVO(D.Cards.Count));

            D.Cards.Add(new FireGolemsVO(D.Cards.Count));
            D.Cards.Add(new IceGolemsVO(D.Cards.Count));
            D.Cards.Add(new FireMagesVO(D.Cards.Count));
            D.Cards.Add(new IceMagesVO(D.Cards.Count));
            D.Cards.Add(new AmotepGunnersVO(D.Cards.Count));
            D.Cards.Add(new AmotepFreezersVO(D.Cards.Count));
            D.Cards.Add(new CatapultsVO(D.Cards.Count));
            D.Cards.Add(new AltemMagesVO(D.Cards.Count));
            D.Cards.Add(new AltemGuardiansVO(D.Cards.Count));

            D.Cards.Add(new FireGolemsVO(D.Cards.Count));
            D.Cards.Add(new IceGolemsVO(D.Cards.Count));
            D.Cards.Add(new FireMagesVO(D.Cards.Count));
            D.Cards.Add(new IceMagesVO(D.Cards.Count));
            D.Cards.Add(new AmotepGunnersVO(D.Cards.Count));
            D.Cards.Add(new AmotepFreezersVO(D.Cards.Count));
            D.Cards.Add(new CatapultsVO(D.Cards.Count));
            D.Cards.Add(new AltemMagesVO(D.Cards.Count));
            D.Cards.Add(new AltemGuardiansVO(D.Cards.Count));

            //  Skills
            D.Cards.Add(new GREEN_FreezingPowerVO(D.Cards.Count));
            D.Cards.Add(new GREEN_PotionMakingVO(D.Cards.Count));
            D.Cards.Add(new GREEN_WhiteCrystalCraftVO(D.Cards.Count));
            D.Cards.Add(new GREEN_GreenCrystalCraftVO(D.Cards.Count));
            D.Cards.Add(new GREEN_RedCrystalCraftVO(D.Cards.Count));
            D.Cards.Add(new GREEN_GlitteringFortuneVO(D.Cards.Count));
            D.Cards.Add(new GREEN_FlightVO(D.Cards.Count));
            D.Cards.Add(new GREEN_UniversalPowerVO(D.Cards.Count));
            D.Cards.Add(new GREEN_MotivationVO(D.Cards.Count));
            //D.Cards.Add(new GREEN_SourceFreezeVO(D.Cards.Count));
            D.Cards.Add(new BLUE_DoubleTimeVO(D.Cards.Count));
            D.Cards.Add(new BLUE_NightSharpshootingVO(D.Cards.Count));
            D.Cards.Add(new BLUE_ColdSwordsmanshipVO(D.Cards.Count));
            D.Cards.Add(new BLUE_ShieldMasteryVO(D.Cards.Count));
            D.Cards.Add(new BLUE_ResistanceBreakVO(D.Cards.Count));
            D.Cards.Add(new BLUE_IFeelNoPainVO(D.Cards.Count));
            D.Cards.Add(new BLUE_IDontGiveaDamnVO(D.Cards.Count));
            D.Cards.Add(new BLUE_WhoNeedsMagicVO(D.Cards.Count));
            D.Cards.Add(new BLUE_MotivationVO(D.Cards.Count));
            //D.Cards.Add(new BLUE_ManaExploitVO(D.Cards.Count));
            D.Cards.Add(new WHITE_ForwardMarchVO(D.Cards.Count));
            D.Cards.Add(new WHITE_DaySharpshootingVO(D.Cards.Count));
            D.Cards.Add(new WHITE_InspirationVO(D.Cards.Count));
            D.Cards.Add(new WHITE_BrightNegotiationVO(D.Cards.Count));
            D.Cards.Add(new WHITE_LeavesintheWindVO(D.Cards.Count));
            D.Cards.Add(new WHITE_WhispersintheTreetopsVO(D.Cards.Count));
            D.Cards.Add(new WHITE_LeadershipVO(D.Cards.Count));
            D.Cards.Add(new WHITE_BondsofLoyaltyVO(D.Cards.Count));
            D.Cards.Add(new WHITE_MotivationVO(D.Cards.Count));
            //D.Cards.Add(new WHITE_PrayerofWeatherVO(D.Cards.Count));
            D.Cards.Add(new RED_DarkPathsVO(D.Cards.Count));
            D.Cards.Add(new RED_BurningPowerVO(D.Cards.Count));
            D.Cards.Add(new RED_HotSwordsmanshipVO(D.Cards.Count));
            D.Cards.Add(new RED_DarkNegotiationVO(D.Cards.Count));
            D.Cards.Add(new RED_DarkFireMagicVO(D.Cards.Count));
            D.Cards.Add(new RED_PowerofPainVO(D.Cards.Count));
            D.Cards.Add(new RED_InvocationVO(D.Cards.Count));
            D.Cards.Add(new RED_PolarizationVO(D.Cards.Count));
            D.Cards.Add(new RED_MotivationVO(D.Cards.Count));
            D.Cards.Add(new RED_HealingRitualVO(D.Cards.Count));


            //  Advanced Cards
            D.Cards.Add(new FireBoltVO(D.Cards.Count));
            D.Cards.Add(new IceBoltVO(D.Cards.Count));
            D.Cards.Add(new SwiftBoltVO(D.Cards.Count));
            D.Cards.Add(new CrushingBoltVO(D.Cards.Count));
            D.Cards.Add(new BloodRageVO(D.Cards.Count));
            D.Cards.Add(new IceShieldVO(D.Cards.Count));
            D.Cards.Add(new AgilityVO(D.Cards.Count));
            D.Cards.Add(new RefreshingWalkVO(D.Cards.Count));
            D.Cards.Add(new IntimidateVO(D.Cards.Count));
            D.Cards.Add(new FrostBridgeVO(D.Cards.Count));
            D.Cards.Add(new SongofWindVO(D.Cards.Count));
            D.Cards.Add(new PathFindingVO(D.Cards.Count));
            D.Cards.Add(new BloodRitualVO(D.Cards.Count));
            D.Cards.Add(new PureMagicVO(D.Cards.Count));
            D.Cards.Add(new HeroicTaleVO(D.Cards.Count));
            D.Cards.Add(new RegenerationVO(D.Cards.Count));
            D.Cards.Add(new IntotheHeatVO(D.Cards.Count));
            D.Cards.Add(new SteadyTempoVO(D.Cards.Count));
            D.Cards.Add(new DiplomacyVO(D.Cards.Count));
            D.Cards.Add(new InNeedVO(D.Cards.Count));
            D.Cards.Add(new DecomposeVO(D.Cards.Count));
            D.Cards.Add(new CrystalMasteryVO(D.Cards.Count));
            D.Cards.Add(new ManaStormVO(D.Cards.Count));
            D.Cards.Add(new AmbushVO(D.Cards.Count));
            D.Cards.Add(new MaximalEffectVO(D.Cards.Count));
            D.Cards.Add(new MagicTalentVO(D.Cards.Count));
            D.Cards.Add(new LearningVO(D.Cards.Count));
            //D.Cards.Add(new TrainingVO(D.Cards.Count));

            //  Spell Cards
            D.Cards.Add(new FireballVO(D.Cards.Count));
            D.Cards.Add(new SnowstormVO(D.Cards.Count));
            D.Cards.Add(new ExposeVO(D.Cards.Count));
            D.Cards.Add(new TremorVO(D.Cards.Count));
            D.Cards.Add(new FlameWallVO(D.Cards.Count));
            D.Cards.Add(new ManaBoltVO(D.Cards.Count));
            D.Cards.Add(new WhirlwindVO(D.Cards.Count));
            D.Cards.Add(new UndergroundTravelVO(D.Cards.Count));
            D.Cards.Add(new BurningShieldVO(D.Cards.Count));
            D.Cards.Add(new ChillVO(D.Cards.Count));
            D.Cards.Add(new WingsofWindVO(D.Cards.Count));
            D.Cards.Add(new RestorationVO(D.Cards.Count));
            D.Cards.Add(new DemolishVO(D.Cards.Count));
            D.Cards.Add(new SpaceBendingVO(D.Cards.Count));
            D.Cards.Add(new CalltoArmsVO(D.Cards.Count));
            D.Cards.Add(new MeditationVO(D.Cards.Count));
            //D.Cards.Add(new ManaMeltdownVO(D.Cards.Count));
            //D.Cards.Add(new ManaClaimVO(D.Cards.Count));
            //D.Cards.Add(new MindReadVO(D.Cards.Count));
            //D.Cards.Add(new EnergyFlowVO(D.Cards.Count));

            //  Artifact Cards
            D.Cards.Add(new BannerofGloryVO(D.Cards.Count));
            D.Cards.Add(new BannerofFearVO(D.Cards.Count));
            D.Cards.Add(new BannerofProtectionVO(D.Cards.Count));
            D.Cards.Add(new BannerofCourageVO(D.Cards.Count));
            D.Cards.Add(new RubyRingVO(D.Cards.Count));
            D.Cards.Add(new SapphireRingVO(D.Cards.Count));
            D.Cards.Add(new DiamondRingVO(D.Cards.Count));
            D.Cards.Add(new EmeraldRingVO(D.Cards.Count));
            D.Cards.Add(new SwordofJusticeVO(D.Cards.Count));
            D.Cards.Add(new HornofWrathVO(D.Cards.Count));
            D.Cards.Add(new GoldenGrailVO(D.Cards.Count));
            D.Cards.Add(new EndlessBagofGoldVO(D.Cards.Count));
            D.Cards.Add(new EndlessGemPouchVO(D.Cards.Count));
            //D.Cards.Add(new BookofWisdomVO(D.Cards.Count));
            D.Cards.Add(new AmuletofSunVO(D.Cards.Count));
            D.Cards.Add(new AmuletofDarknessVO(D.Cards.Count));


            //  Tactics
            D.Cards.Add(new CardTacticVO(D.Cards.Count, "Early Bird", Image_Enum.T_day_1, CardType_Enum.Tactics_Day, 1));
            D.Cards.Add(new CardTacticVO(D.Cards.Count, "Rething", Image_Enum.T_day_2, CardType_Enum.Tactics_Day, 2));
            D.Cards.Add(new CardTacticVO(D.Cards.Count, "Mana Steal", Image_Enum.T_day_3, CardType_Enum.Tactics_Day, 3));
            D.Cards.Add(new CardTacticVO(D.Cards.Count, "Planning", Image_Enum.T_day_4, CardType_Enum.Tactics_Day, 4));
            D.Cards.Add(new CardTacticVO(D.Cards.Count, "Great Start", Image_Enum.T_day_5, CardType_Enum.Tactics_Day, 5));
            D.Cards.Add(new CardTacticVO(D.Cards.Count, "The Right Moment", Image_Enum.T_day_6, CardType_Enum.Tactics_Day, 6));
            D.Cards.Add(new CardTacticVO(D.Cards.Count, "From The Dusk", Image_Enum.T_night_1, CardType_Enum.Tactics_Night, 1));
            D.Cards.Add(new CardTacticVO(D.Cards.Count, "Long Night", Image_Enum.T_night_2, CardType_Enum.Tactics_Night, 2));
            D.Cards.Add(new CardTacticVO(D.Cards.Count, "Mana Search", Image_Enum.T_night_3, CardType_Enum.Tactics_Night, 3));
            D.Cards.Add(new CardTacticVO(D.Cards.Count, "Midnight Meditation", Image_Enum.T_night_4, CardType_Enum.Tactics_Night, 4));
            D.Cards.Add(new CardTacticVO(D.Cards.Count, "Preparation", Image_Enum.T_night_5, CardType_Enum.Tactics_Night, 5));
            D.Cards.Add(new CardTacticVO(D.Cards.Count, "Sparing Power", Image_Enum.T_night_6, CardType_Enum.Tactics_Night, 6));

        }


        public void UpdateCardMetaDataBattleEffects() {

        }

        public void UpdateCardMetaDataGameEffects() {
            D.Cards.Add(new CardGameEffectVO(
                D.Cards.Count,
                "Cold Toughness",
                Image_Enum.I_shield,
                CardType_Enum.GameEffect,
                GameEffect_Enum.ColdToughness,
                GameEffectDuration_Enum.Turn,
                CNAColor.ColorLightBlue,
                "During Block Phase, One Time use, selected monster only, each extra symbol the blocked monster has adds +1 to Ice Block.",
                false, true, true));
            D.Cards.Add(new CardGameEffectVO(
                D.Cards.Count,
                "Utem Guardsmen",
                Image_Enum.I_monsterswift,
                CardType_Enum.GameEffect,
                GameEffect_Enum.UtemGuardsmen,
                GameEffectDuration_Enum.Turn,
                CNAColor.ColorLightBlue,
                "+4 Block against a monster with Swiftness.",
                false, true, true));
            D.Cards.Add(new CardGameEffectVO(
                D.Cards.Count,
                "Resistance Break",
                Image_Enum.I_shield,
                CardType_Enum.GameEffect,
                GameEffect_Enum.BLUE_ResistanceBreak,
                GameEffectDuration_Enum.Turn,
                CNAColor.ColorLightBlue,
                "enemy token gets armor -1 for each resistance it has To a minimum of 1.",
                false, true, true));
            D.Cards.Add(new CardGameEffectVO(
                D.Cards.Count,
                "Leadership",
                Image_Enum.I_attack,
                CardType_Enum.GameEffect,
                GameEffect_Enum.WHITE_Leadership,
                GameEffectDuration_Enum.Turn,
                CNAColor.ColorLightBlue,
                "When activating a Unit, add +3 to its Block, or +2 to its Attack. or +1 to its Ranged (not Siege) Attack, regardless of its elements.",
                false, true, false));
            D.Cards.Add(new CardGameEffectVO(
                D.Cards.Count,
                "Day",
                Image_Enum.I_sun,
                CardType_Enum.GameEffect,
                GameEffect_Enum.Day,
                GameEffectDuration_Enum.Round,
                CNAColor.ColorLightBlue,
                "Day time, Gold mana can be consumed as any basic mana type.  Black mana can not be used.",
                true, false, false));
            D.Cards.Add(new CardGameEffectVO(
                D.Cards.Count,
                "Night",
                Image_Enum.I_moon,
                CardType_Enum.GameEffect,
                GameEffect_Enum.Night,
                GameEffectDuration_Enum.Round,
                CNAColor.ColorLightBlue,
                "Night time, Black mana can be used, however Gold mana can not be used.",
                true, false, false));
            D.Cards.Add(new CardGameEffectVO(
                D.Cards.Count,
                "Foresters",
                Image_Enum.I_boots,
                CardType_Enum.GameEffect,
                GameEffect_Enum.Foresters,
                GameEffectDuration_Enum.Turn,
                CNAColor.ColorLightBlue,
                "The move cost of forests, hills and swamps is reduced by 1 this turn, to a minimum of 0.",
                true, false, true));
            D.Cards.Add(new CardGameEffectVO(
                D.Cards.Count,
                "Flight",
                Image_Enum.I_bootfly,
                CardType_Enum.GameEffect,
                GameEffect_Enum.GREEN_Flight,
                GameEffectDuration_Enum.Turn,
                CNAColor.ColorLightBlue,
                "move to an adjacent space for free, or to move two spaces for 2 Move points. You must end this move in a safe space. This move does not provoke rampaging enemies.",
                true, false, false));
            D.Cards.Add(new AgilityNormalGEVO(D.Cards.Count));
            D.Cards.Add(new AgilityAdvancedGEVO(D.Cards.Count));
            D.Cards.Add(new DiplomacyNormalGEVO(D.Cards.Count));
            D.Cards.Add(new DiplomacyAdvancedGEVO(D.Cards.Count));
            D.Cards.Add(new CardGameEffectVO(
                D.Cards.Count,
                "Crystal Mastery",
                Image_Enum.I_crystal_yellow,
                CardType_Enum.GameEffect,
                GameEffect_Enum.AC_CrystalMastery,
                GameEffectDuration_Enum.Turn,
                CNAColor.ColorLightBlue,
                "Any crystal you spend this turn are returned to your Inventory at the end of the turn.",
                true, false, false));
            D.Cards.Add(new CardGameEffectVO(
                D.Cards.Count,
                "Ambush Normal",
                Image_Enum.I_attack,
                CardType_Enum.GameEffect,
                GameEffect_Enum.AC_Ambush01,
                GameEffectDuration_Enum.Turn,
                CNAColor.ColorLightBlue,
                "Add +1 to your first Attack card of any type or +2 to your first Block card of any type, whichever you play first this turn.",
                true, true, false));
            D.Cards.Add(new CardGameEffectVO(
                D.Cards.Count,
                "Ambush Advanced",
                Image_Enum.I_attack,
                CardType_Enum.GameEffect,
                GameEffect_Enum.AC_Ambush02,
                GameEffectDuration_Enum.Turn,
                CNAColor.ColorLightBlue,
                "Add +2 to your first Attack card of any type or +4 to your first Block card of any type, whichever you play first this turn.",
                true, true, false));
            D.Cards.Add(new CardGameEffectVO(
                D.Cards.Count,
                "Mana Storm",
                Image_Enum.I_mana_gold,
                CardType_Enum.GameEffect,
                GameEffect_Enum.AC_ManaStorm,
                GameEffectDuration_Enum.Turn,
                CNAColor.ColorLightBlue,
                "you can use dice showing black or gold as mana of any basic color, regardless of the Round",
                true, false, false));
            D.Cards.Add(new CardGameEffectVO(
                D.Cards.Count,
                "Ice Shield",
                Image_Enum.I_shield,
                CardType_Enum.GameEffect,
                GameEffect_Enum.AC_IceShield,
                GameEffectDuration_Enum.Turn,
                CNAColor.ColorLightBlue,
                "Reduce the Armor of one enemy blocked this way by 3. Armor cannot be reduced below 1.",
                false, true, true));
            D.Cards.Add(new CardGameEffectVO(
                D.Cards.Count,
                "Heroic Tale Normal",
                Image_Enum.I_influencegrey,
                CardType_Enum.GameEffect,
                GameEffect_Enum.AC_HeroicTale01,
                GameEffectDuration_Enum.Turn,
                CNAColor.ColorLightBlue,
                "+1 Reputation for each Unit recruited this turn.",
                true, false, true));
            D.Cards.Add(new CardGameEffectVO(
                D.Cards.Count,
                "Heroic Tale Advanced",
                Image_Enum.I_influencegold,
                CardType_Enum.GameEffect,
                GameEffect_Enum.AC_HeroicTale02,
                GameEffectDuration_Enum.Turn,
                CNAColor.ColorLightBlue,
                "+1 Fame and +1 Reputation for each Unit recruited this turn.",
                true, false, true));
            D.Cards.Add(new CardGameEffectVO(
                D.Cards.Count,
                "Into the Heat Normal",
                Image_Enum.I_attack,
                CardType_Enum.GameEffect,
                GameEffect_Enum.AC_IntoTheHeat01,
                GameEffectDuration_Enum.Turn,
                CNAColor.ColorLightBlue,
                "All of your Units get their Attack and Block values increased by 2 this combat. You cannot assign damage to Units this turn.",
                false, true, true));
            D.Cards.Add(new CardGameEffectVO(
                D.Cards.Count,
                "Into the Heat Advanced",
                Image_Enum.I_attack,
                CardType_Enum.GameEffect,
                GameEffect_Enum.AC_IntoTheHeat02,
                GameEffectDuration_Enum.Turn,
                CNAColor.ColorLightBlue,
                "All of your Units get their Attack and Block values increased by 3 this combat. You cannot assign damage to Units this turn.",
                false, true, true));
            D.Cards.Add(new CardGameEffectVO(
                D.Cards.Count,
                "Steady Tempo Normal",
                Image_Enum.I_boots,
                CardType_Enum.GameEffect,
                GameEffect_Enum.AC_SteadyTempo01,
                GameEffectDuration_Enum.Turn,
                CNAColor.ColorLightBlue,
                "At the end of your turn, instead of putting this card in your discard pile, you may place it on the bottom of your deed deck as long as it is not empty.",
                true, false, false));
            D.Cards.Add(new CardGameEffectVO(
                D.Cards.Count,
                "Steady Tempo Advanced",
                Image_Enum.I_boots,
                CardType_Enum.GameEffect,
                GameEffect_Enum.AC_SteadyTempo02,
                GameEffectDuration_Enum.Turn,
                CNAColor.ColorLightBlue,
                "At the end of your turn, instead of putting this card in your discard pile, you may place it on top of your Deed deck.",
                true, false, false));

            D.Cards.Add(new CardGameEffectVO(
                D.Cards.Count,
                "Frost Bridge Normal",
                Image_Enum.I_boots,
                CardType_Enum.GameEffect,
                GameEffect_Enum.AC_FrostBridge01,
                GameEffectDuration_Enum.Turn,
                CNAColor.ColorLightBlue,
                "The Move cost of swamps is reduced to 1 this turn.",
                true, false, false));
            D.Cards.Add(new CardGameEffectVO(
                D.Cards.Count,
                "Frost Bridge Advanced",
                Image_Enum.I_boots,
                CardType_Enum.GameEffect,
                GameEffect_Enum.AC_FrostBridge02,
                GameEffectDuration_Enum.Turn,
                CNAColor.ColorLightBlue,
                "You are able to travel through lakes, and the Move cost of lakes and swamps is reduced to 1 this turn.",
                true, false, false));
            D.Cards.Add(new CardGameEffectVO(
                D.Cards.Count,
                "Song of Wind Normal",
                Image_Enum.I_boots,
                CardType_Enum.GameEffect,
                GameEffect_Enum.AC_SongOfWind01,
                GameEffectDuration_Enum.Turn,
                CNAColor.ColorLightBlue,
                "The Move cost of plains, deserts and wastelands is reduced by 1, to a minimum of 0 this turn.",
                true, false, true));
            D.Cards.Add(new SongofWindGEVO(D.Cards.Count));
            D.Cards.Add(new CardGameEffectVO(
                D.Cards.Count,
                "Song of Wind Lake",
                Image_Enum.I_boots,
                CardType_Enum.GameEffect,
                GameEffect_Enum.AC_SongOfWind03,
                GameEffectDuration_Enum.Turn,
                CNAColor.ColorLightBlue,
                "you can travel through lakes for Move cost 0 this turn.",
                true, false, false));
            D.Cards.Add(new CardGameEffectVO(
                D.Cards.Count,
                "Path Finding Normal",
                Image_Enum.I_boots,
                CardType_Enum.GameEffect,
                GameEffect_Enum.AC_PathFinding01,
                GameEffectDuration_Enum.Turn,
                CNAColor.ColorLightBlue,
                "The Move cost of all terrains is reduced by 1, to a minimum of 2, this turn.",
                true, false, true));
            D.Cards.Add(new CardGameEffectVO(
                D.Cards.Count,
                "Path Finding Advanced",
                Image_Enum.I_boots,
                CardType_Enum.GameEffect,
                GameEffect_Enum.AC_PathFinding02,
                GameEffectDuration_Enum.Turn,
                CNAColor.ColorLightBlue,
                "The Move cost of all terrains is reduced to 2 this turn.",
                true, false, false));
            D.Cards.Add(new CardGameEffectVO(
                D.Cards.Count,
                "Expose",
                Image_Enum.I_monsterfortified,
                CardType_Enum.GameEffect,
                GameEffect_Enum.CS_Expose,
                GameEffectDuration_Enum.Turn,
                CNAColor.ColorLightBlue,
                "Target enemy loses all fortifications and resistances this combat.",
                false, true, true));
            D.Cards.Add(new CardGameEffectVO(
                D.Cards.Count,
                "Mass Expose Fortification",
                Image_Enum.I_monsterfortified,
                CardType_Enum.GameEffect,
                GameEffect_Enum.CS_MassExpose01,
                GameEffectDuration_Enum.Turn,
                CNAColor.ColorLightBlue,
                "Enemies lose all fortifications this combat.",
                false, true, false));
            D.Cards.Add(new CardGameEffectVO(
                D.Cards.Count,
                "Mass Expose Resistance",
                Image_Enum.I_monsterfortified,
                CardType_Enum.GameEffect,
                GameEffect_Enum.CS_MassExpose02,
                GameEffectDuration_Enum.Turn,
                CNAColor.ColorLightBlue,
                "Enemies lose all resistances this combat.",
                false, true, false));
            D.Cards.Add(new CardGameEffectVO(
                D.Cards.Count,
                "Tremor Single",
                Image_Enum.I_monsterfortified,
                CardType_Enum.GameEffect,
                GameEffect_Enum.CS_Tremor01,
                GameEffectDuration_Enum.Turn,
                CNAColor.ColorLightBlue,
                "Target enemy gets Armor -3. Armor cannot be reduced below 1.",
                false, true, true));
            D.Cards.Add(new CardGameEffectVO(
                D.Cards.Count,
                "Tremor Multi",
                Image_Enum.I_monsterfortified,
                CardType_Enum.GameEffect,
                GameEffect_Enum.CS_Tremor02,
                GameEffectDuration_Enum.Turn,
                CNAColor.ColorLightBlue,
                "All enemies get Armor -2. Armor cannot be reduced below 1.",
                false, true, false));
            D.Cards.Add(new CardGameEffectVO(
                D.Cards.Count,
                "Earthquake Single",
                Image_Enum.I_monsterfortified,
                CardType_Enum.GameEffect,
                GameEffect_Enum.CS_Earthquake01,
                GameEffectDuration_Enum.Turn,
                CNAColor.ColorLightBlue,
                "Target enemy gets Armor -3 (Armor -6 if it is fortified). Armor cannot be reduced below 1.",
                false, true, true));
            D.Cards.Add(new CardGameEffectVO(
                D.Cards.Count,
                "Earthquake Multi",
                Image_Enum.I_monsterfortified,
                CardType_Enum.GameEffect,
                GameEffect_Enum.CS_Earthquake02,
                GameEffectDuration_Enum.Turn,
                CNAColor.ColorLightBlue,
                "All enemies get Armor -2 (Armor -4 if they are fortified). Armor cannot be reduced below 1.",
                false, true, false));
            D.Cards.Add(new CardGameEffectVO(
                D.Cards.Count,
                "Burning Shield",
                Image_Enum.I_shield,
                CardType_Enum.GameEffect,
                GameEffect_Enum.CS_BurningShield,
                GameEffectDuration_Enum.Turn,
                CNAColor.ColorLightBlue,
                "If this card is used as part of a successful Block, you may use it during your Attack phase as Fire Attack 4.",
                false, true, true));
            D.Cards.Add(new CardGameEffectVO(
                D.Cards.Count,
                "Exploding Shield",
                Image_Enum.I_shield,
                CardType_Enum.GameEffect,
                GameEffect_Enum.CS_ExplodingShield,
                GameEffectDuration_Enum.Turn,
                CNAColor.ColorLightBlue,
                "If this card is used as part of a successful Block, destroy the blocked enemy.",
                false, true, false));
            D.Cards.Add(new CardGameEffectVO(
                D.Cards.Count,
                "Chill",
                Image_Enum.I_resistfire,
                CardType_Enum.GameEffect,
                GameEffect_Enum.CS_Chill,
                GameEffectDuration_Enum.Turn,
                CNAColor.ColorLightBlue,
                "If the monster has Fire Resistance, it loses it for the rest of the turn.",
                false, true, true));
            D.Cards.Add(new CardGameEffectVO(
                D.Cards.Count,
                "Lethal Chill",
                Image_Enum.I_monsterarmor,
                CardType_Enum.GameEffect,
                GameEffect_Enum.CS_LethalChill,
                GameEffectDuration_Enum.Turn,
                CNAColor.ColorLightBlue,
                "The monster gets Armor -4 (to a minimum of 1) for the rest of the turn.",
                false, true, true));
            D.Cards.Add(new CardGameEffectVO(
                D.Cards.Count,
                "Demolish",
                Image_Enum.I_monsterarmor,
                CardType_Enum.GameEffect,
                GameEffect_Enum.CS_Demolish,
                GameEffectDuration_Enum.Turn,
                CNAColor.ColorLightBlue,
                "Ignore site fortifications this turn. Enemies get Armor -1 (to a minimum of 1).",
                false, true, false));
            D.Cards.Add(new CardGameEffectVO(
                D.Cards.Count,
                "Disintegrate",
                Image_Enum.I_monsterarmor,
                CardType_Enum.GameEffect,
                GameEffect_Enum.CS_Disintegrate,
                GameEffectDuration_Enum.Turn,
                CNAColor.ColorLightBlue,
                "Enemies get Armor -1 (to a minimum of 1).",
                false, true, false));
            D.Cards.Add(new CardGameEffectVO(
                D.Cards.Count,
                "Call To Arms",
                Image_Enum.I_cardBackRounded,
                CardType_Enum.GameEffect,
                GameEffect_Enum.CS_CallToArms,
                GameEffectDuration_Enum.Turn,
                CNAColor.ColorLightBlue,
                "You may use an ability of one Unit in the Units offer this turn, as if it were one of your recruits. You cannot assign damage to this Unit.",
                true, false, false));
            D.Cards.Add(new CardGameEffectVO(
                D.Cards.Count,
                "Call To Glory",
                Image_Enum.I_cardBackRounded,
                CardType_Enum.GameEffect,
                GameEffect_Enum.CS_CallToGlory,
                GameEffectDuration_Enum.Turn,
                CNAColor.ColorLightBlue,
                "Recruit any one Unit from the Units offer for free. (If you are at your Command limit, you must disband one of your Units first.)",
                true, false, false));
            D.Cards.Add(new CardGameEffectVO(
                D.Cards.Count,
                "Space Bending",
                Image_Enum.I_bootfly,
                CardType_Enum.GameEffect,
                GameEffect_Enum.CS_SpaceBending,
                GameEffectDuration_Enum.Turn,
                CNAColor.ColorLightBlue,
                "This turn, you may move to spaces that are 2 spaces away from you as if they were adjacent. Ignore any spaces you leap over this way. Your movement does not provoke rampaging enemies this turn.",
                true, false, false));
            D.Cards.Add(new CardGameEffectVO(
                D.Cards.Count,
                "Time Bending",
                Image_Enum.I_hourglass,
                CardType_Enum.GameEffect,
                GameEffect_Enum.CS_TimeBending,
                GameEffectDuration_Enum.Turn,
                CNAColor.ColorLightBlue,
                "At the end of your turn, set this card aside for the rest of the Round. Put all other cards you played this turn (not those discarded or thrown away) back in your hand. Skip the draw new cards portion of your end of turn step. Immediately take another turn.",
                true, false, false));
            D.Cards.Add(new CardGameEffectVO(
                D.Cards.Count,
                "Underground Travel",
                Image_Enum.I_bootfly,
                CardType_Enum.GameEffect,
                GameEffect_Enum.CS_UndergroundTravel,
                GameEffectDuration_Enum.Turn,
                CNAColor.ColorLightBlue,
                "Move by up to 3 revealed spaces on the map. You must end your move on a safe space. Moving this way does not provoke rampaging enemies.",
                true, false, false));
            D.Cards.Add(new CardGameEffectVO(
                D.Cards.Count,
                "Underground Attack",
                Image_Enum.I_bootfly,
                CardType_Enum.GameEffect,
                GameEffect_Enum.CS_UndergroundAttack,
                GameEffectDuration_Enum.Turn,
                CNAColor.ColorLightBlue,
                "Move by up to 3 revealed spaces on the map. You must end your move on a fortified space and trigger combat. Moving this way does not provoke rampaging enemies.",
                true, false, false));
            D.Cards.Add(new CardGameEffectVO(
                D.Cards.Count,
                "Underground Fortification",
                Image_Enum.I_attack,
                CardType_Enum.GameEffect,
                GameEffect_Enum.CS_UndergroundAttackFort,
                GameEffectDuration_Enum.Turn,
                CNAColor.ColorLightBlue,
                "Ignore site fortifications. If withdrawing after the combat, return to your original position.",
                false, true, false));
            D.Cards.Add(new CardGameEffectVO(
                D.Cards.Count,
                "Wings of Wind",
                Image_Enum.I_bootfly,
                CardType_Enum.GameEffect,
                GameEffect_Enum.CS_WingsOfWind,
                GameEffectDuration_Enum.Turn,
                CNAColor.ColorLightBlue,
                "When you play this, spend 1-5 Move points and move one revealed space on the map for each. You must end your move in a safe space. Moving this way does not provoke rampaging enemies.",
                true, false, false));
            D.Cards.Add(new WingsofNightGEVO(D.Cards.Count));
            D.Cards.Add(new CardGameEffectVO(
                D.Cards.Count,
                "Meditation",
                Image_Enum.I_attack,
                CardType_Enum.GameEffect,
                GameEffect_Enum.CS_Meditation,
                GameEffectDuration_Enum.Turn,
                CNAColor.ColorLightBlue,
                "+2 to had limit for next turn.",
                true, false, true));
            D.Cards.Add(new CardGameEffectVO(
                D.Cards.Count,
                "Trance",
                Image_Enum.I_attack,
                CardType_Enum.GameEffect,
                GameEffect_Enum.CS_Trance,
                GameEffectDuration_Enum.Turn,
                CNAColor.ColorLightBlue,
                "+4 to had limit for next turn.",
                true, false, true));
            D.Cards.Add(new CardGameEffectVO(
                D.Cards.Count,
                "Courage Used",
                Image_Enum.I_dizzy,
                CardType_Enum.GameEffect,
                GameEffect_Enum.CT_BannerOfCourageUsed,
                GameEffectDuration_Enum.Turn,
                CNAColor.ColorLightBlue,
                "You have already used the Banner of Courage this turn.",
                true, false, true));
            D.Cards.Add(new CardGameEffectVO(
                D.Cards.Count,
                "Banner of Glory",
                Image_Enum.I_unitbanner,
                CardType_Enum.GameEffect,
                GameEffect_Enum.CT_BannerOfGlory,
                GameEffectDuration_Enum.Turn,
                CNAColor.ColorLightBlue,
                "Units you control get armor +1 and +1 to any attacks or blocks the make this turn. Fame +1 for each unit that attacks or blocks this turn.",
                false, true, true));
            D.Cards.Add(new CardGameEffectVO(
                D.Cards.Count,
                "Ruby Ring",
                Image_Enum.I_crystal_red,
                CardType_Enum.GameEffect,
                GameEffect_Enum.CT_RubyRing,
                GameEffectDuration_Enum.Turn,
                CNAColor.ColorLightBlue,
                "Fame +1 for each red Spell you cast this turn.",
                true, false, false));
            D.Cards.Add(new CardGameEffectVO(
                D.Cards.Count,
                "Sapphire Ring",
                Image_Enum.I_crystal_blue,
                CardType_Enum.GameEffect,
                GameEffect_Enum.CT_SapphireRing,
                GameEffectDuration_Enum.Turn,
                CNAColor.ColorLightBlue,
                "Fame +1 for each blue Spell you cast this turn.",
                true, false, false));
            D.Cards.Add(new CardGameEffectVO(
                D.Cards.Count,
                "Diamond Ring",
                Image_Enum.I_crystal_white,
                CardType_Enum.GameEffect,
                GameEffect_Enum.CT_DiamondRing,
                GameEffectDuration_Enum.Turn,
                CNAColor.ColorLightBlue,
                "Fame +1 for each white Spell you cast this turn.",
                true, false, false));
            D.Cards.Add(new CardGameEffectVO(
                D.Cards.Count,
                "Emerald Ring",
                Image_Enum.I_crystal_green,
                CardType_Enum.GameEffect,
                GameEffect_Enum.CT_EmeraldRing,
                GameEffectDuration_Enum.Turn,
                CNAColor.ColorLightBlue,
                "Fame +1 for each green Spell you cast this turn.",
                true, false, false));
            D.Cards.Add(new CardGameEffectVO(
                D.Cards.Count,
                "Sword of Justice Normal",
                Image_Enum.I_attack,
                CardType_Enum.GameEffect,
                GameEffect_Enum.CT_SwordOfJustice01,
                GameEffectDuration_Enum.Turn,
                CNAColor.ColorLightBlue,
                "+3 Attack for each card you discard. +1 Fame for each monster killed.",
                false, true, false));
            D.Cards.Add(new CardGameEffectVO(
                D.Cards.Count,
                "Sword of Justice Advance",
                Image_Enum.I_attack,
                CardType_Enum.GameEffect,
                GameEffect_Enum.CT_SwordOfJustice02,
                GameEffectDuration_Enum.Turn,
                CNAColor.ColorLightBlue,
                "During Attack phase double damage of all physical attacks, Monsters lose physical resistance. +1 Fame for each monster killed.",
                false, true, false));
            D.Cards.Add(new HornOfWrathGEVO(D.Cards.Count));
            D.Cards.Add(new CardGameEffectVO(
                D.Cards.Count,
                "Golden Grail",
                Image_Enum.I_healHand,
                CardType_Enum.GameEffect,
                GameEffect_Enum.CT_GoldenGrail,
                GameEffectDuration_Enum.Turn,
                CNAColor.ColorLightBlue,
                "Draw a card when you heal a wound!",
                true, false, false));
            D.Cards.Add(new CardGameEffectVO(
                D.Cards.Count,
                "Amulet Of Sun",
                Image_Enum.I_sun,
                CardType_Enum.GameEffect,
                GameEffect_Enum.CT_AmuletOfTheSun,
                GameEffectDuration_Enum.Turn,
                CNAColor.ColorLightBlue,
                "You can use gold mana at night, forest move cost 3",
                true, false, false));
            D.Cards.Add(new CardGameEffectVO(
                D.Cards.Count,
                "Amulet Of Darkness",
                Image_Enum.I_moon,
                CardType_Enum.GameEffect,
                GameEffect_Enum.CT_AmuletOfDarkness,
                GameEffectDuration_Enum.Turn,
                CNAColor.ColorLightBlue,
                "You can use black mana in the day, desert move cost 3!",
                true, false, false));
            D.Cards.Add(new CardGameEffectVO(
                D.Cards.Count,
                "Amotep Freezers",
                Image_Enum.I_attack,
                CardType_Enum.GameEffect,
                GameEffect_Enum.CUE_AmotepFreezers,
                GameEffectDuration_Enum.Turn,
                CNAColor.ColorLightBlue,
                "Monster gets -3 Armor an does not attack.",
                false, true, true));
            D.Cards.Add(new CardGameEffectVO(
                D.Cards.Count,
                "Altem Mages Cold Fire",
                Image_Enum.I_attack_coldfire,
                CardType_Enum.GameEffect,
                GameEffect_Enum.CUE_AltemMages01,
                GameEffectDuration_Enum.Turn,
                CNAColor.ColorLightBlue,
                "All attacks become Cold Fire.",
                false, true, false));
            D.Cards.Add(new CardGameEffectVO(
                D.Cards.Count,
                "Altem Guardians Swift",
                Image_Enum.I_monsterswift,
                CardType_Enum.GameEffect,
                GameEffect_Enum.CUE_AltemGuardians01,
                GameEffectDuration_Enum.Turn,
                CNAColor.ColorLightBlue,
                "+8 Block against a monster with Swiftness.",
                false, true, true));
            D.Cards.Add(new CardGameEffectVO(
                D.Cards.Count,
                "Altem Guardians Resist",
                Image_Enum.I_resistphysical,
                CardType_Enum.GameEffect,
                GameEffect_Enum.CUE_AltemGuardians02,
                GameEffectDuration_Enum.Turn,
                CNAColor.ColorLightBlue,
                "All units gain all resistances.",
                false, true, true));
            D.Cards.Add(new RewardsGEVO(D.Cards.Count));
            D.Cards.Add(new CardGameEffectVO(
                D.Cards.Count,
                "Learning Normal",
                Image_Enum.I_influencegrey,
                CardType_Enum.GameEffect,
                GameEffect_Enum.AC_Learning01,
                GameEffectDuration_Enum.Turn,
                CNAColor.ColorLightBlue,
                "Once during this turn, you may pay Influence 6 to gain an Advanced Action card from the Advanced Actions offer to your discard pile.",
                true, false, false));
            D.Cards.Add(new CardGameEffectVO(
                D.Cards.Count,
                "Learning Advanced",
                Image_Enum.I_influencegold,
                CardType_Enum.GameEffect,
                GameEffect_Enum.AC_Learning02,
                GameEffectDuration_Enum.Turn,
                CNAColor.ColorLightBlue,
                "Once during this turn, you may pay Influence 9 to gain an Advanced Action card from the Advanced Actions offer to your hand.",
                true, false, false));
            D.Cards.Add(new ManaStealGEVO(D.Cards.Count));
            D.Cards.Add(new TheRightMomentGEVO(D.Cards.Count));
            D.Cards.Add(new CardGameEffectVO(
                D.Cards.Count,
                "The Right Moment",
                Image_Enum.I_crystal_yellow,
                CardType_Enum.GameEffect,
                GameEffect_Enum.T_TheRightMoment02,
                GameEffectDuration_Enum.Turn,
                CNAColor.ColorLightBlue,
                "You will take another turn after this one.",
                true, false, false));
            D.Cards.Add(new MidnightMeditationGEVO(D.Cards.Count));
            D.Cards.Add(new ManaSearchGEVO(D.Cards.Count, GameEffect_Enum.T_ManaSearch01));
            D.Cards.Add(new ManaSearchGEVO(D.Cards.Count, GameEffect_Enum.T_ManaSearch02));
            D.Cards.Add(new LongNightGEVO(D.Cards.Count));
            D.Cards.Add(new SparingPowerGEVO(D.Cards.Count));
            D.Cards.Add(new CardGameEffectVO(
                D.Cards.Count,
                "Planning",
                Image_Enum.I_cardBackRounded,
                CardType_Enum.GameEffect,
                GameEffect_Enum.T_Planning,
                GameEffectDuration_Enum.Round,
                CNAColor.ColorLightBlue,
                "If you have at least two cards at the end of your turn, +1 to hand limt.",
                true, false, false));
        }


        public void UpdateLocationsGameEffects() {
            D.Cards.Add(new CardGameEffectVO(
                D.Cards.Count,
                "Keep",
                Image_Enum.SH_Keep,
                CardType_Enum.GameEffect,
                GameEffect_Enum.SH_Keep,
                GameEffectDuration_Enum.Turn,
                CNAColor.ColorLightBlue,
                "Monsters from the Keep are fortified during this battle! -1 Reputation for attack the Keep. +1 hand limit per keep you control, when you are adjacent ot on the keep.",
                false, true, false));
            D.Cards.Add(new CardGameEffectVO(
                D.Cards.Count,
                "Keep",
                Image_Enum.SH_Keep,
                CardType_Enum.GameEffect,
                GameEffect_Enum.SH_Keep_Own,
                GameEffectDuration_Enum.Game,
                CNAColor.ColorLightBlue,
                "+1 hand limit for each keep you control.",
                true, false, false));
            D.Cards.Add(new CardGameEffectVO(
                D.Cards.Count,
                "Mage Tower",
                Image_Enum.SH_MageTower,
                CardType_Enum.GameEffect,
                GameEffect_Enum.SH_MageTower,
                GameEffectDuration_Enum.Turn,
                CNAColor.ColorLightBlue,
                "Monsters from the Mage Tower are fortified during this battle! -1 Reputation for attack the Mage Tower.  Reward +1 Spell.",
                false, true, false));
            D.Cards.Add(new CardGameEffectVO(
                D.Cards.Count,
                "Blue City",
                Image_Enum.SH_City_Blue,
                CardType_Enum.GameEffect,
                GameEffect_Enum.SH_City_Blue,
                GameEffectDuration_Enum.Turn,
                CNAColor.ColorLightBlue,
                "Monsters from the City are fortified during this battle! -1 Reputation for attack the City.  +2 Cold Damage, +2 Fire Damage, + 1 Cold Fire Damage to each monster that allready has the element of attack.",
                false, true, false));
            D.Cards.Add(new CardGameEffectVO(
                D.Cards.Count,
                "Green City",
                Image_Enum.SH_City_Green,
                CardType_Enum.GameEffect,
                GameEffect_Enum.SH_City_Green,
                GameEffectDuration_Enum.Turn,
                CNAColor.ColorLightBlue,
                "Monsters from the City are fortified during this battle! -1 Reputation for attack the City.  Each monster with physical damage gets poison added.",
                false, true, false));
            D.Cards.Add(new CardGameEffectVO(
                D.Cards.Count,
                "Red City",
                Image_Enum.SH_City_Red,
                CardType_Enum.GameEffect,
                GameEffect_Enum.SH_City_Red,
                GameEffectDuration_Enum.Turn,
                CNAColor.ColorLightBlue,
                "Monsters from the City are fortified during this battle! -1 Reputation for attack the City.  Each monster with physical damage gets brutal added.",
                false, true, false));
            D.Cards.Add(new CardGameEffectVO(
                D.Cards.Count,
                "White City",
                Image_Enum.SH_City_White,
                CardType_Enum.GameEffect,
                GameEffect_Enum.SH_City_White,
                GameEffectDuration_Enum.Turn,
                CNAColor.ColorLightBlue,
                "Monsters from the City are fortified during this battle! -1 Reputation for attack the City. Each monster gets +1 armor.",
                false, true, false));
            D.Cards.Add(new CardGameEffectVO(
                D.Cards.Count,
                "City Adjacent",
                Image_Enum.SH_Keep,
                CardType_Enum.GameEffect,
                GameEffect_Enum.SH_City_Own,
                GameEffectDuration_Enum.Game,
                CNAColor.ColorLightBlue,
                "+1 hand limit if you have at least 1 token in an adjacent city, +2 if you are the leader the city.",
                true, false, false));
            D.Cards.Add(new CardGameEffectVO(
                D.Cards.Count,
                "Blue City",
                Image_Enum.SH_Keep,
                CardType_Enum.GameEffect,
                GameEffect_Enum.SH_City_Blue_Own,
                GameEffectDuration_Enum.Game,
                CNAColor.ColorLightBlue,
                "+1 hand limit if you have at least 1 token in an adjacent city, +2 if you are the leader the city. +1 Influence foreach token of your on the city. You can learn spells here for 7 influence and 1 mana of spell color.",
                true, false, false));
            D.Cards.Add(new CardGameEffectVO(
                D.Cards.Count,
                "Red City",
                Image_Enum.SH_Keep,
                CardType_Enum.GameEffect,
                GameEffect_Enum.SH_City_Red_Own,
                GameEffectDuration_Enum.Game,
                CNAColor.ColorLightBlue,
                "+1 hand limit if you have at least 1 token in an adjacent city, +2 if you are the leader the city. +1 Influence foreach token of your on the city. You can buy Artifacts here for 12 influence.",
                true, false, false));
            D.Cards.Add(new CardGameEffectVO(
                D.Cards.Count,
                "Green City",
                Image_Enum.SH_Keep,
                CardType_Enum.GameEffect,
                GameEffect_Enum.SH_City_Green_Own,
                GameEffectDuration_Enum.Game,
                CNAColor.ColorLightBlue,
                "+1 hand limit if you have at least 1 token in an adjacent city, +2 if you are the leader the city. +1 Influence foreach token of your on the city. You can learn Advanced Actions here for 6 influence.",
                true, false, false));
            D.Cards.Add(new CardGameEffectVO(
                D.Cards.Count,
                "White City",
                Image_Enum.SH_Keep,
                CardType_Enum.GameEffect,
                GameEffect_Enum.SH_City_White_Own,
                GameEffectDuration_Enum.Game,
                CNAColor.ColorLightBlue,
                "+1 hand limit if you have at least 1 token in an adjacent city, +2 if you are the leader the city. +1 Influence foreach token of your on the city. You can train any Unit here and add an elite unit for 2 influence.",
                true, false, false));
            D.Cards.Add(new CardGameEffectVO(
                D.Cards.Count,
                "Blue Crystal Mine",
                Image_Enum.SH_CrystalMines_Blue,
                CardType_Enum.GameEffect,
                GameEffect_Enum.SH_CrystalMines_Blue,
                GameEffectDuration_Enum.Game,
                CNAColor.ColorLightBlue,
                "If you end your turn on the crystal mine you will receive +1 Blue Crystal.",
                true, false, false));
            D.Cards.Add(new CardGameEffectVO(
                D.Cards.Count,
                "Red Crystal Mine",
                Image_Enum.SH_CrystalMines_Red,
                CardType_Enum.GameEffect,
                GameEffect_Enum.SH_CrystalMines_Red,
                GameEffectDuration_Enum.Game,
                CNAColor.ColorLightBlue,
                "If you end your turn on the crystal mine you will receive +1 Red Crystal.",
                true, false, false));
            D.Cards.Add(new CardGameEffectVO(
                D.Cards.Count,
                "Green Crystal Mine",
                Image_Enum.SH_CrystalMines_Green,
                CardType_Enum.GameEffect,
                GameEffect_Enum.SH_CrystalMines_Green,
                GameEffectDuration_Enum.Game,
                CNAColor.ColorLightBlue,
                "If you end your turn on the crystal mine you will receive +1 Green Crystal.",
                true, false, false));
            D.Cards.Add(new CardGameEffectVO(
                D.Cards.Count,
                "White Crystal Mine",
                Image_Enum.SH_CrystalMines_White,
                CardType_Enum.GameEffect,
                GameEffect_Enum.SH_CrystalMines_White,
                GameEffectDuration_Enum.Game,
                CNAColor.ColorLightBlue,
                "If you end your turn on the crystal mine you will receive +1 White Crystal.",
                true, false, false));
            D.Cards.Add(new MagicGladeGEVO(D.Cards.Count));
            D.Cards.Add(new CardGameEffectVO(
                D.Cards.Count,
                "Marauding Orcs",
                Image_Enum.SH_MaraudingOrcs,
                CardType_Enum.GameEffect,
                GameEffect_Enum.SH_MaraudingOrcs,
                GameEffectDuration_Enum.Turn,
                CNAColor.ColorLightBlue,
                "+1 Rep when they are defeated.",
                false, false, false));
            D.Cards.Add(new CardGameEffectVO(
                D.Cards.Count,
                "Draconum",
                Image_Enum.SH_Draconum,
                CardType_Enum.GameEffect,
                GameEffect_Enum.SH_Draconum,
                GameEffectDuration_Enum.Turn,
                CNAColor.ColorLightBlue,
                "+2 Rep when they are defeated.",
                false, false, false));
            D.Cards.Add(new CardGameEffectVO(
                D.Cards.Count,
                "Monster Den",
                Image_Enum.SH_MonsterDen,
                CardType_Enum.GameEffect,
                GameEffect_Enum.SH_MonsterDen,
                GameEffectDuration_Enum.Turn,
                CNAColor.ColorLightBlue,
                "Adventure: Fight 1 brown monster. Reward +2 Random Crystals.",
                false, false, false));
            D.Cards.Add(new CardGameEffectVO(
                D.Cards.Count,
                "Spawning Ground",
                Image_Enum.SH_SpawningGround,
                CardType_Enum.GameEffect,
                GameEffect_Enum.SH_SpawningGround,
                GameEffectDuration_Enum.Turn,
                CNAColor.ColorLightBlue,
                "Adventure: Fight 2 brown monster. Reward +3 Random Crystals, +1 Artifact.",
                false, false, false));
            D.Cards.Add(new CardGameEffectVO(
                D.Cards.Count,
                "Monastery",
                Image_Enum.SH_Monastery,
                CardType_Enum.GameEffect,
                GameEffect_Enum.SH_Monastery,
                GameEffectDuration_Enum.Turn,
                CNAColor.ColorLightBlue,
                "Recruiting: units.\nTraining: cost 6 influence.\nHealing: cost 2 influence.\nAdventure: Fight Violet monster, -3 rep, No Units. Reward +1 Artifact.",
                false, true, false));
            D.Cards.Add(new CardGameEffectVO(
                D.Cards.Count,
                "Village",
                Image_Enum.SH_Village,
                CardType_Enum.GameEffect,
                GameEffect_Enum.SH_Village,
                GameEffectDuration_Enum.Game,
                CNAColor.ColorLightBlue,
                "Recruiting: units.\nHealing: cost 3 influence.\nRaid: Start of turn, -1 rep, +2 cards.",
                false, false, false));
            D.Cards.Add(new CardGameEffectVO(
                D.Cards.Count,
                "StartShrine",
                Image_Enum.SH_StartShrine,
                CardType_Enum.GameEffect,
                GameEffect_Enum.SH_StartShrine,
                GameEffectDuration_Enum.Game,
                CNAColor.ColorLightBlue,
                "Starting location for Avatars",
                false, false, false));
            D.Cards.Add(new CardGameEffectVO(
                D.Cards.Count,
                "Ancient Ruins",
                Image_Enum.SH_AncientRuins,
                CardType_Enum.GameEffect,
                GameEffect_Enum.SH_AncientRuins,
                GameEffectDuration_Enum.Turn,
                CNAColor.ColorLightBlue,
                "Adventure: Perform the adventure on the Yellow token. If successful take your reward.",
                false, false, false));
            D.Cards.Add(new CardGameEffectVO(
                D.Cards.Count,
                "Dungeon",
                Image_Enum.SH_Dungeon,
                CardType_Enum.GameEffect,
                GameEffect_Enum.SH_Dungeon,
                GameEffectDuration_Enum.Turn,
                CNAColor.ColorLightBlue,
                "Adventure: Fight 1 Brown monster, Night rules Apply, No Units. Reward +1 Artifact.",
                false, true, false));
            D.Cards.Add(new CardGameEffectVO(
                D.Cards.Count,
                "Tomb",
                Image_Enum.SH_Tomb,
                CardType_Enum.GameEffect,
                GameEffect_Enum.SH_Tomb,
                GameEffectDuration_Enum.Turn,
                CNAColor.ColorLightBlue,
                "Adventure: Fight 1 Red monster, Night rules Apply, No Units. Reward +1 Artifact, +1 Spell.",
                false, true, false));
        }
    }
}
