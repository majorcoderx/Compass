using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

/// <summary>
/// Code auto generation
/// </summary>

namespace Laban
{
    public class LocalValueControl
    {
        public static void SettingLocalValue(string str)
        {
            var localSetting = ApplicationData.Current.LocalSettings;
            localSetting.Values["check"] = str;
        }
        public static string GetValueLocalSetting()
        {
            var localSetting = ApplicationData.Current.LocalSettings;
            if (localSetting.Values["check"] == null)
            {
                return "none";
            }
            else
                return (string)(localSetting.Values["check"]);
        }

        public static void SetCountValueLocalSet(int numb)
        {
            var localSetting = ApplicationData.Current.LocalSettings;
            localSetting.Values["numb"] = numb;
        }

        public static int GetValueLocalSet()
        {
            var localSetting = ApplicationData.Current.LocalSettings;
            if (localSetting.Values["numb"] == null)
            {
                return 0;
            }
            else
                return (int)(localSetting.Values["numb"]);
        }
    }
}
