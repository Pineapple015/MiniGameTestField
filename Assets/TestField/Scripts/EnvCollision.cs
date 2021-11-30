using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestField {
    public class EnvCollision : MonoBehaviour {
        private void OnCollisionEnter2D(Collision2D collision) {
            print($"<color=#FF4040>主环境碰撞检测被触发，请调整 '{collision.gameObject.name}' 所属Layer</color>");
        }
    }
}
