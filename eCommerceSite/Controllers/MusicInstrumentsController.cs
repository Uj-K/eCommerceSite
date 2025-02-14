﻿using eCommerceSite.Data;
using eCommerceSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Text;

namespace eCommerceSite.Controllers
{
    public class MusicInstrumentsController : Controller
    {
        private readonly MusicInstrumentContext _context;

        public MusicInstrumentsController(MusicInstrumentContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Get all data from the DB
            List<MusicInstrument> musicInstruments = await _context.MusicInstruments.ToListAsync();

            /*List<MusicInstrument> musicInstruments = (from musicInstruments in _context.MusicInstruments
             *(이렇게 쓸수도 있다)                        select musicInstruments).ToListAsync*/

            // show them on the page
            return View(musicInstruments);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(MusicInstrument newMusIns) 
        {
            if (ModelState.IsValid) 
            {
                _context.MusicInstruments.Add(newMusIns); //Prepares insert
                await _context.SaveChangesAsync();        // Executes pending insert

                ViewData["Message"] = $"{newMusIns.Title} was added successfully!";

                return View();
            }

            return View(newMusIns);

        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id) 
        {
            MusicInstrument instToEdit = await _context.MusicInstruments.FindAsync(id);

            if (instToEdit == null) 
            { 
                return NotFound();
            }

            return View(instToEdit);
        }
        /* async는 비동기 코드는 작업을 실행하고 기다리는 동안 다른 작업을 수행할 수 있는 코드
         * 추가할때 async, Task<>, await 세개가 세트 인듯 */

        [HttpPost]
        public async Task<IActionResult> Edit(MusicInstrument instModel)
        {
            if (ModelState.IsValid)
            {
                _context.MusicInstruments.Update(instModel);
                await _context.SaveChangesAsync();
                /*ViewData["Message"] = $"{instModel.Title} was successfully edit!";*/
                return RedirectToAction("Index");

            }
            return View(instModel);
        }
    }
}
