using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERP.Domain.Base;
using ERP.Domain.Models;
using ERP.Web.Utils;
using IFA.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OfficeOpenXml;
using System.Security.Claims;
using System.Globalization;
using System.IO;
using System.Net.Mail;

using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;

namespace ERP.Web.Controllers
{
    public class MasterController : BaseController
    {
        public IActionResult Index()
        {
            //kates

            return View();
        }
        #region Kota
        public IActionResult Kota()
        {
            return View();
        }

        public async Task<IActionResult> Giro()
        {
            ApiClient client = factoryClass.APIClientAccess();

            Response response = new Response();

            response = await client.CallAPIGet("SIF_Barang/GetBarangGudang");

            if (response.Success)
            {
                ViewBag.BarangList = response.Message;

            }
            response = await client.CallAPIGet("SIF_Barang/GetSIFBukuBesarCbo");
            if (response.Success)
            {
                ViewBag.PersediaanList = response.Message;

            }
            response = await client.CallAPIGet("SIF_Barang/GetrekPenjualan2Cbo");
            if (response.Success)
            {
                ViewBag.Penjualan2List = response.Message;

            }
            response = await client.CallAPIGet("SIF_Satuan/GetSatuanALL");
            if (response.Success)
            {
                ViewBag.Satuan = response.Message;

            }
            response = await client.CallAPIGet("Helper/GetRekBank");
            if (response.Success)
            {
                ViewBag.banktujuan = response.Message;

            }
            response = await client.CallAPIGet("Helper/GetJenisGiro");
            if (response.Success)
            {
                ViewBag.JenisGiro = response.Message;

            }

            response = await client.CallAPIGet("Helper/GetDivisi");
            if (response.Success)
            {
                ViewBag.Divisi = response.Message;

            }

            response = await client.CallAPIGet("Helper/GetKartuGiro");
            if (response.Success)
            {
                ViewBag.KartuGiro = response.Message;

            }

            response = await client.CallAPIGet("Helper/GetBankAsal");
            if (response.Success)
            {
                ViewBag.BankAsal = response.Message;

            }

            return View();
        }

        public async Task<IActionResult> GetAll()
        {
            IEnumerable<FIN_GIRO> result = new List<FIN_GIRO>();
            ApiClient client = factoryClass.APIClientAccess();

            var response = await client.CallAPIGet("FIN_GIRO/GetALL_Giro");

            if (response.Success)
            {
                result = JsonConvert.DeserializeObject<List<FIN_GIRO>>(response.Message);
            }
            return Ok(result);
        }

        public async Task<IActionResult> GetAllGiro()
        {
            IEnumerable<FIN_GIRO> result = new List<FIN_GIRO>();
            ApiClient client = factoryClass.APIClientAccess();

            var response = await client.CallAPIGet("FIN_GIRO/GetALL_GiroTemp");

            if (response.Success)
            {
                result = JsonConvert.DeserializeObject<List<FIN_GIRO>>(response.Message);
            }
            return Ok(result);
        }

        public async Task<PartialViewResult> EditGiro(string id = null)
        {
            FIN_GIRO model = new FIN_GIRO();

            ApiClient client = factoryClass.APIClientAccess();
            if (id != null)
            {
                var response = await client.CallAPIGet("FIN_GIRO/Get_Giro?nomor=" + id);

                if (response.Success)
                {
                    model = JsonConvert.DeserializeObject<List<FIN_GIRO>>(response.Message).FirstOrDefault();
                }
            }

            return PartialView(model);
        }

        public async Task<IActionResult> SavelistGiro([FromBody] List<FIN_GIRO> data)

        {

            Response response = new Response();
            ApiClient client = factoryClass.APIClientAccess();
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var UserID = claimsIdentity.FindFirst("UserID").Value;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;

            //var Kode_Barang = "";
            //var retNo = await client.CallAPIGet("Helper/GetKdBarang");
            //if (retNo.Success)
            //{
            //    Kode_Barang = JsonConvert.DeserializeObject<string>(retNo.Message);
            //}

            foreach (FIN_GIRO item in data)
            {
                item.Kd_Cabang = BranchID;
                item.jns_trans = "jual";
                item.kd_valuta = "IDR";
                item.kurs_valuta = 1;
                item.status = "DITERIMA";
               // item.jml_trans = 0;
                item.tipe_trans = "JRR-KPT-10";
                //item.tgl_jth_tempo = DateTime.Now;
                //item.tgl_trans = DateTime.Now;
                item.Last_Create_Date = DateTime.Now;
                item.Last_Created_By = UserID;
            }
            response = await client.CallAPIPost("FIN_GIRO/SaveListGiro", data);

            return Ok(response);
        }

