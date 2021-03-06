﻿using System;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Fusion.Models.Callback;
using System.Data.SqlClient;
using System.Data;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web.Configuration;
using Jitbit.Utils;
using System.Web.UI.WebControls;
using System.Drawing;
using System.IO;
using System.Web.UI;


namespace Fusion.Controllers
{
    public class CallbackController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [MyAuthorize(Roles = "BonusClubManager, FusionAdmin, Callback")]
        [HttpGet]
        public ActionResult viewtable()
        {
            TblViewModel.TblsModelDown model2 = new TblViewModel.TblsModelDown();
            model2.Search();
            return View(model2);
        }

        [MyAuthorize(Roles = "BonusClubManager, FusionAdmin, Callback")]
        [HttpPost]
        public ActionResult viewtable(TblViewModel.TblsModelDown model)
        {
            model.Search();
            return View(model);
        }

        //для получения данных из бд
        [HttpGet]
        [MyAuthorize(Roles = "BonusClubManager, FusionAdmin, Callback")]
        public ActionResult uniform(int updorins)
        {
            UniformModel model = new UniformModel();
            //поймать и отдать id отзыва в форму и далее в POST
            int id = updorins;

            if (id >= 0)
            {
                model.id = id;
                #region dbConn
                string connPRTS = WebConfigurationManager.ConnectionStrings["FeedbackConnectionString"].ConnectionString;
                MySqlConnection conn = new MySqlConnection(connPRTS);
                bool testCon = true;
                try
                {
                    conn.Open();
                }
                catch
                {
                    testCon = false;
                }
                #endregion

                #region DownloadFields
                ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                string FIO = @"SELECT FIO FROM tblfeedback WHERE id='" + id + "'";
                string Phone = @"SELECT Phone FROM tblfeedback WHERE id='" + id + "'";
                string Email = @"SELECT Email FROM tblfeedback WHERE id='" + id + "'";
                string Text = @"SELECT Text FROM tblfeedback WHERE id='" + id + "'";
                string Data = @"SELECT Data FROM tblfeedback WHERE id='" + id + "'";
                string Source = @"SELECT Source FROM tblfeedback WHERE id='" + id + "'";
                string Unit = @"SELECT Unit FROM tblfeedback WHERE id='" + id + "'";
                string Rest = @"SELECT Rest FROM tblfeedback WHERE id='" + id + "'";
                string Rating = @"SELECT Rating FROM tblfeedback WHERE id='" + id + "'";
                string Rating2 = @"SELECT Rating2 FROM tblfeedback WHERE id='" + id + "'";
                string Sotrudnik = @"SELECT Sotrudnik FROM tblfeedback WHERE id='" + id + "'";
                string Problem = @"SELECT Problem FROM tblfeedback WHERE id='" + id + "'";
                string Mera = @"SELECT Mera FROM tblfeedback WHERE id='" + id + "'";
                string AnswerForGuest = @"SELECT AnswerForGuest FROM tblfeedback WHERE id='" + id + "'";
                string Cost = @"SELECT Cost FROM tblfeedback WHERE id='" + id + "'";
                string CostPoint = @"SELECT CostPoint FROM tblfeedback WHERE id='" + id + "'";
                string CostSert = @"SELECT CostSert FROM tblfeedback WHERE id='" + id + "'";
                string CostDiscount = @"SELECT CostDiscount FROM tblfeedback WHERE id='" + id + "'";
                string DateClose = @"SELECT DateClose FROM tblfeedback WHERE id='" + id + "'";
                string Type = @"SELECT Type FROM tblfeedback WHERE id='" + id + "'";
                string Guilty = @"SELECT Guilty FROM tblfeedback WHERE id='" + id + "'";
                string Payer = @"SELECT Payer FROM tblfeedback WHERE id='" + id + "'";

                MySqlCommand cmdFIO = new MySqlCommand(FIO, conn);
                MySqlCommand cmdPhone = new MySqlCommand(Phone, conn);
                MySqlCommand cmdEmail = new MySqlCommand(Email, conn);
                MySqlCommand cmdText = new MySqlCommand(Text, conn);
                MySqlCommand cmdData = new MySqlCommand(Data, conn);
                MySqlCommand cmdSource = new MySqlCommand(Source, conn);
                MySqlCommand cmdUnit = new MySqlCommand(Unit, conn);
                MySqlCommand cmdRest = new MySqlCommand(Rest, conn);
                MySqlCommand cmdRating = new MySqlCommand(Rating, conn);
                MySqlCommand cmdRating2 = new MySqlCommand(Rating2, conn);
                MySqlCommand cmdSotrudnik = new MySqlCommand(Sotrudnik, conn);
                MySqlCommand cmdProblem = new MySqlCommand(Problem, conn);
                MySqlCommand cmdMera = new MySqlCommand(Mera, conn);
                MySqlCommand cmdAnswerForGuest = new MySqlCommand(AnswerForGuest, conn);
                MySqlCommand cmdCost = new MySqlCommand(Cost, conn);
                MySqlCommand cmdCostPoint = new MySqlCommand(CostPoint, conn);
                MySqlCommand cmdCostSert = new MySqlCommand(CostSert, conn);
                MySqlCommand cmdDateClose = new MySqlCommand(DateClose, conn);
                MySqlCommand cmdType = new MySqlCommand(Type, conn);
                MySqlCommand cmdGuilty = new MySqlCommand(Guilty, conn);
                MySqlCommand cmdPayer = new MySqlCommand(Payer, conn);
                MySqlCommand cmdCostDiscount = new MySqlCommand(CostDiscount, conn);

                string resFIO = cmdFIO.ExecuteScalar().ToString();
                model.UserName = resFIO;
                string resPhone = cmdPhone.ExecuteScalar().ToString();
                model.phnumber = resPhone;
                string resEmail = cmdEmail.ExecuteScalar().ToString();
                model.email = resEmail;
                string resText = cmdText.ExecuteScalar().ToString();
                model.textkomm = resText;
                string resData = cmdData.ExecuteScalar().ToString();
                model.NewDate = resData.Substring(0, 10);
                string resSource = cmdSource.ExecuteScalar().ToString();
                model.Source = resSource;//----------------------------источник
                string resUnit = cmdUnit.ExecuteScalar().ToString();
                model.Unit = resUnit;//-----------------------------подразделение
                string resRest = cmdRest.ExecuteScalar().ToString();
                model.Rest = resRest;//-----------------------------ресторан
                string resRating = cmdRating.ExecuteScalar().ToString();
                model.Rating1 = resRating;//----------------------------рейтинг радио-батоны               
                string resRating2 = cmdRating2.ExecuteScalar().ToString();
                model.Rating2 = resRating2;//-----------------------------рейтинг 2
                string resSotrudnik = cmdSotrudnik.ExecuteScalar().ToString();
                model.Staff = resSotrudnik;
                string resProblem = cmdProblem.ExecuteScalar().ToString();
                model.Problem = resProblem;
                string resMera = cmdMera.ExecuteScalar().ToString();
                model.mera = resMera;
                string resAnswerForGuest = cmdAnswerForGuest.ExecuteScalar().ToString();
                model.answer = resAnswerForGuest;
                string resCost = cmdCost.ExecuteScalar().ToString();
                model.Cost = resCost;
                string resCostPoint = cmdCostPoint.ExecuteScalar().ToString();
                model.CostPoint = resCostPoint;
                string resCostSert = cmdCostSert.ExecuteScalar().ToString();
                model.CostSert = resCostSert;
                string resDateClose = cmdDateClose.ExecuteScalar().ToString();
                model.OldDate = resDateClose;
                string resType = cmdType.ExecuteScalar().ToString();
                model.Type = resType;

                string resGuilty = cmdGuilty.ExecuteScalar().ToString();
                model.Guilty = resGuilty;
                string resPayer = cmdPayer.ExecuteScalar().ToString();
                model.Payer = resPayer;
                string resCostDiscount = cmdCostDiscount.ExecuteScalar().ToString();
                model.CostDiscount = resCostDiscount;


                if (testCon)
                {
                    conn.Close();
                    conn.Dispose();
                }

                #endregion
            }
            else
            {
                id = -1;
                model.id = id;
            }
            return View(model);
        }

