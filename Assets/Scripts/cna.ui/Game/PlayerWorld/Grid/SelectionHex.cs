using cna.poo;
using TMPro;
using UnityEngine;

namespace cna.ui {
    public class SelectionHex : MonoBehaviour {
        protected enum SH_IMAGE_ENUM {
            NA,
            MOVE,
            ATTACK,
            VIEW,
            FLY,
            NONE
        }
        private Color White = new Color(1f, 1f, 1f, 1f);
        private Color Illegal_Move = new Color(1f, 0f, 0f, 1f);
        private Color Default = new Color(.3f, .3f, .3f, 1f);
        private Color Sufficient_Move = new Color(0f, 1f, 0f, 1f);
        private Color Insufficient_Move = new Color(1f, .73f, 0f, 1f);


        [SerializeField] private SpriteRenderer HexImage;
        [SerializeField] private Grid MainGrid;
        [SerializeField] private Camera WorldCamera;
        [SerializeField] private GameObject GridMovementCost;
        [SerializeField] private TextMeshPro MoveVal;
        [SerializeField] private GameObject AttackImage;
        [SerializeField] private GameObject MoveImage;
        [SerializeField] private GameObject ViewImage;
        [SerializeField] private GameObject FlyImage;
        [SerializeField] private Vector3 mouseInput;
        //[SerializeField] private Vector3 worldPoint;
        //[SerializeField] private Vector3 gridWorldPosition;

        //[SerializeField] private V2IntVO avatarPosition;
        //[SerializeField] private V2IntVO gridPosition;
        //[SerializeField] private int distance;

        private HexItemDetail old;

        public void UpdateUI(HexItemDetail d) {
            if (d.Terrain != Image_Enum.NA) {
                if (!d.Equals(old)) {
                    old = d;
                    //gridWorldPosition = d.GridWorldPosition;
                    //gridPosition = d.GridPosition;
                    //worldPoint = d.WorldPoint;
                    //avatarPosition = d.PlayerLocation;
                    //distance = d.Distance;

                    //GridMovementCost.transform.position = d.GridWorldPosition;
                    if (!d.ShiftDown && (d.isTurn || d.LocalPlayer.PlayerTurnPhase != TurnPhase_Enum.Move)) {
                        if (d.IsFlight) {
                            Flight(d);
                        } else if (d.IsSpaceBending) {
                            SpaceBending(d);
                        } else if (d.IsUndergroundTravel) {
                            UndergroundTravel(d);
                        } else if (d.IsUndergroundAttack) {
                            UndergroundAttack(d);
                        } else if (d.IsWingsOfWind) {
                            WingsOfWind(d);
                        } else {
                            if (d.IsPlayerAdjacent) {
                                if (d.IsLegalMovement) {
                                    if (d.TriggerCombat) {
                                        SetImage(SH_IMAGE_ENUM.ATTACK);
                                    } else {
                                        SetImage(SH_IMAGE_ENUM.MOVE);
                                    }
                                    if (d.PlayerCostMet) {
                                        HexImage.color = Sufficient_Move;
                                    } else {
                                        HexImage.color = Insufficient_Move;
                                    }
                                    MoveVal.text = "" + d.PlayerMovementCost;
                                    GridMovementCost.SetActive(true);
                                } else {
                                    SetImage(SH_IMAGE_ENUM.VIEW);
                                    HexImage.color = Illegal_Move;
                                }
                            } else {
                                SetImage(SH_IMAGE_ENUM.VIEW);
                                HexImage.color = Default;
                            }
                        }
                    } else {
                        SetImage(SH_IMAGE_ENUM.VIEW);
                        HexImage.color = Default;
                    }
                    transform.position = d.GridWorldPosition;
                    Show(true);
                }
            } else {
                Show(false);
            }
        }

        public void Flight(HexItemDetail d) {
            if (d.Distance == 1) {
                if (d.IsLegalMovement && !d.TriggerCombatNoRamp) {
                    SetImage(SH_IMAGE_ENUM.FLY);
                    HexImage.color = Sufficient_Move;
                    MoveVal.text = "0";
                    GridMovementCost.SetActive(true);
                } else {
                    SetImage(SH_IMAGE_ENUM.VIEW);
                    HexImage.color = Illegal_Move;
                }
            } else if (d.Distance == 2) {
                if (d.IsLegalMovement && !d.TriggerCombatNoRamp) {
                    SetImage(SH_IMAGE_ENUM.FLY);
                    MoveVal.text = "2";
                    if (d.LocalPlayer.Movement >= 2) {
                        HexImage.color = Sufficient_Move;
                    } else {
                        HexImage.color = Insufficient_Move;
                    }
                    GridMovementCost.SetActive(true);
                } else {
                    SetImage(SH_IMAGE_ENUM.VIEW);
                    HexImage.color = Illegal_Move;
                }
            } else {
                SetImage(SH_IMAGE_ENUM.VIEW);
                HexImage.color = Default;
            }
        }

