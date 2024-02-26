using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NLayer.Core.DTOs
{
    public class CustomeResponseDto<T>
    {
        public T Data { get; set; }


        [JsonIgnore]
        public int StatusCode { get; set; }


        public List<String> Erros { get; set; }



        public static CustomeResponseDto<T> Success(T data, int statusCode)
        {
            return new CustomeResponseDto<T> { Data = data, StatusCode = statusCode};
        }

        public static CustomeResponseDto<T> Success(int statusCode)
        {
            return new CustomeResponseDto<T> { StatusCode = statusCode, };
        }


        public static CustomeResponseDto<T> Fail(List<string> errors, int statusCode)
        {
            return new CustomeResponseDto<T> { Erros = errors, StatusCode = statusCode };
        }

        public static CustomeResponseDto<T> Fail(string error, int statusCode)
        {
            return new CustomeResponseDto<T> { Erros = new List<string> { error }, StatusCode = statusCode };
        }


    }
}
