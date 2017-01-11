using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.WebPages.Html;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Web;
using System.Xml.Serialization;
using System.IO;
using System.Web.Configuration;
using System.ComponentModel;
using System.Globalization;

namespace Fusion.Models.Callback
{
    //ФОРМА ДЛЯ ГОСТЕЙ
    public class UserFormModel
    {
        //фио гостя     
        [Required(ErrorMessage = "Пожалуйста введите ФИО")]
        [RegularExpression(@"[а-яА-Я]+\s[а-яА-Я]+\s[а-яА-Я]+", ErrorMessage = "Введите свои ФИО")]
        [StringLength(35, MinimumLength = 3, ErrorMessage = "Пожалуйста введите ФИО")]
        [Display(Name = "UserName")]
        //[DataType(DataType.Text)]
        public string UserName { get; set; }

        //№ телефона гостя
        [Required(ErrorMessage = "Введите № телефона")]
        [RegularExpression(@"89[0-9]{2}[0-9]{3}[0-9]{2}[0-9]{2}", ErrorMessage = "Введите № телефона в виде 89001110011")]
        [StringLength(11, MinimumLength = 5, ErrorMessage = "Номер должен содержать 11 цифр")]
        [DataType(DataType.PhoneNumber)]
        public string phnumber2 { get; set; }

        //сам отзыв
        //[Display(Name = "Текст комментария")]
        [Required(ErrorMessage = "Пожалуйста напишите ваш отзыв")]
        [StringLength(300, MinimumLength = 5, ErrorMessage = "Пожалуйста напишите ваш отзыв")]
        [DataType(DataType.MultilineText)]
        public string textkomm { get; set; }

        //дата
        [Required(ErrorMessage = "Введите дату визита")]
        //[RegularExpression(@"\d{2}.?\d{2}.?\d{4}", ErrorMessage = "Введите дату")]
        [DataType(DataType.Date)]
        public string MyDate { get; set; }

        //выпадаюий список для категории отзыва
        private List<SelectListItem> _categories = new List<SelectListItem>();
        [Required(ErrorMessage = "Пожалуйста выберите категорию")]
        public string SelectedCategory { get; set; }

        public List<SelectListItem> Categories
        {
            get
            {
                _categories.Add(new SelectListItem() { Text = "Качество", Value = "1" });
                _categories.Add(new SelectListItem() { Text = "Сервис", Value = "2" });
                _categories.Add(new SelectListItem() { Text = "Скорость обслуживания", Value = "3" });
                _categories.Add(new SelectListItem() { Text = "Другое", Value = "4" });
                return _categories;
            }
        }

        //выпадаюий список для доставки/выноса
        private List<SelectListItem> _reception = new List<SelectListItem>();
        [Required(ErrorMessage = "Пожалуйста выберите способ получения заказа")]
        public string SelectedReception { get; set; }

        public List<SelectListItem> Reception
        {
            get
            {
                _reception.Add(new SelectListItem() { Text = "Доставка", Value = "1" });
                _reception.Add(new SelectListItem() { Text = "Вынос", Value = "2" });
                return _reception;
            }
        }

        //выпадаюий список для ресторанов
        private List<SelectListItem> _rest = new List<SelectListItem>();
        [Required(ErrorMessage = "Пожалуйста выберите ресторан")]
        public string SelectedRest { get; set; }

        public List<SelectListItem> Rest
        {
            get
            {
                _rest.Add(new SelectListItem() { Text = "Светланская 183 В", Value = "1" });
                _rest.Add(new SelectListItem() { Text = "Пр-т Острякова, 8", Value = "2" });
                _rest.Add(new SelectListItem() { Text = "Светланская, 121", Value = "3" });
                _rest.Add(new SelectListItem() { Text = "Пр-т 100 лет Владивостоку, 50А", Value = "4" });
                _rest.Add(new SelectListItem() { Text = "Семеновская, 7В", Value = "5" });
                _rest.Add(new SelectListItem() { Text = "Уссурийск", Value = "6" });
                _rest.Add(new SelectListItem() { Text = "Находка", Value = "7" });
                return _rest;
            }
        }

        //выпадаюий список для оценок
        private List<SelectListItem> _reit = new List<SelectListItem>();
        [Required(ErrorMessage = "Пожалуйста выберите оценку")]
        public string SelectedReit { get; set; }

