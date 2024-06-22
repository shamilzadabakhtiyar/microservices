using FreeCourse.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeCourse.Shared.BaseControllers
{
    public class BaseController: ControllerBase
    {
        public IActionResult CreateResponse<T>(Response<T> response)
        {
            return StatusCode(response.StatusCode, response);
        }
    }
}
