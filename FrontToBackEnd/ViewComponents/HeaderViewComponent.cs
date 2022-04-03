using FrontToBackEnd.Data;
using FrontToBackEnd.Models;
using FrontToBackEnd.Service;
using FrontToBackEnd.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontToBackEnd.ViewComponents
{
    public class HeaderViewComponent: ViewComponent
    {
        private readonly LayoutService _layoutService;
        public HeaderViewComponent(LayoutService layoutService)
        {
            _layoutService = layoutService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Category> categories =  _layoutService.GetCategories();
            List<CategoryName> categoryNames =  _layoutService.GetCategoryNames();
            List<Fruit> fruits =  _layoutService.GetFruit();
            List<Beverage> beverages =  _layoutService.GetBeverage();
            LayoutVM layoutVM = new LayoutVM
            {
                Categories =categories,
                CategoryNames= categoryNames,
                Fruits = fruits,
                Beverages= beverages
            };
            int cardCount = 0;
            decimal totalPrice=0;
            if (Request.Cookies["basket"] != null)
            {
                List<BasketVM> basket = JsonConvert.DeserializeObject<List<BasketVM>>(Request.Cookies["basket"]);
                cardCount = basket.Sum(m => m.Count);
                List<BasketDetailVM> basketDetail = JsonConvert.DeserializeObject<List<BasketDetailVM>>(Request.Cookies["basket"]);
                totalPrice = basketDetail.Sum(m => m.NewPrice);
            }
            else
            {
                cardCount = 0;
                totalPrice = 0;
            }

            ViewBag.count = cardCount;
            ViewBag.totalPrice = totalPrice;
            return (await Task.FromResult(View(layoutVM)));
        }
    }
}