        //для отправки данных в бд
        [HttpPost]
        [MyAuthorize(Roles = "BonusClubManager, FusionAdmin, Callback")]
        public ActionResult uniform(UniformModel model)
        {
            #region UserData
            //переменная для записи изменённых полей
            string Matching = "";

            int id;
            //id = model.id;
            id = Convert.ToInt32(Request.Form["id"]);
            if (id == 0)
            {
                id = model.id;
            }

            string UserName;
            UserName = model.UserName;
            //%%%%%%%
            //переменная для начального значения поля
            string UserNamematching;
            UserNamematching = Request.Form["hidname"];
            //проверяем, было ли поле изменено
            if (UserName != UserNamematching)
            {
                //если изменено - записываем в переменную какое поле было изменено
                Matching = Matching + "Имя гостя, ";
            }
            //%%%%%%%

            string phnumber;
            phnumber = model.phnumber;
            //%%%%%%%
            //переменная для начального значения поля
            string phnumbermatching;
            phnumbermatching = Request.Form["hidphone"];
            //проверяем, было ли поле изменено
            if (phnumber != phnumbermatching)
            {
                //если изменено - записываем в переменную какое поле было изменено
                Matching = Matching + "Номер тел., ";
            }
            //%%%%%%%

            string email;
            email = model.email;
            //%%%%%%%
            //переменная для начального значения поля
            string emailmatching;
            emailmatching = Request.Form["hidmail"];
            //проверяем, было ли поле изменено
            if (email != emailmatching)
            {
                //если изменено - записываем в переменную какое поле было изменено
                Matching = Matching + "Email,";
            }
            //%%%%%%%

            string SelectedUnit;
            SelectedUnit = Request.Form["unitt"];
            //%%%%%%%
            //переменная для начального значения поля
            string SelectedUnitmatching;
            SelectedUnitmatching = Request.Form["hidunit"];
            if (SelectedUnitmatching == "")
            {
                SelectedUnitmatching = null;
            }
            //проверяем, было ли поле изменено
            if (SelectedUnit != SelectedUnitmatching)
            {
                //если изменено - записываем в переменную какое поле было изменено
                Matching = Matching + "Подразделение, ";
            }
            //%%%%%%%

            //string SelectedUnit; test
            //SelectedUnit = model.SelectedUnit;unit

            string Problem;
            Problem = model.Problem;
            //%%%%%%%
            //переменная для начального значения поля
            string Problemmatching;
            Problemmatching = Request.Form["hidproblem"];
            //проверяем, было ли поле изменено
            if (Problem != Problemmatching)
            {
                //если изменено - записываем в переменную какое поле было изменено
                Matching = Matching + "Подробности проблемы, ";
            }
            //%%%%%%%

            string SelectedRest;
            SelectedRest = Request.Form["restt"];
            //%%%%%%%
            //переменная для начального значения поля
            string SelectedRestmatching;
            SelectedRestmatching = Request.Form["hidrest"];
            //проверяем, было ли поле изменено
            if (SelectedRestmatching == "")
            {
                SelectedRestmatching = null;
            }
            if (SelectedRest != SelectedRestmatching)
            {
                //если изменено - записываем в переменную какое поле было изменено
                Matching = Matching + "Ресторан, ";
            }
            //%%%%%%%
            //string SelectedRest;
            //SelectedRest = model.SelectedRest;

            string Staff;
            Staff = model.Staff;
            //%%%%%%%
            //переменная для начального значения поля
            string Staffmatching;
            Staffmatching = Request.Form["hidstaff"];
            //проверяем, было ли поле изменено
            if (Staffmatching == "")
            {
                Staffmatching = null;
            }
            if (Staff != Staffmatching)
            {
                //если изменено - записываем в переменную какое поле было изменено
                Matching = Matching + "Ответственный, ";
            }
            //%%%%%%%

            string SelectedSource;
            SelectedSource = Request.Form["sourcee"];
            //%%%%%%%
            //переменная для начального значения поля
            string SelectedSourcematching;
            SelectedSourcematching = Request.Form["hidsourse"];
            //проверяем, было ли поле изменено
            if (SelectedSourcematching == "")
            {
                SelectedSourcematching = null;
            }
            if (SelectedSource != SelectedSourcematching)
            {
                //если изменено - записываем в переменную какое поле было изменено
                Matching = Matching + "Источник, ";
            }
            //%%%%%%%
            //string SelectedSource;
            //SelectedSource = model.SelectedSource;            

            string NewDate;
            NewDate = model.NewDate;
            //%%%%%%%
            //переменная для начального значения поля
            string NewDatematching;
            NewDatematching = Request.Form["hiddatenew"];
            //проверяем, было ли поле изменено
            if (NewDate != NewDatematching)
            {
                //если изменено - записываем в переменную какое поле было изменено
                Matching = Matching + "Дата создания, ";
            }
            //%%%%%%%

            string NewTime;
            NewTime = model.NewTime;
            NewDate = NewDate + " " + NewTime;

            //string RegDateNew = Regex.Replace(test, patternDate, test);

            string OldDate;
            OldDate = model.OldDate;
            //%%%%%%%
            //переменная для начального значения поля
            string OldDatematching;
            OldDatematching = Request.Form["hiddateold"];
            //проверяем, было ли поле изменено
            if (OldDatematching == "")
            {
                OldDatematching = null;
            }
            if (OldDate != OldDatematching)
            {
                //если изменено - записываем в переменную какое поле было изменено
                Matching = Matching + "Дата закрытия, ";
            }
            //%%%%%%%

            //string OldDate;
            //OldDate = OldDate + " " + OldTime;

            string Guilty;
            Guilty = model.Guilty;
            //%%%%%%%
            //переменная для начального значения поля
            string Guiltymatching;
            Guiltymatching = Request.Form["hidguilty"];
            //проверяем, было ли поле изменено
            if (Guiltymatching == "")
            {
                Guiltymatching = null;
            }
            if (Guilty != Guiltymatching)
            {
                //если изменено - записываем в переменную какое поле было изменено
                Matching = Matching + "Виновный, ";
            }
            //%%%%%%%

            string SelectedPayer;
            SelectedPayer = Request.Form["payerr"];
            //%%%%%%%
            //переменная для начального значения поля
            string SelectedPayermatching;
            SelectedPayermatching = Request.Form["hidpayer"];
            //проверяем, было ли поле изменено
            if (SelectedPayermatching == "")
            {
                SelectedPayermatching = null;
            }
            if (SelectedPayer != SelectedPayermatching)
            {
                //если изменено - записываем в переменную какое поле было изменено
                Matching = Matching + "Кто заплатил, ";
            }
            //%%%%%%%

            string Rating;
            //Rating = model.Rating1;
            Rating = Request.Form["Rating1"];
            string pattern = @"\w+,+";
            string regular = "";
            Rating = Regex.Replace(Rating, pattern, regular);


            switch (Rating)
            {
                case ",Положительный":
                    Rating = "Положительный";
                    break;
                case ",Отрицательный":
                    Rating = "Отрицательный";
                    break;
                case ",Нейтральный":
                    Rating = "Нейтральный";
                    break;
                case ",Критичный":
                    Rating = "Критичный";
                    break;
                case ",ОТРАВЛЕНИЕ":
                    Rating = "ОТРАВЛЕНИЕ";
                    break;
            }
            //%%%%%%%
            //переменная для начального значения поля
            string Ratingmatching;
            Ratingmatching = Request.Form["hidrating1"];
            //проверяем, было ли поле изменено
            if (Rating != Ratingmatching)
            {
                //если изменено - записываем в переменную какое поле было изменено
                Matching = Matching + "Оценка, ";
            }
            //%%%%%%%

            string SelectedRating2;
            SelectedRating2 = Request.Form["reitingg"];
            //%%%%%%%
            //переменная для начального значения поля
            string SelectedRating2matching;
            SelectedRating2matching = Request.Form["hidrating2"];
            //проверяем, было ли поле изменено
            if (SelectedRating2matching == "")
            {
                SelectedRating2matching = null;
            }
            if (SelectedRating2 != SelectedRating2matching)
            {
                //если изменено - записываем в переменную какое поле было изменено
                Matching = Matching + "Оценка2, ";
            }
            //%%%%%%%
            //string SelectedRating2;
            //SelectedRating2 = model.SelectedRating2;

            string textkomm;
            textkomm = model.textkomm;
            //%%%%%%%
            //переменная для начального значения поля
            string textkommmatching;
            textkommmatching = Request.Form["hidtext"];
            //проверяем, было ли поле изменено
            if (textkomm != textkommmatching)
            {
                //если изменено - записываем в переменную какое поле было изменено
                Matching = Matching + "Текст, ";
            }
            //%%%%%%%

            string mera;
            mera = model.mera;
            //%%%%%%%
            //переменная для начального значения поля
            string meramatching;
            meramatching = Request.Form["hidmera"];
            //проверяем, было ли поле изменено
            if (meramatching == "")
            {
                meramatching = null;
            }
            if (mera != meramatching)
            {
                //если изменено - записываем в переменную какое поле было изменено
                Matching = Matching + "Меры, ";
            }
            //%%%%%%%

            string answer;
            answer = model.answer;
            //%%%%%%%
            //переменная для начального значения поля
            string answermatching;
            answermatching = Request.Form["hidanswer"];
            //проверяем, было ли поле изменено
            if (answermatching == "")
            {
                answermatching = null;
            }
            if (answer != answermatching)
            {
                //если изменено - записываем в переменную какое поле было изменено
                Matching = Matching + "Решение для гостя, ";
            }
            //%%%%%%%

            string Cost;
            Cost = model.Cost;
            //%%%%%%%
            //переменная для начального значения поля
            string Costmatching;
            Costmatching = Request.Form["hidcost1"];
            //проверяем, было ли поле изменено
            if (Cost != Costmatching)
            {
                //если изменено - записываем в переменную какое поле было изменено
                Matching = Matching + "Цена в руб., ";
            }
            //%%%%%%%

            string CostPoint;
            CostPoint = model.CostPoint;
            //%%%%%%%
            //переменная для начального значения поля
            string CostPointmatching;
            CostPointmatching = Request.Form["hidcost2"];
            //проверяем, было ли поле изменено
            if (CostPoint != CostPointmatching)
            {
                //если изменено - записываем в переменную какое поле было изменено
                Matching = Matching + "Цена в баллах, ";
            }
            //%%%%%%%

            string CostSert;
            CostSert = model.CostSert;
            string CostDiscount = model.CostDiscount;
            //%%%%%%%
            //переменная для начального значения поля
            string CostSertmatching;
            CostSertmatching = Request.Form["hidcost3"];
            //проверяем, было ли поле изменено
            if (CostSert != CostSertmatching)
            {
                //если изменено - записываем в переменную какое поле было изменено
                Matching = Matching + "Цена сертификатов, ";
            }
            //%%%%%%%

            string Type;
            Type = Request.Form["types"];
            //%%%%%%%
            //переменная для начального значения поля
            string Typematching;
            Typematching = Request.Form["hidtype"];
            //проверяем, было ли поле изменено
            if (Typematching == "")
            {
                Typematching = null;
            }
            if (Type != Typematching)
            {
                //если изменено - записываем в переменную какое поле было изменено
                Matching = Matching + "Тип причины, ";
            }
            //%%%%%%%

            string Theme; //разобарться зачем заводил


            #endregion

            #region dbConn
            string connPRTS = WebConfigurationManager.ConnectionStrings["FeedbackConnectionString"].ConnectionString;
            MySqlConnection conn = new MySqlConnection(connPRTS);
            bool testCon = true;
            try
            {
                conn.Open();
            }
            catch
            {
                testCon = false;
            }
            string UserLog;
            UserLog = User.Identity.GetUserName();
            string dbSTR;
            string dbSTR2;
            bool ind = false;
            string dbSTRid; //для определения id последнего добавленого отзыва
            dbSTR2 = @"INSERT logTable SET Name='" + UserLog + "',Date='" + DateTime.Now + "',Ident='" + id + "',Fields='" + Matching + "',Text='" + textkomm + "'";
            dbSTRid = @"SELECT LAST_INSERT_ID() FROM tblfeedback";
            //проверяем id отзыва
            //idbridge
            if (id >= 0)
            {
                dbSTR = @"UPDATE tblfeedback SET CostDiscount='"+ CostDiscount +"',FIO='" + UserName + "',Phone='" + phnumber + "',Email='" + email + "',Text='" + textkomm + "',Data='" + NewDate + "',Source='" + SelectedSource + "',Unit='" + SelectedUnit + "',Rest='" + SelectedRest + "',Rating='" + Rating + "',Rating2='" + SelectedRating2 + "',Sotrudnik='" + Staff + "',Problem='" + Problem + "',Mera='" + mera + "',AnswerForGuest='" + answer + "',Cost='" + Cost + "',CostPoint='" + CostPoint + "',CostSert='" + CostSert + "',DateClose='" + OldDate + "', Type='" + Type + "', Guilty='" + Guilty + "', Payer='" + SelectedPayer + "'  WHERE id='" + id + "'";
            }
            else
            {
                dbSTR = @"INSERT INTO tblfeedback SET CostDiscount = '"+ CostDiscount +"',FIO='" + UserName + "',Phone='" + phnumber + "',Email='" + email + "',Text='" + textkomm + "',Data='" + NewDate + "',Source='" + SelectedSource + "',Unit='" + SelectedUnit + "',Rest='" + SelectedRest + "',Rating='" + Rating + "',Rating2='" + SelectedRating2 + "',Sotrudnik='" + Staff + "',Problem='" + Problem + "',Mera='" + mera + "',AnswerForGuest='" + answer + "',Cost='" + Cost + "',CostPoint='" + CostPoint + "',CostSert='" + CostSert + "',DateClose='" + OldDate + "', Type='" + Type + "', Guilty='" + Guilty + "', Payer='" + SelectedPayer + "'";
                ind = true;
            }

            MySqlCommand cmd = new MySqlCommand(dbSTR, conn);
            MySqlCommand cmd2 = new MySqlCommand(dbSTR2, conn);
            MySqlCommand cmdID = new MySqlCommand(dbSTRid, conn);

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                int count = cmd.ExecuteNonQuery();
                int count2 = cmd2.ExecuteNonQuery();

                if (ind)
                {
                    string dbID = cmdID.ExecuteScalar().ToString();
                    //отправляем письмо(а) о новом отзыве
                    MailMessage mail = new MailMessage();
                    string FROM = "feedback_vega_tokyo@tokyo-bar.ru";
                    string TO = "website_tokyo@tokyo-bar.ru"; //website_tokyo@tokyo-bar.ru
                    mail.Body = "Пришел новый отзыв от: " + NewDate + ", от имени: " + UserName + ", созданный: " + UserLog + ".\r\nТекст отзыва:" + textkomm + "\r\nОтзыв в Vega: http://vega/Callback/uniform?updorins=" + dbID + "";
                    mail.From = new MailAddress(FROM);
                    mail.To.Add(new MailAddress(TO));
                    mail.Subject = "Новый отзыв"; //добавить свич-кейс для определения ресторана и назначении разных тем письма
                    SmtpClient client = new SmtpClient();//ivermak@tokyo-bar.ru,ag@tokyo-bar.ru
                    client.Host = "srv-ex00.fg.local";//smtp.mail.ru
                    client.Port = 587; //587
                    client.EnableSsl = false;
                    //для mail.ru - не сплитовать (полный адрес), для gmail и yandex - сплитовать
                    client.Credentials = new NetworkCredential(FROM, "OhUjdkku37L"); //FROM.Split('@')[0]
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.Send(mail);
                    mail.Dispose();
                }

            }

