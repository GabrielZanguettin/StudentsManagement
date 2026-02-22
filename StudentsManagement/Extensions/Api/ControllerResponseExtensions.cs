using Microsoft.AspNetCore.Mvc;
using StudentsManagement.Responses;

namespace StudentsManagement.Extensions.Api
{
    public static class ControllerResponseExtensions
    {
        public static IActionResult OkResponse<T>(this ControllerBase controller, string message, T data)
        {
            return controller.Ok(new SuccessResponse<T>
            {
                Message = message,
                Data = data
            });
        }

        public static IActionResult CreatedResponse<T>(
            this ControllerBase controller,
            string actionName,
            object routeValues,
            string message,
            T data)
        {
            return controller.CreatedAtAction(actionName, routeValues, new SuccessResponse<T>
            {
                Message = message,
                Data = data
            });
        }

        public static IActionResult NotFoundResponse(this ControllerBase controller, string message)
        {
            return controller.NotFound(new ErrorResponse
            {
                Message = message
            });
        }

        public static IActionResult BadRequestResponse(this ControllerBase controller, string message)
        {
            return controller.BadRequest(new ErrorResponse
            {
                Message = message
            });
        }
    }
}