using FrontToBackEnd.Data;
using FrontToBackEnd.Models;
using FrontToBackEnd.Utilities.Paginations;
using FrontToBackEnd.ViewModels.Admin.CardViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontToBackEnd.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;
        
        public ProductController(AppDbContext context)
        {
            _context = context;
        }
       
        public async Task<IActionResult> Index(int page = 1, int take = 10)
        {
            List<Card> cards = await _context.Cards
                .Skip((page - 1) * take)
                .Take(take)
                .OrderBy(m => m.Id)
                .ToListAsync();
                

            var cardsVM = GetMapDatas(cards);

            int count = await GetPageCount(take);

            Paginate<CardVM> result = new Paginate<CardVM>(cardsVM, page, count);

            return View(result);
        }


        private async Task<int> GetPageCount(int take)
        {
            var count = await _context.Cards.CountAsync();

            return (int)Math.Ceiling((decimal)count / take);
        }

        private List<CardVM> GetMapDatas(List<Card> cards)
        {
            List<CardVM> cardList = new List<CardVM>();

            foreach (var card in cards)
            {
                CardVM newCard = new CardVM
                {
                    Id = card.Id,
                    Title = card.Title,
                    Image = card.Image,
                    OldPrice = card.OldPrice,
                    NewPrice=card.NewPrice,
                    
                    
                };

                cardList.Add(newCard);
            }

            return cardList;
        }

    }
}
