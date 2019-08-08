using System;
using System.IO;
using System.Net;
using System.Net.Http;

namespace Youtube.Manager.Models.Container
{
    public class VideoStream
    {
        private readonly byte[] _data;

        public VideoStream(byte[] data)
        {
            _data = data;
        }

        public async void WriteToStream(Stream outputStream, HttpContent content, TransportContext context)
        {
            try
            {
                var buffer = new byte[_data.Length];
                content.Headers.ContentLength = _data.Length;
                using (var mStream = new MemoryStream(_data))
                {
                    var length = (int)mStream.Length;
                    var bytesRead = 1;
                    while (length > 0 && bytesRead > 0)
                    {
                        bytesRead = mStream.Read(buffer, 0, Math.Min(length, buffer.Length));
                        await outputStream.WriteAsync(buffer, 0, bytesRead);
                        length -= bytesRead;
                    }
                }
            }
            catch
            {
            }
            finally
            {
                outputStream.Close();
            }
        }
    }
}
