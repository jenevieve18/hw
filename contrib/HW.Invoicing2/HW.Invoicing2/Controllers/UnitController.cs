using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HW.Invoicing2.Models;
using HW.Invoicing.Core.Repositories.Sql;

namespace HW.Invoicing2.Controllers
{
    public class UnitController : Controller
    {
        SqlUnitRepository r = new SqlUnitRepository();

        //
        // GET: /Unit/

        public ActionResult Index()
        {
            return View(new UnitModel { Units = r.FindAll() });
        }

        //
        // GET: /Unit/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Unit/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Unit/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        //
        // GET: /Unit/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Unit/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Unit/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Unit/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
