namespace TestField {
    public static class GlobalSettings {
        /// <summary>
        /// 全局音量
        /// </summary>
        public static int VolumnOfGlobal => _volumnOfGlobal;
        /// <summary>
        /// 音乐音量
        /// </summary>
        public static int VolumnOfMusic => _volumnOfMusic;
        /// <summary>
        /// 效果音量
        /// </summary>
        public static int VolumnOfFX => _VolumnOfFX;

        /// <summary>
        /// 设置全局音量，范围为[1,100]
        /// </summary>
        /// <param name="value"></param>
        public static void SetVolumnOfGlobal(int value) {
            _volumnOfGlobal = GetCorrectedValue(value);
        }
        /// <summary>
        /// 设置音乐音量，范围为[1,100]
        /// </summary>
        /// <param name="value"></param>
        public static void SetVolumnOfMusic(int value) {
            _volumnOfMusic = GetCorrectedValue(value);
        }
        /// <summary>
        /// 设置效果音音量，范围为[1,100]
        /// </summary>
        /// <param name="value"></param>
        public static void SetVolumnOfFX(int value) {
            _VolumnOfFX = GetCorrectedValue(value);
        }
        /// <summary>
        /// 保存设置
        /// </summary>
        public static void Save() {
            _savedVolumnOfGlobal = _volumnOfGlobal;
            _savedVolumnOfMusic = _volumnOfMusic;
            _savedVolumnOfFX = _VolumnOfFX;
        }
        /// <summary>
        /// 读取设置
        /// </summary>
        public static void Load() {
            _volumnOfGlobal = _savedVolumnOfGlobal;
            _volumnOfMusic = _savedVolumnOfMusic;
            _VolumnOfFX = _savedVolumnOfFX;
        }

        private static int _volumnOfGlobal;
        private static int _volumnOfMusic;
        private static int _VolumnOfFX;
        private static int _savedVolumnOfGlobal;
        private static int _savedVolumnOfMusic;
        private static int _savedVolumnOfFX;
        private static int GetCorrectedValue(int value) {
            if (value < 0) {
                return 0;
            }
            else if (value > 100) {
                return 100;
            }
            return value;
        }
    }
}
