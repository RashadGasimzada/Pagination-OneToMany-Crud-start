using FrontToBackEnd.Data;
using FrontToBackEnd.Models;
using FrontToBackEnd.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontToBackEnd.Controllers
{
   
    public class CardController : Controller
    {
        private readonly AppDbContext _context;
        public CardController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            
            ViewBag.cardCount= _context.Cards.Count();
            ViewBag.totalPrice = _context.Cards.Count();

            List<BasketVM> basket = JsonConvert.DeserializeObject<List<BasketVM>>(Request.Cookies["basket"]);
            List<BasketDetailVM> basketDetailItems = new List<BasketDetailVM>();

            foreach (BasketVM item in basket)
            {
                Card card = await _context.Cards.FirstOrDefaultAsync(m => m.Id == item.Id);

                BasketDetailVM basketDetail = new BasketDetailVM
                {
                    Id = item.Id,
                    Title = card.Title,
                    Image = card.Image,
                    Count = item.Count,
                    NewPrice = card.NewPrice * item.Count
                };

                basketDetailItems.Add(basketDetail);

            }
            return View(basketDetailItems);
            
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> AddBasket(int? Id)
        {
            if (Id is null) return NotFound();

            Card dbCard = await GetCardById(Id);
            
            if (dbCard is null) return BadRequest();

            List<BasketVM> basket = GetBasket();
            UpdateBasket(basket, dbCard);

           

            return RedirectToAction("Index","Home");
        }
        private async Task<Card> GetCardById(int? Id)
        {
            return await _context.Cards.FindAsync(Id);
        }

        private void UpdateBasket(List<BasketVM> basket, Card cards)
        {
            var existCard = basket.Find(m => m.Id == cards.Id);
            if (existCard == null)
            {
                basket.Add(new BasketVM
                {
                    Id = cards.Id,
                    Count = 1
                });
            }
            else
            {
                existCard.Count++;
            }



            Response.Cookies.Append("basket", JsonConvert.SerializeObject(basket));
        }

        private List<BasketVM> GetBasket()
        {
            List<BasketVM> basket;

            if (Request.Cookies["basket"] != null)
            {
                basket = JsonConvert.DeserializeObject<List<BasketVM>>(Request.Cookies["basket"]);
            }
            else
            {
                basket = new List<BasketVM>();
            }

            return basket;
        }

       
        //public IActionResult Test()
        //{
            
        //    var cookie = Request.Cookies["surname"];
            
        //    return Json( "User surname:" + cookie);
        //}
    }
}
