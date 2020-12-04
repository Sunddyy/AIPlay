using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
/// <summary>
/// from https://blog.csdn.net/m0_37591671/article/details/78283087?utm_medium=distribute.pc_relevant_download.none-task-blog-blogcommendfrombaidu-2.nonecase&depth_1-utm_source=distribute.pc_relevant_download.none-task-blog-blogcommendfrombaidu-2.nonecas
///  and https://blog.csdn.net/ys999666/article/details/109238964
/// </summary>
namespace ILearnPlayer
{
    //定义一个StrModel的类，用于接受从srt文件读取的文件格式

    public class SubtitleBlock
    {
        public int Index { get; private set; }
        public string Text { get; private set; }
        public double Start { get; private set; }
        public double End { get; private set; }
        public double Length { get; private set; }
        public SubtitleBlock(int index, double start, double end, string text)
        {
            this.Index = index;
            this.Start = start;
            this.End = end;
            this.Length = end - start;
            this.Text = text;
        }
        public SubtitleBlock()
        {
        }

    }

    //解析字幕文件
    public class SubtitleReader
    {
        //public static DataTableList<SubtitleBlock> ParseSrtFile(string srtPath)
        public static DataTable ParseSrtFile(string srtPath)
        {
            //var sbList = new List<SubtitleBlock>();
            var srtDatbl = new DataTable();
            if (!File.Exists(srtPath)) return null;
            using (FileStream fs = new FileStream(srtPath, FileMode.Open))
            {
                using (StreamReader sr = new StreamReader(fs, Encoding.Default))
                {
                    StringBuilder sb = new StringBuilder();
                    string line = sr.ReadToEnd();
                    //sbList = ParseSubtitles(line);
                    srtDatbl = ParseSrtText(line);

                }

            }
            return srtDatbl;
        }
        static public List<SubtitleBlock> ParseSubtitles(string content)
        {
            var subtitles = new List<SubtitleBlock>();
            var regex = new Regex(@"(?<index>\d*\s*)\r*\n*(?<start>\d*:\d*:\d*,\d*)\s*-->\s*(?<end>\d*:\d*:\d*,\d*)\s*\r*\n*(?<content>.*)\r*\n*(?<content2>.*)\r*\n*");
            var matches = regex.Matches(content);
            foreach (Match match in matches)
            {
                var groups = match.Groups;
                int ind = int.Parse(groups["index"].Value);
                TimeSpan fromtime, totime;
                TimeSpan.TryParse(groups["start"].Value.Replace(',', '.'), out fromtime);
                TimeSpan.TryParse(groups["end"].Value.Replace(',', '.'), out totime);
                string contenttext = groups["content"].Value;
                subtitles.Add(new SubtitleBlock(ind, fromtime.TotalSeconds, totime.TotalSeconds, contenttext));
            }
            return subtitles;
        }

        static public DataTable ParseSrtText(string content)
        {
            DataTable dt = new DataTable();
            SubtitleBlock sblk = new SubtitleBlock();
            System.Reflection.PropertyInfo[] p = sblk.GetType().GetProperties();
            foreach (System.Reflection.PropertyInfo pi in p)
            {
                dt.Columns.Add(pi.Name, System.Type.GetType(pi.PropertyType.ToString()));
            }
            var regex = new Regex(@"(?<index>\d*\s*)\r*\n*(?<start>\d*:\d*:\d*,\d*)\s*-->\s*(?<end>\d*:\d*:\d*,\d*)\s*\r*\n*(?<content>.*)\r*\n*(?<content2>.*)\r*\n*");
            var matches = regex.Matches(content);
            foreach (Match match in matches)
            {
                var groups = match.Groups;
                int ind = int.Parse(groups["index"].Value);
                TimeSpan fromtime, totime;
                TimeSpan.TryParse(groups["start"].Value.Replace(',', '.'), out fromtime);
                TimeSpan.TryParse(groups["end"].Value.Replace(',', '.'), out totime);
                string contenttext = groups["content"].Value;
                dt.Rows.Add(ind, contenttext, fromtime.TotalSeconds, totime.TotalSeconds, totime.TotalSeconds- fromtime.TotalSeconds);
            }
            return dt;
        }


        //System.Reflection.PropertyInfo[] p = _list[0].GetType().GetProperties();
        //    foreach (System.Reflection.PropertyInfo pi in p)
        //     {
        //       dt.Columns.Add(pi.Name, System.Type.GetType(pi.PropertyType.ToString()));
        //     }

    }
}
