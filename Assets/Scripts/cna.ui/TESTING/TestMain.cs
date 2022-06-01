using UnityEngine;

namespace cna.ui {
    public class TestMain : MonoBehaviour {

        [SerializeField] private int seed01 = 100;

        [SerializeField] private int seed02 = 100;

        void Start() {

            UnityEngine.Random.InitState(seed01);

            System.Random r1 = new System.Random(seed01);
            System.Random r2 = new System.Random(seed02);


        }

    }
}
