﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HW.Invoicing2.Models;
using HW.Invoicing.Core.Repositories.Sql;

namespace HW.Invoicing2.Controllers
{
    public class ItemController : Controller
    {
        SqlItemRepository r = new SqlItemRepository();
        //
        // GET: /Item/

        public ActionResult Index()
        {
            return View(new ItemModel { Items = r.FindAll() });
        }

        //
        // GET: /Item/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Item/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Item/Create

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
        // GET: /Item/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Item/Edit/5

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
        // GET: /Item/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Item/Delete/5

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