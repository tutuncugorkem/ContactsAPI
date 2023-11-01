using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Contacts.Core.DTOs
{
    public class CustomResponseDto<T>
    {
        public T Data { get; set; }

        [JsonIgnore]     //statuscodu clienta dönmüyoruz, zaten onlar istek yaptığında görecek
        public int StatusCode { get; set; }

        public List<String> Errors { get; set; }

        //alta statik metodlar yazdık, yeni nesneler dönsün duruma göre  : STATIC FACTORY design pattern
        public static CustomResponseDto<T> Success(int statusCode, T data)
        {
            return new CustomResponseDto<T> { Data = data, StatusCode = statusCode };
        }
        public static CustomResponseDto<T> Success(int statusCode)
        {
            return new CustomResponseDto<T> {StatusCode = statusCode };
        }
        public static CustomResponseDto<T> Fail(int statusCode, List<string> errors)
        {
            return new CustomResponseDto<T> { Errors = errors, StatusCode = statusCode };
        }
        public static CustomResponseDto<T> Fail(int statusCode, string error)
        {
            return new CustomResponseDto<T> { Errors = new List<string> { error} , StatusCode = statusCode };
        }
    }
}
