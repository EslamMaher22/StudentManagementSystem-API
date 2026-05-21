using AutoMapper;
using Day2.DTOs.DepartmentDTOs;
using Day2.DTOs.StudentDTOs;
using Day2.Models;
using Day2.UnitOfWorks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Day2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly UnitOfWork unit;
        private readonly IMapper mapper;

        public DepartmentController(UnitOfWork unit,IMapper mapper)
        {
            this.unit = unit;
            this.mapper = mapper;
        }

        [HttpGet]
        [EndpointSummary("Get all departments with optional search and pagination")]
        [EndpointDescription("Get all departments with optional search and pagination")]
        public IActionResult GetAll(string? search, int page = 1, int pageSize = 5)
        {

            var query = unit.DeptReps.GetAll(d => d.Dept_ManagerNavigation, d => d.Students).AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(d => d.Dept_Name.Contains(search));
            }

            var total = query.Count();

            var departments = query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var result = mapper.Map<List<DisplayDepartmentDTO>>(departments);
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        [EndpointSummary("Get a department by ID")]
        [EndpointDescription("Get a department by ID")]
        [ProducesResponseType(200, Type = typeof(DisplayDepartmentDTO))]
        [ProducesResponseType(404)]
        public IActionResult GetById(int id)
        {
            var dept = unit.DeptReps.Find(d => d.Dept_Id == id, new string[] { "Dept_ManagerNavigation", "Students" }).FirstOrDefault();
            if (dept == null)
                return NotFound();
            return Ok(mapper.Map<DisplayDepartmentDTO>(dept));
        }

        [HttpGet("{name:alpha}")]
        [EndpointSummary("Get a department by name")]
        [EndpointDescription("Get a department by name")]
        [ProducesResponseType(200, Type = typeof(DisplayDepartmentDTO))]
        [ProducesResponseType(404)]
        public IActionResult GetByFName(string name)
        {
            var dept = unit.DeptReps.Find(d => d.Dept_Name == name, new string[] { "Dept_ManagerNavigation", "Students" }).FirstOrDefault();
            if (dept == null)
                return NotFound();
            return Ok(mapper.Map<DisplayDepartmentDTO>(dept));
        }

        [HttpPost]
        [EndpointSummary("Add a new department")]
        [EndpointDescription("Add a new department")]
        [ProducesResponseType(201, Type = typeof(AddDepartmentDTO))]
        [ProducesResponseType(400)]
        public IActionResult Add(AddDepartmentDTO Deptdto)
        {
            if (Deptdto == null) return BadRequest();
            if (ModelState.IsValid)
            {
                Department dept = mapper.Map<Department>(Deptdto);
                unit.DeptReps.Add(dept);
                unit.Save();
                return CreatedAtAction("getbyid", new { id = dept.Dept_Id }, Deptdto);
            }
            else return BadRequest(ModelState);
        }

        [HttpPut("{id:int}")]
        [EndpointSummary("Update an existing department")]
        [EndpointDescription("Update an existing department")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult Update(int id, EditDepartmentDTO Deptdto)
        {
            if (Deptdto == null) return BadRequest();
            if (id != Deptdto.Dept_Id) return BadRequest("invalid id");
            if (ModelState.IsValid)
            {
                Department dept = mapper.Map<Department>(Deptdto);
                unit.DeptReps.Update(dept);
                unit.Save();
                return NoContent();
            }
            else return BadRequest(ModelState);
        }

        [HttpDelete("{id:int}")]
        [EndpointSummary("Delete a department by ID")]
        [EndpointDescription("Delete a department by ID")]
        [ProducesResponseType(200, Type = typeof(DisplayDepartmentDTO))]
        [ProducesResponseType(404)]
        public IActionResult Delete(int id)
        {
            var dept = unit.DeptReps.Find(d => d.Dept_Id == id, new string[] { "Dept_ManagerNavigation", "Students" }).FirstOrDefault();
            if (dept == null) return NotFound();

            var Deptdto = mapper.Map<DisplayDepartmentDTO>(dept);
            unit.DeptReps.Delete(id);
            unit.Save();
            return Ok(Deptdto);
        }
    }
}
