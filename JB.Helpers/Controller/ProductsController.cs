using System.Collections.Generic;
using System.Web.Http;
using Umbraco.Web.WebApi;
//using Merchello.Web;
//using Merchello.Web.Models.ContentEditing;

namespace JB.Helpers
{
    public class ProductsController : UmbracoApiController
    {
        [AcceptVerbs("GET")]
        public IEnumerable<Product> Search(string maxSearchItems, string q)
        {
           // var merchello = new MerchelloHelper();

            // Return products in a format to support the view.
            var productList = new List<Product>();
            /*
            // Merchello search - limit search to that configured on the front end.
            var allProducts = merchello.Query.Product.Search(q, 1, Convert.ToInt32(maxSearchItems)).Items.ToList();
           
            // Convert each product for display on the front end.
            foreach (ProductDisplay product in allProducts)
            {
                //TODO: Find correct alternative to this obsolete method.
                var productContent = product.AsProductContent();

                // Access the first image for the product.
                var imageCollectionProperty = productContent.GetProperty("image");
                var imageUrl = string.Empty;

                if (imageCollectionProperty.HasValue){
                    var imageCollection = imageCollectionProperty.Value as List<IPublishedContent>;
                    // TODO: Default image if no image configured?
                    if (imageCollection?.Count > 0)
                    {
                        imageUrl = imageCollection[0].Url;
                    }
                }

                // Product name, image and link to it.
                productList.Add(new Product()
                {
                    Key = product.Key.ToString(),
                    Name = product.Name,
                    Url = productContent.Url,
                    Image = imageUrl
                });
            }
            */
            // Front end requests Json.
            return productList.ToArray();
        }
    }
}