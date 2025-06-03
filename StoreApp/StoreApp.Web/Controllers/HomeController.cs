using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreApp.Data.Abstract;
using StoreApp.Web.Models;

namespace StoreApp.Web.Controllers
{
    public class HomeController : Controller
    {
        public int pageSize = 3;
        private readonly IStoreRepository _storeRepository;
        private readonly IMapper _mapper;

        public HomeController(IStoreRepository storeRepository, IMapper mapper)
        {
            _storeRepository = storeRepository;
            _mapper = mapper;
        }

        public ActionResult Index(string category, int page = 1)
        {
            try
            {
                return View(new ProductListViewModel
                {
                    Products = _storeRepository.GetProductsByCategory(category, page, pageSize).Select(p => _mapper.Map<ProductViewModel>(p)),
                    PageInfos = new PageInfo
                    {
                        ItemsPerPage = pageSize,
                        CurrentPage = page,
                        TotalItems = _storeRepository.GetProductCount(category)
                    }
                });

            }
            catch (Exception ex)
            {

                return View(ex.Message, StatusCodes.Status404NotFound);
            }
        }

    }
}
