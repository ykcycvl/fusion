using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Reflection;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Text.RegularExpressions;
using Fusion.ZupWS;
using System.Web.Script.Serialization;

namespace Fusion.Models
{
    public class SalaryModels
    {
        public class EmployeeTimeSheet
        {
            public string Code { get; set; }
            public string Organization { get; set; }
            public string DocNumber { get; set; }
            public string Period { get; set; }
            public DateTime Date { get; set; }
            public string Hours { get; set; }
            public string source { get; set; }
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
        public class EmployeeAccrualsAndDetentions
        {
            public string Code { get; set; }
            public string Organization { get; set; }
            public string DocNumber { get; set; }
            public string Period { get; set; }
            public DateTime Date { get; set; }
            public List<ZupWS.Accrual> Accruals { get; set; }
            public List<ZupWS.Detention> Detentions { get; set; }
        }
        public class TimeSheet
        {
            public dynamic connection;
            public string DocNumber { get; set; }
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
                    this.DocNumber = ets.DocNumber;
                    this.OrgName = ets.Organization;
                    this.Period = Convert.ToDateTime(ets.Period);
                }
                
                try
                {
                    try
                    {
                        dynamic doc = null;

                        if (String.IsNullOrEmpty(DocNumber))
                        {
                            doc = connection.Документы.ИндивидуальныйГрафик.СоздатьДокумент();
                        }
                        else
                        { 
                            dynamic QueryTo1C = connection.NewObject("Запрос");
                            QueryTo1C.Text = String.Format(@"ВЫБРАТЬ * ИЗ Документ.ИндивидуальныйГрафик КАК ИндивидуальныйГрафик ГДЕ ИндивидуальныйГрафик.Номер = ""{0}""", DocNumber);
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
                        DocNumber = doc.Номер;

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
            public string EmployeeCode { get; set; }
            public string Code { get; set; }
            public Decimal? Sum { get; set; }
            public string Comment { get; set; }
            public string Name { get; set; }
        }
        public class Accrual
        {
            public string EmployeeCode { get; set; }
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
            public string Hours { get; set; }
            public List<ZupWS.Detention> Detentions { get; set; }
            public List<ZupWS.Accrual> Accruals { get; set; }
            public List<HourPerDay> TimeSheet { get; set; }
            public List<HourPerDay> BioTimeData { get; set; }
            public Decimal AccrualsSum { get; set; }
            public Decimal DetentionsSum { get; set; }
            public string Rate { get; set; }
            public string RateNew { get; set; }
            public List<SelectListItem> RatesList = new List<SelectListItem>();
            public IEnumerable<SelectListItem> Rates 
            {
                get
                {
                    return RatesList;
                }
            }
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

        public class Vega1CWS
        {
            public class DocumentInfo
            {
                public string DocNumber { get; set; }
                public string FullName { get; set; }
                [DataType(DataType.Date)]
                public DateTime Period { get; set; }
                [DataType(DataType.Date)]
                public DateTime Date { get; set; }
                public bool CarriedOut { get; set; }
                public List<Subdivision> Subdivisions = new List<Subdivision>();
                public List<ZupWS.Organization> Organizations = new List<ZupWS.Organization>();
                public List<ZupWS.Document> Documents = new List<Document>();

                public IEnumerable<SelectListItem> OrganizationsSelectList
                {
                    get
                    {
                        List<SelectListItem> Orgs = new List<SelectListItem>();

                        foreach (ZupWS.Organization org in Organizations)
                            Orgs.Add(new SelectListItem() { Text = org.Name, Value = org.Name });

                        SelectListItem sli = Orgs.FirstOrDefault(p => p.Value == FullName);

                        if (sli != null)
                            sli.Selected = true;

                        return Orgs;
                    }
                }
                public void GetOrganizationList(string username)
                {
                    using (ZupWS.VegaWS service = new ZupWS.VegaWS())
                    {
                        service.SoapVersion = System.Web.Services.Protocols.SoapProtocolVersion.Soap12;
                        Organizations = service.GetOrganizations(username).ToList();
                    }

                    if (Organizations.Count > 0 && String.IsNullOrEmpty(FullName))
                        FullName = Organizations[0].Name;
                }
            }
            public class TimeSheetViewModel : DocumentInfo
            {
                public List<EmployeeTimeSheet> ETS { get; set; }
                private EmployeeTimeSheet[] Deserialize(string JSONString)
                {
                    return JsonConvert.DeserializeObject<EmployeeTimeSheet[]>(JSONString);
                }
                public void GetTimeSheetList(string orgname, string username)
                {
                    if (orgname == null)
                        orgname = "";

                    GetOrganizationList(username);

                    if (orgname == "")
                        orgname = FullName;

                    using (ZupWS.VegaWS service = new ZupWS.VegaWS())
                    {
                        service.SoapVersion = System.Web.Services.Protocols.SoapProtocolVersion.Soap12;

                        var docs = service.GetTimeSheets(orgname, username);

                        foreach (var doc in docs)
                            if (!doc.Deleted)
                                Documents.Add(doc);
                    }
                }
                public void GetDocument(string Number, int year, string username)
                {
                    using (ZupWS.VegaWS service = new ZupWS.VegaWS())
                    {
                        service.SoapVersion = System.Web.Services.Protocols.SoapProtocolVersion.Soap12;

                        var doc = service.GetTimeSheetInfo(username, Number, year);
                        DocNumber = doc.Number;
                        Date = doc.Date;
                        CarriedOut = doc.CarriedOut;
                        FullName = doc.Organization.Name;
                        Period = doc.RegistrationPeriod;

                        foreach (var etsi in doc.Employees)
                        {
                            if (Subdivisions == null)
                                Subdivisions = new List<Subdivision>();

                            Subdivision s = Subdivisions.FirstOrDefault(p => p.Code == etsi.SubdivisionCode);

                            if (s == null)
                            {
                                s = new Subdivision() { Employees = new List<Employee>(), Code = etsi.SubdivisionCode, FullName = etsi.Subdivision };
                                Subdivisions.Add(s);
                            }

                            Employee e = s.Employees.FirstOrDefault(p => p.Code == etsi.Code);

                            if (e == null)
                            {
                                e = new Employee() { Code = etsi.Code, FullName = etsi.Name, Position = etsi.Position, Rate = etsi.Rate, TimeSheet = new List<HourPerDay>() };
                                s.Employees.Add(e);
                            }

                            e.TimeSheet = new List<HourPerDay>();
                            e.BioTimeData = new List<HourPerDay>();

                            if (Convert.ToDecimal(etsi.TimeSheet.d1) != 0)
                                e.TimeSheet.Add(new HourPerDay() { Day = new DateTime(Period.Year, Period.Month, 1), Hours = Convert.ToDecimal(etsi.TimeSheet.d1) });
                            if (Convert.ToDecimal(etsi.TimeSheet.d2) != 0)
                                e.TimeSheet.Add(new HourPerDay() { Day = new DateTime(Period.Year, Period.Month, 2), Hours = Convert.ToDecimal(etsi.TimeSheet.d2) });
                            if (Convert.ToDecimal(etsi.TimeSheet.d3) != 0)
                                e.TimeSheet.Add(new HourPerDay() { Day = new DateTime(Period.Year, Period.Month, 3), Hours = Convert.ToDecimal(etsi.TimeSheet.d3) });
                            if (Convert.ToDecimal(etsi.TimeSheet.d4) != 0)
                                e.TimeSheet.Add(new HourPerDay() { Day = new DateTime(Period.Year, Period.Month, 4), Hours = Convert.ToDecimal(etsi.TimeSheet.d4) });
                            if (Convert.ToDecimal(etsi.TimeSheet.d5) != 0)
                                e.TimeSheet.Add(new HourPerDay() { Day = new DateTime(Period.Year, Period.Month, 5), Hours = Convert.ToDecimal(etsi.TimeSheet.d5) });
                            if (Convert.ToDecimal(etsi.TimeSheet.d6) != 0)
                                e.TimeSheet.Add(new HourPerDay() { Day = new DateTime(Period.Year, Period.Month, 6), Hours = Convert.ToDecimal(etsi.TimeSheet.d6) });
                            if (Convert.ToDecimal(etsi.TimeSheet.d7) != 0)
                                e.TimeSheet.Add(new HourPerDay() { Day = new DateTime(Period.Year, Period.Month, 7), Hours = Convert.ToDecimal(etsi.TimeSheet.d7) });
                            if (Convert.ToDecimal(etsi.TimeSheet.d8) != 0)
                                e.TimeSheet.Add(new HourPerDay() { Day = new DateTime(Period.Year, Period.Month, 8), Hours = Convert.ToDecimal(etsi.TimeSheet.d8) });
                            if (Convert.ToDecimal(etsi.TimeSheet.d9) != 0)
                                e.TimeSheet.Add(new HourPerDay() { Day = new DateTime(Period.Year, Period.Month, 9), Hours = Convert.ToDecimal(etsi.TimeSheet.d9) });
                            if (Convert.ToDecimal(etsi.TimeSheet.d10) != 0)
                                e.TimeSheet.Add(new HourPerDay() { Day = new DateTime(Period.Year, Period.Month, 10), Hours = Convert.ToDecimal(etsi.TimeSheet.d10) });
                            if (Convert.ToDecimal(etsi.TimeSheet.d11) != 0)
                                e.TimeSheet.Add(new HourPerDay() { Day = new DateTime(Period.Year, Period.Month, 11), Hours = Convert.ToDecimal(etsi.TimeSheet.d11) });
                            if (Convert.ToDecimal(etsi.TimeSheet.d12) != 0)
                                e.TimeSheet.Add(new HourPerDay() { Day = new DateTime(Period.Year, Period.Month, 12), Hours = Convert.ToDecimal(etsi.TimeSheet.d12) });
                            if (Convert.ToDecimal(etsi.TimeSheet.d13) != 0)
                                e.TimeSheet.Add(new HourPerDay() { Day = new DateTime(Period.Year, Period.Month, 13), Hours = Convert.ToDecimal(etsi.TimeSheet.d13) });
                            if (Convert.ToDecimal(etsi.TimeSheet.d14) != 0)
                                e.TimeSheet.Add(new HourPerDay() { Day = new DateTime(Period.Year, Period.Month, 14), Hours = Convert.ToDecimal(etsi.TimeSheet.d14) });
                            if (Convert.ToDecimal(etsi.TimeSheet.d15) != 0)
                                e.TimeSheet.Add(new HourPerDay() { Day = new DateTime(Period.Year, Period.Month, 15), Hours = Convert.ToDecimal(etsi.TimeSheet.d15) });
                            if (Convert.ToDecimal(etsi.TimeSheet.d16) != 0)
                                e.TimeSheet.Add(new HourPerDay() { Day = new DateTime(Period.Year, Period.Month, 16), Hours = Convert.ToDecimal(etsi.TimeSheet.d16) });
                            if (Convert.ToDecimal(etsi.TimeSheet.d17) != 0)
                                e.TimeSheet.Add(new HourPerDay() { Day = new DateTime(Period.Year, Period.Month, 17), Hours = Convert.ToDecimal(etsi.TimeSheet.d17) });
                            if (Convert.ToDecimal(etsi.TimeSheet.d18) != 0)
                                e.TimeSheet.Add(new HourPerDay() { Day = new DateTime(Period.Year, Period.Month, 18), Hours = Convert.ToDecimal(etsi.TimeSheet.d18) });
                            if (Convert.ToDecimal(etsi.TimeSheet.d19) != 0)
                                e.TimeSheet.Add(new HourPerDay() { Day = new DateTime(Period.Year, Period.Month, 19), Hours = Convert.ToDecimal(etsi.TimeSheet.d19) });
                            if (Convert.ToDecimal(etsi.TimeSheet.d20) != 0)
                                e.TimeSheet.Add(new HourPerDay() { Day = new DateTime(Period.Year, Period.Month, 20), Hours = Convert.ToDecimal(etsi.TimeSheet.d20) });
                            if (Convert.ToDecimal(etsi.TimeSheet.d21) != 0)
                                e.TimeSheet.Add(new HourPerDay() { Day = new DateTime(Period.Year, Period.Month, 21), Hours = Convert.ToDecimal(etsi.TimeSheet.d21) });
                            if (Convert.ToDecimal(etsi.TimeSheet.d22) != 0)
                                e.TimeSheet.Add(new HourPerDay() { Day = new DateTime(Period.Year, Period.Month, 22), Hours = Convert.ToDecimal(etsi.TimeSheet.d22) });
                            if (Convert.ToDecimal(etsi.TimeSheet.d23) != 0)
                                e.TimeSheet.Add(new HourPerDay() { Day = new DateTime(Period.Year, Period.Month, 23), Hours = Convert.ToDecimal(etsi.TimeSheet.d23) });
                            if (Convert.ToDecimal(etsi.TimeSheet.d24) != 0)
                                e.TimeSheet.Add(new HourPerDay() { Day = new DateTime(Period.Year, Period.Month, 24), Hours = Convert.ToDecimal(etsi.TimeSheet.d24) });
                            if (Convert.ToDecimal(etsi.TimeSheet.d25) != 0)
                                e.TimeSheet.Add(new HourPerDay() { Day = new DateTime(Period.Year, Period.Month, 25), Hours = Convert.ToDecimal(etsi.TimeSheet.d25) });
                            if (Convert.ToDecimal(etsi.TimeSheet.d26) != 0)
                                e.TimeSheet.Add(new HourPerDay() { Day = new DateTime(Period.Year, Period.Month, 26), Hours = Convert.ToDecimal(etsi.TimeSheet.d26) });
                            if (Convert.ToDecimal(etsi.TimeSheet.d27) != 0)
                                e.TimeSheet.Add(new HourPerDay() { Day = new DateTime(Period.Year, Period.Month, 27), Hours = Convert.ToDecimal(etsi.TimeSheet.d27) });
                            if (Convert.ToDecimal(etsi.TimeSheet.d28) != 0)
                                e.TimeSheet.Add(new HourPerDay() { Day = new DateTime(Period.Year, Period.Month, 28), Hours = Convert.ToDecimal(etsi.TimeSheet.d28) });

                            try
                            {
                                if (Convert.ToDecimal(etsi.TimeSheet.d29) != 0)
                                    e.TimeSheet.Add(new HourPerDay() { Day = new DateTime(Period.Year, Period.Month, 29), Hours = Convert.ToDecimal(etsi.TimeSheet.d29) });
                                if (Convert.ToDecimal(etsi.TimeSheet.d30) != 0)
                                    e.TimeSheet.Add(new HourPerDay() { Day = new DateTime(Period.Year, Period.Month, 30), Hours = Convert.ToDecimal(etsi.TimeSheet.d30) });
                                if (Convert.ToDecimal(etsi.TimeSheet.d31) != 0)
                                    e.TimeSheet.Add(new HourPerDay() { Day = new DateTime(Period.Year, Period.Month, 31), Hours = Convert.ToDecimal(etsi.TimeSheet.d31) });
                            }
                            catch { }

                            s.Employees.OrderBy(p => p.Position).ThenBy(p => p.FullName);
                        }
                    }
                }
                public void GetBioTimeData(string Number, int year, string username)
                {
                    GetDocument(Number, year, username);

                    using (ZupWS.VegaWS service = new ZupWS.VegaWS())
                    {
                        service.SoapVersion = System.Web.Services.Protocols.SoapProtocolVersion.Soap12;

                        var doc = service.GetBIOLINKInfo(username, Number, year);
                        DocNumber = doc.Number;
                        Date = doc.Date;
                        CarriedOut = doc.CarriedOut;
                        FullName = doc.Organization.Name;
                        Period = doc.RegistrationPeriod;

                        foreach (var etsi in doc.Employees)
                        {
                            if (Subdivisions == null)
                                Subdivisions = new List<Subdivision>();

                            Subdivision s = Subdivisions.FirstOrDefault(p => p.Code == etsi.SubdivisionCode);

                            if (s == null)
                            {
                                s = new Subdivision() { Employees = new List<Employee>(), Code = etsi.SubdivisionCode, FullName = etsi.Subdivision };
                                Subdivisions.Add(s);
                            }

                            Employee e = s.Employees.FirstOrDefault(p => p.Code == etsi.Code);

                            if (e == null)
                            {
                                e = new Employee() { Code = etsi.Code, FullName = etsi.Name, Position = etsi.Position, Rate = etsi.Rate, BioTimeData = new List<HourPerDay>() };
                                s.Employees.Add(e);
                            }

                            if (Convert.ToDecimal(etsi.TimeSheet.d1) != 0)
                                e.BioTimeData.Add(new HourPerDay() { Day = new DateTime(Period.Year, Period.Month, 1), Hours = Math.Floor(Convert.ToDecimal(etsi.TimeSheet.d1)) });
                            if (Convert.ToDecimal(etsi.TimeSheet.d2) != 0)
                                e.BioTimeData.Add(new HourPerDay() { Day = new DateTime(Period.Year, Period.Month, 2), Hours = Math.Floor(Convert.ToDecimal(etsi.TimeSheet.d2)) });
                            if (Convert.ToDecimal(etsi.TimeSheet.d3) != 0)
                                e.BioTimeData.Add(new HourPerDay() { Day = new DateTime(Period.Year, Period.Month, 3), Hours = Math.Floor(Convert.ToDecimal(etsi.TimeSheet.d3)) });
                            if (Convert.ToDecimal(etsi.TimeSheet.d4) != 0)
                                e.BioTimeData.Add(new HourPerDay() { Day = new DateTime(Period.Year, Period.Month, 4), Hours = Math.Floor(Convert.ToDecimal(etsi.TimeSheet.d4)) });
                            if (Convert.ToDecimal(etsi.TimeSheet.d5) != 0)
                                e.BioTimeData.Add(new HourPerDay() { Day = new DateTime(Period.Year, Period.Month, 5), Hours = Math.Floor(Convert.ToDecimal(etsi.TimeSheet.d5)) });
                            if (Convert.ToDecimal(etsi.TimeSheet.d6) != 0)
                                e.BioTimeData.Add(new HourPerDay() { Day = new DateTime(Period.Year, Period.Month, 6), Hours = Math.Floor(Convert.ToDecimal(etsi.TimeSheet.d6)) });
                            if (Convert.ToDecimal(etsi.TimeSheet.d7) != 0)
                                e.BioTimeData.Add(new HourPerDay() { Day = new DateTime(Period.Year, Period.Month, 7), Hours = Math.Floor(Convert.ToDecimal(etsi.TimeSheet.d7)) });
                            if (Convert.ToDecimal(etsi.TimeSheet.d8) != 0)
                                e.BioTimeData.Add(new HourPerDay() { Day = new DateTime(Period.Year, Period.Month, 8), Hours = Math.Floor(Convert.ToDecimal(etsi.TimeSheet.d8)) });
                            if (Convert.ToDecimal(etsi.TimeSheet.d9) != 0)
                                e.BioTimeData.Add(new HourPerDay() { Day = new DateTime(Period.Year, Period.Month, 9), Hours = Math.Floor(Convert.ToDecimal(etsi.TimeSheet.d9)) });
                            if (Convert.ToDecimal(etsi.TimeSheet.d10) != 0)
                                e.BioTimeData.Add(new HourPerDay() { Day = new DateTime(Period.Year, Period.Month, 10), Hours = Math.Floor(Convert.ToDecimal(etsi.TimeSheet.d10)) });
                            if (Convert.ToDecimal(etsi.TimeSheet.d11) != 0)
                                e.BioTimeData.Add(new HourPerDay() { Day = new DateTime(Period.Year, Period.Month, 11), Hours = Math.Floor(Convert.ToDecimal(etsi.TimeSheet.d11)) });
                            if (Convert.ToDecimal(etsi.TimeSheet.d12) != 0)
                                e.BioTimeData.Add(new HourPerDay() { Day = new DateTime(Period.Year, Period.Month, 12), Hours = Math.Floor(Convert.ToDecimal(etsi.TimeSheet.d12)) });
                            if (Convert.ToDecimal(etsi.TimeSheet.d13) != 0)
                                e.BioTimeData.Add(new HourPerDay() { Day = new DateTime(Period.Year, Period.Month, 13), Hours = Math.Floor(Convert.ToDecimal(etsi.TimeSheet.d13)) });
                            if (Convert.ToDecimal(etsi.TimeSheet.d14) != 0)
                                e.BioTimeData.Add(new HourPerDay() { Day = new DateTime(Period.Year, Period.Month, 14), Hours = Math.Floor(Convert.ToDecimal(etsi.TimeSheet.d14)) });
                            if (Convert.ToDecimal(etsi.TimeSheet.d15) != 0)
                                e.BioTimeData.Add(new HourPerDay() { Day = new DateTime(Period.Year, Period.Month, 15), Hours = Math.Floor(Convert.ToDecimal(etsi.TimeSheet.d15)) });
                            if (Convert.ToDecimal(etsi.TimeSheet.d16) != 0)
                                e.BioTimeData.Add(new HourPerDay() { Day = new DateTime(Period.Year, Period.Month, 16), Hours = Math.Floor(Convert.ToDecimal(etsi.TimeSheet.d16)) });
                            if (Convert.ToDecimal(etsi.TimeSheet.d17) != 0)
                                e.BioTimeData.Add(new HourPerDay() { Day = new DateTime(Period.Year, Period.Month, 17), Hours = Math.Floor(Convert.ToDecimal(etsi.TimeSheet.d17)) });
                            if (Convert.ToDecimal(etsi.TimeSheet.d18) != 0)
                                e.BioTimeData.Add(new HourPerDay() { Day = new DateTime(Period.Year, Period.Month, 18), Hours = Math.Floor(Convert.ToDecimal(etsi.TimeSheet.d18)) });
                            if (Convert.ToDecimal(etsi.TimeSheet.d19) != 0)
                                e.BioTimeData.Add(new HourPerDay() { Day = new DateTime(Period.Year, Period.Month, 19), Hours = Math.Floor(Convert.ToDecimal(etsi.TimeSheet.d19)) });
                            if (Convert.ToDecimal(etsi.TimeSheet.d20) != 0)
                                e.BioTimeData.Add(new HourPerDay() { Day = new DateTime(Period.Year, Period.Month, 20), Hours = Math.Floor(Convert.ToDecimal(etsi.TimeSheet.d20)) });
                            if (Convert.ToDecimal(etsi.TimeSheet.d21) != 0)
                                e.BioTimeData.Add(new HourPerDay() { Day = new DateTime(Period.Year, Period.Month, 21), Hours = Math.Floor(Convert.ToDecimal(etsi.TimeSheet.d21)) });
                            if (Convert.ToDecimal(etsi.TimeSheet.d22) != 0)
                                e.BioTimeData.Add(new HourPerDay() { Day = new DateTime(Period.Year, Period.Month, 22), Hours = Math.Floor(Convert.ToDecimal(etsi.TimeSheet.d22)) });
                            if (Convert.ToDecimal(etsi.TimeSheet.d23) != 0)
                                e.BioTimeData.Add(new HourPerDay() { Day = new DateTime(Period.Year, Period.Month, 23), Hours = Math.Floor(Convert.ToDecimal(etsi.TimeSheet.d23)) });
                            if (Convert.ToDecimal(etsi.TimeSheet.d24) != 0)
                                e.BioTimeData.Add(new HourPerDay() { Day = new DateTime(Period.Year, Period.Month, 24), Hours = Math.Floor(Convert.ToDecimal(etsi.TimeSheet.d24)) });
                            if (Convert.ToDecimal(etsi.TimeSheet.d25) != 0)
                                e.BioTimeData.Add(new HourPerDay() { Day = new DateTime(Period.Year, Period.Month, 25), Hours = Math.Floor(Convert.ToDecimal(etsi.TimeSheet.d25)) });
                            if (Convert.ToDecimal(etsi.TimeSheet.d26) != 0)
                                e.BioTimeData.Add(new HourPerDay() { Day = new DateTime(Period.Year, Period.Month, 26), Hours = Math.Floor(Convert.ToDecimal(etsi.TimeSheet.d26)) });
                            if (Convert.ToDecimal(etsi.TimeSheet.d27) != 0)
                                e.BioTimeData.Add(new HourPerDay() { Day = new DateTime(Period.Year, Period.Month, 27), Hours = Math.Floor(Convert.ToDecimal(etsi.TimeSheet.d27)) });
                            if (Convert.ToDecimal(etsi.TimeSheet.d28) != 0)
                                e.BioTimeData.Add(new HourPerDay() { Day = new DateTime(Period.Year, Period.Month, 28), Hours = Math.Floor(Convert.ToDecimal(etsi.TimeSheet.d28)) });

                            try
                            {
                                if (Convert.ToDecimal(etsi.TimeSheet.d29) != 0)
                                    e.BioTimeData.Add(new HourPerDay() { Day = new DateTime(Period.Year, Period.Month, 29), Hours = Math.Floor(Convert.ToDecimal(etsi.TimeSheet.d29)) });
                                if (Convert.ToDecimal(etsi.TimeSheet.d30) != 0)
                                    e.BioTimeData.Add(new HourPerDay() { Day = new DateTime(Period.Year, Period.Month, 30), Hours = Math.Floor(Convert.ToDecimal(etsi.TimeSheet.d30)) });
                                if (Convert.ToDecimal(etsi.TimeSheet.d31) != 0)
                                    e.BioTimeData.Add(new HourPerDay() { Day = new DateTime(Period.Year, Period.Month, 31), Hours = Math.Floor(Convert.ToDecimal(etsi.TimeSheet.d31)) });
                            }
                            catch { }

                            s.Employees.OrderBy(p => p.Position).ThenBy(p => p.FullName);
                        }
                    }
                }
                public void CreateDocument(string username)
                {
                    using (ZupWS.VegaWS service = new ZupWS.VegaWS())
                    {
                        service.SoapVersion = System.Web.Services.Protocols.SoapProtocolVersion.Soap12;
                        var document = service.CreateTimeSheet(FullName, username, Period.ToString("yyyyMM01"), new DateTime(Period.Year, Period.Month, DateTime.DaysInMonth(Period.Year, Period.Month)).ToString("yyyyMMdd"));

                        DocNumber = document.Number;
                        Date = document.Date;
                    }
                }
                public bool SaveDocument(string JSONString, string username)
                {
                    try
                    {
                        string DocNumber = "";
                        int year = 0;
                        ETS = Deserialize(JSONString.ToString()).Where(p => p.Code != null).ToList();

                        for (int i = ETS.Count - 1; i >= 0; i--)
                        {
                            int total = 0;
                            int.TryParse(ETS[i].Hours, out total);

                            if (total <= 0)
                                ETS.Remove(ETS[i]);
                        }

                        List<ZupWS.Employee> TimeSheet = new List<ZupWS.Employee>();

                        foreach (var e in ETS)
                        {
                            if (e.source != null && e.source == "BioTime")
                                continue;

                            DocNumber = e.DocNumber;
                            year = e.Date.Year;
                            ZupWS.Employee employee = new ZupWS.Employee();
                            employee.TimeSheet = new ZupWS.TimeSheet();
                            employee.Code = e.Code;
                            employee.TimeSheet.d1 = e.d1 == null ? 0 : (double)e.d1;
                            employee.TimeSheet.d2 = e.d2 == null ? 0 : (double)e.d2;
                            employee.TimeSheet.d3 = e.d3 == null ? 0 : (double)e.d3;
                            employee.TimeSheet.d4 = e.d4 == null ? 0 : (double)e.d4;
                            employee.TimeSheet.d5 = e.d5 == null ? 0 : (double)e.d5;
                            employee.TimeSheet.d6 = e.d6 == null ? 0 : (double)e.d6;
                            employee.TimeSheet.d7 = e.d7 == null ? 0 : (double)e.d7;
                            employee.TimeSheet.d8 = e.d8 == null ? 0 : (double)e.d8;
                            employee.TimeSheet.d9 = e.d9 == null ? 0 : (double)e.d9;
                            employee.TimeSheet.d10 = e.d10 == null ? 0 : (double)e.d10;
                            employee.TimeSheet.d11 = e.d11 == null ? 0 : (double)e.d11;
                            employee.TimeSheet.d12 = e.d12 == null ? 0 : (double)e.d12;
                            employee.TimeSheet.d13 = e.d13 == null ? 0 : (double)e.d13;
                            employee.TimeSheet.d14 = e.d14 == null ? 0 : (double)e.d14;
                            employee.TimeSheet.d15 = e.d15 == null ? 0 : (double)e.d15;
                            employee.TimeSheet.d16 = e.d16 == null ? 0 : (double)e.d16;
                            employee.TimeSheet.d17 = e.d17 == null ? 0 : (double)e.d17;
                            employee.TimeSheet.d18 = e.d18 == null ? 0 : (double)e.d18;
                            employee.TimeSheet.d19 = e.d19 == null ? 0 : (double)e.d19;
                            employee.TimeSheet.d20 = e.d20 == null ? 0 : (double)e.d20;
                            employee.TimeSheet.d21 = e.d21 == null ? 0 : (double)e.d21;
                            employee.TimeSheet.d22 = e.d22 == null ? 0 : (double)e.d22;
                            employee.TimeSheet.d23 = e.d23 == null ? 0 : (double)e.d23;
                            employee.TimeSheet.d24 = e.d24 == null ? 0 : (double)e.d24;
                            employee.TimeSheet.d25 = e.d25 == null ? 0 : (double)e.d25;
                            employee.TimeSheet.d26 = e.d26 == null ? 0 : (double)e.d26;
                            employee.TimeSheet.d27 = e.d27 == null ? 0 : (double)e.d27;
                            employee.TimeSheet.d28 = e.d28 == null ? 0 : (double)e.d28;
                            employee.TimeSheet.d29 = e.d29 == null ? 0 : (double)e.d29;
                            employee.TimeSheet.d30 = e.d30 == null ? 0 : (double)e.d30;
                            employee.TimeSheet.d31 = e.d31 == null ? 0 : (double)e.d31;
                            TimeSheet.Add(employee);
                        }

                        using (ZupWS.VegaWS service = new ZupWS.VegaWS())
                        {
                            service.SoapVersion = System.Web.Services.Protocols.SoapProtocolVersion.Soap12;
                            service.SaveTimeSheet(username, TimeSheet.ToArray(), DocNumber, year);
                        }

                        return true;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
            public class AccrualAndDetentionViewModel : DocumentInfo
            {
                public List<EmployeeAccrualsAndDetentions> EAAD = new List<EmployeeAccrualsAndDetentions>();
                private EmployeeAccrualsAndDetentions[] Deserialize(string JSONString)
                {
                    return JsonConvert.DeserializeObject<EmployeeAccrualsAndDetentions[]>(JSONString);
                }

                /*
                public void GetEmployees(string orgname, string username)
                {
                    using (ZupWS.VegaWS service = new ZupWS.VegaWS())
                    {
                        service.SoapVersion = System.Web.Services.Protocols.SoapProtocolVersion.Soap12;

                        var employees = service.GetEmployees(orgname, username, );

                        foreach (var e in employees)
                        {
                            Subdivision s = Subdivisions.FirstOrDefault(p => p.Code == e.SubdivisionCode);

                            if (s == null)
                            {
                                s = new Subdivision() { Code = e.SubdivisionCode, FullName = e.Subdivision, Employees = new List<Employee>() };
                                Subdivisions.Add(s);
                            }

                            Employee emp = s.Employees.FirstOrDefault(p => p.Code == e.Code);

                            if (emp == null)
                            {
                                emp = new Employee();
                                s.Employees.Add(emp);
                            }

                            emp.Code = e.Code;
                            emp.FullName = e.Name;
                            emp.Rate = e.Rate;
                            emp.Position = e.Position;
                        }
                    }
                }
                */
                public void GetEmployeesAccrualsType(string username)
                {
                    using (ZupWS.VegaWS service = new ZupWS.VegaWS())
                    {
                        service.SoapVersion = System.Web.Services.Protocols.SoapProtocolVersion.Soap12;

                        var accruals = service.GetAccrualsTypes();

                        foreach (var s in Subdivisions)
                            foreach (var e in s.Employees)
                                e.Accruals = accruals.ToList();
                    }
                }
                public void GetEmployeesDetentionsType(string username)
                {
                    using (ZupWS.VegaWS service = new ZupWS.VegaWS())
                    {
                        service.SoapVersion = System.Web.Services.Protocols.SoapProtocolVersion.Soap12;

                        var detentions = service.GetDetentionsTypes();

                        foreach (var s in Subdivisions)
                            foreach (var e in s.Employees)
                                e.Detentions = detentions.ToList();
                    }
                }
                public void GetAccrualsAndDetentionsDocuments(string orgname, string username)
                {
                    if (orgname == null)
                        orgname = "";

                    GetOrganizationList(username);

                    if (orgname == "")
                        orgname = FullName;

                    using (ZupWS.VegaWS service = new ZupWS.VegaWS())
                    {
                        service.SoapVersion = System.Web.Services.Protocols.SoapProtocolVersion.Soap12;

                        var docs = service.GetAccrualsAndDetentionsDocuments(username, orgname);

                        foreach (var doc in docs)
                            if (!doc.Deleted)
                                Documents.Add(doc);
                    }
                }
                public void GetDocument(string number, int year, string username)
                {
                    using (ZupWS.VegaWS service = new ZupWS.VegaWS())
                    {
                        service.SoapVersion = System.Web.Services.Protocols.SoapProtocolVersion.Soap12;

                        var doc = service.GetAccrualsAndDetentionsCharges(username, number, year);
                        DocNumber = doc.Number;
                        Date = doc.Date;
                        CarriedOut = doc.CarriedOut;
                        FullName = doc.Organization.Name;
                        Period = doc.RegistrationPeriod;

                        foreach (var item in doc.Employees)
                        {
                            if (Subdivisions == null)
                                Subdivisions = new List<Subdivision>();

                            Subdivision s = Subdivisions.FirstOrDefault(p => p.Code == item.SubdivisionCode);

                            if (s == null)
                            {
                                s = new Subdivision() { Employees = new List<Employee>(), Code = item.SubdivisionCode, FullName = item.Subdivision };
                                Subdivisions.Add(s);
                            }

                            Employee e = s.Employees.FirstOrDefault(p => p.Code == item.Code);

                            if (e == null)
                            {
                                e = new Employee() { Code = item.Code, FullName = item.Name, Position = item.Position, Rate = item.Rate, Hours = item.Hours, TimeSheet = new List<HourPerDay>() };
                                s.Employees.Add(e);
                            }

                            e.Accruals = item.Accruals.ToList();
                            e.Detentions = item.Detentions.ToList();
                        }
                    }
                }
                public bool SaveDocument(string JSONString, string username)
                {
                    try
                    {
                        object DocNumber = null;
                        object DocDate = null;
                        int year = 0;

                        var serializer = new JavaScriptSerializer();
                        var heapdata = serializer.DeserializeObject(JSONString);

                        foreach (var undata in (Array)heapdata)
                        {
                            var r = (Dictionary<string, object>)undata;

                            if (r.Keys.Contains("Code") && r.Keys.Contains("DocNumber") && r.Keys.Contains("Date"))
                            {
                                if(DocNumber == null)
                                    r.TryGetValue("DocNumber", out DocNumber);

                                if (DocDate == null)
                                    r.TryGetValue("Date", out DocDate);

                                var accruals = r.Where(p => p.Key.Contains("_ACCCODE_")).ToList();
                                var detentions = r.Where(p => p.Key.Contains("_DETCODE_")).ToList();

                                EmployeeAccrualsAndDetentions e = new EmployeeAccrualsAndDetentions();
                                object Code = null;
                                r.TryGetValue("Code", out Code);
                                object Organization = null;
                                r.TryGetValue("Code", out Organization);
                                object Period = null;
                                r.TryGetValue("Code", out Period);
                                e.Accruals = new List<ZupWS.Accrual>();
                                e.Detentions = new List<ZupWS.Detention>();

                                e.Code = Code.ToString();
                                e.Date = DateTime.Parse(DocDate.ToString());
                                e.DocNumber = DocNumber.ToString();
                                e.Organization = Organization.ToString();
                                e.Period = Period.ToString();
                                EAAD.Add(e);

                                foreach (var a in accruals)
                                {
                                    string acccode = a.Key.Replace("_ACCCODE_", "");
                                    Decimal value = 0;
                                    Decimal.TryParse(a.Value.ToString(), out value);

                                    if(value != 0)
                                        e.Accruals.Add(new ZupWS.Accrual() { Code = acccode, Sum = value });
                                }

                                foreach (var d in detentions)
                                {
                                    string detcode = d.Key.Replace("_DETCODE_", "");
                                    Decimal value = 0;
                                    Decimal.TryParse(d.Value.ToString(), out value);

                                    if (value != 0)
                                        e.Detentions.Add(new ZupWS.Detention() { Code = detcode, Sum = value });
                                }
                            }
                        }

                        List<ZupWS.Employee> Employees = new List<ZupWS.Employee>();

                        foreach (var e in EAAD)
                        {
                            if (e.Accruals.Sum(p => p.Sum) == 0 && e.Detentions.Sum(p => p.Sum) == 0)
                                continue;

                            DocNumber = e.DocNumber;
                            year = e.Date.Year;
                            ZupWS.Employee employee = new ZupWS.Employee();
                            employee.Accruals = e.Accruals.ToArray();
                            employee.Detentions = e.Detentions.ToArray();
                            employee.Code = e.Code;
                            Employees.Add(employee);
                        }

                        if (Employees.Count > 0)
                            using (ZupWS.VegaWS service = new ZupWS.VegaWS())
                            {
                                service.SoapVersion = System.Web.Services.Protocols.SoapProtocolVersion.Soap12;
                                ZupWS.Document doc = service.SaveAccrualsAndDetentions(username, Employees.ToArray(), DocNumber.ToString(), year);
                            }

                        return true;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                public void CreateDocument(string username)
                {
                    using (ZupWS.VegaWS service = new ZupWS.VegaWS())
                    {
                        service.SoapVersion = System.Web.Services.Protocols.SoapProtocolVersion.Soap12;
                        var document = service.CreateAccrualsAndDetentionsDocument(FullName, username, Period.ToString("yyyyMM01"));

                        DocNumber = document.Number;
                        Date = document.Date;
                    }
                }
            }
            public class EmployeesRateModel
            {
                public DateTime Period { get; set; }
                public string FullName { get; set; }
                public List<ZupWS.Organization> Organizations = new List<ZupWS.Organization>();
                public IEnumerable<SelectListItem> OrganizationsSelectList
                {
                    get
                    {
                        List<SelectListItem> Orgs = new List<SelectListItem>();

                        foreach (ZupWS.Organization org in Organizations)
                            Orgs.Add(new SelectListItem() { Text = org.Name, Value = org.Name });

                        SelectListItem sli = Orgs.FirstOrDefault(p => p.Value == FullName);

                        if (sli != null)
                            sli.Selected = true;

                        return Orgs;
                    }
                }
                public void GetOrganizationList(string username)
                {
                    using (ZupWS.VegaWS service = new ZupWS.VegaWS())
                    {
                        service.SoapVersion = System.Web.Services.Protocols.SoapProtocolVersion.Soap12;
                        Organizations = service.GetOrganizations(username).ToList();
                    }

                    if (Organizations.Count > 0 && String.IsNullOrEmpty(FullName))
                        FullName = Organizations[0].Name;
                }
                public List<Subdivision> Subdivisions { get; set; }
                public void GetEmployees(string orgname, string username)
                {
                    using (ZupWS.VegaWS service = new ZupWS.VegaWS())
                    {
                        service.SoapVersion = System.Web.Services.Protocols.SoapProtocolVersion.Soap12;

                        var employees = service.GetEmployees(orgname, username);

                        foreach (var employee in employees)
                        {
                            if (Subdivisions == null)
                                Subdivisions = new List<Subdivision>();

                            Subdivision s = Subdivisions.FirstOrDefault(p => p.Code == employee.SubdivisionCode);

                            if (s == null)
                            {
                                s = new Subdivision() { Employees = new List<Employee>(), Code = employee.SubdivisionCode, FullName = employee.Subdivision };
                                Subdivisions.Add(s);
                            }

                            Employee e = s.Employees.FirstOrDefault(p => p.Code == employee.Code);

                            if (e == null)
                            {
                                e = new Employee() { Code = employee.Code, FullName = employee.Name, Position = employee.Position, Rate = employee.Rate, RateNew = employee.Rate, TimeSheet = new List<HourPerDay>() };
                                s.Employees.Add(e);
                            }

                            var ratesForPosition = service.GetListRate(orgname, e.Position);
                            e.RatesList = new List<SelectListItem>();

                            for(int i = 0; i < ratesForPosition.Length; i++)
                            {
                                if (e.Rate == ratesForPosition[i])
                                    e.RatesList.Add(new SelectListItem() { Text = ratesForPosition[i], Value = ratesForPosition[i], Selected = true });
                                else
                                    e.RatesList.Add(new SelectListItem() { Text = ratesForPosition[i], Value = ratesForPosition[i] });
                            }

                            s.Employees.OrderBy(p => p.Position).ThenBy(p => p.FullName);
                        }
                    }
                }
                public void Save(string username)
                {
                    List<ZupWS.Employee> Employees = new List<ZupWS.Employee>();

                    foreach (var sub in Subdivisions)
                    {
                        foreach (var emp in sub.Employees)
                        {
                            ZupWS.Employee e = new ZupWS.Employee();
                            e.Code = emp.Code;
                            e.Rate = emp.Rate;
                            e.RateNew = emp.RateNew;
                            Employees.Add(e);
                        }
                    }

                    using (ZupWS.VegaWS service = new ZupWS.VegaWS())
                    {
                        service.SoapVersion = System.Web.Services.Protocols.SoapProtocolVersion.Soap12;
                        service.SaveListRate(Employees.ToArray(), FullName, username, Period.ToString("dd.MM.yyyy"));
                    }
                }
            }
            public class BioTimeViewModel
            { 
                public void Test(string userName)
                {
                    using (ZupWS.VegaWS service = new VegaWS())
                    {
                        service.SoapVersion = System.Web.Services.Protocols.SoapProtocolVersion.Soap12;
                        var t = service.GetBIOLINKInfo(userName, "00ЗК-000175", 2017);
                    }
                }
            }
        }
    }
}