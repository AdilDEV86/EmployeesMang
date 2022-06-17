using API.Employees.Model;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace API.Employees.Controllers
{
 
    [ApiController]
    [Route("api/Employee")]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeContext _context;
        private readonly IWebHostEnvironment _env;

        public EmployeeController(EmployeeContext context , IWebHostEnvironment env )
        {
            _context = context;
            _env = env;
        }

        // GET: api/Employee
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblEmployee>>> GetTblEmployee()
        {
            //return await _context.TblEmployee.ToListAsync();

            var employees = (from e in _context.TblEmployee
                             join d in _context.TblDesignation
                             on e.DesignationID equals d.Id

                             select new TblEmployee
                             {
                                 Id = e.Id,
                                 Name = e.Name,
                                 LastName = e.LastName,
                                 Email = e.Email,
                                 Age = e.Age,
                                 DesignationID = e.DesignationID,
                                 Designation = d.Designation,
                                 Doj = e.Doj,
                                 Gender = e.Gender,
                                 IsActive = e.IsActive,
                                 IsMarried = e.IsMarried
                             }
                            ).ToListAsync();




            return await employees;
        }

        // GET: api/Employee/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TblEmployee>> GetTblEmployee(int id)
        {
            var tblEmployee = await _context.TblEmployee.FindAsync(id);

           

            if (tblEmployee == null)
            {
                return NotFound();
            }
            var designationLabel = _context.TblDesignation.Where(s => s.Id == tblEmployee.DesignationID).FirstOrDefault().Designation;
            tblEmployee.Designation = designationLabel;

            return tblEmployee;
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblEmployee(int id, TblEmployee tblEmployee)
        {
            if (id != tblEmployee.Id)
            {
                return BadRequest();
            }

            _context.Entry(tblEmployee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblEmployeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<TblEmployee>> PostTblEmployee(TblEmployee tblEmployee)
        {
            _context.TblEmployee.Add(tblEmployee);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTblEmployee", new { id = tblEmployee.Id }, tblEmployee);
        }

        // DELETE: api/Employee/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TblEmployee>> DeleteTblEmployee(int id)
        {
            var tblEmployee = await _context.TblEmployee.FindAsync(id);
            if (tblEmployee == null)
            {
                return NotFound();
            }

            _context.TblEmployee.Remove(tblEmployee);
            await _context.SaveChangesAsync();

            return tblEmployee;
        }
        [HttpPost]
        [Route("SaveFile")]

        public JsonResult SaveFile()
        {
            try
            {
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                string FileName = postedFile.FileName;
                var PhysicalPatch = _env.ContentRootPath + "/Photos/" + FileName;
                using (var stream = new FileStream(PhysicalPatch, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }

                return new JsonResult(FileName);
            }
            catch (Exception)
            {

                return new JsonResult("anonymous.jpg");
            }
        }
        private string GenerateFileName(string fileName, string CustomerName)
        {
            try
            {
                string strFileName = string.Empty;
                string[] strName = fileName.Split('.');
                strFileName = CustomerName + DateTime.Now.ToUniversalTime().ToString("yyyy-MM-dd") + "/"
                   + DateTime.Now.ToUniversalTime().ToString("yyyyMMdd\\THHmmssfff") + "." +
                   strName[strName.Length - 1];
                return strFileName;
            }
            catch (Exception ex)
            {
                return fileName;
            }
        }
        [HttpPost]
        [Route("SaveFileAzure")]
        public JsonResult SaveFileAzure()
        {
            try
            {
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                Guid guid = Guid.NewGuid();
                var filename =  guid.ToString() + postedFile.FileName;
                var fileUrl = "";
                BlobContainerClient container = new BlobContainerClient("DefaultEndpointsProtocol=https;AccountName=mystoregejana2020;AccountKey=OfvKSt+QhJmYk+HUuXxxa5FLjcjcoyLIIPYrUudeH4mxfgFKzzfIpkoXBlW4DS17dPjgoI1Eboxs+AStow0CIw==;EndpointSuffix=core.windows.net",
                "wydadcasa2022");
               
                    BlobClient blob = container.GetBlobClient(filename);
                    using (Stream stream = postedFile.OpenReadStream())
                    {
                        blob.Upload(stream);
                    }
                    fileUrl = blob.Uri.AbsoluteUri;
                
            
                var result = fileUrl;
            

                return new JsonResult(result);
            }
            catch (Exception ex)
            {

                return new JsonResult("anonymous.jpg");
            }
        }

        private bool TblEmployeeExists(int id)
        {
            return _context.TblEmployee.Any(e => e.Id == id);
        }


      
    }
}
