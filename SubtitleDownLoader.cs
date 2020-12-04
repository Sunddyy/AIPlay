using Ivony.Html;
using Ivony.Html.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// Function:Download subtitle files from web
/// The first web is http://www.zimuku.la/
/// Refer to http://www.360doc.com/content/20/0817/17/71178898_930801275.shtml
/// </summary>
namespace ILearnPlayer
{
    class SubtitleDownLoader
    {

        // 获取对应网页的 HTML Dom TREE
        public static IHtmlDocument GetHtmlDocument(string url)
        {
            IHtmlDocument document;
            try
            {
                document = new JumonyParser().LoadDocument(url);
            }
            catch
            {
                document = null;
            }
            return document;
        }
        public static string EntryPoint;
        //<a href="/detail/145499.html" target="_blank" title="战士 第2季第09集【YYeTs字幕组 简繁英双语字幕】Warrior.2019.S02E09.Enter.the.Dragon.720p/1080p.REPACK.AMZN.WEB-DL.DDP5.1.H.264-NTb"><b>战士 第2季第09集【YYeTs字幕组 简繁英双语字幕】Warrior.2019.S02E09.Enter.the.Dragon.720p/1080p.REPACK.AMZN.WEB-DL.DDP5.1.H.264-NTb</b></a>
        public static List<SubtitleFileItem> GetSunbtitle(int page)
        {
            List<SubtitleFileItem> result = new List<SubtitleFileItem>();

            string url = EntryPoint;

            IHtmlDocument document = GetHtmlDocument(url);
            if (document == null)
                return result;


            //var aLinks = document.Find("a");//获取所有的meta标签
            //foreach (var aLink in aLinks)
            //{
            //    if (aLink.Attribute("name").Value() == "keywords")
            //    {
            //        var name = aLink.Attribute("content").Value();//无疆,无疆最新章节,无疆全文阅读
            //    }
            //}

            List<IHtmlElement> listsTable = document.Find("table").ToList();
            List<IHtmlElement> listsSub = listsTable[0].Find("a").ToList();// find <a href...

            for (int i = 0; i < listsSub.Count; i++)
            {

                SubtitleFileItem item = new SubtitleFileItem();

                IHtmlElement subItem = listsSub[i];
                item.url = subItem.Attribute("href").AttributeValue;
                item.title = subItem.Attribute("title").AttributeValue;
                //item.lang = subItem.Attribute("href").AttributeValue;
                //item.url = subItem.Attribute("href").AttributeValue;
                //item.url = subItem.Attribute("href").AttributeValue;
                //item.url = subItem.Attribute("href").AttributeValue;



                result.Add(item);
            }
            return result;
        }

    }

   

    // used for the subtitle file (item)
    public class SubtitleFileItem
    {
        /// <summary>
        /// title
        /// </summary>
        public String title { set; get; }
        /// <summary>
        /// Language
        /// </summary>
        public String lang { set; get; }
        /// <summary>
        /// ranking
        /// </summary>
        public int rank { set; get; }
        /// <summary>
        /// download times
        /// </summary>
        public int dldTimes { set; get; }

        /// <summary>
        /// Updloader
        /// </summary>
        public String uploader { set; get; }
        /// <summary>
        /// upload time
        /// </summary>
        public String upldTime { set; get; }

        /// <summary>
        /// URL
        /// </summary>
        public String url { set; get; }
    }
}
