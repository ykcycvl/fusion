using System;
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
            string UserLog; 
            UserLog = User.Identity.GetUserName();
            string dbSTR;
            string dbSTR2;
            dbSTR2 = @"INSERT logTable SET Name='" + UserLog + "',Date='" + DateTime.Now + "',Ident='" + id +"',Fields='" + Matching + "',Text='" + textkomm + "'";
            //проверяем id отзыва
            //idbridge
            if (id >= 0)
            {
                dbSTR = @"UPDATE tblfeedback SET FIO='" + UserName + "',Phone='" + phnumber + "',Email='" + email + "',Text='" + textkomm + "',Data='" + NewDate + "',Source='" + SelectedSource + "',Unit='" + SelectedUnit + "',Rest='" + SelectedRest + "',Rating='" + Rating + "',Rating2='" + SelectedRating2 + "',Sotrudnik='" + Staff + "',Problem='" + Problem + "',Mera='" + mera + "',AnswerForGuest='" + answer + "',Cost='" + Cost + "',CostPoint='" + CostPoint + "',CostSert='" + CostSert + "',DateClose='" + OldDate + "', Type='" + Type + "', Guilty='" + Guilty + "', Payer='" + SelectedPayer + "'  WHERE id='" + id + "'";

            }
            else
            {
                dbSTR = @"INSERT tblfeedback SET FIO='" + UserName + "',Phone='" + phnumber + "',Email='" + email + "',Text='" + textkomm + "',Data='" + NewDate + "',Source='" + SelectedSource + "',Unit='" + SelectedUnit + "',Rest='" + SelectedRest + "',Rating='" + Rating + "',Rating2='" + SelectedRating2 + "',Sotrudnik='" + Staff + "',Problem='" + Problem + "',Mera='" + mera + "',AnswerForGuest='" + answer + "',Cost='" + Cost + "',CostPoint='" + CostPoint + "',CostSert='" + CostSert + "',DateClose='" + OldDate + "', Type='" + Type + "', Guilty='" + Guilty + "', Payer='" + SelectedPayer + "'";

            }
            MySqlCommand cmd = new MySqlCommand(dbSTR, conn);
            MySqlCommand cmd2 = new MySqlCommand(dbSTR2, conn);

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                int count = cmd.ExecuteNonQuery();
                int count2 = cmd2.ExecuteNonQuery();
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
