
using UnityEngine;

namespace My_Editor
{
    public enum Axis
    {
        Vertical,
        Horizontal,
    }
    public enum EdgeVertical
    {
        Up,
        Down,
    }
    public enum EdgeHorizontal
    {
        Left,
        Right,
    }

    public class UIAlign : MonoBehaviour
    {
        [SerializeField]
        public Axis direction;

        [SerializeField]
        public EdgeVertical startCorner_V;

        [SerializeField]
        public EdgeHorizontal startCorner_H;

        [ContextMenu("RealignChildren")]
        public void ReAlignChildren()
        {
            int childNum = transform.childCount;

            RectTransform[] childTransforms = GetComponentsInChildren<RectTransform>();

            float curDis = 0;

            float lastElement = 0;

            // 获取方向

            //if (direction == Axis.Horizontal)
            //{
            //    if (startCorner_H == EdgeHorizontal.Left)
            //    {
            //        Vector2 target = Vector2.left;
            //    }
            //    else
            //    {
            //        Vector2 target = Vector2.down;
            //    }
            //}

            for (int i = 0; i < childTransforms.Length; i++)
            {
                childTransforms[i].anchorMin = Vector2.up;
                childTransforms[i].anchorMax = Vector2.one;

                curDis += lastElement / 2 + childTransforms[i].sizeDelta.y / 2;

                lastElement = childTransforms[i].sizeDelta.y;

                childTransforms[i].anchoredPosition = new Vector2(0, curDis);
            }
        }

        [ContextMenu("ContextMenu1")]
        public void ContextMenuFunc1()
        {
            Debug.Log("ContextMenu1");
        }

        public int a = 0;
        public string b = "";

        [ContextMenuItem("add testName", "ContextMenuFunc2")]
        public string testName = "";
        private void ContextMenuFunc2()
        {
            testName = "testName";
        }
    }

}

