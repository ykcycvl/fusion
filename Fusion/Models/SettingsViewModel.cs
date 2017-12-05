using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fusion.Models
{
    public class SettingsViewModel
    {
        Entities db = new Entities();
        public class Setting
        {
            string SettingName { get; set; }
            string SettingValue { get; set; }
        }

        List<Setting> Settings { get; set; }

        public SettingsViewModel()
        {

        }
    }
}