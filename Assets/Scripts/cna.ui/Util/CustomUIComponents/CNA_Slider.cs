using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace cna.ui {
    public class CNA_Slider : MonoBehaviour {
        [SerializeField] private Slider sliderControl;
        [SerializeField] private TextMeshProUGUI valueText;

        public float Value {
            get => sliderControl.value;
            set => sliderControl.value = value;
        }

        public float MinValue {
            get => sliderControl.minValue;
            set {
                if (sliderControl.minValue != value) {
                    sliderControl.minValue = value;
                    if (sliderControl.minValue > sliderControl.value) {
                        sliderControl.value = sliderControl.minValue;
                    }
                }
            }
        }
        public float MaxValue {
            get => sliderControl.maxValue;
            set {
                if (sliderControl.maxValue != value) {
                    sliderControl.maxValue = value;
                    if (sliderControl.maxValue < sliderControl.value) {
                        sliderControl.value = sliderControl.maxValue;
                    }
                }
            }
        }

        private void Start() {
            valueText.text = sliderControl.value.ToString();
            sliderControl.onValueChanged.AddListener(OnValueChangeCallback);

        }

        private void OnValueChangeCallback(float value) {
            valueText.text = value.ToString();
        }

        public void Setup(int min, int max, int value, UnityAction<float> callback) {
            MinValue = min;
            MaxValue = max;
            Value = value;
            sliderControl.onValueChanged.AddListener(callback);
        }
    }
}
