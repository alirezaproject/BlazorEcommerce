using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace BlazorEcommerce.Client.Shared
{
    public partial class FeaturedProducts
    {
        protected override  void OnInitialized()
        {
            ProductService.ProductChanged += StateHasChanged;
        }

        public void Dispose()
        {
            ProductService.ProductChanged -= StateHasChanged;
        }
    }
}
