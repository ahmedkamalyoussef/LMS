namespace LMS.Application.Interfaces
{
    public interface IStudentService
    {
        public Task<int> GetStudentsCount();
        public Task<int> EnrolledStudentsCount();
    }
}
