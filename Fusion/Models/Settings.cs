using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fusion.Models
{
    public class Settings
    {
        private Entities db = new Entities();

        public List<bitrixUser> UserDataBinds { get; set; }

        public void GetUserBidings()
        {
            UserDataBinds = db.bitrixUser.ToList();
        }

        public void SaveUserBindings()
        {
            foreach (var b in UserDataBinds)
            {
                var tmp = db.bitrixUser.FirstOrDefault(p => p.id == b.id);

                if (tmp == null)
                    db.bitrixUser.Add(b);
                else
                    tmp = b;
            }

            db.SaveChanges();
        }

        public class Setting
        {
            public int SettingID { get; set; }
            public string SettingTitle { get; set; }
            public string SettingType { get; set; }
            public string StringValue { get; set; }
            public bool BoolValue { get; set; }
            public int IntValue { get; set; }
        }
        public class Personal
        {
            public List<Setting> UserSettings { get; set; }
        }

        public Personal PersonalSettings { get; set; }

        public void GetUserSettings(string UserName)
        {
            PersonalSettings = new Personal();
            PersonalSettings.UserSettings = new List<Setting>();
            var settings = db.VegaSetting.Where(p => p.IsActive).ToList();

            foreach (var s in settings)
            {
                var val = db.VegaPersonalSetting.FirstOrDefault(p => p.UserName.ToLower() == UserName.ToLower() && p.SettingID == s.id);
                string settingValue = null;

                if (val != null)
                    settingValue = val.SettingValue;

                PersonalSettings.UserSettings.Add(new Setting() { SettingID = s.id, SettingTitle = s.SettingTitle, SettingType = s.SettingType });

                if (settingValue != null)
                    if (s.SettingType == "System.Boolean")
                        PersonalSettings.UserSettings[PersonalSettings.UserSettings.Count - 1].BoolValue = Convert.ToBoolean(settingValue);
                    else
                        if (s.SettingType == "System.Int32")
                            PersonalSettings.UserSettings[PersonalSettings.UserSettings.Count - 1].IntValue = Convert.ToInt32(settingValue);
                        else 
                            if(s.SettingType == "System.String")
                                PersonalSettings.UserSettings[PersonalSettings.UserSettings.Count - 1].StringValue = settingValue;
            }
        }

        public void SaveUserSettings(string UserName)
        {
            foreach (var s in PersonalSettings.UserSettings)
            {
                var t = db.VegaPersonalSetting.FirstOrDefault(p => p.SettingID == s.SettingID && p.UserName.ToLower() == UserName.ToLower());

                if (t == null)
                {
                    t = new VegaPersonalSetting() { SettingID = s.SettingID, UserName = UserName };
                    db.VegaPersonalSetting.Add(t);
                }

                if (s.SettingType == "System.Boolean")
                    t.SettingValue = s.BoolValue.ToString().ToLower();
                else
                    if (s.SettingType == "System.Int32")
                        t.SettingValue = s.IntValue.ToString();
                    else
                        if (s.SettingType == "System.String")
                            t.SettingValue = s.StringValue;

                db.SaveChanges();
            }
        }
    }
}