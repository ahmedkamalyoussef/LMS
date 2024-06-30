namespace LMS.Application.Interfaces
{
    public interface ITeacherService
    {
        public Task<bool> EditTeacherImage(string imagePath);
        public Task<int> GetTeachersCount();

    }
}
