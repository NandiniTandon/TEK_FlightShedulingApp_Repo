using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace TEKApp.Models.Parameters
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum DayScheduleEnum
    {
        [EnumMember(Value = "1")]
        Day1 =1,
        [EnumMember(Value = "2")]
        Day2 =2,
       
    }

}

