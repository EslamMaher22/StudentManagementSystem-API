namespace Day2.DTOs.DepartmentDTOs
{
    public class AddDepartmentDTO
    {
        public int Dept_Id { get; set; }

        public string Dept_Name { get; set; }

        public string Dept_Desc { get; set; }

        public string Dept_Location { get; set; }

        public int? Dept_Manager { get; set; }
    }
}
