using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MiniGame {
    public class RankingRecord : IComparable<RankingRecord> {
        public string Name { get; set; }
        public int Score { get; set; }

        /// <summary>
        /// IComparable下的CompareTo方法，用于比较排名，该方法为虚方法，可以在派生类中重写
        /// </summary>
        public virtual int CompareTo(RankingRecord other) {
            return Score.CompareTo(other.Score);
        }
    }
}
