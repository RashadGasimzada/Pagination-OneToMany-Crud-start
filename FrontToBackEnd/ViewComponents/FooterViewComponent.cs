using FrontToBackEnd.Models;
using FrontToBackEnd.Service;
using FrontToBackEnd.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontToBackEnd.ViewComponents
{
    public class FooterViewComponent: ViewComponent
    {
        private readonly LayoutService _layoutService;
        public FooterViewComponent(LayoutService layoutService)
        {
            _layoutService = layoutService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Category> categories = _layoutService.GetCategories();
            List<CategoryName> categoryNames = _layoutService.GetCategoryNames();
            List<Fruit> fruits = _layoutService.GetFruit();
            List<Beverage> beverages = _layoutService.GetBeverage();
            LayoutVM layoutVM = new LayoutVM
            {
                Categories = categories,
                CategoryNames = categoryNames,
                Fruits = fruits,
                Beverages = beverages
            };
            return (await Task.FromResult(View(layoutVM)));
        }
    }
}
