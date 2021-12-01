using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace TestField {
    public class SampleGame : MiniGameBase {
        public Text MessageDisplayer;
        public GameObject Ball;
        public Camera Camera;

        void Start() {
            // 侦听所有Wall的碰撞事件，当小球碰撞到墙后引发GameCompleted事件，并简单地将墙的ID作为返回码
            foreach (var wall in transform.Find("Walls").GetComponentsInChildren<Wall>()) {
                wall.WallCollided += (wallID) => {
                    OnGameCompleted(wallID);
                };
            }
        }
        void Update() {
            // 游戏暂停时不检测做任何操作
            if (!_gameAcitved) {
                return;
            }

            // 如果当前模式为VN模式，按下鼠标左键时显示/跳过/隐藏对话
            if (Input.GetMouseButtonDown(0)) {
                // 如果当前没有对话，则显示一条对话
                if (!_hasDialog) {
                    VNSimulation.Current.ShowDialog("阿草", "小伙伴你好！建议收到！", "阿草-通常");
                    _hasDialog = true;
                }
                else {
                    // 否则如果对话的打字机动画未完成，此次点击视为跳过动画；否则视为隐藏对话
                    if (!VNSimulation.Current.IsDialogTextAnimationCompleted()) {
                        VNSimulation.Current.SkipDialogTextAnimation();
                    }
                    else {
                        VNSimulation.Current.HideDialog();
                        _hasDialog = false;
                    }
                }
            }

            // 没什么特殊操作，就是AD移动小球
            if (Input.GetKey(KeyCode.A)) {
                _targetPos = Ball.transform.position;
                _targetPos.x -= 1.0f;
            }
            else if (Input.GetKey(KeyCode.D)) {
                _targetPos = Ball.transform.position;
                _targetPos.x += 1.0f;
            }
            if (Vector3.SqrMagnitude(_targetPos - Ball.transform.position) >= 0.0001) {
                Ball.transform.position = Vector3.Lerp(Ball.transform.position, _targetPos, 10 * Time.deltaTime);
            }
        }

        public override void LoadGame(Action onCompleted, MiniGameLoadMode loadMode) {
            _gameMode = loadMode;
            // 仅作为简单示例，从VN载入时小球为蓝色，从营地载入时小球为红色
            switch (_gameMode) {
                case MiniGameLoadMode.FromVisualNovel:
                    Ball.transform.Find("Sprite").GetComponent<SpriteRenderer>().color = Color.blue;
                    MessageDisplayer.text = "小游戏已从VN中载入，小球当前为蓝色";
                    break;
                case MiniGameLoadMode.FromCampsite:
                    Ball.transform.Find("Sprite").GetComponent<SpriteRenderer>().color = Color.red;
                    MessageDisplayer.text = "小游戏已从营地中载入，小球当前为红色";
                    break;
            }
            // 重置小球位置
            Ball.transform.position = Vector2.zero;
            _targetPos = Vector2.zero;
            onCompleted?.Invoke();
            _gameAcitved = true;
        }
        public override void UnloadGame(Action onCompleted) {
            _gameAcitved = false;
            // 将小球变为白色并重置小球位置
            Ball.transform.Find("Sprite").GetComponent<SpriteRenderer>().color = Color.white;
            Ball.transform.position = Vector2.zero;
            _targetPos = Vector2.zero;
            MessageDisplayer.text = "小游戏已关闭，小球当前为白色";
            onCompleted?.Invoke();
        }

        private MiniGameLoadMode _gameMode;
        private Vector3 _targetPos;
        private bool _gameAcitved;
        private bool _hasDialog;
    }
}
