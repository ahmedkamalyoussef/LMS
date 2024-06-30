namespace LMS.Application.Interfaces
{
    public interface ITeacherService
    {
        public Task<bool> EditTeacherImage(string imagePath);
        public Task<bool> DeleteTeacherPictureAsync();
        public Task<int> GetTeachersCount();

    }
}
