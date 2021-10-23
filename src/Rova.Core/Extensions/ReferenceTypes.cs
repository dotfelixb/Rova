using System;
using System.Text;
using System.Text.Json;

namespace Rova.Core.Extensions
{
    public static class ReferenceTypes
    {
        public static StringContent ToJsonStringContent<T>(this T self)
            where T : class =>
            new(JsonSerializer.Serialize(self)
                , Encoding.UTF8
                , "application/json");

        public static Guid ToGuid(this string self) =>
            Guid.TryParse(self, out var rst) ? rst : Guid.Empty;

        public static DateTimeOffset ToDateTimeTz(this string self) =>
            DateTimeOffset.TryParse(self, out var rst) ? rst : DateTimeOffset.MinValue;

        public static decimal ToDecimal(this string self) =>
            decimal.TryParse(self, out var rst) ? rst : 0.0M;
    }
}

