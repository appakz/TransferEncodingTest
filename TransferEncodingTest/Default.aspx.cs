using System;
using System.IO;

namespace TransferEncodingTest
{
    public partial class Default : System.Web.UI.Page
    {
        protected void btnCreatePdf_Click(object sender, EventArgs e)
        {
            RegisterFilter();
            var filePath = Server.MapPath("~/TestPdf.pdf");
            var fileInfo = new FileInfo(filePath);
            Response.ClearHeaders();
            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", "filename=test.pdf");
            Response.AddHeader("Content-Length", fileInfo.Length.ToString());

            if(chkSendChunked.Checked)
            {
                SendFileInChunks(filePath);
            }
            else
            {
                SendFileInOneShot(filePath, fileInfo.Length);
            }
        }

        private void SendFileInChunks(string filePath)
        {
            var buffer = new byte[1024];
            int offset = 0;
            using (var fs = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                int read;
                while ((read = fs.Read(buffer, offset, 1024)) > 0)
                {
                    if (!Response.IsClientConnected) break;
                    Response.OutputStream.Write(buffer, 0, read);
                    Response.Flush();
                }
            } 
        }

        private void SendFileInOneShot(string filePath, long fileSize)
        {
            var buffer = new byte[fileSize];
            using (var fs = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                fs.Read(buffer, 0, (int)fileSize);
                Response.OutputStream.Write(buffer, 0, (int)fileSize);
            }
        }

        private void RegisterFilter()
        {
            if (!chkUseFilter.Checked) return;
            Response.Filter = new DoNothingFilter(Response.Filter);
        }
    }

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