﻿using _20GRPED.MVC2.Domain.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace _20GRPED.MVC2.Data.Context.Configuration
{
    public class ProfessorConfiguration : IEntityTypeConfiguration<ProfessorEntity>
    {
        public void Configure(EntityTypeBuilder<ProfessorEntity> builder)
        {

        }
    }
}
