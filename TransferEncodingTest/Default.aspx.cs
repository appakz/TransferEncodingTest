using System;
using System.Globalization;
using System.IO;
using TransferEncodingTest.Http;
using sharpPDF.Enumerators;

namespace TransferEncodingTest
{
    public partial class Default : System.Web.UI.Page
    {
        protected void btnCreatePdf_Click(object sender, EventArgs e)
        {
            RegisterFilter();

            var fileBytes = CreateFile();
            Response.ClearHeaders();
            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", "filename=test.pdf");
            Response.AddHeader("Content-Length", fileBytes.Length.ToString(CultureInfo.InvariantCulture));

            if (chkSendChunked.Checked)
            {
                SendFileInChunks(fileBytes);
            }
            else
            {
                SendFileInOneShot(fileBytes);
            }
        }

        private void SendFileInChunks(byte[] fileBytes)
        {
            var buffer = new byte[1024];
            int offset = 0;

            int read;
            var stream = new MemoryStream(fileBytes);
            while ((read = stream.Read(buffer, offset, 1024)) > 0)
            {
                if (!Response.IsClientConnected) break;
                Response.OutputStream.Write(buffer, 0, read);
                Response.Flush();
            }
        }

        private void SendFileInOneShot(byte[] fileBytes)
        {
            Response.OutputStream.Write(fileBytes, 0, fileBytes.Length);
        }

        private void RegisterFilter()
        {
            if (!chkUseFilter.Checked) return;
            Response.Filter = new DoNothingFilter(Response.Filter);
        }

        private static byte[] CreateFile()
        {
            var stream = new MemoryStream();
            using (var document = new sharpPDF.pdfDocument("test", "Jesse Taber"))
            {
                for(var i=0;i<10;i++)
                {
                    var page = document.addPage();
                    page.addText("Hello World!!", 200, 450, document.getFontReference(predefinedFont.csHelvetica), 20);
                }
                document.createPDF(stream);
            }
            return stream.ToArray();
        }
    }
}