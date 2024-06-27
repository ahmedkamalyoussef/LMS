using LMS.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace LMS.Domain.Configuration
{
    public class ExamResultEntityTypeConfiguration : IEntityTypeConfiguration<ExamResult>
    {
        public void Configure(EntityTypeBuilder<ExamResult> builder)
        {
            builder.HasKey(sc => new { sc.StudentId, sc.ExamId });

            builder
                .HasOne(exr => exr.Student)
                .WithMany(s => s.ExamResults)
                .HasForeignKey(exr => exr.StudentId)
                .OnDelete(DeleteBehavior.NoAction);
            builder
                .HasOne(exr => exr.Exam)
                .WithMany(e => e.ExamResults)
                .HasForeignKey(exr => exr.ExamId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

}
