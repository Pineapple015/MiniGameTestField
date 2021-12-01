using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace TestField {
    public class VNSimulation : MonoBehaviour {
        public static VNSimulation Current => _current;
        public Text TextDisplayer;

        void Awake() {
            _current = this;
        }

        /// <summary>
        /// 判断对话文字的打字机动画是否播放完毕
        /// </summary>
        /// <returns></returns>
        public bool IsDialogTextAnimationCompleted() {
            return _isDialogTextAnimationCompleted;
        }
        /// <summary>
        /// 显示一条对话（无角色名与立绘图片）
        /// </summary>
        /// <param name="dialogText">对话内容</param>
        public void ShowDialog(string dialogText) {
            ShowDialog(null, dialogText, null);
        }
        /// <summary>
        /// 显示一条对话，指定角色名、对话内容（无立绘图片）
        /// </summary>
        /// <param name="roleName">角色名</param>
        /// <param name="dialogText">对话内容</param>
        public void ShowDialog(string roleName, string dialogText) {
            ShowDialog(roleName, dialogText, null);
        }
        /// <summary>
        /// 显示一条对话，指定角色名、对话内容与立绘图片
        /// </summary>
        /// <param name="roleName">角色名</param>
        /// <param name="dialogText">对话内容</param>
        /// <param name="roleImage">立绘名</param>
        public void ShowDialog(string roleName, string dialogText, string roleImage) {
            _skipAnimationCalled = false;
            if (_dialogTextShownCoroutine != null) {
                StopCoroutine(_dialogTextShownCoroutine);
            }
            _dialogTextShownCoroutine = StartCoroutine(ShowDialogHelper($"[{roleImage}]{roleName}: {dialogText}"));
        }
        /// <summary>
        /// 隐藏当前显示的对话
        /// </summary>
        public void HideDialog() {
            TextDisplayer.text = "";
        }
        /// <summary>
        /// 跳过当前的对话的打字机动画，直接显示所有文字
        /// </summary>
        public void SkipDialogTextAnimation() {
            _skipAnimationCalled = true;
        }

        private static VNSimulation _current;
        private bool _isDialogTextAnimationCompleted;
        private Coroutine _dialogTextShownCoroutine;
        private bool _skipAnimationCalled;
        private IEnumerator ShowDialogHelper(string text) {
            _isDialogTextAnimationCompleted = false;
            for (int i = 0; i <= text.Length; i++) {
                if (_skipAnimationCalled) {
                    break;
                }
                TextDisplayer.text = text.Substring(0, i);
                yield return new WaitForSeconds(0.1f);
            }
            TextDisplayer.text = text;
            _isDialogTextAnimationCompleted = true;
        }
    }
}
