using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERP.Api.Utils;
using ERP.Domain.Base;
using IFA.Api.Repositories;
using IFA.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using ERP.Api;
using System.IO;
using IFA.Domain.Utils;

namespace IFA.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class INV_OPNAMEController : ControllerBase
    {
        [HttpGet("GetOpname")]
        public ActionResult<string> GetOpname(string no_trans = null)
        {
            var successReponse = new Response();
            IEnumerable<INV_OPNAME> ListOpname = new List<INV_OPNAME>();
            IEnumerable<INV_OPNAME_DTL> ListOpnameDTL = new List<INV_OPNAME_DTL>();
            try
            {
                if (no_trans != null)
                {
                    ListOpname = INV_OPNAMERepo.GetOpname(no_trans);
                    ListOpnameDTL = INV_OPNAMERepo.GetOpnameDTL(no_trans);

                    if (ListOpnameDTL != null)
                    {
                        ListOpname.FirstOrDefault().opnamedtl = new List<INV_OPNAME_DTL>();

                        foreach (INV_OPNAME_DTL detail in ListOpnameDTL)
                        {
                            ListOpname.FirstOrDefault().opnamedtl.Add(detail);
                        }
                    }

                }

                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(ListOpname);
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

            }
            return JsonConvert.SerializeObject(successReponse);
        }

        [HttpGet("GetMonOpname")]
        public ActionResult<string> GetMonOpname(string no_trans = null, string kd_cabang = null)
        {
            var successReponse = new Response();
            IEnumerable<INV_OPNAME> ListOpname = new List<INV_OPNAME>();

            try
            {
                ListOpname = INV_OPNAMERepo.GetOpname(no_trans, kd_cabang);

                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(ListOpname);
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

            }
            return JsonConvert.SerializeObject(successReponse);
        }

        [HttpGet("GetMonOpnameDTL")]
        public ActionResult<string> GetMonOpnameDTL(string no_trans = null)
        {
            var successReponse = new Response();
            IEnumerable<INV_OPNAME_DTL> ListOpname = new List<INV_OPNAME_DTL>();

            try
            {

                ListOpname = INV_OPNAMERepo.GetOpnameDTL(no_trans).OrderBy(x => x.no_seq);

                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(ListOpname);
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

            }
            return JsonConvert.SerializeObject(successReponse);
        }

        [HttpPost("SaveOpname")]
        [RequestFormLimits(ValueCountLimit = 214748364)]
        public async Task<ActionResult<Response>> SaveOpname([FromForm] INV_OPNAME data)
        {
            var successReponse = new Response();
            var conn = DataAccess.GetConnection();
            var trans = DataAccess.OpenTransaction(conn);
            bool stat_adj = false;
            bool brg_out = false;
            bool brg_in = false;

            try
            {
                await INV_OPNAMERepo.SaveOpname(data, trans);
                if (data.opnamedtl != null)
                {
                    INV_OPNAMERepo.DeleteDetail(data.opnamedtl[0].no_trans, trans);
                    foreach (INV_OPNAME_DTL detail in data.opnamedtl)
                    {
                        decimal qty_kartu = 0;

                        await INV_OPNAMERepo.SaveDetail(detail, trans);
                       
                        int rowkartu = await INV_OPNAMERepo.CountRow(detail.kode_gudang, detail.kd_stok, detail.bultah, trans);
                        if (rowkartu == 0)
                        {
                            qty_kartu = await INV_OPNAMERepo.CekSaldoAwal(detail.Kd_Cabang, detail.kd_stok, detail.bultah, trans);
                        }
                        else
                        {
                            qty_kartu = await INV_OPNAMERepo.CekKartuStok(detail.kode_gudang, detail.kd_stok, detail.bultah, trans);
                        }



                        detail.kd_jenis = "ADJ-OPN"; // add tipe trans adjust opname
                        if (qty_kartu != detail.qty_data)
                        {
                            stat_adj = true;


                            //adjustment di samakan antara saldo dan karto stok
                            if (qty_kartu < detail.qty_data)
                            {
                                brg_in = true;
                                detail.qty_kartu = detail.qty_data - qty_kartu; //selisih kekurangan di insert

                                await INV_GUDANG_IN_D_Repo.SaveGD_dtl_opn(detail, trans);
                            }
                            else //  qty_kartu > detail.qty_data
                            {
                                brg_out = true;
                                detail.qty_kartu = qty_kartu - detail.qty_data; //selisih kelebihan di outkn
                                await GUDANG_OUTRepo.SaveOpnDetail(detail, trans);
                            }

                        }

                    }
                    if (stat_adj == true)
                    {
                        data.blthn = DateTime.Now.ToString("yyyyMM");
                        data.tipe_trans = "ADJ-OPN";// string tipe = "ADJ-OPN"; // add tipe trans adjust opname
                        if (brg_in == true)
                        {
                            await INV_GUDANG_IN_Repo.SaveAdjGudangIn(data, trans);
                        }
                        if (brg_out == true) // jika gd out
                        {
                            await GUDANG_OUTRepo.SaveAdjGudangOut(data, trans);

                        }

                    }
                }

                await INV_OPNAMERepo.SPOpname(data.no_trans, trans, conn);

                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = "success";

                //all save success -> commit set true
                DataAccess.CloseTransaction(conn, trans, true);
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

                //ada fail -> rollback
                DataAccess.CloseTransaction(conn, trans, false);

                StackTrace st = new StackTrace(e, true);
                StackFrame frame = st.GetFrame(st.FrameCount - 1);
                string fileName = frame.GetFileName();
                string methodName = frame.GetMethod().Name;
                int line = frame.GetFileLineNumber();

                if (Startup.application != "development")
                {
                    var path = Path.Combine(Startup.contentRoot, "appsettings.json");

                    string emailbody = EmailErrorLog.createHtml(e.Message, fileName, line, methodName, path);
                    EmailErrorLog.SendEmail(emailbody, path);
                }

            }
            finally
            {
                //close transaction -> commit success or fail
                DataAccess.DisposeConnectionAndTransaction(conn, trans);
            }

            return successReponse;
        }
    }
}