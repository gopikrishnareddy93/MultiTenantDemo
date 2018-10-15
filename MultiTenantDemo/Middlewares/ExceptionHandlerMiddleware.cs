﻿using System;
using System.Net;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace MultiTenantDemo.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate request;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            this.request = next;
        }
        
        public Task Invoke(HttpContext context) => this.InvokeAsync(context); 

        async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await this.request(context);
            }
            catch (Exception exception)
            {
                var httpStatusCode = ConfigurateExceptionTypes(exception);

                context.Response.StatusCode = httpStatusCode;
                //TODO: [Temp fix] Rebuild this later to proper JSON error object serialization 
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync("{error: "+ exception.Message + "}");
                context.Response.Headers.Clear();
            }
        }

        private static int ConfigurateExceptionTypes(Exception exception)
        {
            int httpStatusCode;

            // Exception type To Http Status configuration 
            switch (exception)
            {
                case var _ when exception is ValidationException:
                    httpStatusCode = (int) HttpStatusCode.BadRequest;
                   break;
                default:
                    httpStatusCode = (int) HttpStatusCode.InternalServerError;
                  break;
            }

            return httpStatusCode;
        }
    }
}