        public async Task<IActionResult> UpdateGiro([FromBody] List<FIN_GIRO> data)

        {

            Response response = new Response();
            ApiClient client = factoryClass.APIClientAccess();

            response = await client.CallAPIPost("FIN_GIRO/UpdateFINGIRO", data);

            return Ok(response);
        }

        public async Task<IActionResult> DeleteGiro([FromBody] List<SIF_Barang> data)

        {

            Response response = new Response();
            ApiClient client = factoryClass.APIClientAccess();

            response = await client.CallAPIPost("FIN_GIRO/DeleteGiro", data);

            return Ok(response);
        }

        public MasterController(FactoryClass factoryClass,
           IHttpContextAccessor httpContextAccessor) : base(factoryClass, httpContextAccessor)
        {
        }
        public async Task<IActionResult> GetKota()
        {
            IEnumerable<SIF_Kota> result = new List<SIF_Kota>();
            ApiClient client = factoryClass.APIClientAccess();

            var response = await client.CallAPIGet("SIF_Kota/GetSIFKota");

            if (response.Success)
            {
                result = JsonConvert.DeserializeObject<List<SIF_Kota>>(response.Message);
            }
            return Ok(result);
        }

        public async Task<PartialViewResult> EditKota(string id = null)
        {
            SIF_Kota model = new SIF_Kota();

            ApiClient client = factoryClass.APIClientAccess();
            if (id != null)
            {
                var response = await client.CallAPIGet("SIF_Kota/GetSIFKota?kode_kota=" + id);

                if (response.Success)
                {
                    model = JsonConvert.DeserializeObject<List<SIF_Kota>>(response.Message).FirstOrDefault();
                }
            }

            return PartialView(model);
        }

        public async Task<IActionResult> SaveKota(SIF_Kota data)
        {
            Response response = new Response();
            ApiClient client = factoryClass.APIClientAccess();
            data.Kd_Cabang = "1";

            data.Rec_Stat = "Y";
            response = await client.CallAPIPost("SIF_Kota/SaveSIFKota", data);

            return Ok(response);
        }
        public async Task<IActionResult> UpdateKota(SIF_Kota data)
        {
            Response response = new Response();
            ApiClient client = factoryClass.APIClientAccess();
            data.Rec_Stat = "Y";
            data.Kd_Cabang = "1";

            response = await client.CallAPIPost("SIF_Kota/UpdateSIFKota", data);

            return Ok(response);
        }

