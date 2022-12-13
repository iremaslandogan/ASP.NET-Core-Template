using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class CustomResponseDto<T>
    {
        public T data { get; set; }

        [JsonIgnore]
        public int statusCode { get; set; }

        public int count { get; set; }

        public string message { get; set; }

        public static CustomResponseDto<T> Success( int statusCode, string message, int count, T data)
        {
            return new CustomResponseDto<T> { statusCode = statusCode, message = message, count = count, data = data };
        }
        public static CustomResponseDto<T> Success(int statusCode, string message)
        {
            return new CustomResponseDto<T> { statusCode = statusCode, message = message };
        }

        public static CustomResponseDto<T> Fail(int statusCode, string message)
        {
            return new CustomResponseDto<T> { statusCode = statusCode, message = message };
        }

        
    }
}
