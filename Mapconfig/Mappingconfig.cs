using AutoMapper;
using Day2.DTOs.DepartmentDTOs;
using Day2.DTOs.StudentDTOs;
using Day2.Models;

namespace Day2.Mapconfig
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            // Student Mapping
            CreateMap<Student, DisplayStudentDTO>().AfterMap(
                (src, dest) =>
                {
                    dest.departmentName = src.Dept?.Dept_Name;
                    dest.supervisorName = src.St_superNavigation?.St_Fname + " " + src.St_superNavigation?.St_Lname;
                });


            CreateMap<AddStudentDTO, Student>();
            CreateMap<EditStudentDTO, Student>();

            // Department Mapping
            CreateMap<Department, DisplayDepartmentDTO>().AfterMap(
                (src, dest) =>
                {
                    dest.Dept_ManagerName = src.Dept_ManagerNavigation?.Ins_Name;
                    dest.NumerOfStudents = src.Students?.Count ?? 0;
                });

            CreateMap<AddDepartmentDTO, Department>();
            CreateMap<EditDepartmentDTO, Department>();
        }
    }
}
