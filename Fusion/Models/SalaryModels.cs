using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using V83;
using System.Web.Configuration;
using System.Reflection;

namespace Fusion.Models
{
    public class SalaryModels
    {
        public class HourPerDay
        {
            public DateTime Day { get; set; }
            public Decimal Hours { get; set; }
        }
        public class Detention
        {
            public string Code { get; set; }
            public Decimal Sum { get; set; }
            public string Comment { get; set; }
            public string Name { get; set; }
        }
        public class Accrual
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
            public List<Accrual> Accruals { get; set; }
            public List<HourPerDay> TimeSheet { get; set; }
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
            public Subdivision()
            {
                Employees = new List<Employee>();
            }
        }

        public class Organization
        {
            public dynamic connection;
            public string FullName { get; set; }
            public List<Subdivision> Subdivisions { get; set; }
            public List<Detention> Detentions { get; set; }
            public List<Accrual> Accruals { get; set; }
            public DateTime Period { get; set; }
            public void GetFullData()
            {
                //Список возможных удержаний из 1С
                Subdivisions = new List<Subdivision>();
                Detentions = GetDetentions();
                Accruals = GetAccruals();
                //Получение информации по организации, ее подразделениям и сотрудникам
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

                    Employee e = new Employee() { Code = res.Сотрудник.Код, FullName = res.Сотрудник.Наименование, Detentions = Detentions, Accruals = Accruals, Position = res.Должность.Наименование, TimeSheet = new List<HourPerDay>() };

                    for (int i = 1; i <= DateTime.DaysInMonth(Period.Year, Period.Month); i++)
                    {
                        e.TimeSheet.Add(new HourPerDay() { Day = new DateTime(Period.Year, Period.Month, i), Hours = 0 });
                    }

                    s.Employees.Add(e);
                }

                QueryTo1C = null;
            }

