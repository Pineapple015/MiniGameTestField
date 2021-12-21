using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace MiniGame {
    public static class RankingManager {
        /// <summary>
        /// 该字段指示存档列表的容量，为7，分别是五位姑娘、阿草与一个魂
        /// </summary>
        public static readonly int MaxRecords = 7;

        /// <summary>
        ///  保存小游戏排行榜数据至指定文件
        /// </summary>
        /// <param name="rankingRecords">排行数据</param>
        /// <param name="gameName">游戏名</param>
        /// <returns></returns>
        public static bool SaveRankingRecords(List<RankingRecord> rankingRecords, string gameName) {
            return SaveRankingRecords<RankingRecord>(rankingRecords, gameName);
        }
        /// <summary>
        /// 保存小游戏排行榜数据至指定文件方法的泛型版本
        /// </summary>
        /// <typeparam name="TRankingRecord">排名记录类型，该类型需继承自RankingRecord</typeparam>
        /// <param name="rankingRecords">排行数据</param>
        /// <param name="gameName">游戏名</param>
        /// <returns></returns>
        public static bool SaveRankingRecords<TRankingRecord>(List<TRankingRecord> rankingRecords, string gameName) where TRankingRecord : RankingRecord {
            try {
                // 新开一个列表储存排序后的列表，避免对原列表的副作用
                var sortedList = new List<TRankingRecord>();
                sortedList.AddRange(rankingRecords);
                // 输出的文件的具体相对路径，文件名直接使用游戏名+.json
                string outputFile = Path.Combine("", "", $"{gameName}.json");
                // 格式化为Json后写入到输出文件中
                using (StreamWriter writer = new StreamWriter(outputFile)) {
                    sortedList.Sort();
                    sortedList.Reverse();
                    sortedList.Take(MaxRecords);
                    // 对排名进行排序后反转，取前MaxRecords个后写入到文件中
                    writer.Write(JsonConvert.SerializeObject(sortedList));
                }
                return true;
            }
            catch (Exception) {
                throw; // Debug阶段就直接抛出了方便查错，Release的时候再修改
            }
        }

        /// <summary>
        /// 载入指定小游戏的排行信息
        /// </summary>
        /// <param name="gameName">要获取排行信息的游戏名</param>
        /// <returns>读取结果；若文件不存在或文件损坏，返回null</returns>
        public static List<RankingRecord> LoadRankingRecords(string gameName) {
            return LoadRankingRecords<RankingRecord>(gameName);
        }
        /// <summary>
        /// 载入指定小游戏的排行信息方法的泛型版本
        /// </summary>
        /// <typeparam name="TRankingRecord">排名记录类型，该类型需继承自RankingRecord</typeparam>
        /// <param name="gameName">要获取排行信息的游戏名</param>
        /// <returns>读取结果；若文件不存在或文件损坏，返回null</returns>
        public static List<TRankingRecord> LoadRankingRecords<TRankingRecord>(string gameName) where TRankingRecord : RankingRecord {
            try {
                // 读取文件的具体相对路径，文件名直接使用游戏名+.json
                string inputFile = Path.Combine("", "", $"{gameName}.json");
                using (StreamReader reader = new StreamReader(inputFile)) {
                    string jsonText = reader.ReadToEnd();
                    var result = JsonConvert.DeserializeObject<List<TRankingRecord>>(jsonText);
                    return new List<TRankingRecord>(result.Take(MaxRecords));
                }
            }
            catch (Exception) {
                throw; // Debug阶段就直接抛出了方便查错，Release的时候再修改
            }
        }
    }
}
