using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiEmulardorCLiente.Core.Helps;

public static class PeruDateTime
{
    public static DateTime Now
    {
        get
        {
            TimeZoneInfo mawson = TimeZoneInfo.CreateCustomTimeZone("Peru", new TimeSpan(-5, 00, 00), "(GMT-05:00) Peru Time", "Peru");
            return TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Local, mawson);
        }

    }
}
public static class ClassAux
{
    public static string ToJson(this object val)
    {
        return JsonConvert.SerializeObject(val, Formatting.Indented, new JsonSerializerSettings() { PreserveReferencesHandling = PreserveReferencesHandling.None, ReferenceLoopHandling = ReferenceLoopHandling.Ignore, ObjectCreationHandling = ObjectCreationHandling.Reuse });
    }
    public static Int32 ToInteger(this object val)
    {
        return ToInteger(val, 0);
    }
    public static Int32 ToInteger(this object val, Int32 def)
    {
        try
        {
            Int32 reval = 0;

            if (Int32.TryParse(val.ToString(), out reval))
                return reval;
        }
        catch (Exception)
        {
        }
        return def;
    }
}
