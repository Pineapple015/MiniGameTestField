using System.Collections.Generic;

namespace TestField {
    public static class AchievementManager {
        /// <summary>
        /// 查询成就是否解锁
        /// </summary>
        /// <param name="apiName">成就名</param>
        /// <returns>是否解锁</returns>
        public static bool IsUnlocked(string apiName) {
            if (_achiStatus.ContainsKey(apiName)) {
                return _achiStatus[apiName];
            }
            return false;
        }
        /// <summary>
        /// 解锁成就
        /// </summary>
        /// <param name="apiName">成就名</param>
        public static void Unlock(string apiName) {
            _achiStatus[apiName] = true;
        }
        /// <summary>
        /// 锁定成就
        /// </summary>
        /// <param name="apiName">成就名</param>
        public static void Lock(string apiName) {
            _achiStatus[apiName] = false;
        }
        /// <summary>
        /// 获取成就进度
        /// </summary>
        /// <param name="apiName">成就名</param>
        /// <returns></returns>
        public static int GetProgress(string apiName) {
            if (_achiProgress.ContainsKey(apiName)) {
                return _achiProgress[apiName];
            }
            return 0;
        }
        /// <summary>
        /// 设置成就进度
        /// </summary>
        /// <param name="apiName">成就名</param>
        /// <param name="value">成就进度</param>
        public static void SetProgress(string apiName, int value) {
            _achiProgress[apiName] = value;
        }

        #region 下面是测试用的字段，实际中会使用其他实现，请勿关注具体实现
        private static readonly Dictionary<string, bool> _achiStatus = new Dictionary<string, bool>();
        private static readonly Dictionary<string, int> _achiProgress = new Dictionary<string, int>();
        #endregion
    }
}