        public async Task<IActionResult> GetPegawai()
        {
            IEnumerable<SIF_Pegawai> result = new List<SIF_Pegawai>();
            ApiClient client = factoryClass.APIClientAccess();

            var response = await client.CallAPIGet("SIF_Pegawai/GetPegawaiALL");

            if (response.Success)
            {
                result = JsonConvert.DeserializeObject<List<SIF_Pegawai>>(response.Message);
            }


            return Ok(result);
        }
        public async Task<IActionResult> GetRole()
        {
            IEnumerable<MUSER_ROLE> result = new List<MUSER_ROLE>();
            ApiClient client = factoryClass.APIClientAccess();

            var response = await client.CallAPIGet("Master/GetRole");

            if (response.Success)
            {
                result = JsonConvert.DeserializeObject<List<MUSER_ROLE>>(response.Message);
            }


            return Ok(result);
        }
        public async Task<IActionResult> SavePegawai([FromBody] List<SIF_Pegawai> data, string kode_Pegawai)
        {
            Response response = new Response();
            ApiClient client = factoryClass.APIClientAccess();
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var UserID = claimsIdentity.FindFirst("UserID").Value;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;
            var Kode_Pegawai = "";
            var retNo = await client.CallAPIGet("Helper/GetNoTrans?prefix=MPEG&transdate=" + DateTime.Now + "&kdcabang=" + BranchID);
            if (retNo.Success)
            {
                Kode_Pegawai = JsonConvert.DeserializeObject<string>(retNo.Message);
            }




            foreach (SIF_Pegawai item in data)
            {
                item.Kd_Cabang = BranchID;
                item.Kode_Pegawai = Kode_Pegawai;
                item.Nama_Pegawai = item.Nama_Pegawai;
                item.Last_Create_Date = DateTime.Now;
                item.Last_Created_By = UserID;
                item.Rec_Stat = "1";




            }

            response = await client.CallAPIPost("SIF_Pegawai/SavePegawai", data);

            return Ok(response);
        }
        public async Task<IActionResult> SaveRole([FromBody] List<MUSER_ROLE> data, string kode_Pegawai)
        {
            Response response = new Response();
            ApiClient client = factoryClass.APIClientAccess();
            //var claimsIdentity = User.Identity as ClaimsIdentity;
            //var UserID = claimsIdentity.FindFirst("UserID").Value;
            //var BranchID = claimsIdentity.FindFirst("BranchID").Value;
            //var Kode_Pegawai = "";
            //var retNo = await client.CallAPIGet("Helper/GetNoTrans?prefix=MPEG&transdate=" + DateTime.Now + "&kdcabang=" + BranchID);
            //if (retNo.Success)
            //{
            //    Kode_Pegawai = JsonConvert.DeserializeObject<string>(retNo.Message);
            //}




            //foreach (MUSER_ROLE item in data)
            //{
            //    item.Kd_Cabang = BranchID;
            //    item.Kode_Pegawai = Kode_Pegawai;
            //    item.Nama_Pegawai = item.Nama_Pegawai;
            //    item.Last_Create_Date = DateTime.Now;
            //    item.Last_Created_By = UserID;
            //    item.Rec_Stat = "1";




            //}

            response = await client.CallAPIPost("Master/SaveRole", data);

            return Ok(response);
        }
        public async Task<IActionResult> UpdatePegawai([FromBody] List<SIF_Pegawai> data)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var UserID = claimsIdentity.FindFirst("UserID").Value;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;

            Response response = new Response();
            ApiClient client = factoryClass.APIClientAccess();

            foreach (SIF_Pegawai item in data)
            {
                item.Rec_Stat = "1";
                item.Nama_Pegawai = item.Nama_Pegawai;
                item.Kode_Pegawai = item.Kode_Pegawai;
                item.Last_Updated_By = UserID;
                item.Last_Update_Date = DateTime.Now;
            }

            response = await client.CallAPIPost("SIF_Pegawai/UpdateSIFPegawai", data);

            return Ok(response);
        }

        public async Task<IActionResult> UpdateRole([FromBody] List<MUSER_ROLE> data)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var UserID = claimsIdentity.FindFirst("UserID").Value;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;

            Response response = new Response();
            ApiClient client = factoryClass.APIClientAccess();

            ////foreach (MUSER_ROLE item in data)
            ////{
            ////    item.Rec_Stat = "1";
            ////    item.Nama_Pegawai = item.Nama_Pegawai;
            ////    item.Kode_Pegawai = item.Kode_Pegawai;
            ////    item.Last_Updated_By = UserID;
            ////    item.Last_Update_Date = DateTime.Now;
            ////}

            response = await client.CallAPIPost("Master/UpdateRole", data);

