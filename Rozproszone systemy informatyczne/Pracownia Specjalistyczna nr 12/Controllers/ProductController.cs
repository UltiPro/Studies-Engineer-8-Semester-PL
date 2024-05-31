using Microsoft.AspNetCore.Mvc;
using WebApplication1.Classes;

namespace WebApplication1.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private static List<Product> products = new List<Product>
    {
        new Product(0, "Chleb", "Pyszny chlebek", 5),
        new Product(1, "Masło", "Dobre do chlebka", 10),
        new Product(2, "Szynka", "Idealna na chlebek", 20),
        new Product(3, "Ser w plasterkach", "Polecany na chlebku", 10),
        new Product(4, "CocaCola", "Bo czymś trzeba popić", 5)
    };

    [HttpGet("all")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult Get() => Ok(products);

    [HttpPost("specific")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult GetSpecific(FindByProduct findByProduct)
    {
        var productsByPrice = new List<Product>();
        if (findByProduct.Price is not null) productsByPrice = products.Where(p => p.Price <= findByProduct.Price).ToList();
        var productsByDescription = new List<Product>();
        if (findByProduct.Description is not null) productsByDescription = products.Where(p => p.Description.StartsWith(findByProduct.Description, StringComparison.OrdinalIgnoreCase)).ToList();
        var productsByName = new List<Product>();
        if (findByProduct.Name is not null) productsByName = products.Where(p => p.Name.StartsWith(findByProduct.Name, StringComparison.OrdinalIgnoreCase)).ToList();
        return Ok(productsByName.Union(productsByDescription).Union(productsByPrice));
    }
}
