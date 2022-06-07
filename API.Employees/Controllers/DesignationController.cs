using API.Employees.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Employees.Controllers
{
    [Route("api/Designation")]
    [ApiController]
    public class DesignationController : ControllerBase
    {
        private readonly EmployeeContext _context;
        public DesignationController(EmployeeContext context)
        {
            _context = context;
        }
        // GET: api/lDesignation
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblDesignation>>> GetTblDesignation()
        {
            return await _context.TblDesignation.ToListAsync();
        }
    }
}
