using cna.poo;
using UnityEngine;

namespace cna.ui {
    public class PlayerInfoMagicPrefab : MonoBehaviour {
        [SerializeField] private GameObject Physical;
        [SerializeField] private GameObject Fire;
        [SerializeField] private GameObject Cold;
        [SerializeField] private GameObject Coldfire;

        public void UpdateUI(AttackData a) {
            Physical.SetActive(a.Physical > 0);
            Fire.SetActive(a.Fire > 0);
            Cold.SetActive(a.Cold > 0);
            Coldfire.SetActive(a.ColdFire > 0);
        }
    }
}
