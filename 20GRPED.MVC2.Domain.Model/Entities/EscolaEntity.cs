using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace _20GRPED.MVC2.Domain.Model.Entities
{
    public class EscolaEntity
    {
        public int Id { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "{0} deve ter entre {2} e {1} caracteres.", MinimumLength = 3)]
        public string Nome { get; set; }


        [Required]
        [DataType(DataType.Date)]
        public DateTime Data { get; set; }

        public List<ProfessorEntity> Professores { get; set; }
    }
}