            return Ok(response);
        }



        public async Task<IActionResult> DeleteKota(string id)
        {
            Response response = new Response();
            ApiClient client = factoryClass.APIClientAccess();
            SIF_Kota data = new SIF_Kota();
            data.Kode_Kota = id;
            data.Rec_Stat = "N";
            data.Kd_Cabang = "1";

            response = await client.CallAPIPost("SIF_Kota/UpdateSIFKota", data);

            return Ok(response);
        }
        public IActionResult Buku_Besar()
        {
            return View();
        }

        public async Task<IActionResult> GetSIFBukuBesar()
        {
            IEnumerable<SIF_buku_besar> result = new List<SIF_buku_besar>();
            ApiClient client = factoryClass.APIClientAccess();

            var response = await client.CallAPIGet("master/GetSIFBukuBesar");

            if (response.Success)
            {
                result = JsonConvert.DeserializeObject<List<SIF_buku_besar>>(response.Message);
            }
            return Ok(result);
        }

        public async Task<PartialViewResult> EditBukuBesar(string id = null)
        {
            SIF_buku_besar model = new SIF_buku_besar();

            ApiClient client = factoryClass.APIClientAccess();
            if (id != null)
            {
                var response = await client.CallAPIGet("master/GetSIFBukuBesar?kd_buku_besar=" + id);

                if (response.Success)
                {
                    model = JsonConvert.DeserializeObject<List<SIF_buku_besar>>(response.Message).FirstOrDefault();
                }
            }

            return PartialView(model);
        }
        //public async Task<IActionResult> SaveBukuBesar(SIF_buku_besar data)
        //{
        //    Response response = new Response();
        //    ApiClient client = factoryClass.APIClientAccess();
        //    data.Kd_Cabang = "1";

        //    data.Rec_Stat = "Y";
        //    response = await client.CallAPIPost("Master/SaveBukuBesar", data);

        //    return Ok(response);
        //}
        public async Task<IActionResult> SaveBukuBesar([FromBody] List<SIF_buku_besar> data)

        {
            Response response = new Response();
            ApiClient client = factoryClass.APIClientAccess();
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var UserID = claimsIdentity.FindFirst("UserID").Value;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;


            //var Kode_Supplier = "";
            //var retNo = await client.CallAPIGet("Helper/GetNoTrans?prefix=MBB&transdate=" + DateTime.Now + "&kdcabang=" + BranchID);
            //if (retNo.Success)
            //{
            //    Kode_Supplier = JsonConvert.DeserializeObject<string>(retNo.Message);
            //}

            foreach (SIF_buku_besar item in data)
            {
                item.Kd_Cabang = BranchID;
                item.Rec_Stat = "1";
            }
            response = await client.CallAPIPost("Master/SaveBukuBesar", data);

            return Ok(response);
        }
        //public async Task<IActionResult> UpdateBukuBesar(SIF_buku_besar data)
        //{
        //    Response response = new Response();
        //    ApiClient client = factoryClass.APIClientAccess();
        //    data.Rec_Stat = "Y";
        //    data.Kd_Cabang = "1";

        //    response = await client.CallAPIPost("Master/UpdateBukuBesar", data);

        //    return Ok(response);
        //}

        public async Task<IActionResult> UpdateBukuBesar([FromBody] List<SIF_buku_besar> data)

        {

            Response response = new Response();
            ApiClient client = factoryClass.APIClientAccess();

            response = await client.CallAPIPost("Master/UpdateBukuBesar", data);

            return Ok(response);
        }

        public async Task<IActionResult> DeleteSupplier([FromBody] List<SIF_buku_besar> data)

        {

            Response response = new Response();
            ApiClient client = factoryClass.APIClientAccess();

            response = await client.CallAPIPost("Master/DeleteBukuBesar", data);

            return Ok(response);
        }
        //public async Task<IActionResult> DeleteBukuBesar(string id)
        //{
        //    Response response = new Response();
        //    ApiClient client = factoryClass.APIClientAccess();
        //    SIF_buku_besar data = new SIF_buku_besar();
        //    data.kd_buku_besar = id;
        //    data.Rec_Stat = "N";
        //    data.Kd_Cabang = "1";

        //    response = await client.CallAPIPost("Master/DeleteBukuBesar", data);

        //    return Ok(response);
        //}

        public IActionResult Harga()
        {
            return View();
        }
        public IActionResult Role()
        {
            return View();
        }
        public async Task<IActionResult> GetSIFHarga()
        {
            IEnumerable<SIF_Harga> result = new List<SIF_Harga>();
            ApiClient client = factoryClass.APIClientAccess();

            var claimsIdentity = User.Identity as ClaimsIdentity;
            var UserID = claimsIdentity.FindFirst("UserID").Value;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;
            var response = await client.CallAPIGet("Master/GetSIFHarga?kd_Cabang=" + BranchID);

            if (response.Success)
            {
                result = JsonConvert.DeserializeObject<List<SIF_Harga>>(response.Message);
            }
            return Ok(result);
        }

        public async Task<IActionResult> UpdateHargaExcel(IFormFile files)
        {
            Response response = new Response();
            ApiClient client = factoryClass.APIClientAccess();
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var UserID = claimsIdentity.FindFirst("UserID").Value;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;
            List<SIF_Harga> LIstBarang = new List<SIF_Harga>();
            string no_trans = "";
            // The Name of the Upload component is "files"
            if (files != null)
            {
                response = await client.CallAPIGet("Helper/GetNoTrans?prefix=HRG&transdate=" + DateTime.Now + "&kdcabang=" + BranchID);
                if (response.Success)
                {
                    no_trans = JsonConvert.DeserializeObject<string>(response.Message);

                }
                var filename = files.FileName;
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", filename);

                using (var stream = System.IO.File.Create(filePath))
                {
                    await files.CopyToAsync(stream);
                    using (var package = new ExcelPackage(stream))
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                        var rowCount = worksheet.Dimension.Rows;

                        for (int row = 2; row <= rowCount; row++)
                        {
                            SIF_Harga model = new SIF_Harga();
                            string a = worksheet.Cells[row, 1].Value.ToString().Trim();
                            string b = worksheet.Cells[row, 2].Value.ToString().Trim();
                            string c = worksheet.Cells[row, 3].Value.ToString().Trim();
                            string d = worksheet.Cells[row, 4].Value.ToString().Trim();

                            model.Kd_Cabang = BranchID;
                            model.no_trans = no_trans;
                            model.Kode_Barang = worksheet.Cells[row, 1].Value.ToString().Trim();
                            model.nama_Barang = worksheet.Cells[row, 2].Value.ToString().Trim();
                            model.Harga_Rupiah = decimal.Parse(worksheet.Cells[row, 3].Value.ToString().Trim());
                            model.Harga_Rupiah2 = decimal.Parse(worksheet.Cells[row, 4].Value.ToString().Trim());
                            model.Harga_Rupiah3 = decimal.Parse(worksheet.Cells[row, 5].Value.ToString().Trim());
                            model.Harga_Rupiah4 = decimal.Parse(worksheet.Cells[row, 6].Value.ToString().Trim());
                            model.qty_harga1_min = int.Parse(worksheet.Cells[row, 7].Value.ToString().Trim());
                            model.qty_harga2_min = int.Parse(worksheet.Cells[row, 8].Value.ToString().Trim());
                            model.qty_harga3_min = int.Parse(worksheet.Cells[row, 9].Value.ToString().Trim());
                            string date = worksheet.Cells[row, 10].Value.ToString().Trim();
                            model.Start_Date = DateTime.Parse(date, CultureInfo.CreateSpecificCulture("id-ID"));


                            LIstBarang.Add(model);

                        }
                    }

                }
                response = await client.CallAPIPost("Master/UpdateHarga", LIstBarang);
            }



            // Return an empty string to signify success
            return Content("");
        }
        public async Task<IActionResult> SaveUpdate([FromBody] List<SIF_Harga> data)
        {
            Response response = new Response();
            ApiClient client = factoryClass.APIClientAccess();
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var UserID = claimsIdentity.FindFirst("UserID").Value;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;

            for (int i = 0; i <= data.Count() - 1; i++)
            {

                data[i].Kd_Cabang = BranchID;
                data[i].Last_Updated_By = UserID;
                data[i].Last_Update_Date = DateTime.Now;


            }

            response = await client.CallAPIPost("Master/UpdateHarga", data);
            if (response.Success)
            {
                try
                {

                    string textBody = " <table style= font-size:" + 12 + "px  border=" + 1 + " cellpadding=" + 0 + " cellspacing=" + 0 + " width = " + 100 + "%><tr bgcolor='#4da6ff'><td><b>Nama Barang</b></td> <td> <b>Harga 1</b> </td><td> <b>Harga 2</b> </td><td> <b>Harga 3</b> </td> <td> <b>Harga Lama</b> </td> <td> <b>Harga Lama 2</b> </td> <td> <b>Harga Lama 3</b> </td> <td> <b>Selisih</b> </td><td> <b>Selisih 2</b> </td><td> <b>Selisih 3</b> </td></tr>";
                    for (int loopCount = 0; loopCount < data.Count; loopCount++)
                    {
                        textBody += "<tr><td>" + data[loopCount].nama_Barang + "</td><td>Rp " + data[loopCount].Harga_Rupiah + "</td><td>Rp " + data[loopCount].Harga_Rupiah2 + "</td><td>Rp " + data[loopCount].Harga_Rupiah3 + "</td><td>Rp " + data[loopCount].Harga_RupiahOld + "</td><td>Rp " + data[loopCount].Harga_RupiahOld2 + "</td><td>Rp " + data[loopCount].Harga_RupiahOld3 + "</td><td> " + data[loopCount].Selisih + "</td><td> " + data[loopCount].Selisih2 + "</td><td> " + data[loopCount].Selisih3 + "</td> </tr>";
                    }
                    textBody += "</table>";
                    MailMessage mail = new MailMessage();
                    SmtpClient SmtpServer = new SmtpClient();

                    mail.From = new MailAddress("erpliquid@gmail.com");
                    mail.To.Add("Widya.Hudzhifah@gmail.com");
                    mail.To.Add("ncuzziena@gmail.com");
                    mail.Subject = "Update Harga";
                    mail.Body = textBody;
                    mail.IsBodyHtml = true;
                    SmtpServer.Port = 587;
                    SmtpServer.Host = "smtp.gmail.com";
                    SmtpServer.UseDefaultCredentials = false;
                    SmtpServer.Credentials = new System.Net.NetworkCredential("erpliquid@gmail.com", "limwahsai123");
                    SmtpServer.EnableSsl = true;

                    SmtpServer.Send(mail);
                }
                catch (Exception e)
                {
                    string cok = e.Message;
                }

            }
            return Ok(response);
        }


        public IActionResult Customer()
        {
            //kates
            return View();
        }

        public async Task<IActionResult> GetCustomer()
        {
            IEnumerable<CustomerVM> result = new List<CustomerVM>();
            ApiClient client = factoryClass.APIClientAccess();

            var response = await client.CallAPIGet("Master/GetCustomer");

            if (response.Success)
            {
                result = JsonConvert.DeserializeObject<List<CustomerVM>>(response.Message);
            }
            return Ok(result);
        }

        public async Task<IActionResult> UpdateCustomer([FromBody] List<CustomerVM> data)

        {

            Response response = new Response();
            ApiClient client = factoryClass.APIClientAccess();

            response = await client.CallAPIPost("Master/UpdateCustomer", data);

            return Ok(response);
        }
        public async Task<IActionResult> DeletePegawai([FromBody] List<SIF_Pegawai> data)

        {

            Response response = new Response();
            ApiClient client = factoryClass.APIClientAccess();

            response = await client.CallAPIPost("SIF_Pegawai/DeletePegawai", data);

            return Ok(response);
        }

        public async Task<IActionResult> DeleteRole([FromBody] List<MUSER_ROLE> data)

        {

            Response response = new Response();
            ApiClient client = factoryClass.APIClientAccess();

            response = await client.CallAPIPost("Master/DeleteRole", data);

            return Ok(response);
        }

        public async Task<IActionResult> DeleteCustomer([FromBody] List<CustomerVM> data)

        {

            Response response = new Response();
            ApiClient client = factoryClass.APIClientAccess();

            response = await client.CallAPIPost("Master/DeleteCustomer", data);

            return Ok(response);
        }

        public async Task<IActionResult> SaveCustomer([FromBody] List<CustomerVM> data)

        {

            Response response = new Response();
            ApiClient client = factoryClass.APIClientAccess();
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var UserID = claimsIdentity.FindFirst("UserID").Value;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;

            var Kd_Customer = "";
            var retNo = await client.CallAPIGet("Helper/GetNoTrans?prefix=MCST&transdate=" + DateTime.Now + "&kdcabang=" + BranchID);
            if (retNo.Success)
            {
                Kd_Customer = JsonConvert.DeserializeObject<string>(retNo.Message);
            }

            foreach (CustomerVM item in data)
            {
                item.Kd_Customer = Kd_Customer;
            }
            response = await client.CallAPIPost("Master/SaveListCustomer", data);

            return Ok(response);
        }

        public IActionResult Supplier()
        {
            //kates
            return View();
        }

        public async Task<IActionResult> GetSupplier()
        {
            IEnumerable<SIF_Supplier> result = new List<SIF_Supplier>();
            ApiClient client = factoryClass.APIClientAccess();

            var response = await client.CallAPIGet("Master/GetSupplier");

            if (response.Success)
            {
                result = JsonConvert.DeserializeObject<List<SIF_Supplier>>(response.Message);
            }
            return Ok(result);
        }

        public async Task<IActionResult> UpdateSupplier([FromBody] List<SIF_Supplier> data)

        {

            Response response = new Response();
            ApiClient client = factoryClass.APIClientAccess();

            response = await client.CallAPIPost("Master/UpdateSUpplier", data);

            return Ok(response);
        }

        public async Task<IActionResult> DeleteSupplier([FromBody] List<SIF_Supplier> data)

        {

            Response response = new Response();
            ApiClient client = factoryClass.APIClientAccess();

            response = await client.CallAPIPost("Master/DeleteSupplier", data);

            return Ok(response);
        }

        public async Task<IActionResult> SaveSupplier([FromBody] List<SIF_Supplier> data)

        {

            Response response = new Response();
            ApiClient client = factoryClass.APIClientAccess();
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var UserID = claimsIdentity.FindFirst("UserID").Value;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;

            var Kode_Supplier = "";
            var retNo = await client.CallAPIGet("Helper/GetNoTrans?prefix=MSUP&transdate=" + DateTime.Now + "&kdcabang=" + BranchID);
            if (retNo.Success)
            {
                Kode_Supplier = JsonConvert.DeserializeObject<string>(retNo.Message);
            }

            foreach (SIF_Supplier item in data)
            {
                item.Kode_Supplier = Kode_Supplier;
            }
            response = await client.CallAPIPost("Master/SaveListSupplier", data);

            return Ok(response);
        }

        #endregion

        #region Sopir
        public async Task<IActionResult> Sopir()
        {
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();
            response = null;
            response = await client.CallAPIGet("SIF_Pegawai/GetPegawaiALL");
            if (response.Success)
            {
                ViewBag.GudangList = response.Message;

            }
            return View();

        }

        public async Task<IActionResult> GetSopir()
        {
            IEnumerable<CustomerVM> result = new List<CustomerVM>();
            ApiClient client = factoryClass.APIClientAccess();

            var response = await client.CallAPIGet("Master/GetCustomer");

            if (response.Success)
            {
                result = JsonConvert.DeserializeObject<List<CustomerVM>>(response.Message);
            }
            return Ok(result);
        }



        #endregion

        #region Kendaraan
        public IActionResult Kendaraan()
        {
            return View();
        }

        public async Task<IActionResult> GetKendaraan()
        {
            IEnumerable<Kendaraan> result = new List<Kendaraan>();
            ApiClient client = factoryClass.APIClientAccess();

            var response = await client.CallAPIGet("Master/GetKendaraan");

            if (response.Success)
            {
                result = JsonConvert.DeserializeObject<List<Kendaraan>>(response.Message);
            }
            return Ok(result);
        }

        public async Task<IActionResult> SaveKendaraan([FromBody] List<Kendaraan> data)

        {

            Response response = new Response();
            ApiClient client = factoryClass.APIClientAccess();
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var UserID = claimsIdentity.FindFirst("UserID").Value;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;

            var Kode_Supplier = "";
            var retNo = await client.CallAPIGet("Helper/GetNoTrans?prefix=MKEN&transdate=" + DateTime.Now + "&kdcabang=" + BranchID);
            if (retNo.Success)
            {
                Kode_Supplier = JsonConvert.DeserializeObject<string>(retNo.Message);
            }

            foreach (Kendaraan item in data)
            {
                item.Kode_Kendaraan = Kode_Supplier;
            }
            response = await client.CallAPIPost("Master/SaveListKendaraan", data);

            return Ok(response);
        }

        public async Task<IActionResult> UpdateKendaraan([FromBody] List<Kendaraan> data)

        {

            Response response = new Response();
            ApiClient client = factoryClass.APIClientAccess();

            response = await client.CallAPIPost("Master/UpdateKendaraan", data);

            return Ok(response);
        }

        public async Task<IActionResult> DeleteKendaraan([FromBody] List<Kendaraan> data)

        {

            Response response = new Response();
            ApiClient client = factoryClass.APIClientAccess();

            response = await client.CallAPIPost("Master/DeleteKendaraan", data);

            return Ok(response);
        }



        #endregion

    }


}