using cna.poo;
using UnityEngine;

namespace cna.ui {
    public abstract class ICNA_Base : MonoBehaviour {
        public virtual void SetupUI(string buttonText, Color buttonColor, Image_Enum buttonImageid = Image_Enum.NA, bool isActive = true) { }
    }
}
