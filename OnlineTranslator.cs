using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// Create date: 2020-11-10
///  Provide a method to translate English to Chinese or vice versa
/// </summary>
namespace ILearnPlayer
{
    class OnlineTranslator
    {
        String m_URL = @"http://fanyi.youdao.com/openapi.do?keyfrom=<keyfrom>&key=<key>&type=data&doctype=<doctype>&version=1.1&q=";

        public String getTranslation(String word)
        {
            if (word == "") return"";
            String translated = "";
            
            translated = getTranslationString(word);
            return translated;

        }
        private String getTranslationString(String newWord)
        {
            String transLated = newWord +"\r\n";
            Dictionary<String, String> dic = new Dictionary<String, String>();
            string url = "https://openapi.youdao.com/api";
            string q = newWord;
            string appKey = "395ebd28afe2e00a";
            string appSecret = "SLrDfTOGjzTJ2gkbDOVQnHYSEiZlADhW";
            string salt = DateTime.Now.Millisecond.ToString();
            dic.Add("from", "en");
            dic.Add("to", "zh-CHS");
            dic.Add("signType", "v3");
            TimeSpan ts = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc));
            long millis = (long)ts.TotalMilliseconds;
            string curtime = Convert.ToString(millis / 1000);
            dic.Add("curtime", curtime);
            string signStr = appKey + Truncate(q) + salt + curtime + appSecret; ;
            string sign = ComputeHash(signStr, new SHA256CryptoServiceProvider());
            dic.Add("q", System.Web.HttpUtility.UrlEncode(q));
            dic.Add("appKey", appKey);
            dic.Add("salt", salt);
            dic.Add("sign", sign);
            List<String> explains = Post(url, dic);
            for(int i = 0; i < explains.Count; i++)
            {
                transLated += explains[i] + "\r\n";
            }

            return transLated;

        }
        protected static string ComputeHash(string input, HashAlgorithm algorithm)
        {
            Byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            Byte[] hashedBytes = algorithm.ComputeHash(inputBytes);
            return BitConverter.ToString(hashedBytes).Replace("-", "");
        }

        protected static List<String> Post(string url, Dictionary<String, String> dic)
        {
            string result = "";
            List<String> explains = new List<string>();
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            StringBuilder builder = new StringBuilder();
            int i = 0;
            foreach (var item in dic)
            {
                if (i > 0)
                    builder.Append("&");
                builder.AppendFormat("{0}={1}", item.Key, item.Value);
                i++;
            }
            byte[] data = Encoding.UTF8.GetBytes(builder.ToString());
            req.ContentLength = data.Length;
            using (Stream reqStream = req.GetRequestStream())
            {
                reqStream.Write(data, 0, data.Length);
                reqStream.Close();
            }
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            if (resp.ContentType.ToLower().Equals("audio/mp3"))
            {
                SaveBinaryFile(resp, "合成的音频存储路径");
            }
            else
            {
                Stream stream = resp.GetResponseStream();
                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                {
                    result = reader.ReadToEnd();
                    //result.Replace("\\\"","\"");
                    youDaoTransObject ydtobj = JsonConvert.DeserializeObject<youDaoTransObject>(result);
                    if (ydtobj.basic != null)
                    {
                        explains = ydtobj.basic.explains;
                    }
                }
                Console.WriteLine(result);
            }
            return explains;
        }

        protected static string Truncate(string q)
        {
            if (q == null)
            {
                return null;
            }
            int len = q.Length;
            return len <= 20 ? q : (q.Substring(0, 10) + len + q.Substring(len - 10, 10));
        }

        private static bool SaveBinaryFile(WebResponse response, string FileName)
        {
            string FilePath = FileName + DateTime.Now.Millisecond.ToString() + ".mp3";
            bool Value = true;
            byte[] buffer = new byte[1024];

            try
            {
                if (File.Exists(FilePath))
                    File.Delete(FilePath);
                Stream outStream = System.IO.File.Create(FilePath);
                Stream inStream = response.GetResponseStream();

                int l;
                do
                {
                    l = inStream.Read(buffer, 0, buffer.Length);
                    if (l > 0)
                        outStream.Write(buffer, 0, l);
                }
                while (l > 0);

                outStream.Close();
                inStream.Close();
            }
            catch
            {
                Value = false;
            }
            return Value;
        }

    }


    public class web
    {
        public List<String> value { get; set; }
        public string key { get; set; }
    }

    public class Dict
    {
        public string url { get; set; }
    }

    public class Webdict
    {
        public string url { get; set; }
    }
    public class Basic
    {
        public List<String> exam_type { get; set; }
        public string us_phonetic { get; set; }
        public string phonetic { get; set; }
        public string uk_phonetic { get; set; }
        public string uk_speech { get; set; }
        public List<String> explains { get; set; }
        public string us_speech { get; set; }
    }

    public class youDaoTransObject
    {
        public List<String> returnPhrase { get; set; }
        public string query { get; set; }
        public string errorCode { get; set; }
        public string l { get; set; }
        public string tSpeakUrl { get; set; }
        public List<web> web { get; set; }
        public string requestId { get; set; }
        public List<String> translation { get; set; }
        public Dict dict { get; set; }
        public Webdict webdict { get; set; }
        public Basic basic { get; set; }
        public string isWord { get; set; }
        public string speakUrl { get; set; }
    }
}