        public List<SelectListItem> Reit
        {
            get
            {
                _reit.Add(new SelectListItem() { Text = "Отлично", Value = "1" });
                _reit.Add(new SelectListItem() { Text = "Хорошо", Value = "2" });
                _reit.Add(new SelectListItem() { Text = "Средне", Value = "3" });
                _reit.Add(new SelectListItem() { Text = "Плохо", Value = "4" });
                _reit.Add(new SelectListItem() { Text = "Ужасно", Value = "5" });
                return _reit;
            }
        }



    }


    //ФОРМА ДЛЯ ПЕРСОНАЛА
    public class StaffFormModel
    {
        //фио гостя
        [Required(ErrorMessage = "Введите ФИО гостя")]
        [RegularExpression(@"[а-яА-Я]+\s[а-яА-Я]+\s[а-яА-Я]+", ErrorMessage = "Введите ФИО гостя")]
        [StringLength(35, MinimumLength = 3, ErrorMessage = "Введите ФИО гостя")]
        [Display(Name = "UserName")]
        public string UserName { get; set; }

        //№ телефона гостя
        [Required(ErrorMessage = "Введите № телефона")]
        [RegularExpression(@"89[0-9]{2}[0-9]{3}[0-9]{2}[0-9]{2}", ErrorMessage = "Введите № телефона в виде 89001110011")]
        [StringLength(11, MinimumLength = 5, ErrorMessage = "Номер должен содержать 11 цифр")]
        [DataType(DataType.PhoneNumber)]
        public string phnumber { get; set; }

        //сам отзыв
        [Required(ErrorMessage = "Напишите отзыв")]
        [StringLength(18000,MinimumLength = 5, ErrorMessage = "Слишком короткий отзыв")]
        [DataType(DataType.MultilineText)]
        public string textkomm { get; set; }

        //фио сотрудника
        [Required(ErrorMessage = "Пожалуйста введите ФИО")]
        [RegularExpression(@"[а-яА-Я]+\s[а-яА-Я]+\s[а-яА-Я]+", ErrorMessage = "Введите свои ФИО")]
        [StringLength(35, MinimumLength = 3, ErrorMessage = "Пожалуйста введите ФИО")]
        [Display(Name = "Staff")]
        public string Staff { get; set; }

        //дата
        //public string MyDate { get; set; }

        //дата
        [Required(ErrorMessage = "Введите дату визита")]
        [DataType(DataType.Date)]
        public string MyDate { get; set; }

        //выпадаюий список для категории отзыва
        private List<SelectListItem> _categories = new List<SelectListItem>();
        [Required(ErrorMessage = "Пожалуйста выберите категорию")]
        public string SelectedCategory { get; set; }

        public List<SelectListItem> Categories
        {
            get
            {
                _categories.Add(new SelectListItem() { Text = "Качество", Value = "1" });
                _categories.Add(new SelectListItem() { Text = "Сервис", Value = "2" });
                _categories.Add(new SelectListItem() { Text = "Скорость обслуживания", Value = "3" });
                _categories.Add(new SelectListItem() { Text = "Другое", Value = "4" });
                return _categories;
            }
        }

        //выпадаюий список для доставки/выноса
        private List<SelectListItem> _reception = new List<SelectListItem>();
        [Required(ErrorMessage = "Пожалуйста выберите способ получения заказа")]
        public string SelectedReception { get; set; }

        public List<SelectListItem> Reception
        {
            get
            {
                _reception.Add(new SelectListItem() { Text = "Доставка", Value = "1" });
                _reception.Add(new SelectListItem() { Text = "Вынос", Value = "2" });
                return _reception;
            }
        }

        //выпадаюий список для ресторанов
        private List<SelectListItem> _rest = new List<SelectListItem>();
        [Required(ErrorMessage = "Пожалуйста выберите ресторан")]
        public string SelectedRest { get; set; }

