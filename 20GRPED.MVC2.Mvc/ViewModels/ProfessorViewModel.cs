using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using _20GRPED.MVC2.Domain.Model.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace _20GRPED.MVC2.Mvc.ViewModels
{
    public class ProfessorViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "{0} deve ter entre {2} e {1} caracteres.", MinimumLength = 3)]
        public string Nome { get; set; }

        [DataType(DataType.Date)]
        public DateTime Data { get; set; }

        public int EscolaEntityId { get; set; }
        public EscolaEntity Escola { get; set; }

        public List<SelectListItem> Escolas { get; }

        public ProfessorViewModel() { }

        public ProfessorViewModel(ProfessorEntity professorModel)
        {
            Nome = professorModel.Nome;
            EscolaEntityId = professorModel.EscolaEntityId;
            Escola = professorModel.Escola;
        }

        public ProfessorViewModel(ProfessorEntity professorModel, IEnumerable<EscolaEntity> escolas) : this(professorModel)
        {
            Escolas = ToEscolaSelectListItem(escolas);
        }

        public ProfessorViewModel(IEnumerable<EscolaEntity> escolas)
        {
            Escolas = ToEscolaSelectListItem(escolas);
        }

        private static List<SelectListItem> ToEscolaSelectListItem(IEnumerable<EscolaEntity> autores)
        {
            return autores.Select(x => new SelectListItem
                { Text = $"{x.Nome} {x.Data}", Value = x.Id.ToString() }).ToList();
        }
    }
}
