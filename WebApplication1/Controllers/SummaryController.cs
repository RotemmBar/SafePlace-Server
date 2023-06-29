﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DATA;
using WebApplication1.Dto;

namespace WebApplication1.Controllers
{
    public class SummaryController : ApiController
    {

        [HttpGet]
        [Route("api/GetSummaryByDate/{PatientId}/{Date}")]
        public IHttpActionResult GetSummaryByNum(string PatientId, string date)
        {
            try
            {
                SafePlaceDbContextt db = new SafePlaceDbContextt();

                string therid = db.TblTreats.Where(p => p.Patient_Id == PatientId).Select(o => o.Therapist_Id).ToString();

                SummaryDto Summary = db.TblSummary.Where(a => a.Summary_Date.ToString().Substring(0, 10) == date && a.WrittenBy == therid 
                && a.TblWrittenFor.FirstOrDefault().TblTreatment.TblTreats.FirstOrDefault().Patient_Id == PatientId).Select(x => new SummaryDto()

                {
                    Summary_Num = x.Summary_Num,
                    WrittenBy = x.WrittenBy,
                    Summary_Date = x.Summary_Date.ToString().Substring(0, 10),
                    ImportanttoNote = x.ImportentToNote,
                    Content = x.Content,
                    StartTime = (DateTime)x.TblWrittenFor.FirstOrDefault().TblTreatment.StartTime,
                    EndTime = (DateTime)x.TblWrittenFor.FirstOrDefault().TblTreatment.EndTime,
                    FirstNameP = x.TblWrittenFor.FirstOrDefault().TblTreatment.TblTreats.FirstOrDefault().TblPatient.FirstName,
                    LastNameP = x.TblWrittenFor.FirstOrDefault().TblTreatment.TblTreats.FirstOrDefault().TblPatient.LastName

                }).FirstOrDefault();


                return Ok(Summary);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpGet]
        [Route("api/GetAllSummary/{PatientId}")]
        public IHttpActionResult GetAllSummary(string PatientId)
        {
            try
            {
                SafePlaceDbContextt db = new SafePlaceDbContextt();
                List<SummaryDto> allSummaries = db.TblSummary.Where(x => x.TblWrittenFor.FirstOrDefault().TblTreatment.TblTreats.FirstOrDefault().Patient_Id == PatientId)

                    .Select(s => new SummaryDto()
                    {
                        Summary_Num = 0,
                        Summary_Date = s.Summary_Date.ToString().Substring(0, 10),
                        Patient_Id = s.TblWrittenFor.FirstOrDefault().TblTreatment.TblTreats.FirstOrDefault().Patient_Id

                    }).ToList();

                return Ok(allSummaries);

            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPost]
        [Route("api/PostSummary")]
        public IHttpActionResult Post([FromBody] NewSummaryDto value)
        {
            SafePlaceDbContextt db = new SafePlaceDbContextt();
            try
            {
                var usertype = db.TblUsers.Where(o => o.Email == value.WrittenBy).Select(p => p.UserType).FirstOrDefault();
                var writtenby = "";

                if (usertype==0)
                {
                    writtenby = "p";
                }
                else if (usertype==1)
                {
                    writtenby = "t";
                }

                TblSummary newSummary = new TblSummary();
                int nextSummaryNum = db.TblSummary.Any() ? db.TblSummary.Max(s => s.Summary_Num) + 1 : 1;
                newSummary.Summary_Num = nextSummaryNum;
                newSummary.WrittenBy = writtenby;
                newSummary.Content = value.Content;
                newSummary.Summary_Date = value.Summary_Date;
                newSummary.ImportentToNote = value.ImportanttoNote;
                db.TblSummary.Add(newSummary);

                TblWrittenFor newWrittenFor = new TblWrittenFor();
                newWrittenFor.Summary_Num = nextSummaryNum;
                newWrittenFor.Treatment_Id = value.Treatment_Id;
                db.TblWrittenFor.Add(newWrittenFor);


                db.SaveChanges();
                return Ok("Save");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //[HttpGet]
        //[Route("api/GetPatientSummaries")]
        //public IHttpActionResult GetPatientSummaries(string email)
        //{
        //    try
        //    {
        //        SafePlaceDbContextt db = new SafePlaceDbContextt();

        //        List<TblSummary> writtenbyp = db.TblSummary.Where(o => o.WrittenBy == "p").ToList();

        //        List<TblWrittenFor> alltrearmentId = writtenbyp.Where(o => o.TblWrittenFor.Select(p => p.Treatment_Id).ToList();

        //            .Select(s => new SummaryDto()
        //            {
        //                Summary_Num = 0,
        //                Summary_Date = s.Summary_Date.ToString().Substring(0, 10),
        //                Patient_Id = s.TblWrittenFor.FirstOrDefault().TblTreatment.TblTreats.FirstOrDefault().Patient_Id

        //            }).ToList();

        //        return Ok(allSummaries);

        //    }
        //    catch (Exception ex)
        //    {
        //        return Content(HttpStatusCode.BadRequest, ex);
        //    }
        //}




    }






}