            if (testCon)
            {
                conn.Close();
                conn.Dispose();
            }

            #endregion

            return Redirect("~/Callback/viewtable");
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult SendEmail(string MailBody)
        {
            //string message = System.Web.HttpUtility.HtmlDecode(MailBody);
            MailMessage mail = new MailMessage();
            string FROM = "feedback_vega_tokyo@tokyo-bar.ru";
            string TO = "website_tokyo@tokyo-bar.ru"; //website_tokyo@tokyo-bar.ru
            mail.Body = MailBody;
            mail.From = new MailAddress(FROM);
            mail.To.Add(new MailAddress(TO));
            mail.Subject = "Новый отзыв с сайта";
            mail.SubjectEncoding = Encoding.UTF8;
            mail.BodyEncoding = Encoding.UTF8;
            mail.IsBodyHtml = true;
            SmtpClient client = new SmtpClient();
            client.Host = "srv-ex00.fg.local";
            client.Port = 587; //587
            client.EnableSsl = false;
            client.Credentials = new NetworkCredential(FROM, "OhUjdkku37L");
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            try
            {
                client.Send(mail);
            }
            catch (Exception ex)
            {
                throw new Exception("Упс! Письмо с отзывом не отправилось! Обратитесь к администратору почтового сервера",ex);
            }            
            mail.Dispose();
            return View();
        }
        /*public ActionResult Index(DateTime? date_start, DateTime? date_end)
        {
            FeedbackModel model = new FeedbackModel();
            if (date_start == null)
                date_start = DateTime.Today.AddDays(-5);
            if (date_end == null)
                date_end = DateTime.Today;
            model.Date_Start = date_start;
            model.Date_End = date_end;
            model.GetInfo();
            model.GetFeedbackList(date_start, date_end);
            return View(model);
        }
        [HttpPost]
        public ActionResult Index(FeedbackModel model)
        {
            return RedirectToAction("Index", new { date_start = model.Date_Start, date_end = model.Date_End });
        }
        public ActionResult EditFeedback(int? id)
        {
            FeedbackModel model = new FeedbackModel();
            model.GetInfo();
            if(id != null)
            {
                model.GetFeedback(id);
            }
            else
            {
                model.CreateFeedback();
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult EditFeedback(FeedbackModel model)
        {
            model.SaveFeedback(model.Feedback);
            return RedirectToAction("Index");
        }*/
        [HttpPost]
        public ActionResult Export()
        {
            TblViewModel.TblsModelDown model = new TblViewModel.TblsModelDown();
            model.Search();
            /*CsvExport export = new CsvExport();
            foreach(var it in model.persons)
            {
                export.AddRow();
                export["ФИО"] = it.FIO;
                export["Телефон"] = it.Phone;
                export["Ресторан"] = it.Rest;
                export["Виновный"] = it.Guilty;
                export["Ответственный"] = it.Employee;
                export["Источник"] = it.Source;
                export["Дата создания"] = it.Data;
                export["Дата закрытия"] = it.DateClose;
                export["Текст отзыва"] = it.Text;
                export["Принятые меры"] = it.Mera;
                export["Решение для гостя"] = it.Answer;
            }
            //return View();
            string filename = String.Format("Отчет по ОС за {0} - {1}.csv", model.DateNEW1, model.DateNEW2);
            return File(export.ExportToBytesWin(), "text/csv", filename);*/
            var list = new System.Data.DataTable();
            list.Columns.Add("ФИО", typeof(string));
            list.Columns.Add("Телефон", typeof(string));
            list.Columns.Add("Ресторан", typeof(string));
            list.Columns.Add("Виновный", typeof(string));
            list.Columns.Add("Ответственный", typeof(string));
            list.Columns.Add("Источник", typeof(string));
            list.Columns.Add("Дата создания", typeof(string));
            list.Columns.Add("Дата закрытия", typeof(string));
            list.Columns.Add("Текст отзыва", typeof(string));
            list.Columns.Add("Принятые меры", typeof(string));
            list.Columns.Add("Решение для гостя", typeof(string));
            foreach (var it in model.persons)
            {
                System.Data.DataRow row;
                row = list.NewRow();
                row["ФИО"] = it.FIO;
                row["Телефон"] = it.Phone;
                row["Ресторан"] = it.Rest;
                row["Виновный"] = it.Guilty;
                row["Ответственный"] = it.Employee;
                row["Источник"] = it.Source;
                row["Дата создания"] = it.Data;
                row["Дата закрытия"] = it.DateClose;
                row["Текст отзыва"] = it.Text;
                row["Принятые меры"] = it.Mera;
                row["Решение для гостя"] = it.Answer;
                list.Rows.Add(row);
            }
            var grid = new GridView();
            grid.DataSource = list;
            grid.DataBind();
            grid.HeaderRow.BackColor = Color.FromArgb(221, 221, 221);
            Response.ClearContent();
            Response.Buffer = true;
            string attachment = String.Format("attachment; filename=Отчет за {0} - {1}.xls", model.DateNEW1, model.DateNEW2);
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/vnd.ms-excel";
            Response.ContentEncoding = System.Text.Encoding.Unicode;
            Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());
            Response.Charset = "";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            grid.RenderControl(htw);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
            return View();
        }
    }
}