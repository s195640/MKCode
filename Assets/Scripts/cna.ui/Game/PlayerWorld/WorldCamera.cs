using UnityEngine;

namespace cna.ui {
    public class WorldCamera : MonoBehaviour {
        private Vector2 ZOOM = new Vector2(1f, 10f);
        private bool Drag = false;
        private Vector3 Difference;
        private Vector3 Origin;
        private Camera _cam;
        private Camera Cam {
            get { if (_cam == null) { _cam = GetComponent<Camera>(); } return _cam; }
        }

        public void UpdateUI(HexItemDetail h) {
            updateCameraPosition(h.WorldPoint);
            rightClick(h.AvatarWorldPosition);
            scrollWheel();
        }

        public void updateCameraPosition(Vector3 worldPoint) {
            if (Input.GetMouseButton(0)) {
                Difference = worldPoint - transform.localPosition;
                if (!Drag) {
                    Drag = true;
                    Origin = worldPoint;
                }
                if (Drag) {
                    transform.localPosition = Origin - Difference;
                }
            } else {
                Drag = false;
            }
        }

        public void rightClick(Vector3 avatarWorldPosition) {
            if (Input.GetMouseButton(1)) {
                transform.localPosition = new Vector3(avatarWorldPosition.x, avatarWorldPosition.y, -1f);
                GetComponent<Camera>().orthographicSize = 4f;
            }
        }

        public void scrollWheel() {
            if (Input.mouseScrollDelta.y != 0.0) {
                if ((Input.mouseScrollDelta.y < 0.0 && Cam.orthographicSize < ZOOM.y) ||
                    (Input.mouseScrollDelta.y > 0.0 && Cam.orthographicSize > ZOOM.x)) {
                    Cam.orthographicSize += (Input.mouseScrollDelta.y * -0.5f);
                    if (Cam.orthographicSize < ZOOM.x) {
                        Cam.orthographicSize = ZOOM.x;
                    }
                    if (Cam.orthographicSize > ZOOM.y) {
                        Cam.orthographicSize = ZOOM.y;
                    }
                }
            }
        }
    }
}
