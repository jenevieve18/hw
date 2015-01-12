//	<file>
//		<license></license>
//		<owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
//	</file>

using System;
using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Threading;

namespace HW.Grp
{
	public class R
	{
        static readonly string English = "HW.Grp.Properties.en-US";
        static readonly string Swedish = "HW.Grp.Properties.sv-SE";
        const int EnglishID = 2;
        const int SwedishID = 1;
        const int DefaultID = SwedishID;

        static string GetLangID(int langID)
        {
            switch (langID)
            {
                case EnglishID:
                    return English;
                case SwedishID:
                    return Swedish;
                default:
                    return English;
            }
        }

        /*public static CultureInfo GetCultureInfo(int langID)
        {
            switch (langID)
            {
                case SwedishID: return CultureInfo.GetCultureInfo("sv-SE");
                default: return CultureInfo.GetCultureInfo("en-US");
            }
        }*/

        public static string Str(int langID, string name)
        {
            return Str(langID, name, "");
        }

        public static string Str(int langID, string name, string def)
        {
            try
            {
                ResourceManager resource = new ResourceManager(GetLangID(langID), Assembly.GetExecutingAssembly());
                return resource.GetString(name).ToString();
            }
            catch (Exception ex)
            {
                return def;
            }
        }

        public static string Str(string name, string def)
        {
            return Str(DefaultID, name, def);
        }

        public static string Str(string name)
        {
            return Str(name, "");
        }
	}
}
