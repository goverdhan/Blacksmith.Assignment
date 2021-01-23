using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blacksmith.Assignment.Models
{
    public class Response<T> where T : class
    {
        public Response(bool status, string message, int? errorCode, List<string> errors, List<T> objectList, T obj)
        {
            Status = status;
            Message = message;
            ErrorCode = errorCode;
            Errors = errors;
            ObjectList = objectList;
            Object = obj;
        }

        public bool Status { get; set; }
        public string Message { get; set; }

        public int? ErrorCode { get; set; }
        public List<string> Errors { get; set; }
        public List<T> ObjectList { get; set; }
        public T Object { get; set; }

        public static Response<T> GetResponse(T obj, string sucessMessage)
        {
            return new Response<T>(true, sucessMessage, null, null, null, obj);
        }

        public static Response<T> GetResponse(int errorCode,List<string> errors)
        {

            return new Response<T>(false,string.Empty, errorCode, errors, null, null);

        }




        public static Response<T> GetResponse(List<T> objList, string sucessMessage)
        {
           
                return new Response<T>(true, sucessMessage,null, null, objList, null);
            
           
        }



    }
}
