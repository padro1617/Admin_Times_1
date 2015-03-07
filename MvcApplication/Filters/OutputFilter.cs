using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Text.RegularExpressions;
using System.IO;
using System.Threading;
using Tools;
using ShowAlone.Model;

namespace MvcApplication
{
    public class OutputFilter : IHttpModule
    {
        private HttpApplication _contextApplication;
        public void Init(HttpApplication context) {
            _contextApplication = context;
            //绑定事件，在对此请求处理过程全部结束后进行过滤操作
            context.ReleaseRequestState += ContextReleaseRequestState;
        }

        public void Dispose() {
            _contextApplication.Dispose();
            _contextApplication = null;
        }


        private static readonly string OutputFilterState = System.Configuration.ConfigurationManager.AppSettings["OutputFilter"];
        /// <summary>
        /// 对此HTTP请求处理的过程全部结束
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void ContextReleaseRequestState(object sender, EventArgs e) {
            //判断Web.config中的过滤开关
            if (OutputFilterState == null || OutputFilterState == "0")
                return;

            var application = (HttpApplication)sender;
            //当服务器成功返回网页
            if (application.Response.StatusCode != 200)
                return;
            //获取方式必须为GET及内容类型为HTML
            if (application.Request.HttpMethod.ToUpper() != "GET" || application.Response.ContentType.ToLower() != "text/html") {
                return;
            }

            if (application.Request.Url != null && application.Request.Url.ToString().ToLower().Contains("manager/")) {
                return;
            }

            //装配过滤器
            application.Response.Filter = new RawFilter(application.Response.Filter);
        }
    }

    /// <summary>
    /// 定义原始数据EventArgs,便于在截获完整数据后，由事件传递数据
    /// </summary>
    public class RawDataEventArgs : EventArgs
    {
        public RawDataEventArgs(string sourceCode) {
            SourceCode = sourceCode;
        }

        public string SourceCode { get; set; }
    }



    //自定义过滤器
    public class RawFilter : Stream
    {
        private static readonly string WordType = System.Configuration.ConfigurationManager.AppSettings["OutputFilterWordType"];
        private static readonly string FilterSaveLog = System.Configuration.ConfigurationManager.AppSettings["FilterSaveLog"];

        private static FastFilter fastFilter;
        private readonly Stream _responseStream;

        /// <summary>
        /// 当原始数据采集成功后激发。
        /// </summary>
        //public event EventHandler<RawDataEventArgs> OnRawDataRecordedEvent;

        public RawFilter(Stream inputStream) {
            _responseStream = inputStream;
            if (fastFilter == null && !string.IsNullOrEmpty(WordType)) {
                fastFilter = new FastFilter();
                IList<FilterInfo> filters = ShowAlone.BLL.Filters.GetFilterInfoList(WordType);
                foreach (var words in filters) {
                    fastFilter.AddKey(words.Word, words.Replacement);
                }
            }
            //_responseHtml = new StringBuilder();
        }

        //实现Stream 虚方法Filter Overrides
        public override bool CanRead {
            get {
                return true;
            }
        }

        public override bool CanSeek {
            get {
                return true;
            }
        }

        public override bool CanWrite {
            get {
                return true;
            }
        }

        public override void Close() {
            _responseStream.Close();
        }

        public override void Flush() {
            _responseStream.Flush();
        }

        public override long Length {
            get {
                return 0;
            }
        }

        public override long Position { get; set; }

        public override int Read(byte[] buffer, int offset, int count) {
            return _responseStream.Read(buffer, offset, count);
        }

        public override long Seek(long offset, SeekOrigin origin) {
            return _responseStream.Seek(offset, origin);
        }

        public override void SetLength(long length) {
            _responseStream.SetLength(length);
        }

        private readonly IList<byte[]> _buffers = new List<byte[]>();

        //在HttpResponse输入内容的时候，一定会调用此方法输入数据，所以要在此方法内截获数据
        public override void Write(byte[] buffer, int offset, int count) {
            //将页面内容数组添加到记录数据中
            _buffers.Add(buffer);

            var e = Encoding.GetEncoding(HttpContext.Current.Response.Charset);
            string strBuffer = e.GetString(buffer, offset, count);

            //页面输出已经完毕，截获内容
            int allCount = 0;
            foreach (var b in _buffers) {
                allCount += b.Length;
            }
            var allbuffer = new byte[allCount];

            int lastOffset = 0;
            foreach (var b in _buffers) {
                b.CopyTo(allbuffer, lastOffset);
                lastOffset += b.Length;
            }

            e = Encoding.GetEncoding(HttpContext.Current.Response.Charset);
            //整个页面的Html文本内容
            string text = e.GetString(allbuffer, 0, allCount);

            //替换之前必须填充--构造函数填充
            if (!string.IsNullOrEmpty(WordType) && !string.IsNullOrEmpty(text) && fastFilter.HasBadWord(text)) {
                string words = string.Empty;
                text = fastFilter.Replace(text, ref words);
                if (FilterSaveLog != null && FilterSaveLog.ToString() == "True") {
                    YIZPublic.BLL.Filter.SaveLog(HttpContext.Current.Request.Url.Host, HttpContext.Current.Request.RawUrl, words);
                }

            }

            _buffers.Clear();
            _responseStream.Write(e.GetBytes(text), 0, e.GetByteCount(text));
        }
    }
}
