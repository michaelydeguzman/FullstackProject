using FullStackPractice.Persistence.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace FullStackPractice.Persistence.Configurations
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.HasKey(e => new { e.DepartmentId });
            builder.ToTable("Departments");
            builder.HasData(
                new Department() { DepartmentId = 1, DepartmentName = "IT" },
                new Department() { DepartmentId = 2, DepartmentName = "Support" }
           );
        }
    }
}
