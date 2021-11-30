using UnityEngine;
using System;

namespace TestField {
    public class Wall : MonoBehaviour {
        public Action<int> WallCollided;
        
        public int ID;
        
        public void OnCollisionEnter2D(Collision2D collision) {
            WallCollided?.Invoke(ID);
        }
    }
}
