using System.Collections;
using cna.poo;
using UnityEngine;

namespace cna.ui {
    public class UIUtil {


        public static IEnumerator ShakeCO(Transform t, Vector3 originalPos) {
            float xdiff = 5f;
            for (int i = 0; i < 4; i++) {
                xdiff *= -1;
                t.localPosition = new Vector3(originalPos.x + xdiff, originalPos.y, originalPos.z);
                yield return new WaitForSeconds(.05f);
            }
            t.localPosition = originalPos;
        }

        public static Color getColor(CardColor_Enum cardColor) {
            switch (cardColor) {
                case CardColor_Enum.Blue: {
                    return new Color(0f, .5f, 1f, 1f);
                }
                case CardColor_Enum.Green: {
                    return new Color(.2f, .8f, .2f, 1f);
                }
                case CardColor_Enum.Red: {
                    return new Color(1f, .2f, .2f, 1f);
                }
                case CardColor_Enum.White: {
                    return new Color(1f, 1f, 1f, 1f);
                }
                case CardColor_Enum.Black: {
                    return new Color(.3f, .3f, .3f, 1f);
                }
                case CardColor_Enum.Orange: {
                    return new Color(.72f, .48f, 0f, 1f);
                }
                default: {
                    return new Color(0.8666667f, 0.8470588f, 0.7490196f, 1f);
                }
            }
        }
        public static Color getColor(Crystal_Enum cardColor) {
            switch (cardColor) {
                case Crystal_Enum.Blue: {
                    return new Color(0f, .5f, 1f, 1f);
                }
                case Crystal_Enum.Green: {
                    return new Color(.2f, .8f, .2f, 1f);
                }
                case Crystal_Enum.Red: {
                    return new Color(1f, .2f, .2f, 1f);
                }
                case Crystal_Enum.White: {
                    return new Color(1f, 1f, 1f, 1f);
                }
                default: {
                    return new Color(0.8666667f, 0.8470588f, 0.7490196f, 1f);
                }
            }
        }
    }
}
