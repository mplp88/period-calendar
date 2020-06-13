using Calendario.Helpers;
using Calendario.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Calendario.Models
{
    public class Periodo
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Este campo es requerido")]
        public DateTime Inicio { get; set; }
        public DateTime? Fin { get; set; }
        public DateTime? ProximaFechaPosible { get; set; }
        public int? Duracion { get; set; }

        public Periodo()
        {

        }

        public static Periodo FromViewModel(PeriodoViewModel vm)
        {
            var periodo = new Periodo();
            ViewModelHelper.MapModel(vm, periodo);
            return periodo;
        }
    }
}