        public List<SelectListItem> Rest
        {
            get
            {
                _rest.Add(new SelectListItem() { Text = "Светланская 183 В", Value = "1" });
                _rest.Add(new SelectListItem() { Text = "Пр-т Острякова, 8", Value = "2" });
                _rest.Add(new SelectListItem() { Text = "Светланская, 121", Value = "3" });
                _rest.Add(new SelectListItem() { Text = "Пр-т 100 лет Владивостоку, 50А", Value = "4" });
                _rest.Add(new SelectListItem() { Text = "Семеновская, 7В", Value = "5" });
                _rest.Add(new SelectListItem() { Text = "Уссурийск", Value = "6" });
                _rest.Add(new SelectListItem() { Text = "Находка", Value = "7" });
                return _rest;
            }
        }

        //выпадаюий список для оценок
        private List<SelectListItem> _reit = new List<SelectListItem>();
        [Required(ErrorMessage = "Пожалуйста выберите оценку")]
        public string SelectedReit { get; set; }

        public List<SelectListItem> Reit
        {
            get
            {
                _reit.Add(new SelectListItem() { Text = "Отлично", Value = "1" });
                _reit.Add(new SelectListItem() { Text = "Хорошо", Value = "2" });
                _reit.Add(new SelectListItem() { Text = "Средне", Value = "3" });
                _reit.Add(new SelectListItem() { Text = "Плохо", Value = "4" });
                _reit.Add(new SelectListItem() { Text = "Ужасно", Value = "5" });
                return _reit;
            }
        }

        //выпадаюий список для источника отзывов
        private List<SelectListItem> _source = new List<SelectListItem>();
        [Required(ErrorMessage = "Пожалуйста выберите источник")]
        public string SelectedSource { get; set; }

        public List<SelectListItem> Source
        {
            get
            {
                _source.Add(new SelectListItem() { Text = "Сайт", Value = "1" });
                _source.Add(new SelectListItem() { Text = "Инста", Value = "2" });
                _source.Add(new SelectListItem() { Text = "Соцсети", Value = "3" });
                _source.Add(new SelectListItem() { Text = "Телефон с чека", Value = "4" });
                _source.Add(new SelectListItem() { Text = "Лично/книга жалоб", Value = "5" });
                return _source;
            }
        }



    }

    //Вывод списка отзывов
    public class TblViewModel
    {
        public class TblViewM
        {
            public int id { get; set; }
            public string FIO { get; set; }
            public string Data { get; set; }
            public string Phone { get; set; }
            public string Unit { get; set; }
            public string Rest { get; set; }
            public string Rating { get; set; }

            public string DateClose { get; set; }
            public string Cost { get; set; }
            public string CostPoint { get; set; }
            public string CostSert { get; set; }
            public string Type { get; set; }

            public string Source { get; set; }
        }

        public class TblsModelDown
        {
            [Required(ErrorMessage = "Введите начальную дату")]
            [DataType(DataType.Date)]
            public string DateNEW1 { get; set; }

            [Required(ErrorMessage = "Введите конечную дату")]
            [DataType(DataType.Date)]
            public string DateNEW2 { get; set; }

            private static MySqlConnection GetConnect()
            {
                string connPRTS = @"server=192.168.0.102;User Id=feedback;database=feedback;port=3306;password=73915;";
                MySqlConnection conn = new MySqlConnection(connPRTS);
                conn.Open();
                return conn;
            }

            public List<TblViewM> persons = new List<TblViewM>();

