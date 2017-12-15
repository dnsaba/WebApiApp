using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiApp.Responses
{
    public class ItemResponse<T> : SuccessResponse
    {
        public T Item { get; set; }
    }
}