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
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Fusion.Models
{
    public class SalaryModels
    {
        public class EmployeeTimeSheet
        {
            public string Code { get; set; }
            public string Organization { get; set; }
            public string SheetNumber { get; set; }
            public string Period { get; set; }
            public string Hours { get; set; }
            public int? d1 { get; set; }
            public int? d2 { get; set; }
            public int? d3 { get; set; }
            public int? d4 { get; set; }
            public int? d5 { get; set; }
            public int? d6 { get; set; }
            public int? d7 { get; set; }
            public int? d8 { get; set; }
            public int? d9 { get; set; }
            public int? d10 { get; set; }
            public int? d11 { get; set; }
            public int? d12 { get; set; }
            public int? d13 { get; set; }
            public int? d14 { get; set; }
            public int? d15 { get; set; }
            public int? d16 { get; set; }
            public int? d17 { get; set; }
            public int? d18 { get; set; }
            public int? d19 { get; set; }
            public int? d20 { get; set; }
            public int? d21 { get; set; }
            public int? d22 { get; set; }
            public int? d23 { get; set; }
            public int? d24 { get; set; }
            public int? d25 { get; set; }
            public int? d26 { get; set; }
            public int? d27 { get; set; }
            public int? d28 { get; set; }
            public int? d29 { get; set; }
            public int? d30 { get; set; }
            public int? d31 { get; set; }
        }
        public class TimeSheet
        {
            public dynamic connection;
            public string SheetNumber { get; set; }
            public string OrgName { get; set; }
            [DataType(DataType.Date)]
            public DateTime Period { get; set; }
            public List<EmployeeTimeSheet> ETS { get; set; }
            public EmployeeTimeSheet[] Deserialize(string JSONString)
            {
                return JsonConvert.DeserializeObject<EmployeeTimeSheet[]>(JSONString);
            }
            public bool Save()
            {
                var ets = ETS.FirstOrDefault(p => p.Code != null);

                if (ets != null)
                {
                    this.SheetNumber = ets.SheetNumber;
                    this.OrgName = ets.Organization;
                    this.Period = Convert.ToDateTime(ets.Period);
                }
                
                try
                {
                    try
                    {
                        dynamic doc = null;

                        if (String.IsNullOrEmpty(SheetNumber))
                        {
                            doc = connection.Документы.ИндивидуальныйГрафик.СоздатьДокумент();
                        }
                        else
                        { 
                            dynamic QueryTo1C = connection.NewObject("Запрос");
                            QueryTo1C.Text = String.Format(@"ВЫБРАТЬ * ИЗ Документ.ИндивидуальныйГрафик КАК ИндивидуальныйГрафик ГДЕ ИндивидуальныйГрафик.Номер = ""{0}""", SheetNumber);
                            dynamic res = QueryTo1C.Execute().Choose();

                            while (res.Next())
                            {
                                doc = res.Ссылка.ПолучитьОбъект();
                                doc.ДанныеОВремени.Очистить();
                            }

                            if(res != null)
                                Marshal.Release(Marshal.GetIDispatchForObject(res));

                            res = null;

                            if(QueryTo1C != null)
                                Marshal.Release(Marshal.GetIDispatchForObject(QueryTo1C));

                            QueryTo1C = null;
                            GC.Collect();
                            GC.WaitForPendingFinalizers();
                        }

                        if (doc == null)
                            return false;


                        doc.ПериодРегистрации = Period.ToString("yyyyMM01");
                        doc.Организация = connection.Справочники.Организации.НайтиПоНаименованию(OrgName);
                        doc.ДатаНачалаПериода = Period.ToString("yyyyMM01");
                        doc.ДатаОкончанияПериода = new DateTime(Period.Year, Period.Month, DateTime.DaysInMonth(Period.Year, Period.Month)).ToString("yyyyMMdd");
                        doc.ПериодВводаДанныхОВремени = connection.Перечисления.ПериодыВводаДанныхОВремени.ТекущийМесяц;
                        doc.Дата = DateTime.Today.ToString("yyyyMMdd");

                        foreach (var e in ETS)
                        {
                            int total = 0;
                            int.TryParse(e.Hours, out total);

                            if (total > 0)
                            {
                                dynamic dot = doc.ДанныеОВремени.Добавить();
                                dynamic Str = connection.NewObject("Структура");
                                Str.Вставить("Сотрудник", connection.Справочники.Сотрудники.НайтиПоКоду(e.Code));

                                int daysCount = DateTime.DaysInMonth(this.Period.Year, this.Period.Month);

                                for (int i = 1; i <= daysCount; i++)
                                {

                                    var t = e.GetType();
                                    var v = t.GetField("<d" + i.ToString() + ">k__BackingField", BindingFlags.Public | BindingFlags.NonPublic |
                                             BindingFlags.Static | BindingFlags.Instance | BindingFlags.DeclaredOnly).GetValue(e);

                                    v = v == null ? 0 : v;
                                    Str.Вставить("Часов" + i.ToString(), v);

                                    if ((int)v == 0)
                                        Str.Вставить("ВидВремени" + i.ToString(), connection.Справочники.ВидыИспользованияРабочегоВремени.ВыходныеДни);
                                    else
                                        Str.Вставить("ВидВремени" + i.ToString(), connection.Справочники.ВидыИспользованияРабочегоВремени.Явка);
                                }

                                connection.ЗаполнитьЗначенияСвойств(dot, Str);

                                if(Str != null)
                                    Marshal.Release(Marshal.GetIDispatchForObject(Str));

                                Str = null;

                                if(dot != null)
                                    Marshal.Release(Marshal.GetIDispatchForObject(dot));

                                dot = null;
                            }
                        }

                        doc.Записать();
                        SheetNumber = doc.Номер;

                        if(doc != null)
                            Marshal.Release(Marshal.GetIDispatchForObject(doc));

                        doc = null;
                        GC.Collect();
                        GC.WaitForPendingFinalizers();

                        return true;
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
            public string Salary { get; set; }
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
            public EmployeeTimeSheet[] EmployeesTimeSheet { get; set; }
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

                if(QueryTo1C != null)
                    Marshal.Release(Marshal.GetIDispatchForObject(QueryTo1C));

                QueryTo1C = null;

                if(res != null)
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
 КадроваяИсторияСотрудниковСрезПоследних.Сотрудник КАК Сотрудник,
 КадроваяИсторияСотрудниковСрезПоследних.Организация КАК Организация,
 КадроваяИсторияСотрудниковСрезПоследних.Подразделение КАК Подразделение,
 КадроваяИсторияСотрудниковСрезПоследних.Должность КАК Должность,
 ДанныеСостоянийСотрудников.Начало КАК ДатаУвольнения
ПОМЕСТИТЬ ДанныеСотрудников
ИЗ
 РегистрСведений.КадроваяИсторияСотрудников.СрезПоследних(, ) КАК КадроваяИсторияСотрудниковСрезПоследних
  ЛЕВОЕ СОЕДИНЕНИЕ РегистрСведений.ДанныеСостоянийСотрудников КАК ДанныеСостоянийСотрудников
  ПО (КадроваяИсторияСотрудниковСрезПоследних.Сотрудник = ДанныеСостоянийСотрудников.Сотрудник)
   И (ДанныеСостоянийСотрудников.Состояние = ЗНАЧЕНИЕ(ПЕРЕЧИСЛЕНИЕ.СостоянияСотрудника.Увольнение))
ГДЕ КадроваяИсторияСотрудниковСрезПоследних.Организация.Наименование = ""{0}""  
;

////////////////////////////////////////////////////////////////////////////////
ВЫБРАТЬ
 ДанныеСотрудников.Сотрудник КАК Сотрудник,
 ДанныеСотрудников.Организация КАК Организация,
 ДанныеСотрудников.Подразделение КАК Подразделение,
 ДанныеСотрудников.Должность КАК Должность,
 МАКСИМУМ(ЗначенияПериодическихПоказателейРасчетаЗарплатыСотрудниковСрезПоследних.Значение) КАК МАКС,
 МИНИМУМ(ЗначенияПериодическихПоказателейРасчетаЗарплатыСотрудниковСрезПоследних.Значение) КАК МИН,
 ДанныеСотрудников.ДатаУвольнения КАК ДатаУвольнения
ИЗ
 ДанныеСотрудников КАК ДанныеСотрудников
 ЛЕВОЕ СОЕДИНЕНИЕ
 	РегистрСведений.ЗначенияПериодическихПоказателейРасчетаЗарплатыСотрудников.СрезПоследних КАК ЗначенияПериодическихПоказателейРасчетаЗарплатыСотрудниковСрезПоследних
 	ПО (ЗначенияПериодическихПоказателейРасчетаЗарплатыСотрудниковСрезПоследних.Сотрудник = ДанныеСотрудников.Сотрудник)
ГДЕ
 ДанныеСотрудников.ДатаУвольнения ЕСТЬ NULL
СГРУППИРОВАТЬ ПО
 ДанныеСотрудников.Сотрудник,
 ДанныеСотрудников.Организация,
 ДанныеСотрудников.Подразделение,
 ДанныеСотрудников.Должность,
 ДанныеСотрудников.ДатаУвольнения
УПОРЯДОЧИТЬ ПО
 Подразделение, Должность, Сотрудник", FullName);
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

                    e.Salary = (res.МАКС).ToString();

                    if(res.МИН != res.МАКС)
                        e.Salary += " / " + (res.МИН).ToString();

                    s.Employees.Add(e);
                }

                if(res != null)
                    Marshal.Release(Marshal.GetIDispatchForObject(res));

                res = null;

                if(QueryTo1C != null)
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

                if(res != null)
                    Marshal.Release(Marshal.GetIDispatchForObject(res));

                res = null;

                if(QueryTo1C != null)
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

                if(res != null)
                    Marshal.Release(Marshal.GetIDispatchForObject(res));

                res = null;

                if(QueryTo1C != null)
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

                if(res != null)
                    Marshal.Release(Marshal.GetIDispatchForObject(res));

                res = null;

                if(QueryTo1C != null)
                    Marshal.Release(Marshal.GetIDispatchForObject(QueryTo1C));

                QueryTo1C = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();

                return Accruals;
            }
            public void CreateTimeSheet()
            {
                try
                {
                    dynamic doc = null;

                    dynamic QueryTo1C = connection.NewObject("Запрос");
                    QueryTo1C.Text = String.Format(@"ВЫБРАТЬ * ИЗ Документ.ИндивидуальныйГрафик КАК ИндивидуальныйГрафик ГДЕ ИндивидуальныйГрафик.Организация.Наименование = ""{0}"" И ИндивидуальныйГрафик.ПометкаУдаления = false И ИндивидуальныйГрафик.ПериодРегистрации = ДАТАВРЕМЯ({1})", FullName, Period.ToString("yyyy, MM, 01"));
                    dynamic res = QueryTo1C.Execute().Choose();

                    while (res.Next())
                        doc = res.Ссылка.ПолучитьОбъект();

                    if (doc == null)
                    {
                        doc = connection.Документы.ИндивидуальныйГрафик.СоздатьДокумент();
                        doc.ПериодРегистрации = Period.ToString("yyyyMM01");
                        doc.Организация = connection.Справочники.Организации.НайтиПоНаименованию(FullName);
                        doc.ДатаНачалаПериода = Period.ToString("yyyyMM01");
                        doc.ДатаОкончанияПериода = new DateTime(Period.Year, Period.Month, DateTime.DaysInMonth(Period.Year, Period.Month)).ToString("yyyyMMdd");
                        doc.ПериодВводаДанныхОВремени = connection.Перечисления.ПериодыВводаДанныхОВремени.ТекущийМесяц;
                        doc.Дата = DateTime.Today.ToString("yyyyMMdd");
                        doc.Записать();
                    }

                    SheetNumber = doc.Номер;

                    if(res != null)
                        Marshal.Release(Marshal.GetIDispatchForObject(res));

                    res = null;

                    if (QueryTo1C != null)
                        Marshal.Release(Marshal.GetIDispatchForObject(QueryTo1C));

                    QueryTo1C = null;

                    if (doc != null)
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
                string s = String.Format(@"ВЫБРАТЬ * ИЗ Документ.НачислениеЗарплаты КАК НачислениеЗарплаты ГДЕ НачислениеЗарплаты.Номер = ""{0}""", number);
                QueryTo1C.Text = s;

                dynamic res = QueryTo1C.Execute().Choose();

                while (res.Next())
                {
                    Period = Convert.ToDateTime(res.МесяцНачисления);
                    Number = Convert.ToString(res.Номер);
                    OrganizationName = Convert.ToString(res.Организация.Наименование);
                    TotalAccrued = Convert.ToDecimal(res.Начислено);
                    TotalDetended = Convert.ToDecimal(res.Удержано);

                    dynamic accruals = res.Начисления.Выбрать();

                    while (accruals.Next())
                    {
                        Employee e = Employees.FirstOrDefault(p => p.Code == accruals.Сотрудник.Код);

                        if (e == null)
                        {
                            e = new Employee();
                            e.Code = Convert.ToString(accruals.Сотрудник.Код);
                            e.FullName = Convert.ToString(accruals.Сотрудник.Наименование);
                            Employees.Add(e);
                        }

                        e.AccrualsSum += Convert.ToDecimal(accruals.Результат);
                    }

                    Marshal.Release(Marshal.GetIDispatchForObject(accruals));
                    accruals = null;

                    dynamic detentions = res.Удержания.Выбрать();

                    while (detentions.Next())
                    {
                        Employee e = Employees.FirstOrDefault(p => p.Code == detentions.Сотрудник.Код);

                        if (e == null)
                        {
                            e = new Employee();
                            e.Code = Convert.ToString(detentions.Сотрудник.Код);
                            e.FullName += Convert.ToString(detentions.Сотрудник.Наименование);
                            Employees.Add(e);
                        }

                        e.DetentionsSum = Convert.ToDecimal(detentions.Результат);
                    }

                    Marshal.Release(Marshal.GetIDispatchForObject(detentions));
                    detentions = null;
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