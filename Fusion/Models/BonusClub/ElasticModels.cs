using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Npgsql;
using NpgsqlTypes;
using System.Data.Common;
using System.Web.Configuration;

namespace Fusion.Models.BC
{
    public class ElasticModels1
    {
        private static NpgsqlConnection GetConnection()
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["BCConnectionString"].ConnectionString;
            NpgsqlConnection dbConnection = new NpgsqlConnection(connectionString);
            dbConnection.Open();
            return dbConnection;
        }
        public class LinkChannelType
        {
            Dictionary<string, string> Items = new Dictionary<string, string>();
            public LinkChannelType()
            {
                Items.Add("Phone", "Телефон");
                Items.Add("Mobile", "Моб. телефон");
                Items.Add("E-Mail", "Эл. почта");
            }
            public string DisplayName { get; set; }
            public string name { get; set; }
            private string[] _DisplayName = { "Телефон", "Моб. телефон", "Эл. почта" };
            private string[] _Name = { "Phone", "Mobile", "E-Mail" };
            public List<LinkChannelType> ToList()
            {
                List<LinkChannelType> LinkChannelsTypes = new List<LinkChannelType>();

                for (int i = 0; i < _DisplayName.Length; i++)
                {
                    LinkChannelType linkChannelType = new LinkChannelType();
                    linkChannelType.DisplayName = _DisplayName[i];
                    linkChannelType.name = _Name[i];
                    LinkChannelsTypes.Add(linkChannelType);
                }

                return LinkChannelsTypes;
            }
        }

        public class HolidayType
        {
            public string DisplayName { get; set; }
            public string name { get; set; }
            private string[] _DisplayName = { "День рождения", "Свадьба" };
            private string[] _Name = { "Birthday", "WeddingDay" };
            public List<HolidayType> ToList()
            {
                List<HolidayType> HolidayTypes = new List<HolidayType>();

                for (int i = 0; i < _DisplayName.Length; i++)
                {
                    HolidayType holidayType = new HolidayType();
                    holidayType.DisplayName = _DisplayName[i];
                    holidayType.name = _Name[i];
                    HolidayTypes.Add(holidayType);
                }

                return HolidayTypes;
            }
        }
        public class GenderType
        {
            public string DisplayName { get; set; }
            public string name { get; set; }
            private string[] _DisplayName = { "—", "Мужской", "Женский" };
            private string[] _Name = { "undefined", "male", "female" };
            public List<GenderType> ToList()
            {
                List<GenderType> GenderTypes = new List<GenderType>();

                for (int i = 0; i < _DisplayName.Length; i++)
                {
                    GenderType genderType = new GenderType();
                    genderType.DisplayName = _DisplayName[i];
                    genderType.name = _Name[i];
                    GenderTypes.Add(genderType);
                }

                return GenderTypes;
            }
        }
        public class MaritalStatusType
        {
            public string DisplayName { get; set; }
            public string name { get; set; }
            private string[] _DisplayName = { "—", "Мужской", "Женский" };
            private string[] _Name = { "undefined", "male", "female" };
            public List<MaritalStatusType> ToList()
            {
                List<MaritalStatusType> maritalStatusTypes = new List<MaritalStatusType>();

                for (int i = 0; i < _DisplayName.Length; i++)
                {
                    MaritalStatusType maritalStatusType = new MaritalStatusType();
                    maritalStatusType.DisplayName = _DisplayName[i];
                    maritalStatusType.name = _Name[i];
                    maritalStatusTypes.Add(maritalStatusType);
                }

                return maritalStatusTypes;
            }
        }
        public class Address
        {
            public int id { get; set; }
            public int country_id { get; set; }
            [DisplayName("Город")]
            public string city { get; set; }
            [DisplayName("Улица")]
            public string street { get; set; }
            [DisplayName("Дом")]
            public string house { get; set; }
            [DisplayName("Квартира")]
            public string flat { get; set; }
            [DisplayName("Индекс")]
            public string postal_index { get; set; }
        }
        public class Country
        {
            public int id { get; set; }
            [DisplayName("Название")]
            public string name { get; set; }
            [DisplayName("Полное название")]
            public string full_name { get; set; }
            [DisplayName("Символьный код")]
            public string symbol { get; set; }
            [DisplayName("Код")]
            public int code { get; set; }
        }
        public class Counterparty
        {
            public int id { get; set; }
            public int user_id { get; set; }
            public int party_id { get; set; }
            public int organization_id { get; set; }
        }
        public class CounterpartyAdditionalParam
        {
            public int id { get; set; }
            [DisplayName("Параметр")]
            public string name { get; set; }
            [DisplayName("Значение")]
            public string value { get; set; }
            public int counterparty_id { get; set; }
        }
        public class Organization
        {
            public int id { get; set; }
            [DisplayName("Название")]
            public string name { get; set; }
            [DisplayName("Регистрационный номер")]
            public string reg_no { get; set; }
        }
        public class OU
        {
            public int id { get; set; }
            public int parent_id { get; set; }
            public string name { get; set; }
            public int party_id { get; set; }
            public int organization_id { get; set; }
            public int lft { get; set; }
            public int rght { get; set; }
            public int tree_id { get; set; }
            public int level { get; set; }
        }
        public class Holiday
        {
            public int id { get; set; }
            public string name { get; set; }
            public DateTime holiday { get; set; }
            public int party_id { get; set; }
        }
        public class LinkChannel
        {
            public int id { get; set; }
            private string _name;
            public string name 
            {
                get
                {
                    return _name;
                }
                set
                { 
                    _name = value;
                    _DisplayName = value;
                }
            }
            private string _DisplayName;
            public string DisplayName
            {
                get
                {
                    return _DisplayName;
                }
            }
            public string value { get; set; }
            public bool is_verified { get; set; }
            public int party_id { get; set; }
        }
        public class Party
        {
            public int id { get; set; }
            public string name { get; set; }
            public string last_name { get; set; }
            public string middle_name { get; set; }
            public string first_name { get; set; }
            public string marital_status { get; set; }
            public string gender { get;set; }
        }
    }
}