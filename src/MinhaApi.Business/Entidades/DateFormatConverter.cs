using Newtonsoft.Json.Converters;

namespace MinhaApi.Business.Entidades
{
    public class DateFormatConverter : IsoDateTimeConverter
    {
        public DateFormatConverter(string format)
        {
            base.DateTimeFormat = format;
        }
    }
}