            public List<Detention> GetDetentions()
            {
                List<Detention> Detentions = new List<Detention>();

                try
                {
                    dynamic QueryTo1C = connection.NewObject("Запрос");

                    try
                    {
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
                        QueryTo1C = null;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                return Detentions;
            }
            public List<Accrual> GetAccruals()
            {
                List<Accrual> Accruals = new List<Accrual>();

                try
                {
                    dynamic QueryTo1C = connection.NewObject("Запрос");

                    try
                    {
                        QueryTo1C.Text = @"ВЫБРАТЬ * ИЗ ПланВидовРасчета.Начисления.ДополнительныеРеквизиты КАК НачисленияДополнительныеРеквизиты ГДЕ НачисленияДополнительныеРеквизиты.Свойство.Заголовок = ""Выгружаемое""";
                        dynamic res = QueryTo1C.Execute().Choose();

                        while (res.Next())
                        {
                            Accrual a = new Accrual();
                            a.Code = res.Ссылка.Код;
                            a.Name = res.Ссылка.Наименование;
                            a.Sum = 0;
                            a.Comment = "";
                            Accruals.Add(a);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        QueryTo1C = null;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                return Accruals;
            }

            public void Post()
            {
                try
                {
                    try
                    {
                        /* Удержания */
                        dynamic doc = connection.Документы.НачислениеЗарплаты.СоздатьДокумент();
                        doc.Организация = connection.Справочники.Организации.НайтиПоНаименованию(FullName);
                        doc.МесяцНачисления = Period.ToString("yyyyMM01");
                        doc.РежимДоначисления = false;
                        doc.Дата = DateTime.Today.ToString("yyyyMMdd");

                        for (int i = 0; i < Subdivisions.Count; i++)
                        {
                            for (int j = 0; j < Subdivisions[i].Employees.Count; j++)
                            {
                                for (int k = 0; k < Subdivisions[i].Employees[j].Detentions.Count; k++)
                                {
                                    if (Subdivisions[i].Employees[j].Detentions[k].Sum > 0)
                                    {
                                        dynamic det = doc.Удержания.Добавить();
                                        det.ФизическоеЛицо = connection.Справочники.Сотрудники.НайтиПоКоду(Subdivisions[i].Employees[j].Code).ФизическоеЛицо;
                                        det.Удержание = connection.ПланыВидовРасчета.Удержания.НайтиПоКоду(Subdivisions[i].Employees[j].Detentions[k].Code);
                                        det.ДатаНачала = Period.ToString("yyyyMM01");
                                        det.ДатаОкончания = Period.ToString("yyyyMM") + DateTime.DaysInMonth(Period.Year, Period.Month).ToString();
                                        det.Результат = Subdivisions[i].Employees[j].Detentions[k].Sum;
                                        det.Сотрудник = connection.Справочники.Сотрудники.НайтиПоКоду(Subdivisions[i].Employees[j].Code);
                                        det = false;
                                        det = null;
                                    }
                                }
                            }
                        }

                        doc.Записать(connection.РежимЗаписиДокумента.Проведение);

                        /* Конец Удержания*/

                        doc = false;
                        doc = null;

                        /* Индивидуальные графики */

                        doc = connection.Документы.ИндивидуальныйГрафик.СоздатьДокумент();
                        doc.ПериодРегистрации = Period.ToString("yyyyMM01");
                        doc.Организация = connection.Справочники.Организации.НайтиПоНаименованию(FullName);
                        doc.ДатаНачалаПериода = Period.ToString("yyyyMM01");
                        doc.ДатаОкончанияПериода = new DateTime(Period.Year, Period.Month, DateTime.DaysInMonth(Period.Year, Period.Month)).ToString("yyyyMMdd");
                        doc.ПериодВводаДанныхОВремени = connection.Перечисления.ПериодыВводаДанныхОВремени.ТекущийМесяц;
                        doc.Дата = DateTime.Today.ToString("yyyyMMdd");

                        for (int i = 0; i < Subdivisions.Count; i++)
                        {
                            for (int j = 0; j < Subdivisions[i].Employees.Count; j++)
                            {
                                Decimal total = Subdivisions[i].Employees[j].TimeSheet.Sum(p => p.Hours);

                                if (total > 0)
                                {
                                    dynamic dot = doc.ДанныеОВремени.Добавить();

                                    for (int k = 0; k < Subdivisions[i].Employees[j].TimeSheet.Count; k++)
                                    {
                                        if (Subdivisions[i].Employees[j].TimeSheet[k].Hours > 0)
                                        {
                                            dynamic Str = connection.NewObject("Структура");
                                            Str.Вставить("Сотрудник", connection.Справочники.Сотрудники.НайтиПоКоду(Subdivisions[i].Employees[j].Code));
                                            Str.Вставить("Часов" + Subdivisions[i].Employees[j].TimeSheet[k].Day.Day.ToString(), Subdivisions[i].Employees[j].TimeSheet[k].Hours);
                                            Str.Вставить("ВидВремени" + Subdivisions[i].Employees[j].TimeSheet[k].Day.Day.ToString(), connection.Справочники.ВидыИспользованияРабочегоВремени.Явка);
                                            connection.ЗаполнитьЗначенияСвойств(dot, Str);
                                        }
                                        else
                                        {
                                            dynamic Str = connection.NewObject("Структура");
                                            Str.Вставить("Сотрудник", connection.Справочники.Сотрудники.НайтиПоКоду(Subdivisions[i].Employees[j].Code));
                                            Str.Вставить("Часов" + Subdivisions[i].Employees[j].TimeSheet[k].Day.Day.ToString(), Subdivisions[i].Employees[j].TimeSheet[k].Hours);
                                            Str.Вставить("ВидВремени" + Subdivisions[i].Employees[j].TimeSheet[k].Day.Day.ToString(), connection.Справочники.ВидыИспользованияРабочегоВремени.ВыходныеДни);
                                            connection.ЗаполнитьЗначенияСвойств(dot, Str);
                                        }
                                    }
                                }
                            }
                        }

                        doc.Записать(connection.РежимЗаписиДокумента.Проведение);
                        doc = false;
                        doc = null;

                        /* Конец Индивидуальные графики */
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}