using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace _20GRPED.MVC2.Domain.Model.Entities 
{ 
    //[Table("livro_entity")]
    public class ProfessorEntity
    {
        //[Column("LivroPk")]
        public int Id { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "{0} deve ter entre {2} e {1} caracteres.", MinimumLength = 3)]
        public string Nome { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "{0} deve ter entre {2} e {1} caracteres.", MinimumLength = 10)]
        public string Sobrenome { get; set; }

        [DataType(DataType.Date)]
        public DateTime Nascimento { get; set; }

        public int EscolaEntityId { get; set; }
        public EscolaEntity Escola { get; set; }
    }
}
