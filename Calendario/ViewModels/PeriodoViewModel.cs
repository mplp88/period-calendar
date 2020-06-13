using Calendario.Helpers;
using Calendario.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Calendario.ViewModels
{
    public class PeriodoViewModel
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Este campo es requerido")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Inicio { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? Fin { get; set; }
        [Display(Name = "Próxima Fecha Posible")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? ProximaFechaPosible { get; set; }
        [Display(Name = "Duración del periodo")]
        public int? Duracion { get; set; }

        public PeriodoViewModel()
        {

        }

        protected PeriodoViewModel(Periodo periodo)
        {
        }

        public static PeriodoViewModel FromPeriodo(Periodo periodo)
        {
            var periodoViewModel = new PeriodoViewModel();
            ViewModelHelper.MapModel(periodo, periodoViewModel);
            return periodoViewModel;
        }

        public static List<PeriodoViewModel> FromPeriodoList(List<Periodo> periodos)
        {
            var periodosViewModel = new List<PeriodoViewModel>();

            foreach(var periodo in periodos)
            {
                periodosViewModel.Add(FromPeriodo(periodo));
            }

            return periodosViewModel;
        }
    }
}
