using Microsoft.AspNetCore.Components.Web;

namespace Api.DataAccess
{
    public class ResponseModel<T>
    {
        public bool? IsError { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
    }
}
