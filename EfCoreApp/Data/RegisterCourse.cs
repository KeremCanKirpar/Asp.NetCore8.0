namespace EfCoreApp.Data
{
    public class RegisterCourse
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; } = null!;
        public int CourseId { get; set; }
        public Course Course { get; set; } = null!;


        public DateTime RegisterDate { get; set; }
    }
}