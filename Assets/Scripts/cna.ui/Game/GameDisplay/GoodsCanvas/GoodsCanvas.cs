using System;
using System.Collections;
using System.Collections.Generic;
using cna.poo;
using UnityEngine;
using UnityEngine.UIElements;

namespace cna.ui {
    public class GoodsCanvas : MonoBehaviour {

        private class goodVO {
            public int value;
            public Image_Enum image;

            public goodVO(int value, Image_Enum image) {
                this.value = value;
                this.image = image;
            }
        }

        [SerializeField] private GoodsContainer goodsContainer_Prefab;
        [SerializeField] private List<GoodsContainer> goods = new List<GoodsContainer>();
        private Queue<goodVO> goodsQueue = new Queue<goodVO>();

        ////  FOR TESTING
        //private void Update() {
        //    if (Input.GetKeyDown(KeyCode.Space)) {
        //        Add(-1, Image_Enum.I_boots);
        //    }
        //}

        public void UpdateUI() { }
        private void Start() {
            InvokeRepeating("ProcessGoods", 0f, 0.5f);
        }

        public void ProcessGoods() {
            if (goodsQueue.Count > 0) {
                goodVO g = goodsQueue.Dequeue();
                GoodsContainer good = Instantiate(goodsContainer_Prefab, Vector3.zero, Quaternion.identity);
                good.transform.SetParent(transform);
                good.transform.localScale = Vector3.one;
                RectTransform rt = good.GetComponent<RectTransform>();
                rt.anchoredPosition = Vector2.zero;
                goods.Remove(good);
                good.SetupUI(g.value, g.image, Remove);
            }
        }

        public void Clear() {
            goods.ForEach(g => Destroy(g.gameObject));
            goods.Clear();
            goodsQueue.Clear();
        }


        public void Add(int value, Image_Enum image) {
            goodsQueue.Enqueue(new goodVO(value, image));

            //GoodsContainer good = Instantiate(goodsContainer_Prefab, Vector3.zero, Quaternion.identity);
            //good.transform.SetParent(transform);
            //good.transform.localScale = Vector3.one;
            //RectTransform rt = good.GetComponent<RectTransform>();
            //rt.anchoredPosition = Vector2.zero;
            //goods.Remove(good);
            //good.SetupUI(value, image, Remove);
        }

        private void Remove(GoodsContainer g) {
            goods.Remove(g);
            Destroy(g.gameObject);
        }
    }
}
