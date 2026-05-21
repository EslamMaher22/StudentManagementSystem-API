using AutoMapper;
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
    public class StudentsController : ControllerBase
    {
        private readonly UnitOfWork unit;
        private readonly IMapper mapper;

        public StudentsController(UnitOfWork unit, IMapper mapper)
        {
            this.unit = unit;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll(string? search, int page = 1, int pageSize = 5)
        {

            //var query = unit.StudReps.GetAll().Include(s => s.Dept).Include(s => s.St_superNavigation).AsQueryable();
            var query = unit.StudReps.GetAll(s => s.Dept, s => s.St_superNavigation).AsQueryable();
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(s => s.St_Fname.Contains(search));
            }

            var total = query.Count();

            var students = query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var result = mapper.Map<List<DisplayStudentDTO>>(students);
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var student = unit.StudReps.Find(s => s.St_Id == id, new string[] { "Dept", "St_superNavigation" }).FirstOrDefault();
            if (student == null)
                return NotFound();
            return Ok(mapper.Map<DisplayStudentDTO>(student));
        }

        [HttpGet("{Fname:alpha}")]
        public IActionResult GetByFName(string Fname)
        {
            var student = unit.StudReps.Find(s => s.St_Fname == Fname, new string[] { "Dept", "St_superNavigation" }).FirstOrDefault();
            if (student == null)
                return NotFound();
            return Ok(mapper.Map<DisplayStudentDTO>(student));
        }

        [HttpPost]
        public IActionResult Add(AddStudentDTO Sdto)
        {
            if (Sdto == null) return BadRequest();
            if (ModelState.IsValid)
            {
                Student s = mapper.Map<Student>(Sdto);
                unit.StudReps.Add(s);
                unit.Save();
                return CreatedAtAction("getbyid", new { id = s.St_Id }, Sdto);

            }
            else return BadRequest(ModelState);
        }

        [HttpPut("{id:int}")]
        public IActionResult Update(int id, EditStudentDTO Sdto)
        {
            if (Sdto == null) return BadRequest();
            if (id != Sdto.St_Id) return BadRequest("invalid id");
            if (ModelState.IsValid)
            {
                Student s = mapper.Map<Student>(Sdto);
                unit.StudReps.Update(s);
                unit.Save();
                return NoContent();
            }
            else return BadRequest(ModelState);
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var student = unit.StudReps.GetById(id);
            if (student == null) return NotFound();

            var Sdto = mapper.Map<DisplayStudentDTO>(student);
            unit.StudReps.Delete(id);
            unit.Save();
            return Ok(Sdto);
        }
    }
}