        public void UndergroundTravel(HexItemDetail d) {
            if (d.Distance <= 3) {
                if (d.IsLegalMovement && !d.TriggerCombatNoRamp) {
                    SetImage(SH_IMAGE_ENUM.FLY);
                    HexImage.color = Sufficient_Move;
                    MoveVal.text = "0";
                    GridMovementCost.SetActive(true);
                } else {
                    SetImage(SH_IMAGE_ENUM.VIEW);
                    HexImage.color = Illegal_Move;
                }
            } else {
                SetImage(SH_IMAGE_ENUM.VIEW);
                HexImage.color = Default;
            }
        }

        public void UndergroundAttack(HexItemDetail d) {
            if (d.Distance <= 3) {
                if (d.IsLegalMovement && d.TriggerCombatNoRamp && d.IsSiteFortified) {
                    SetImage(SH_IMAGE_ENUM.ATTACK);
                    HexImage.color = Sufficient_Move;
                    MoveVal.text = "0";
                    GridMovementCost.SetActive(true);
                } else {
                    SetImage(SH_IMAGE_ENUM.VIEW);
                    HexImage.color = Illegal_Move;
                }
            } else {
                SetImage(SH_IMAGE_ENUM.VIEW);
                HexImage.color = Default;
            }
        }
        public void WingsOfWind(HexItemDetail d) {
            if (d.Distance <= 5) {
                if (d.IsLegalMovement && !d.TriggerCombatNoRamp) {
                    SetImage(SH_IMAGE_ENUM.FLY);
                    MoveVal.text = "" + d.Distance;
                    if (d.LocalPlayer.Movement >= d.Distance) {
                        HexImage.color = Sufficient_Move;
                    } else {
                        HexImage.color = Insufficient_Move;
                    }
                    GridMovementCost.SetActive(true);
                } else {
                    SetImage(SH_IMAGE_ENUM.VIEW);
                    HexImage.color = Illegal_Move;
                }
            } else {
                SetImage(SH_IMAGE_ENUM.VIEW);
                HexImage.color = Default;
            }
        }
        public void SpaceBending(HexItemDetail d) {
            if (d.Distance <= 2) {
                if (d.IsLegalMovement && !d.TriggerCombatNoRamp) {
                    SetImage(SH_IMAGE_ENUM.FLY);
                    HexImage.color = Sufficient_Move;
                    MoveVal.text = "0";
                    GridMovementCost.SetActive(true);
                } else {
                    SetImage(SH_IMAGE_ENUM.VIEW);
                    HexImage.color = Illegal_Move;
                }
            } else {
                SetImage(SH_IMAGE_ENUM.VIEW);
                HexImage.color = Default;
            }
        }

        public void Show(bool show) {
            HexImage.gameObject.SetActive(show);
            if (!show) {
                GridMovementCost.SetActive(false);
            }
        }


        protected void SetImage(SH_IMAGE_ENUM img) {
            GridMovementCost.SetActive(false);
            AttackImage.SetActive(false);
            MoveImage.SetActive(false);
            ViewImage.SetActive(false);
            FlyImage.SetActive(false);
            MoveVal.color = White;
            MoveVal.gameObject.SetActive(false);
            switch (img) {
                case SH_IMAGE_ENUM.ATTACK: {
                    MoveVal.color = Illegal_Move;
                    MoveVal.gameObject.SetActive(true);
                    GridMovementCost.SetActive(true);
                    AttackImage.SetActive(true);
                    break;
                }
                case SH_IMAGE_ENUM.MOVE: {
                    MoveVal.gameObject.SetActive(true);
                    GridMovementCost.SetActive(true);
                    MoveImage.SetActive(true);
                    break;
                }
                case SH_IMAGE_ENUM.FLY: {
                    MoveVal.gameObject.SetActive(true);
                    GridMovementCost.SetActive(true);
                    FlyImage.SetActive(true);
                    break;
                }
                case SH_IMAGE_ENUM.VIEW: {
                    GridMovementCost.SetActive(true);
                    ViewImage.SetActive(true);
                    break;
                }
                default: {
                    break;
                }
            }
        }
    }
}
