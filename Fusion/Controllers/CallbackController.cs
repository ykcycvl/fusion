using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Fusion.Models.Callback;
using System.Data.SqlClient;
using System.Data;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;

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
                string connPRTS = @"server=192.168.0.102;user Id=feedback;database=feedback;port=3306;password=73915;";
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

            int id;
            //id = model.id;
            id = Convert.ToInt32(Request.Form["id"]);
            if(id==0)
            {
                id = model.id;
            }
            
            string UserName;
            UserName = model.UserName;

            string phnumber;
            phnumber = model.phnumber;

            string email;
            email = model.email;

            string SelectedUnit;
            SelectedUnit = Request.Form["unitt"];
            
            //string SelectedUnit; test
            //SelectedUnit = model.SelectedUnit;unit

            string Problem;
            Problem = model.Problem;

            string SelectedRest;
            SelectedRest = Request.Form["restt"];
            //string SelectedRest;
            //SelectedRest = model.SelectedRest;

            string Staff;
            Staff = model.Staff;

            string SelectedSource;
            SelectedSource = Request.Form["sourcee"];
            //string SelectedSource;
            //SelectedSource = model.SelectedSource;            

            string NewDate;
            NewDate = model.NewDate;

            string NewTime;
            NewTime = model.NewTime;
            NewDate = NewDate +" "+ NewTime;
            
            //string RegDateNew = Regex.Replace(test, patternDate, test);

            string OldDate;
            OldDate = model.OldDate;

            //string OldDate;
            //OldDate = OldDate + " " + OldTime;
            
            string Guilty;
            Guilty = model.Guilty;

            string SelectedPayer;
            SelectedPayer = Request.Form["payerr"];

            string Rating;
            //Rating = model.Rating1;
            Rating = Request.Form["Rating1"];
            string pattern = @"\w+,+";
            string regular = "";
            Rating = Regex.Replace(Rating, pattern, regular);


            switch(Rating)
            {
                case ",Положительный":
                    Rating = "Положительный";
                    break;
                case ",Отрицательный":
                    Rating = "Отрицательный";
                    break;
                case ",Критичный":
                    Rating = "Критичный";
                    break;
                case ",ОТРАВЛЕНИЕ":
                    Rating = "ОТРАВЛЕНИЕ";
                    break;
            }

            string SelectedRating2;
            SelectedRating2 = Request.Form["reitingg"];
            //string SelectedRating2;
            //SelectedRating2 = model.SelectedRating2;

            string textkomm;
            textkomm = model.textkomm;

            string mera;
            mera = model.mera;

            string answer;
            answer = model.answer;

            string Cost;
            Cost = model.Cost;

            string CostPoint;
            CostPoint = model.CostPoint;

            string CostSert;
            CostSert = model.CostSert;

            string Type;
            Type = Request.Form["types"];

            string Theme; //разобарться зачем заводил
            

            #endregion

            #region dbConn
            string connPRTS = @"server=192.168.0.102;user=feedback;database=feedback;port=3306;password=73915;";
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

            string dbSTR;
            //проверяем id отзыва
            //idbridge
            if (id>=0)
            {
                dbSTR = @"UPDATE tblfeedback SET FIO='" + UserName + "',Phone='" + phnumber + "',Email='" + email + "',Text='" + textkomm + "',Data='" + NewDate + "',Source='" + SelectedSource + "',Unit='" + SelectedUnit + "',Rest='" + SelectedRest + "',Rating='" + Rating + "',Rating2='" + SelectedRating2 + "',Sotrudnik='" + Staff + "',Problem='" + Problem + "',Mera='" + mera + "',AnswerForGuest='" + answer + "',Cost='" + Cost + "',CostPoint='" + CostPoint + "',CostSert='" + CostSert + "',DateClose='" + OldDate + "', Type='" + Type + "', Guilty='" + Guilty + "', Payer='" + SelectedPayer + "'  WHERE id='" + id + "'";

            }
            else
            {
                dbSTR = @"INSERT tblfeedback SET FIO='" + UserName + "',Phone='" + phnumber + "',Email='" + email + "',Text='" + textkomm + "',Data='" + NewDate + "',Source='" + SelectedSource + "',Unit='" + SelectedUnit + "',Rest='" + SelectedRest + "',Rating='" + Rating + "',Rating2='" + SelectedRating2 + "',Sotrudnik='" + Staff + "',Problem='" + Problem + "',Mera='" + mera + "',AnswerForGuest='" + answer + "',Cost='" + Cost + "',CostPoint='" + CostPoint + "',CostSert='" + CostSert + "',DateClose='" + OldDate + "', Type='" + Type + "', Guilty='" + Guilty + "', Payer='" + SelectedPayer + "'";

            }
            MySqlCommand cmd = new MySqlCommand(dbSTR, conn);

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                int count = cmd.ExecuteNonQuery();
            }

            if (testCon)
            {
                conn.Close();
                conn.Dispose();
            }

            #endregion

            return Redirect("~/Callback/viewtable");
        }
	}
}