            public void Search() /*++++++++++++++++++++++++++++++++++++++++++++++++++*/
            {
                
                string time = " 23:59:59";
                MySqlConnection con = GetConnect();
                MySqlCommand command;
                if (DateNEW1 == null && DateNEW2 == null)
                {                    
                    DateTime now = DateTime.Now;
                    //DateTime.TryParseExact(now, "YY-mm-dd", null,DateTimeStyles.NoCurrentDateDefault, now);
                    DateTime yesterday = DateTime.Now.Subtract(new TimeSpan(10, 0, 0, 0));
                    DateNEW1 = Convert.ToString(yesterday);
                    DateNEW1 = (DateNEW1.Substring(6, 4))+"-" + (DateNEW1.Substring(3, 2))+ "-" + (DateNEW1.Substring(0, 2));
                    DateNEW2 = Convert.ToString(now);
                    string DateNEW2tosql;
                    if(DateNEW2.Count()>=19)
                    {
                        DateNEW2tosql = (DateNEW2.Substring(6, 4)) + "-" + (DateNEW2.Substring(3, 2)) + "-" + (DateNEW2.Substring(0, 2)) + " " + (DateNEW2.Substring(11, 8));
                    }
                    else
                    {
                        DateNEW2tosql = (DateNEW2.Substring(6, 4)) + "-" + (DateNEW2.Substring(3, 2)) + "-" + (DateNEW2.Substring(0, 2)) + " 0" + (DateNEW2.Substring(11, 7));
                    }
                    DateNEW2 = (DateNEW2.Substring(6, 4)) + "-" + (DateNEW2.Substring(3, 2)) + "-" + (DateNEW2.Substring(0, 2));
                    command = new MySqlCommand(@"SELECT id, FIO, Data, Phone, Unit, Rest, DateClose, Rating, Cost, CostPoint, CostSert, Type, Source FROM tblfeedback WHERE Data BETWEEN '" + DateNEW1 + "' AND '" + DateNEW2tosql + "'", con);
                }
                else
                {
                    command = new MySqlCommand(@"SELECT id, FIO, Data, Phone, Unit, Rest, DateClose, Rating, Cost, CostPoint, CostSert, Type, Source FROM tblfeedback WHERE Data BETWEEN '" + DateNEW1 + "' AND '" + DateNEW2 + time + "'", con);
                }
                //command.Parameters.AddWithValue("id", );

                MySqlDataReader rdr = command.ExecuteReader();
                
                if (rdr.HasRows)
                {
                    foreach (DbDataRecord record in rdr)
                    {
                        TblViewM p = new TblViewM();

                        if (record["id"] != null)
                            p.id = Convert.ToInt32(record["id"]);
                        if (record["FIO"] != null)
                            p.FIO = Convert.ToString(record["FIO"]);
                        if (record["Data"] != null)
                        {
                            string Date = Convert.ToString(record["Data"]);
                            string year = Date.Substring(0,4);
                            string month = Date.Substring(5,2);
                            string day = Date.Substring(8,2);
                            Date = day + "." + month + "." + year;
                            p.Data = Date;
                        }
                           
                        if (record["Phone"] != null)
                            p.Phone = Convert.ToString(record["Phone"]);
                        if (record["Unit"] != null)
                            p.Unit = Convert.ToString(record["Unit"]);
                        if (record["Rest"] != null)
                            p.Rest = Convert.ToString(record["Rest"]);
                        if (record["Rating"] != null)
                            p.Rating = Convert.ToString(record["Rating"]);

                        if (Convert.ToString(record["DateClose"]) != "")
                        {
                            string DateClose = Convert.ToString(record["DateClose"]);
                            string year = DateClose.Substring(0, 4);
                            string month = DateClose.Substring(5, 2);
                            string day = DateClose.Substring(8, 2);
                            DateClose = day + "." + month + "." + year;
                            //p.DateClose = Convert.ToString(record["DateClose"]);
                            p.DateClose = DateClose;
                        }

                        if (Convert.ToString(record["Cost"]) != "")
                            p.Cost = Convert.ToString(record["Cost"]);
                        if (Convert.ToString(record["CostPoint"]) != "")
                            p.CostPoint = Convert.ToString(record["CostPoint"]);
                        if (Convert.ToString(record["CostSert"]) != "")
                            p.CostSert = Convert.ToString(record["CostSert"]);

                        if (Convert.ToString(record["Type"]) != "")
                            p.Type = Convert.ToString(record["Type"]);

                        if (Convert.ToString(record["Source"]) != "")
                            p.Source = Convert.ToString(record["Source"]);
                            
                        persons.Add(p);
               
                    }
                    persons.Reverse();
                }

                rdr.Close();
                con.Close();
            }

        }
    }

    public class testmodel
    {

    }

    //ЕДИНАЯ форма для редактирования/просмотра
    public class UniformModel
    {
        //id отзыва
        public int id { get; set; }

        //фио гостя
        [Required(ErrorMessage = "Введите ФИО гостя")]//добавить латиницу, проверить регулярки
        [RegularExpression(@"([а-яА-Я]+\s[а-яА-Я]+\s[а-яА-Я]+)|([а-яА-Я]+\s[а-яА-Я]+)|([а-яА-Я]+)", ErrorMessage = "Введите ФИО гостя")]
        [StringLength(35, MinimumLength = 2, ErrorMessage = "Введите ФИО гостя")]
        [Display(Name = "UserName")]
        public string UserName { get; set; }

