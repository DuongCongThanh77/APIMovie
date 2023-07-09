using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Web;
using System.IO;
using Microsoft.AspNetCore.Http;
using Azure.Core;
using System.Web.Http;
using Microsoft.Data.SqlClient;
using Microsoft.Data;
using System.Data;
using System.Text;
using WebAPI.Model;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System.Net;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace WebAPI.Controllers
{
    [ApiController]
    [System.Web.Http.Route("[controller]")]
    public class UPloadFileController 
    {
        [Microsoft.AspNetCore.Mvc.HttpPost("api/upload")]
       // [Microsoft.AspNetCore.Mvc.HttpPost("UploadFiles")]
        public async Task<IActionResult> Post(List<IFormFile> files)
        {
            long size = files.Sum(f 
                => f.Length);

            // full path to file in temp location
            var filePath = "D:\\C#\\Project\\";

            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    using (var stream = new FileStream(filePath + formFile.FileName, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
            }

            // process uploaded files
            // Don't rely on or trust the FileName property without validation.

            return new JsonResult("{status: 1}");
        }

        [Microsoft.AspNetCore.Mvc.HttpPost("api/createfile")]
        public JsonResult CreateFile()
        {
            SqlConnection con = new SqlConnection(@"Data Source=MSI\MSSQLSERVER02;Initial Catalog=Movie;Integrated Security=True;Encrypt=false;TrustServerCertificate=true");
            DataTable tb = new DataTable();
            con.Open();
            SqlDataAdapter dt = new SqlDataAdapter(new SqlCommand("Select * from Movie", con));
            dt.Fill(tb);
            con.Close();
            var csv = new StringBuilder();

            List<Movie> items = tb.AsEnumerable().Select((row, c) =>
                new Movie
                (
                   row.Field<int>("id"),
                   row.Field<string>("Title"),
                   row.Field<int>("ReleaseYear"),
                   row.Field<string>("Genre"),
                   row.Field<string>("Director"),
                   row.Field<decimal>("Rating")
                )).Where( row => row.ID == 2 || row.ID == 3 ).OrderByDescending(row => row.ID ).ToList();

            csv.AppendLine(new Movie().getHeader());
           
            string cv = String.Join("\n", items.Select( (row,index) => row.getString()).ToArray());

            File.WriteAllText("D:\\C#\\Project\\test.csv", csv.ToString());

            File.WriteAllText("D:\\C#\\Project\\test2.csv", cv.ToString());

   
            BinaryReader br = new BinaryReader(new FileStream("D:\\C#\\Project\\test.csv", FileMode.Open));
            BinaryReader br2 = new BinaryReader(new FileStream("D:\\C#\\Project\\test2.csv", FileMode.Open));
           
            long numBytes = new FileInfo("D:\\C#\\Project\\test.csv").Length;
            long numBytes2 = new FileInfo("D:\\C#\\Project\\test2.csv").Length;

            var c =ConcatFile(br.ReadBytes((int)numBytes), br2.ReadBytes((int)numBytes2));
            File.WriteAllBytes("D:\\C#\\Project\\test3.csv", c);
            br.Close();
            br2.Close();
            return new JsonResult ( "status=1" );
        }
        public static byte[] ConcatFile(byte[] first, byte[] second)
        {
            return first.Concat(second).ToArray();
        }


        [Microsoft.AspNetCore.Mvc.HttpPost("api/pdf")]
        public JsonResult createFDF()
        {
            PdfDocument one = PdfReader.Open("D:\\C#\\Project\\1.pdf", PdfDocumentOpenMode.Import);
            PdfDocument two = PdfReader.Open("D:\\C#\\Project\\4.pdf", PdfDocumentOpenMode.Import);
             PdfDocument outPdf = new PdfDocument();
            
          //  PdfDocument outPdf2 = PdfReader.Open("D:\\C#\\Project\\3.pdf", PdfDocumentOpenMode.Modify);
            CopyPages(one, outPdf);
            CopyPages(two, outPdf);
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            outPdf.Save("D:\\C#\\Project\\5.pdf");
            //  outPdf2.Close();
            return new JsonResult("status=1");
        }
        void CopyPages(PdfDocument from, PdfDocument to)
        {
            for (int i = 0; i < from.PageCount; i++)
            {
                to.AddPage(from.Pages[i]);
            }
        }
        [Microsoft.AspNetCore.Mvc.HttpGet("api/getdata")]
        public JsonResult getdata()
        {
            String JSON = new WebClient().DownloadString("http://127.0.0.1:5000/index");
            JsonModel.ModeJson model = JsonConvert.DeserializeObject<JsonModel.ModeJson>(JSON);
            return new JsonResult(model);
        }
    }
}
