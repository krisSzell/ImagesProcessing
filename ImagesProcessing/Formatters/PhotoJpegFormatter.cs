using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.Web;

namespace ImagesProcessing.Formatters
{
    public class PhotoJpegFormatter : MediaTypeFormatter
    {
        public PhotoJpegFormatter()
        {
            SupportedMediaTypes.Add(new System.Net.Http.Headers.MediaTypeHeaderValue("image/jpeg"));
        }

        public override bool CanReadType(Type type)
        {
            if (type == typeof(byte[]))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override bool CanWriteType(Type type)
        {
            if (type == typeof(byte[]))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override Task<object> ReadFromStreamAsync(Type type, Stream readStream, HttpContent content, IFormatterLogger formatterLogger)
        {
            var taskCompletionSource = new TaskCompletionSource<object>();
            try
            {
                var memoryStream = new MemoryStream();
                readStream.CopyTo(memoryStream);
                var s = System.Text.Encoding.UTF8.GetBytes(memoryStream.ToString());
                taskCompletionSource.SetResult(s);
            }
            catch (Exception e)
            {
                taskCompletionSource.SetException(e);
            }
            return taskCompletionSource.Task;
        }

        public override Task WriteToStreamAsync(Type type, object value, Stream writeStream, HttpContent content, TransportContext transportContext)
        {
            var taskCompletionSource = new TaskCompletionSource<object>();
            try
            {
                taskCompletionSource.SetResult(value);
            }
            catch (Exception e)
            {
                taskCompletionSource.SetException(e);
            }
            return taskCompletionSource.Task;
        }
    }
}