        //№ телефона гостя
        [Required(ErrorMessage = "Введите № телефона")]
        [RegularExpression(@"(89\d{2}\d{3}\d{2}\d{2})|(\+7.?\(\d{3}\).?\d{2,3}-\d{2,3}-\d{2,3})|(8\(\d{3}\).?\d{2,3}-\d{2,3}-\d{2,3})|(\+79\d{2}\d{3}\d{2}\d{2})", ErrorMessage = "Введите № телефона в виде 89001110011 либо +79001110011")]
        [StringLength(18, MinimumLength = 6, ErrorMessage = "Номер должен содержать 6-11 цифр")]
        [DataType(DataType.PhoneNumber)]
        public string phnumber { get; set; }

        //email гостя
        //[Required(ErrorMessage = "Введите email гостя")]
        //a-z{5,20}\@a-z{4,10}\.a-z{2,3}
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Введите email-адрес в формате iam@gmail.com")]
        [StringLength(40, MinimumLength = 0, ErrorMessage = "Введите корректный email-адрес")]
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }

        //выпадаюий список для подразделений
        public string Unit { get; set; }
       

        //детали проблемы
        [Required(ErrorMessage = "Введите детали проблемы")]
        [StringLength(254, MinimumLength = 0, ErrorMessage = "Введите детали проблемы")]
        [Display(Name = "Problem")]
        public string Problem { get; set; }

        //выпадаюий список для ресторанов
        public string Rest { get; set; }
        
        //фио сотрудника
        //[Required(ErrorMessage = "Пожалуйста введите ФИО")]
        //[RegularExpression(@"([а-яА-Я]+\s[а-яА-Я]+\s[а-яА-Я]+)|([а-яА-Я]+\s[а-яА-Я]+)|([а-яА-Я]+)|([а-яА-Я]+-*[а-яА-Я]+)", ErrorMessage = "Введите ФИО ответственного")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Пожалуйста введите ФИО ответственного")]
        [Display(Name = "Staff")]
        public string Staff { get; set; }

        //выпадаюий список для источника отзывов
        public string Source { get; set; }
      

        //дата создания отзыва
        [Required(ErrorMessage = "Введите дату визита гостя")]
        [DataType(DataType.Date)]
        public string NewDate { get; set; }

        //время создания отзыва
        [DataType(DataType.Time)]
        public string NewTime { get; set; }

      
        [DataType(DataType.Date)]
        
        public string OldDate { get; set; }

        //выбор оценки отзыва
     
        public string Rating1 { get; set; }

        //выпадаюий список для подробной оценки
        public string Rating2 { get; set; }

        //сам отзыв
        [Required(ErrorMessage = "Текст отзыва")]
        [StringLength(18000,MinimumLength = 5, ErrorMessage = "Слишком короткий отзыв")]
        [DataType(DataType.MultilineText)]
        public string textkomm { get; set; }

        //принятые меры
        //[Required(ErrorMessage = "Принятые меры")]
        [StringLength(254, MinimumLength = 0, ErrorMessage = "Слишком подробно, выражайте свои мысли лаконичнее")]
        [DataType(DataType.MultilineText)]
        public string mera { get; set; }

        //решение для гостя
        //[Required(ErrorMessage = "Решение для гостя")]
        [StringLength(254, MinimumLength = 0, ErrorMessage = "Слишком подробно, выражайте свои мысли лаконичнее")]
        [DataType(DataType.MultilineText)]
        public string answer { get; set; }

        //цена решения
        [Required(ErrorMessage = "Введите цену решения проблемы")]
        [RegularExpression(@"\d+", ErrorMessage = "Введите стоимость ТОЛЬКО цифрами")]
        [StringLength(15, MinimumLength = 1, ErrorMessage = "Введите цену решения проблемы")]
        [Display(Name = "Cost")]
        public string Cost { get; set; }
        public string CostPoint { get; set; }
        public string CostSert { get; set; }

        public string Type { get; set; }

        public string Payer { get; set; }

        [StringLength(50, MinimumLength = 0, ErrorMessage = "Слишком длинные ФИО")]
        [Display(Name = "Guilty")]
        public string Guilty { get; set; }
    }
}
