using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using V83;
using System.Web.Configuration;
using System.Reflection;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

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
            public string FullName { get; set; }
            public List<string> Organizations { get; set; }
            public List<Subdivision> Subdivisions { get; set; }
            public List<Detention> Detentions { get; set; }
            public List<Accrual> Accruals { get; set; }
            [DataType(DataType.Date)]
            public DateTime Period { get; set; }

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

            public void GetDivisionList()
            {
                Organizations = new List<string>();

                try
                {
                    dynamic QueryTo1C = connection.NewObject("Запрос");

                    try
                    {
                        QueryTo1C.Text = @"ВЫБРАТЬ Организации.Наименование ИЗ Справочник.Организации КАК Организации";
                        dynamic res = QueryTo1C.Execute().Choose();

                        while (res.Next())
                        {
                            Organizations.Add(res.Наименование);
                        }

                        if(Organizations.Count > 0 && FullName != null && FullName != "")
                            FullName = Organizations[0];
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
            }
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
                        e.TimeSheet.Add(new HourPerDay() { Day = new DateTime(Period.Year, Period.Month, i) });
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
                                        det = false;
                                        det = null;
                                    }
                                }

                                /* Начисления */
                                for (int k = 0; k < Subdivisions[i].Employees[j].Accruals.Count; k++)
                                {
                                    if (Subdivisions[i].Employees[j].Accruals[k].Sum != null && Subdivisions[i].Employees[j].Accruals[k].Sum > 0)
                                    {
                                        dynamic det = doc.Начисления.Добавить();
                                        det.Начисление = connection.ПланыВидовРасчета.Начисления.НайтиПоКоду(Subdivisions[i].Employees[j].Accruals[k].Code);
                                        det.ДатаНачала = Period.ToString("yyyyMM01");
                                        det.ДатаОкончания = Period.ToString("yyyyMM") + DateTime.DaysInMonth(Period.Year, Period.Month).ToString();
                                        det.Результат = Subdivisions[i].Employees[j].Accruals[k].Sum;
                                        det.Сотрудник = connection.Справочники.Сотрудники.НайтиПоКоду(Subdivisions[i].Employees[j].Code);
                                        det = false;
                                        det = null;
                                    }
                                }
                            }
                        }

                        doc.Записать();

                        /* Конец Удержания и начисления*/

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
                                        }
                                        else
                                        {
                                            dynamic Str = connection.NewObject("Структура");
                                            Str.Вставить("Сотрудник", connection.Справочники.Сотрудники.НайтиПоКоду(Subdivisions[i].Employees[j].Code));
                                            Str.Вставить("Часов" + Subdivisions[i].Employees[j].TimeSheet[k].Day.Day.ToString(), 0);
                                            Str.Вставить("ВидВремени" + Subdivisions[i].Employees[j].TimeSheet[k].Day.Day.ToString(), connection.Справочники.ВидыИспользованияРабочегоВремени.ВыходныеДни);
                                            connection.ЗаполнитьЗначенияСвойств(dot, Str);
                                        }
                                    }
                                }
                            }
                        }

                        doc.Записать();
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
                public string DAType { get; set; }
                public DateTime period { get; set; }
                public DateTime Date { get; set; }
                public string OrganizationName { get; set; }
                public Decimal Accrued { get; set; }
                public Decimal Detented { get; set; }
            }
            public List<DA> Items = new List<DA>();
            public void GetDAList()
            {
                dynamic QueryTo1C = connection.NewObject("Запрос");
                QueryTo1C.Text = String.Format(@"ВЫБРАТЬ 
	                РАЗЛИЧНЫЕ
	                НачисленияУдержанияПоСотрудникам.Период,
	                НачисленияУдержанияПоСотрудникам.Регистратор.Номер КАК Номер,
	                НачисленияУдержанияПоСотрудникам.Регистратор.Дата КАК Дата,
	                ПРЕДСТАВЛЕНИЕ(ТИПЗНАЧЕНИЯ(НачисленияУдержанияПоСотрудникам.Регистратор)) Как Тип,
	                НачисленияУдержанияПоСотрудникам.Организация.Наименование КАК Наименование, 
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
	                РегистрНакопления.НачисленияУдержанияПоСотрудникам КАК НачисленияУдержанияПоСотрудникам
                СГРУППИРОВАТЬ ПО
	                НачисленияУдержанияПоСотрудникам.Период,
	                НачисленияУдержанияПоСотрудникам.Регистратор, 
	                НачисленияУдержанияПоСотрудникам.Организация.Наименование");
                dynamic res = QueryTo1C.Execute().Choose();

                while (res.Next())
                {
                    DA da = new DA();
                    da.period = DateTime.Parse(Convert.ToString(res.Период));
                    da.Date = DateTime.Parse(Convert.ToString(res.Дата));
                    da.DAType = Convert.ToString(res.Тип);
                    da.Number = Convert.ToString(res.Номер);
                    da.OrganizationName = Convert.ToString(res.Наименование);
                    da.Accrued = Convert.ToDecimal(res.Начислено);
                    da.Detented = Convert.ToDecimal(res.Удержано);
                    Items.Add(da);
                }

                QueryTo1C = null;
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

                QueryTo1C = null;
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
                        НачисленияУдержанияПоСотрудникам.Сотрудник.Код,
                        НачисленияУдержанияПоСотрудникам.Период КАК Период,
	                    НачисленияУдержанияПоСотрудникам.Регистратор.Номер КАК Номер,
	                    НачисленияУдержанияПоСотрудникам.Организация.Наименование КАК НаименованиеОрганизации,
	                    НачисленияУдержанияПоСотрудникам.Сотрудник.Наименование КАК ИмяСотрудника,
	                    НачисленияУдержанияПоСотрудникам.НачислениеУдержание.Наименование КАК НаименованиеНачисленияУдержания,
	                    НачисленияУдержанияПоСотрудникам.ГруппаНачисленияУдержанияВыплаты КАК ГуппаНачисленияУдержания,
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
                        Employees.Add(e);
                    }

                    e.Accruals = new List<Accrual>();
                    e.Detentions = new List<Detention>();
                    e.Code = ecode;
                    e.FullName = Convert.ToString(res.ИмяСотрудника);
                    e.DetentionsSum = Convert.ToDecimal(res.Удержано);

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

                QueryTo1C = null;
            }
        }
    }
}