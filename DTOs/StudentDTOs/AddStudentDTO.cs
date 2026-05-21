namespace Day2.DTOs.StudentDTOs
{
    public class AddStudentDTO
    {
        public string St_Fname { get; set; }

        public string St_Lname { get; set; }

        public string St_Address { get; set; }

        public int? St_Age { get; set; }

        public int? Dept_Id { get; set; }

        public int? St_super { get; set; }
    }
}
