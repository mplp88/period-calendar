using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Calendario.Models;
using Calendario.Services;
using Calendario.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Calendario.Controllers
{
    public class PeriodoController : Controller
    {
        PeriodoService service;

        public PeriodoController(PeriodoService _service)
        {
            service = _service;
        }
        // GET: Periodo
        public ActionResult Index()
        {
            return View(service.Get());
        }

        // GET: Periodo/Details/5
        public ActionResult Details(int id)
        {
            return View(service.Get(id));
        }

        // GET: Periodo/Create
        public ActionResult Create()
        {
            var periodo = new PeriodoViewModel();
            periodo.Inicio = DateTime.Today;

            return View(periodo);
        }

        // POST: Periodo/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PeriodoViewModel vm)
        {
            try
            {
                var periodo = Periodo.FromViewModel(vm);
                service.Add(periodo);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Periodo/Edit/5
        public ActionResult Edit(int id)
        {
            return View(service.Get(id));
        }

        // POST: Periodo/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, PeriodoViewModel vm)
        {
            var periodo = Periodo.FromViewModel(vm);

            try
            {
                service.Update(id, periodo);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(periodo);
            }
        }

        // GET: Periodo/Delete/5
        public ActionResult Delete(int id)
        {
            return View(service.Get(id));
        }

        // POST: Periodo/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult DeleteDo(int id)
        {
            try
            {
                var ok = service.Delete(id);

                if (ok)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View(service.Get(id));
                }
            }
            catch
            {
                return View(service.Get(id));
            }
        }
    }
}