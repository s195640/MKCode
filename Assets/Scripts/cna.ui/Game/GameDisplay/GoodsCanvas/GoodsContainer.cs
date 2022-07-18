using System;
using System.Collections;
using System.Collections.Generic;
using cna.poo;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements.Experimental;

namespace cna.ui {
    public class GoodsContainer : MonoBehaviour {

        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private TextMeshProUGUI greenText;
        [SerializeField] private TextMeshProUGUI redText;
        [SerializeField] private AddressableImage image;
        [SerializeField] private RectTransform forScale;

        [Header("Transparent")]
        [SerializeField] private Image imageColor;



        public void SetupUI(int val, Image_Enum i, Action<GoodsContainer> finished) {
            if (val >= 0) {
                greenText.gameObject.SetActive(true);
                redText.gameObject.SetActive(false);
                if (val == 0) {
                    greenText.text = "0";
                } else {
                    greenText.text = "+" + val;
                }
            } else {
                greenText.gameObject.SetActive(false);
                redText.gameObject.SetActive(true);
                redText.text = "" + val;
            }
            image.ImageEnum = i;
            StartCoroutine(Lerp(finished));
        }



        float lerpDuration = 3;
        //  Move
        Vector2 move_start = new Vector2(.5f, 1f);
        Vector2 move_end = new Vector2(.9f, 1f);
        //  Scale
        Vector3 scale_start = Vector3.one;
        Vector3 scale_end = .1f * Vector3.one;
        //  Trans
        float transA_start = 1f;
        float transA_end = .1f;

        IEnumerator Lerp(Action<GoodsContainer> finished) {
            float timeElapsed = 0;
            while (timeElapsed < lerpDuration) {
                float t = timeElapsed / lerpDuration;
                float move_t = EaseOut(t);
                rectTransform.anchorMin = Vector2.Lerp(move_start, move_end, move_t);
                rectTransform.anchorMax = Vector2.Lerp(move_start, move_end, move_t);
                rectTransform.pivot = Vector2.Lerp(move_start, move_end, move_t);

                float scale_t = t;
                forScale.localScale = Vector3.Lerp(scale_start, scale_end, scale_t);

                float transA_t = EaseOut(t);
                float transA_lerp = Mathf.Lerp(transA_start, transA_end, transA_t);
                setTrans(transA_lerp);

                timeElapsed += Time.deltaTime;
                yield return null;
            }
            finished(this);
        }

        public void setTrans(float v) {
            imageColor.color = new Color(imageColor.color.r, imageColor.color.g, imageColor.color.b, v);
            greenText.color = new Color(greenText.color.r, greenText.color.g, greenText.color.b, v);
            redText.color = new Color(redText.color.r, redText.color.g, redText.color.b, v);
        }


        static float Flip(float x) {
            return 1 - x;
        }
        public static float EaseIn(float t) {
            return Square(t);
        }
        public static float Square(float t) {
            return t * t;
        }
        public static float EaseOut(float t) {
            return Flip(Square(Flip(t)));
        }
    }
}
