using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using V83;
using System.Web.Configuration;
using System.Reflection;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace Fusion.Models
{
    public class SalaryModels
    {
        public class HourPerDay
        {
            public DateTime Day { get; set; }
            public Decimal? Hours { get; set; }
        }
        public class Detention
        {
            public string Code { get; set; }
            public Decimal? Sum { get; set; }
            public string Comment { get; set; }
            public string Name { get; set; }
        }
        public class Accrual
        {
            public string Code { get; set; }
            public Decimal? Sum { get; set; }
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
            public Decimal AccrualsSum { get; set; }
            public Decimal DetentionsSum { get; set; }
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
            public string SheetNumber { get; set; }
            public string FullName { get; set; }
            public List<string> Organizations { get; set; }
            public List<Subdivision> Subdivisions { get; set; }
            public List<Detention> Detentions { get; set; }
            public List<Accrual> Accruals { get; set; }
            [DataType(DataType.Date)]
            public DateTime Period { get; set; }
            private class TimeSheetInfo
            {
                public string EmployeeCode { get; set; }
                public List<HourPerDay> TimeSheet { get; set; }
            }
            private List<TimeSheetInfo> tsiList = new List<TimeSheetInfo>();
            public IEnumerable<SelectListItem> OrganizationsSelectList
            {
                get
                {
                    List<SelectListItem> Orgs = new List<SelectListItem>();

                    for (int i = 0; i < Organizations.Count; i++)
                    {
                        Orgs.Add(new SelectListItem() { Text = Organizations[i], Value = Organizations[i] });
                    }

                    SelectListItem sli = Orgs.FirstOrDefault(p => p.Value == FullName);

                    if (sli != null)
                        sli.Selected = true;

                    return Orgs;
                }
            }

            public void GetOrganizationList()
            {
                Organizations = new List<string>();
                dynamic QueryTo1C = connection.NewObject("Запрос");
                QueryTo1C.Text = @"ВЫБРАТЬ Организации.Наименование ИЗ Справочник.Организации КАК Организации";
                dynamic res = QueryTo1C.Execute().Choose();

                while (res.Next())
                {
                    Organizations.Add(res.Наименование);
                }

                if (Organizations.Count > 0 && FullName != null && FullName != "")
                    FullName = Organizations[0];

                Marshal.Release(Marshal.GetIDispatchForObject(QueryTo1C));
                QueryTo1C = null;
                Marshal.Release(Marshal.GetIDispatchForObject(res));
                res = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();
                
            }
            public void GetEmployees()
            {
                Subdivisions = new List<Subdivision>();
                //Получение информации по организации, ее подразделениям и сотрудникам
                dynamic QueryTo1C = connection.NewObject("Запрос");
                QueryTo1C.Text = String.Format(@"ВЫБРАТЬ
РАЗЛИЧНЫЕ
 КадроваяИсторияСотрудниковСрезПоследних.Сотрудник КАК Сотрудник,
 КадроваяИсторияСотрудниковСрезПоследних.Организация КАК Организация,
 КадроваяИсторияСотрудниковСрезПоследних.ФизическоеЛицо КАК ФизическоеЛицо,
 КадроваяИсторияСотрудниковСрезПоследних.Подразделение КАК Подразделение,
 КадроваяИсторияСотрудниковСрезПоследних.Должность КАК Должность,
 КадроваяИсторияСотрудниковСрезПоследних.ДолжностьПоШтатномуРасписанию КАК ДолжностьПоШтатномуРасписанию,
 КадроваяИсторияСотрудниковСрезПоследних.ЭтоГоловнойСотрудник КАК ЭтоГоловнойСотрудник,
 КадроваяИсторияСотрудниковСрезПоследних.ВидСобытия КАК ВидСобытия,
 КадроваяИсторияСотрудниковСрезПоследних.КоличествоСтавок КАК КоличествоСтавок,
 КадроваяИсторияСотрудниковСрезПоследних.ВидЗанятости КАК ВидЗанятости,
 КадроваяИсторияСотрудниковСрезПоследних.ВидДоговора КАК ВидДоговора,
 КадроваяИсторияСотрудниковСрезПоследних.ФизическоеЛицо.Наименование КАК ФизическоеЛицоНаименование
ИЗ
 РегистрСведений.КадроваяИсторияСотрудников.СрезПоследних(, ) КАК КадроваяИсторияСотрудниковСрезПоследних
 ЛЕВОЕ СОЕДИНЕНИЕ РегистрСведений.ЗначенияПериодическихПоказателейРасчетаЗарплатыСотрудников.СрезПоследних КАК ЗначенияПериодическихПоказателейРасчетаЗарплатыСотрудниковСрезПоследних 
 	ПО (КадроваяИсторияСотрудниковСрезПоследних.Сотрудник = ЗначенияПериодическихПоказателейРасчетаЗарплатыСотрудниковСрезПоследних.Сотрудник)
ГДЕ
 КадроваяИсторияСотрудниковСрезПоследних.Организация.Наименование = ""{0}""
УПОРЯДОЧИТЬ ПО
 КадроваяИсторияСотрудниковСрезПоследних.Должность,
 ФизическоеЛицоНаименование", FullName);
                dynamic res = QueryTo1C.Execute().Choose();

                while (res.Next())
                {
                    Subdivision s = Subdivisions.FirstOrDefault(p => p.Code == res.Подразделение.Код && p.FullName == res.Подразделение.Наименование);

                    if (s == null)
                    {
                        s = new Subdivision(res.Подразделение.Код, res.Подразделение.Наименование);
                        Subdivisions.Add(s);
                    }

                    Employee e = new Employee() { Code = res.Сотрудник.Код, FullName = res.Сотрудник.Наименование, Position = res.Должность.Наименование, TimeSheet = new List<HourPerDay>() };

                    for (int i = 1; i <= DateTime.DaysInMonth(Period.Year, Period.Month); i++)
                    {
                        TimeSheetInfo tsi = tsiList.FirstOrDefault(p => p.EmployeeCode == e.Code);

                        if (tsi == null)
                            e.TimeSheet.Add(new HourPerDay() { Day = new DateTime(Period.Year, Period.Month, i) });
                        else
                        {
                            e.TimeSheet = tsi.TimeSheet;
                            break;
                        }
                    }

                    if (Accruals != null)
                        e.Accruals = Accruals;

                    if (Detentions != null)
                        e.Detentions = Detentions;

                    s.Employees.Add(e);
                }

                Marshal.Release(Marshal.GetIDispatchForObject(res));
                res = null;
                Marshal.Release(Marshal.GetIDispatchForObject(QueryTo1C));
                QueryTo1C = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
            public void GetSheetData()
            {
                dynamic QueryTo1C = connection.NewObject("Запрос");
                QueryTo1C.Text = String.Format(@"ВЫБРАТЬ * ИЗ Документ.ИндивидуальныйГрафик КАК ИндивидуальныйГрафик ГДЕ ИндивидуальныйГрафик.Номер = ""{0}""", SheetNumber);
                dynamic res = QueryTo1C.Execute().Choose();

                while (res.Next())
                {
                    Period = Convert.ToDateTime(res.ПериодРегистрации);
                    FullName = Convert.ToString(res.Организация.Наименование);
                    dynamic tRes = res.ДанныеОВремени.Choose();

                    while (tRes.Next())
                    {
                        TimeSheetInfo tsi = new TimeSheetInfo();
                        tsi.EmployeeCode = Convert.ToString(tRes.Сотрудник.Код);
                        tsi.TimeSheet = new List<HourPerDay>();
                        tsi.TimeSheet.Add(new HourPerDay() { Day = new DateTime(Period.Year, Period.Month, 1), Hours = Convert.ToDecimal(tRes.Часов1) == 0 ? null : Convert.ToDecimal(tRes.Часов1) });
                        tsi.TimeSheet.Add(new HourPerDay() { Day = new DateTime(Period.Year, Period.Month, 2), Hours = Convert.ToDecimal(tRes.Часов2) == 0 ? null : Convert.ToDecimal(tRes.Часов2) });
                        tsi.TimeSheet.Add(new HourPerDay() { Day = new DateTime(Period.Year, Period.Month, 3), Hours = Convert.ToDecimal(tRes.Часов3) == 0 ? null : Convert.ToDecimal(tRes.Часов3) });
                        tsi.TimeSheet.Add(new HourPerDay() { Day = new DateTime(Period.Year, Period.Month, 4), Hours = Convert.ToDecimal(tRes.Часов4) == 0 ? null : Convert.ToDecimal(tRes.Часов4) });
                        tsi.TimeSheet.Add(new HourPerDay() { Day = new DateTime(Period.Year, Period.Month, 5), Hours = Convert.ToDecimal(tRes.Часов5) == 0 ? null : Convert.ToDecimal(tRes.Часов5) });
                        tsi.TimeSheet.Add(new HourPerDay() { Day = new DateTime(Period.Year, Period.Month, 6), Hours = Convert.ToDecimal(tRes.Часов6) == 0 ? null : Convert.ToDecimal(tRes.Часов6) });
                        tsi.TimeSheet.Add(new HourPerDay() { Day = new DateTime(Period.Year, Period.Month, 7), Hours = Convert.ToDecimal(tRes.Часов7) == 0 ? null : Convert.ToDecimal(tRes.Часов7) });
                        tsi.TimeSheet.Add(new HourPerDay() { Day = new DateTime(Period.Year, Period.Month, 8), Hours = Convert.ToDecimal(tRes.Часов8) == 0 ? null : Convert.ToDecimal(tRes.Часов8) });
                        tsi.TimeSheet.Add(new HourPerDay() { Day = new DateTime(Period.Year, Period.Month, 9), Hours = Convert.ToDecimal(tRes.Часов9) == 0 ? null : Convert.ToDecimal(tRes.Часов9) });
                        tsi.TimeSheet.Add(new HourPerDay() { Day = new DateTime(Period.Year, Period.Month, 10), Hours = Convert.ToDecimal(tRes.Часов10) == 0 ? null : Convert.ToDecimal(tRes.Часов10) });
                        tsi.TimeSheet.Add(new HourPerDay() { Day = new DateTime(Period.Year, Period.Month, 11), Hours = Convert.ToDecimal(tRes.Часов11) == 0 ? null : Convert.ToDecimal(tRes.Часов11) });
                        tsi.TimeSheet.Add(new HourPerDay() { Day = new DateTime(Period.Year, Period.Month, 12), Hours = Convert.ToDecimal(tRes.Часов12) == 0 ? null : Convert.ToDecimal(tRes.Часов12) });
                        tsi.TimeSheet.Add(new HourPerDay() { Day = new DateTime(Period.Year, Period.Month, 13), Hours = Convert.ToDecimal(tRes.Часов13) == 0 ? null : Convert.ToDecimal(tRes.Часов13) });
                        tsi.TimeSheet.Add(new HourPerDay() { Day = new DateTime(Period.Year, Period.Month, 14), Hours = Convert.ToDecimal(tRes.Часов14) == 0 ? null : Convert.ToDecimal(tRes.Часов14) });
                        tsi.TimeSheet.Add(new HourPerDay() { Day = new DateTime(Period.Year, Period.Month, 15), Hours = Convert.ToDecimal(tRes.Часов15) == 0 ? null : Convert.ToDecimal(tRes.Часов15) });
                        tsi.TimeSheet.Add(new HourPerDay() { Day = new DateTime(Period.Year, Period.Month, 16), Hours = Convert.ToDecimal(tRes.Часов16) == 0 ? null : Convert.ToDecimal(tRes.Часов16) });
                        tsi.TimeSheet.Add(new HourPerDay() { Day = new DateTime(Period.Year, Period.Month, 17), Hours = Convert.ToDecimal(tRes.Часов17) == 0 ? null : Convert.ToDecimal(tRes.Часов17) });
                        tsi.TimeSheet.Add(new HourPerDay() { Day = new DateTime(Period.Year, Period.Month, 18), Hours = Convert.ToDecimal(tRes.Часов18) == 0 ? null : Convert.ToDecimal(tRes.Часов18) });
                        tsi.TimeSheet.Add(new HourPerDay() { Day = new DateTime(Period.Year, Period.Month, 19), Hours = Convert.ToDecimal(tRes.Часов19) == 0 ? null : Convert.ToDecimal(tRes.Часов19) });
                        tsi.TimeSheet.Add(new HourPerDay() { Day = new DateTime(Period.Year, Period.Month, 20), Hours = Convert.ToDecimal(tRes.Часов20) == 0 ? null : Convert.ToDecimal(tRes.Часов20) });
                        tsi.TimeSheet.Add(new HourPerDay() { Day = new DateTime(Period.Year, Period.Month, 21), Hours = Convert.ToDecimal(tRes.Часов21) == 0 ? null : Convert.ToDecimal(tRes.Часов21) });
                        tsi.TimeSheet.Add(new HourPerDay() { Day = new DateTime(Period.Year, Period.Month, 22), Hours = Convert.ToDecimal(tRes.Часов22) == 0 ? null : Convert.ToDecimal(tRes.Часов22) });
                        tsi.TimeSheet.Add(new HourPerDay() { Day = new DateTime(Period.Year, Period.Month, 23), Hours = Convert.ToDecimal(tRes.Часов23) == 0 ? null : Convert.ToDecimal(tRes.Часов23) });
                        tsi.TimeSheet.Add(new HourPerDay() { Day = new DateTime(Period.Year, Period.Month, 24), Hours = Convert.ToDecimal(tRes.Часов24) == 0 ? null : Convert.ToDecimal(tRes.Часов24) });
                        tsi.TimeSheet.Add(new HourPerDay() { Day = new DateTime(Period.Year, Period.Month, 25), Hours = Convert.ToDecimal(tRes.Часов25) == 0 ? null : Convert.ToDecimal(tRes.Часов25) });
                        tsi.TimeSheet.Add(new HourPerDay() { Day = new DateTime(Period.Year, Period.Month, 26), Hours = Convert.ToDecimal(tRes.Часов26) == 0 ? null : Convert.ToDecimal(tRes.Часов26) });
                        tsi.TimeSheet.Add(new HourPerDay() { Day = new DateTime(Period.Year, Period.Month, 27), Hours = Convert.ToDecimal(tRes.Часов27) == 0 ? null : Convert.ToDecimal(tRes.Часов27) });
                        tsi.TimeSheet.Add(new HourPerDay() { Day = new DateTime(Period.Year, Period.Month, 28), Hours = Convert.ToDecimal(tRes.Часов28) == 0 ? null : Convert.ToDecimal(tRes.Часов28) });

                        try
                        {
                            tsi.TimeSheet.Add(new HourPerDay() { Day = new DateTime(Period.Year, Period.Month, 29), Hours = Convert.ToDecimal(tRes.Часов29) == 0 ? null : Convert.ToDecimal(tRes.Часов29) });
                            tsi.TimeSheet.Add(new HourPerDay() { Day = new DateTime(Period.Year, Period.Month, 30), Hours = Convert.ToDecimal(tRes.Часов30) == 0 ? null : Convert.ToDecimal(tRes.Часов30) });
                            tsi.TimeSheet.Add(new HourPerDay() { Day = new DateTime(Period.Year, Period.Month, 31), Hours = Convert.ToDecimal(tRes.Часов31) == 0 ? null : Convert.ToDecimal(tRes.Часов31) });
                        }
                        catch { }

                        tsiList.Add(tsi);
                    }
                }

                GetEmployees();
                Marshal.Release(Marshal.GetIDispatchForObject(res));
                res = null;
                Marshal.Release(Marshal.GetIDispatchForObject(QueryTo1C));
                QueryTo1C = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
            public List<Detention> GetDetentions()
            {
                List<Detention> Detentions = new List<Detention>();
                dynamic QueryTo1C = connection.NewObject("Запрос");
                QueryTo1C.Text = @"ВЫБРАТЬ * ИЗ ПланВидовРасчета.Удержания.ДополнительныеРеквизиты КАК УдержанияДополнительныеРеквизиты ГДЕ УдержанияДополнительныеРеквизиты.Свойство.Заголовок = ""Выгружаемое""";
                dynamic res = QueryTo1C.Execute().Choose();

                while (res.Next())
                {
                    Detention d = new Detention();
                    d.Code = res.Ссылка.Код;
                    d.Name = res.Ссылка.Наименование;
                    d.Comment = "";
                    Detentions.Add(d);
                }

                Marshal.Release(Marshal.GetIDispatchForObject(res));
                res = null;
                Marshal.Release(Marshal.GetIDispatchForObject(QueryTo1C));
                QueryTo1C = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();

                return Detentions;
            }
            public List<Accrual> GetAccruals()
            {
                List<Accrual> Accruals = new List<Accrual>();
                dynamic QueryTo1C = connection.NewObject("Запрос");
                QueryTo1C.Text = @"ВЫБРАТЬ * ИЗ ПланВидовРасчета.Начисления.ДополнительныеРеквизиты КАК НачисленияДополнительныеРеквизиты ГДЕ НачисленияДополнительныеРеквизиты.Свойство.Заголовок = ""Выгружаемое""";
                dynamic res = QueryTo1C.Execute().Choose();

                while (res.Next())
                {
                    Accrual a = new Accrual();
                    a.Code = res.Ссылка.Код;
                    a.Name = res.Ссылка.Наименование;
                    a.Comment = "";
                    Accruals.Add(a);
                }

                Marshal.Release(Marshal.GetIDispatchForObject(res));
                res = null;
                Marshal.Release(Marshal.GetIDispatchForObject(QueryTo1C));
                QueryTo1C = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();

                return Accruals;
            }
            public void PostTimeSheet()
            {
                try
                {
                    try
                    {
                        if (String.IsNullOrEmpty(SheetNumber))
                        {
                            dynamic doc = connection.Документы.ИндивидуальныйГрафик.СоздатьДокумент();
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
                                    Decimal? total = Subdivisions[i].Employees[j].TimeSheet.Sum(p => p.Hours);

                                    if (total != null && total > 0)
                                    {
                                        dynamic dot = doc.ДанныеОВремени.Добавить();

                                        for (int k = 0; k < Subdivisions[i].Employees[j].TimeSheet.Count; k++)
                                        {
                                            if (Subdivisions[i].Employees[j].TimeSheet[k].Hours != null && Subdivisions[i].Employees[j].TimeSheet[k].Hours > 0)
                                            {
                                                dynamic Str = connection.NewObject("Структура");
                                                Str.Вставить("Сотрудник", connection.Справочники.Сотрудники.НайтиПоКоду(Subdivisions[i].Employees[j].Code));
                                                Str.Вставить("Часов" + Subdivisions[i].Employees[j].TimeSheet[k].Day.Day.ToString(), Subdivisions[i].Employees[j].TimeSheet[k].Hours);
                                                Str.Вставить("ВидВремени" + Subdivisions[i].Employees[j].TimeSheet[k].Day.Day.ToString(), connection.Справочники.ВидыИспользованияРабочегоВремени.Явка);
                                                connection.ЗаполнитьЗначенияСвойств(dot, Str);
                                                Marshal.Release(Marshal.GetIDispatchForObject(Str));
                                                Str = null;
                                            }
                                            else
                                            {
                                                dynamic Str = connection.NewObject("Структура");
                                                Str.Вставить("Сотрудник", connection.Справочники.Сотрудники.НайтиПоКоду(Subdivisions[i].Employees[j].Code));
                                                Str.Вставить("Часов" + Subdivisions[i].Employees[j].TimeSheet[k].Day.Day.ToString(), 0);
                                                Str.Вставить("ВидВремени" + Subdivisions[i].Employees[j].TimeSheet[k].Day.Day.ToString(), connection.Справочники.ВидыИспользованияРабочегоВремени.ВыходныеДни);
                                                connection.ЗаполнитьЗначенияСвойств(dot, Str);
                                                Marshal.Release(Marshal.GetIDispatchForObject(Str));
                                                Str = null;
                                            }
                                        }

                                        Marshal.Release(Marshal.GetIDispatchForObject(dot));
                                        dot = null;
                                    }
                                }
                            }

                            doc.Записать();
                            SheetNumber = doc.Номер;
                            Marshal.Release(Marshal.GetIDispatchForObject(doc));
                            doc = null;
                            GC.Collect();
                            GC.WaitForPendingFinalizers();
                        }
                        else
                        {
                            dynamic QueryTo1C = connection.NewObject("Запрос");
                            QueryTo1C.Text = String.Format(@"ВЫБРАТЬ * ИЗ Документ.ИндивидуальныйГрафик КАК ИндивидуальныйГрафик ГДЕ ИндивидуальныйГрафик.Номер = ""{0}""", SheetNumber);
                            dynamic res = QueryTo1C.Execute().Choose();

                            while (res.Next())
                            {
                                dynamic doc = res.Ссылка.ПолучитьОбъект();
                                doc.ПериодРегистрации = Period.ToString("yyyyMM01");
                                doc.Организация = connection.Справочники.Организации.НайтиПоНаименованию(FullName);
                                doc.ДатаНачалаПериода = Period.ToString("yyyyMM01");
                                doc.ДатаОкончанияПериода = new DateTime(Period.Year, Period.Month, DateTime.DaysInMonth(Period.Year, Period.Month)).ToString("yyyyMMdd");
                                doc.ПериодВводаДанныхОВремени = connection.Перечисления.ПериодыВводаДанныхОВремени.ТекущийМесяц;
                                doc.Дата = DateTime.Today.ToString("yyyyMMdd");
                                doc.ДанныеОВремени.Очистить();

                                for (int i = 0; i < Subdivisions.Count; i++)
                                {
                                    for (int j = 0; j < Subdivisions[i].Employees.Count; j++)
                                    {
                                        Decimal? total = Subdivisions[i].Employees[j].TimeSheet.Sum(p => p.Hours);

                                        if (total != null && total > 0)
                                        {
                                            dynamic dot = doc.ДанныеОВремени.Добавить();

                                            for (int k = 0; k < Subdivisions[i].Employees[j].TimeSheet.Count; k++)
                                            {
                                                if (Subdivisions[i].Employees[j].TimeSheet[k].Hours != null && Subdivisions[i].Employees[j].TimeSheet[k].Hours > 0)
                                                {
                                                    dynamic Str = connection.NewObject("Структура");
                                                    Str.Вставить("Сотрудник", connection.Справочники.Сотрудники.НайтиПоКоду(Subdivisions[i].Employees[j].Code));
                                                    Str.Вставить("Часов" + Subdivisions[i].Employees[j].TimeSheet[k].Day.Day.ToString(), Subdivisions[i].Employees[j].TimeSheet[k].Hours);
                                                    Str.Вставить("ВидВремени" + Subdivisions[i].Employees[j].TimeSheet[k].Day.Day.ToString(), connection.Справочники.ВидыИспользованияРабочегоВремени.Явка);
                                                    connection.ЗаполнитьЗначенияСвойств(dot, Str);
                                                    Marshal.Release(Marshal.GetIDispatchForObject(Str));
                                                    Str = null;
                                                }
                                                else
                                                {
                                                    dynamic Str = connection.NewObject("Структура");
                                                    Str.Вставить("Сотрудник", connection.Справочники.Сотрудники.НайтиПоКоду(Subdivisions[i].Employees[j].Code));
                                                    Str.Вставить("Часов" + Subdivisions[i].Employees[j].TimeSheet[k].Day.Day.ToString(), 0);
                                                    Str.Вставить("ВидВремени" + Subdivisions[i].Employees[j].TimeSheet[k].Day.Day.ToString(), connection.Справочники.ВидыИспользованияРабочегоВремени.ВыходныеДни);
                                                    connection.ЗаполнитьЗначенияСвойств(dot, Str);
                                                    Marshal.Release(Marshal.GetIDispatchForObject(Str));
                                                    Str = null;
                                                }
                                            }

                                            Marshal.Release(Marshal.GetIDispatchForObject(dot));
                                            dot = null;
                                        }
                                    }
                                }

                                doc.Записать();
                                Marshal.Release(Marshal.GetIDispatchForObject(doc));
                                doc = null;
                            }

                            GetEmployees();

                            Marshal.Release(Marshal.GetIDispatchForObject(res));
                            res = null;
                            Marshal.Release(Marshal.GetIDispatchForObject(QueryTo1C));
                            QueryTo1C = null;
                            GC.Collect();
                            GC.WaitForPendingFinalizers();
                        }
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
            public void PostDetentionsAndAccruals()
            {
                try
                {
                    try
                    {
                        /* Удержания и начисления */
                        dynamic doc = connection.Документы.НачислениеЗарплаты.СоздатьДокумент();
                        doc.Организация = connection.Справочники.Организации.НайтиПоНаименованию(FullName);
                        doc.МесяцНачисления = Period.ToString("yyyyMM01");
                        doc.РежимДоначисления = false;
                        doc.Дата = DateTime.Today.ToString("yyyyMMdd");

                        for (int i = 0; i < Subdivisions.Count; i++)
                        {
                            for (int j = 0; j < Subdivisions[i].Employees.Count; j++)
                            {

                                /* Удержания */
                                for (int k = 0; k < Subdivisions[i].Employees[j].Detentions.Count; k++)
                                {
                                    if (Subdivisions[i].Employees[j].Detentions[k].Sum != null && Subdivisions[i].Employees[j].Detentions[k].Sum > 0)
                                    {
                                        dynamic det = doc.Удержания.Добавить();
                                        det.ФизическоеЛицо = connection.Справочники.Сотрудники.НайтиПоКоду(Subdivisions[i].Employees[j].Code).ФизическоеЛицо;
                                        det.Удержание = connection.ПланыВидовРасчета.Удержания.НайтиПоКоду(Subdivisions[i].Employees[j].Detentions[k].Code);
                                        det.ДатаНачала = Period.ToString("yyyyMM01");
                                        det.ДатаОкончания = Period.ToString("yyyyMM") + DateTime.DaysInMonth(Period.Year, Period.Month).ToString();
                                        det.Результат = Subdivisions[i].Employees[j].Detentions[k].Sum;
                                        det.Сотрудник = connection.Справочники.Сотрудники.НайтиПоКоду(Subdivisions[i].Employees[j].Code);
                                        Marshal.Release(Marshal.GetIDispatchForObject(det));
                                        det = null;
                                    }
                                }

                                /* Начисления */
                                for (int k = 0; k < Subdivisions[i].Employees[j].Accruals.Count; k++)
                                {
                                    if (Subdivisions[i].Employees[j].Accruals[k].Sum != null && Subdivisions[i].Employees[j].Accruals[k].Sum > 0)
                                    {
                                        dynamic det = doc.Начисления.Добавить();
                                        dynamic d = connection.ПланыВидовРасчета.Начисления.НайтиПоКоду(Subdivisions[i].Employees[j].Accruals[k].Code);
                                        det.Начисление = d;
                                        det.ДатаНачала = Period.ToString("yyyyMM01");
                                        det.ДатаОкончания = Period.ToString("yyyyMM") + DateTime.DaysInMonth(Period.Year, Period.Month).ToString();
                                        det.Результат = Subdivisions[i].Employees[j].Accruals[k].Sum;
                                        det.Сотрудник = connection.Справочники.Сотрудники.НайтиПоКоду(Subdivisions[i].Employees[j].Code);
                                        dynamic ttt = connection.Метаданные().Перечисления.ВариантыИспользованияПериодаНачисления.EnumValues.Get(connection.Перечисления.ВариантыИспользованияПериодаНачисления.Индекс(d.ИспользованиеПериода)).Имя;

                                        if (connection.Метаданные().Перечисления.ВариантыИспользованияПериодаНачисления.EnumValues.Get(connection.Перечисления.ВариантыИспользованияПериодаНачисления.Индекс(d.ИспользованиеПериода)).Имя == "НеИспользовать")
                                        {
                                            det.ПериодДействия = Period.ToString("yyyyMM01");
                                        }

                                        Marshal.Release(Marshal.GetIDispatchForObject(det));
                                        det = null;
                                        Marshal.Release(Marshal.GetIDispatchForObject(ttt));
                                        ttt = null;
                                        Marshal.Release(Marshal.GetIDispatchForObject(d));
                                        d = null;
                                    }
                                }
                            }
                        }

                        doc.Записать();
                        Marshal.Release(Marshal.GetIDispatchForObject(doc));
                        doc = null;
                        GC.Collect();
                        GC.WaitForPendingFinalizers();
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
            public Organization()
            { 
                Organizations = new List<string>();
                Subdivisions = new List<Subdivision>();
                Detentions = new List<Detention>();
                Accruals = new List<Accrual>();
            }
        }
        public class DetAccList
        {
            public dynamic connection;
            public class DA
            {
                public string Number { get; set; }
                public DateTime period { get; set; }
                public DateTime Date { get; set; }
                public string OrganizationName { get; set; }
                public Decimal Accrued { get; set; }
                public Decimal Detented { get; set; }
                public bool Deleted { get; set; }
                public bool Passed { get; set; }
            }
            public List<DA> Items = new List<DA>();
            public void GetDAList()
            {
                dynamic QueryTo1C = connection.NewObject("Запрос");
                QueryTo1C.Text = String.Format(@"ВЫБРАТЬ 
РАЗЛИЧНЫЕ 
	НачислениеЗарплаты.МесяцНачисления КАК Период,
	НачислениеЗарплаты.Номер КАК Номер,
	НачислениеЗарплаты.Дата КАК Дата,
	НачислениеЗарплаты.Начислено КАК Начислено,
	НачислениеЗарплаты.Удержано КАК Удержано,
	НачислениеЗарплаты.Проведен КАК Проведен,
    НачислениеЗарплаты.ПометкаУдаления КАК ПометкаУдаления,
	НачислениеЗарплаты.Организация.Наименование КАК Наименование,
	НачислениеЗарплаты.Подразделение КАК Подразделение
ИЗ Документ.НачислениеЗарплаты КАК НачислениеЗарплаты
УПОРЯДОЧИТЬ ПО
	НачислениеЗарплаты.МесяцНачисления,
	НачислениеЗарплаты.Дата");
                dynamic res = QueryTo1C.Execute().Choose();

                while (res.Next())
                {
                    DA da = new DA();
                    da.period = DateTime.Parse(Convert.ToString(res.Период));
                    da.Date = DateTime.Parse(Convert.ToString(res.Дата));
                    da.Number = Convert.ToString(res.Номер);
                    da.OrganizationName = Convert.ToString(res.Наименование);
                    da.Accrued = Convert.ToDecimal(res.Начислено);
                    da.Detented = Convert.ToDecimal(res.Удержано);
                    da.Deleted = Convert.ToBoolean(res.ПометкаУдаления);
                    da.Passed = Convert.ToBoolean(res.Проведен);
                    Items.Add(da);
                }

                Marshal.Release(Marshal.GetIDispatchForObject(QueryTo1C));
                QueryTo1C = null;
                Marshal.Release(Marshal.GetIDispatchForObject(res));
                res = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }
        public class SalariesAndContributions
        {
            public dynamic connection;
            public List<Employee> Employees { get; set; }
            public string Number { get; set; }
            public DateTime Period { get; set; }
            public string OrganizationName { get; set; }
            public Decimal TotalAccrued { get; set; }
            public Decimal TotalDetended { get; set; }
            public void Get(string number)
            {
                Employees = new List<Employee>();
                dynamic QueryTo1C = connection.NewObject("Запрос");
                string s = String.Format(@"ВЫБРАТЬ 
                    НачисленияУдержанияПоСотрудникам.Сотрудник.Код КАК КодСотрудника,
                    НачисленияУдержанияПоСотрудникам.Период КАК Период,
	                НачисленияУдержанияПоСотрудникам.Регистратор.Номер КАК Номер,
	                НачисленияУдержанияПоСотрудникам.Организация.Наименование КАК НаименованиеОрганизации,
	                НачисленияУдержанияПоСотрудникам.Сотрудник.Наименование КАК ИмяСотрудника,
	                СУММА(
	                    ВЫБОР 
	                        КОГДА НачисленияУдержанияПоСотрудникам.ГруппаНачисленияУдержанияВыплаты = ЗНАЧЕНИЕ(Перечисление.ГруппыНачисленияУдержанияВыплаты.Начислено) ТОГДА
	                            НачисленияУдержанияПоСотрудникам.Сумма
	                        ИНАЧЕ 0
	                        КОНЕЦ
	                    ) КАК Начислено,
	                СУММА(
	                    ВЫБОР 
	                        КОГДА НачисленияУдержанияПоСотрудникам.ГруппаНачисленияУдержанияВыплаты = ЗНАЧЕНИЕ(Перечисление.ГруппыНачисленияУдержанияВыплаты.Удержано) ТОГДА
	                            НачисленияУдержанияПоСотрудникам.Сумма
	                        ИНАЧЕ 0
	                        КОНЕЦ
	                    ) КАК Удержано
                    ИЗ 
                        РегистрНакопления.НачисленияУдержанияПоСотрудникам 
                    КАК НачисленияУдержанияПоСотрудникам ГДЕ НачисленияУдержанияПоСотрудникам.Регистратор.Номер = ""{0}""
                    СГРУППИРОВАТЬ ПО
                        НачисленияУдержанияПоСотрудникам.Период,
                        НачисленияУдержанияПоСотрудникам.Регистратор.Номер,
                        НачисленияУдержанияПоСотрудникам.Организация.Наименование,
                        НачисленияУдержанияПоСотрудникам.Сотрудник.Наименование", number);
                QueryTo1C.Text = String.Format(@"ВЫБРАТЬ 
                    НачисленияУдержанияПоСотрудникам.Сотрудник.Код КАК КодСотрудника,
                    НачисленияУдержанияПоСотрудникам.Период КАК Период,
	                НачисленияУдержанияПоСотрудникам.Регистратор.Номер КАК Номер,
	                НачисленияУдержанияПоСотрудникам.Организация.Наименование КАК НаименованиеОрганизации,
	                НачисленияУдержанияПоСотрудникам.Сотрудник.Наименование КАК ИмяСотрудника,
	                СУММА(
	                    ВЫБОР 
	                        КОГДА НачисленияУдержанияПоСотрудникам.ГруппаНачисленияУдержанияВыплаты = ЗНАЧЕНИЕ(Перечисление.ГруппыНачисленияУдержанияВыплаты.Начислено) ТОГДА
	                            НачисленияУдержанияПоСотрудникам.Сумма
	                        ИНАЧЕ 0
	                        КОНЕЦ
	                    ) КАК Начислено,
	                СУММА(
	                    ВЫБОР 
	                        КОГДА НачисленияУдержанияПоСотрудникам.ГруппаНачисленияУдержанияВыплаты = ЗНАЧЕНИЕ(Перечисление.ГруппыНачисленияУдержанияВыплаты.Удержано) ТОГДА
	                            НачисленияУдержанияПоСотрудникам.Сумма
	                        ИНАЧЕ 0
	                        КОНЕЦ
	                    ) КАК Удержано
                    ИЗ 
                        РегистрНакопления.НачисленияУдержанияПоСотрудникам 
                    КАК НачисленияУдержанияПоСотрудникам ГДЕ НачисленияУдержанияПоСотрудникам.Регистратор.Номер = ""{0}""
                    СГРУППИРОВАТЬ ПО
                        НачисленияУдержанияПоСотрудникам.Сотрудник.Код,
                        НачисленияУдержанияПоСотрудникам.Период,
                        НачисленияУдержанияПоСотрудникам.Регистратор.Номер,
                        НачисленияУдержанияПоСотрудникам.Организация.Наименование,
                        НачисленияУдержанияПоСотрудникам.Сотрудник.Наименование", number);
                dynamic res = QueryTo1C.Execute().Choose();

                while (res.Next())
                {
                    Employee e = new Employee();
                    e.Code = Convert.ToString(res.КодСотрудника);
                    e.FullName = Convert.ToString(res.ИмяСотрудника);
                    e.AccrualsSum = Convert.ToDecimal(res.Начислено);
                    e.DetentionsSum = Convert.ToDecimal(res.Удержано);
                    Employees.Add(e);

                    Period = Convert.ToDateTime(res.Период);
                    Number = Convert.ToString(res.Номер);
                    OrganizationName = Convert.ToString(res.НаименованиеОрганизации);
                    TotalAccrued += e.AccrualsSum;
                    TotalDetended += e.DetentionsSum;
                }

                Marshal.Release(Marshal.GetIDispatchForObject(QueryTo1C));
                QueryTo1C = null;
                Marshal.Release(Marshal.GetIDispatchForObject(res));
                res = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }
        public class EmployeeSalaryDetail
        {
            public dynamic connection;
            public List<Employee> Employees { get; set; }
            public string Number { get; set; }
            public DateTime Period { get; set; }
            public string OrganizationName { get; set; }
            public void Get(string number, string code)
            {
                Employees = new List<Employee>();
                dynamic QueryTo1C = connection.NewObject("Запрос");
                QueryTo1C.Text = String.Format(@"ВЫБРАТЬ 
                        НачисленияУдержанияПоСотрудникам.Сотрудник.Код КАК КодСотрудника,
                        НачисленияУдержанияПоСотрудникам.Период КАК Период,
	                    НачисленияУдержанияПоСотрудникам.Регистратор.Номер КАК Номер,
	                    НачисленияУдержанияПоСотрудникам.Организация.Наименование КАК НаименованиеОрганизации,
	                    НачисленияУдержанияПоСотрудникам.Сотрудник.Наименование КАК ИмяСотрудника,
	                    НачисленияУдержанияПоСотрудникам.НачислениеУдержание.Наименование КАК НаименованиеНачисленияУдержания,
	                    ВЫБОР 
			                КОГДА НачисленияУдержанияПоСотрудникам.ГруппаНачисленияУдержанияВыплаты = ЗНАЧЕНИЕ(Перечисление.ГруппыНачисленияУдержанияВыплаты.Начислено) ТОГДА
				                ""Начислено""
			                ИНАЧЕ
			                	""Удержано""
			                КОНЕЦ
		                КАК ГуппаНачисленияУдержания,
	                    НачисленияУдержанияПоСотрудникам.Сумма КАК Сумма
                    ИЗ 
                        РегистрНакопления.НачисленияУдержанияПоСотрудникам 
                    КАК НачисленияУдержанияПоСотрудникам ГДЕ НачисленияУдержанияПоСотрудникам.Регистратор.Номер = ""{0}"" И НачисленияУдержанияПоСотрудникам.Сотрудник.Код = ""{1}""", number, code);
                dynamic res = QueryTo1C.Execute().Choose();

                while (res.Next())
                {
                    Period = Convert.ToDateTime(res.Период);
                    Number = Convert.ToString(res.Номер);
                    OrganizationName = Convert.ToString(res.НаименованиеОрганизации);
                    string ecode = Convert.ToString(res.КодСотрудника);
                    string otype = Convert.ToString(res.ГуппаНачисленияУдержания);

                    Employee e = Employees.FirstOrDefault(p => p.Code == ecode);

                    if (e == null)
                    {
                        e = new Employee();
                        e.Accruals = new List<Accrual>();
                        e.Detentions = new List<Detention>();
                        e.Code = ecode;
                        e.FullName = Convert.ToString(res.ИмяСотрудника);
                        Employees.Add(e);
                    }

                    if (otype == "Удержано")
                    {
                        Detention d = new Detention() { Name = Convert.ToString(res.НаименованиеНачисленияУдержания), Sum = Convert.ToDecimal(res.Сумма) };
                        e.Detentions.Add(d);
                    }
                    else
                    {
                        Accrual a = new Accrual() { Name = Convert.ToString(res.НаименованиеНачисленияУдержания), Sum = Convert.ToDecimal(res.Сумма) };
                        e.Accruals.Add(a);
                    }
                }

                Marshal.Release(Marshal.GetIDispatchForObject(QueryTo1C));
                QueryTo1C = null;
                Marshal.Release(Marshal.GetIDispatchForObject(res));
                res = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }
        public class TimeSheetsViewModel
        {
            public dynamic connection;
            public class TimeSheet
            {
                public DateTime Date { get; set; }
                public string Number { get; set; }
                public DateTime Period { get; set; }
                public String OrganizationName { get; set; }
                public bool Deleted { get; set; }
                public bool Passed { get; set; }
            }
            public string OrganizationName { get; set; }
            public List<string> Organizations { get; set; }
            public List<TimeSheet> TimeSheets = new List<TimeSheet>();
            public IEnumerable<SelectListItem> OrganizationsSelectList
            {
                get
                {
                    List<SelectListItem> Orgs = new List<SelectListItem>();

                    if(Organizations != null)
                        for (int i = 0; i < Organizations.Count; i++)
                            Orgs.Add(new SelectListItem() { Text = Organizations[i], Value = Organizations[i] });

                    SelectListItem sli = Orgs.FirstOrDefault(p => p.Value == OrganizationName);

                    if (sli != null)
                        sli.Selected = true;

                    return Orgs;
                }
            }
            public void GetOrganizationList()
            {
                Organizations = new List<string>();
                dynamic QueryTo1C = connection.NewObject("Запрос");
                QueryTo1C.Text = @"ВЫБРАТЬ Организации.Наименование ИЗ Справочник.Организации КАК Организации";
                dynamic res = QueryTo1C.Execute().Choose();

                while (res.Next())
                    Organizations.Add(res.Наименование);

                if (Organizations.Count > 0 && !String.IsNullOrEmpty(OrganizationName))
                    OrganizationName = Organizations[0];

                Marshal.Release(Marshal.GetIDispatchForObject(QueryTo1C));
                QueryTo1C = null;
                Marshal.Release(Marshal.GetIDispatchForObject(res));
                res = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
            public void Get()
            {
                dynamic QueryTo1C = connection.NewObject("Запрос");

                if (String.IsNullOrEmpty(OrganizationName))
                    return;
                else
                    QueryTo1C.Text = String.Format(@"ВЫБРАТЬ * ИЗ Документ.ИндивидуальныйГрафик КАК ИндивидуальныйГрафик ГДЕ ИндивидуальныйГрафик.Организация.Наименование = ""{0}""", OrganizationName);

                dynamic res = QueryTo1C.Execute().Choose();

                while (res.Next())
                {
                    TimeSheet ts = new TimeSheet() { Date = Convert.ToDateTime(res.Дата), Deleted = Convert.ToBoolean(res.ПометкаУдаления), Number = Convert.ToString(res.Ссылка.Номер), OrganizationName = Convert.ToString(res.Организация.Наименование), Passed =  Convert.ToBoolean(res.Проведен), Period = Convert.ToDateTime(res.ПериодРегистрации) };
                    TimeSheets.Add(ts);
                }

                Marshal.Release(Marshal.GetIDispatchForObject(QueryTo1C));
                QueryTo1C = null;
                Marshal.Release(Marshal.GetIDispatchForObject(res));
                res = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }
    }
}