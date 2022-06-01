using System.Collections;
using TMPro;
using UnityEngine;

namespace cna.ui {
    public class CNA_Input : UnityEngine.MonoBehaviour {

        private string originalPhText;
        private Color originalPhColor;
        private FontWeight originalPhFontWeight;
        private Vector3 originalPhPosition;

        [Header("GameObjects")]
        [SerializeField] private TMP_InputField input;
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private TextMeshProUGUI placeholder;

        [Header("Error Setup")]
        [SerializeField] private string errorText;
        [SerializeField] private Color errorColor = Color.red;
        [SerializeField] private FontWeight errorFontWeight = FontWeight.Bold;

        public string InputValue { get { return input.text; } set { input.text = value; } }


        private void Start() {
            originalPhText = placeholder.text;
            originalPhColor = placeholder.color;
            originalPhFontWeight = placeholder.fontWeight;
            originalPhPosition = placeholder.transform.position;

            if (string.IsNullOrWhiteSpace(errorText)) {
                errorText = "** " + originalPhText + " **";
            }
        }

        public bool ValidateNotEmpty() {
            if (text.text.Length <= 1) {
                TriggerValidator();
                return false;
            }
            return true;
        }

        public void TriggerValidator() {
            StartCoroutine(invalidTextFieldCO());
        }

        private IEnumerator invalidTextFieldCO() {
            setPlaceHolderToErrorState();
            StartCoroutine(UIUtil.ShakeCO(placeholder.transform, originalPhPosition));
            yield return new WaitForSeconds(3f);
            resetPlaceHolder();
        }

        public void setPlaceHolderToErrorState() {
            placeholder.text = errorText;
            placeholder.color = errorColor;
            placeholder.fontWeight = errorFontWeight;
        }

        public void resetPlaceHolder() {
            placeholder.text = originalPhText;
            placeholder.color = originalPhColor;
            placeholder.fontWeight = originalPhFontWeight;
            placeholder.transform.position = originalPhPosition;
        }
    }
}