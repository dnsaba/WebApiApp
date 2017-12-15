﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiApp.Responses
{
    public class ErrorResponse : BaseResponse
    {
        public List<string> Errors { get; set; }

        public ErrorResponse(string errMsg)
        {
            Errors = new List<string>();

            Errors.Add(errMsg);
            this.IsSuccessful = false;
        }
    }
}