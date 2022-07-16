using System;
using cna.poo;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace cna.ui {
    public class ChatWindow : MonoBehaviour {
        [Header("GameObjects")]
        [SerializeField] private Transform largeWindowContent;
        [SerializeField] private TMP_InputField sendTextInput;
        [SerializeField] private ScrollRect scrollbar;
        [SerializeField] private GameObject largeChatWindow;
        [SerializeField] private GameObject smallChatWindow;
        [SerializeField] private TextMeshProUGUI smallChatButtonText;

        [Header("Prefab")]
        [SerializeField] private ChatItem chatItem_Pref;

        private int unread = 0;


        public void Start() {
            sendTextInput.onSubmit.AddListener(delegate { OnSubmit(); });
        }

        public void Update() {
            if (D.ChatQueue.Count > 0) {
                addNewChatItem(D.ChatQueue.Dequeue());
            }
        }

        public void UpdateUI() { }


        public void OnSubmit() {
            DateTime dt = DateTime.Now;
            long t = dt.Ticks;
            string msg = sendTextInput.text;
            sendTextInput.text = "";
            sendTextInput.ActivateInputField();
            ChatItemData cid = new ChatItemData(D.Connector.Player.Name, msg, t);
            addNewChatItem(cid);
            D.C.Send_Chat(cid);
        }

        public void addNewChatItem(ChatItemData cid) {
            DateTime dt = new DateTime(cid.Time);
            string timeStr = dt.ToString("hh:mm tt");
            ChatItem go = Instantiate(chatItem_Pref, new Vector3(0, 0, 0), Quaternion.identity);
            go.UpdateUI(cid.UserName, cid.Message, timeStr);
            go.transform.SetParent(largeWindowContent, false);
            Canvas.ForceUpdateCanvases();
            scrollbar.verticalNormalizedPosition = 0f;
            if (smallChatWindow.activeSelf) {
                unread++;
                smallChatButtonText.text = "Chat (" + unread + ")";
            }
        }

        public void LargeWindow_OnClick() {
            largeChatWindow.SetActive(false);
            smallChatWindow.SetActive(true);
            unread = 0;
            smallChatButtonText.text = "Chat (" + unread + ")";
        }

        public void SmallWindow_OnClick() {
            largeChatWindow.SetActive(true);
            smallChatWindow.SetActive(false);
        }
    }
}
