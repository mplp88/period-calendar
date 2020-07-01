using Calendario.DAL;
using Calendario.Models;
using Calendario.ViewModels;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Calendario.Services
{
    public class PeriodoService
    {
        private readonly DatabaseContext context;

        public PeriodoService(DatabaseContext _context)
        {
            context = _context;
        }

        public List<PeriodoViewModel> Get()
        {
            try
            {
                var periodos = context.Periodos.OrderBy(p => p.Inicio).ToList();
                return PeriodoViewModel.FromPeriodoList(periodos);
            }
            catch (Exception e)
            {
                return new List<PeriodoViewModel>();
            }
        }

        public PeriodoViewModel Get(int id)
        {
            try
            {
                var p = context.Periodos.First(p => p.Id == id);
                return PeriodoViewModel.FromPeriodo(p);
            }
            catch(Exception e)
            {
                return null;
            }
        }

        public PeriodoViewModel Add(Periodo periodo)
        {
            if(periodo.Fin != null)
            {
                if(periodo.Fin <= periodo.Inicio)
                {
                    throw new Exception("La fecha de fin debe ser posterior a la fecha de inicio.");
                }
                periodo.ProximaFechaPosible = ((DateTime)periodo.Fin).AddDays(21);
                periodo.Duracion = Convert.ToInt32(((DateTime)periodo.Fin).Subtract(periodo.Inicio).TotalDays + 1);
            }
            context.Periodos.Add(periodo);
            context.SaveChanges();
            var periodoViewModel = PeriodoViewModel.FromPeriodo(periodo);
            return periodoViewModel;
        }

        public PeriodoViewModel Update(int id, Periodo nuevoPeriodo)
        {
            var periodoViewModel = new PeriodoViewModel();
            if (context.Periodos.Any(p => p.Id == id))
            {
                if (nuevoPeriodo.Fin != null)
                {
                    if (nuevoPeriodo.Fin <= nuevoPeriodo.Inicio)
                    {
                        throw new Exception("La fecha de fin debe ser posterior a la fecha de inicio.");
                    }
                    nuevoPeriodo.ProximaFechaPosible = ((DateTime)nuevoPeriodo.Fin).AddDays(21);
                    nuevoPeriodo.Duracion = Convert.ToInt32(((DateTime)nuevoPeriodo.Fin).Subtract(nuevoPeriodo.Inicio).TotalDays + 1);
                }
                context.Periodos.Update(nuevoPeriodo);
                context.SaveChanges();
                periodoViewModel = PeriodoViewModel.FromPeriodo(nuevoPeriodo);
            }

            return periodoViewModel;
        }

        public bool Delete(int id)
        {
            try
            {
                if (context.Periodos.Any(p => p.Id == id))
                {
                    var p = context.Periodos.FirstOrDefault(p => p.Id == id);
                    context.Periodos.Remove(p);
                    context.SaveChanges();
                    return true;
                }

                return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
