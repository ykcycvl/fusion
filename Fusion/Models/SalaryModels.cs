using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using V83;
using System.Web.Configuration;

namespace Fusion.Models
{
    public class SalaryModels
    {
        public class Detention
        {
            public string Code { get; set; }
            public Decimal Sum { get; set; }
            public string Comment { get; set; }
            public string Name { get; set; }
        }

        public class Employee
        {
            public string Code { get; set; }
            public string FullName { get; set; }
            public string Position { get; set; }
            public List<Detention> Detentions { get; set; }
        }

        public class Subdivision
        {
            public string Code { get; set; }
            public string FullName { get; set; }
            public List<Employee> Employees { get; set; }

            public Subdivision(string code, string fullname)
            {
                Code = code;
                FullName = fullname;
                Employees = new List<Employee>();
            }
        }

        public class Organization
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["ZupConnectionString"].ConnectionString;
            public string FullName { get; set; }
            public List<Subdivision> Subdivisions { get; set; }
            public List<Detention> Detentions;
            public void GetFullData()
            {
                //Список возможных удержаний из 1С
                Subdivisions = new List<Subdivision>();
                Detentions = GetDetentions();
                //Получение информации по организации, ее подразделениям и сотрудникам
                COMConnector connector = new COMConnector();
                dynamic connection = connector.Connect(connectionString);
                dynamic QueryTo1C = connection.NewObject("Запрос");
                QueryTo1C.Text = String.Format(@"ВЫБРАТЬ * ИЗ РегистрСведений.ДанныеДляПодбораСотрудников КАК ДанныеДляПодбораСотрудников ГДЕ ДанныеДляПодбораСотрудников.Организация.Наименование = ""{0}"" И ДанныеДляПодбораСотрудников.Подразделение.Наименование <> """"", FullName);
                dynamic res = QueryTo1C.Execute().Choose();

                while (res.Next())
                {
                    Subdivision s = Subdivisions.FirstOrDefault(p => p.Code == res.Подразделение.Код && p.FullName == res.Подразделение.Наименование);

                    if (s == null)
                    {
                        s = new Subdivision(res.Подразделение.Код, res.Подразделение.Наименование);
                        Subdivisions.Add(s);
                    }

                    s.Employees.Add(new Employee() { Code = res.Сотрудник.Код, FullName = res.Сотрудник.Наименование, Detentions = Detentions, Position = res.Должность.Наименование });
                }

                connection = false;
                connection = null;
                connector = null;
            }

            public List<Detention> GetDetentions()
            {
                List<Detention> Detentions = new List<Detention>();
                COMConnector connector;

                try
                {
                    connector = new COMConnector();

                    dynamic connection = connector.Connect(connectionString);

                    try
                    {
                        dynamic QueryTo1C = connection.NewObject("Запрос");
                        QueryTo1C.Text = @"ВЫБРАТЬ * ИЗ ПланВидовРасчета.Удержания.ДополнительныеРеквизиты КАК УдержанияДополнительныеРеквизиты ГДЕ УдержанияДополнительныеРеквизиты.Свойство.Заголовок = ""Выгружаемое""";
                        dynamic res = QueryTo1C.Execute().Choose();

                        while (res.Next())
                        {
                            Detention d = new Detention();
                            d.Code = res.Ссылка.Код;
                            d.Name = res.Ссылка.Наименование;
                            d.Sum = 0;
                            d.Comment = "";
                            Detentions.Add(d);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        connection = false;
                        connection = null;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    connector = null;
                }

                return Detentions;
            }
        }
    }
}