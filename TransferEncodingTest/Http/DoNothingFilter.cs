using System.IO;

namespace TransferEncodingTest.Http
{
    public class DoNothingFilter : MemoryStream
    {
        private readonly Stream _outputStream;

        public DoNothingFilter(Stream stream)
        {
            _outputStream = stream;
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            //do nothing, just pass this call through
            _outputStream.Write(buffer, offset, count);
        }
    }
}