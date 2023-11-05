using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using ASP.NET_LR9.Models;
namespace ASP.NET_LR9.Components
{
    public class Table : ViewComponent
    {
        public IViewComponentResult Invoke(List<Product> products)
        {
            string tableContent = "";

            foreach (Product p in products)
            {
                tableContent += $@"
        <tr>
            <td>{p.Id}</td>
            <td>{p.Name}</td>
            <td>{p.Price}$</td>
        </tr>
    ";
            }

            string tableHtml = $@"
    <div class='text-center'>
        <table class='table'>
            <tr>
                <td class='col'>ID</td>
                <td class='col'>Name</td>
                <td class='col'>Price</td>
            </tr>
            {tableContent}
        </table>
    </div>
";

            return new HtmlContentViewComponentResult(new HtmlString(tableHtml));

        }
    }
}