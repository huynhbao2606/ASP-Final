using ASP_Final.Dao.IRepository;
using ASP_Final.Helpers;
using ASP_Final.Models;

namespace ASP_Final.Services
{
    public class BasketService : IBasketService
    {
        const string ShoppingCartSessionVariable = "_ShoppingCartSessionVariable";
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _contextAccessor;

        public BasketService(IUnitOfWork unitOfWork, IHttpContextAccessor contextAccessor)
        {
            _unitOfWork = unitOfWork;
            _contextAccessor = contextAccessor;
        }

        public void AddItem(int id, int quantity)
        {
            /*List<BasketItem> shoppingCartList;
            if (_contextAccessor.HttpContext.Session.Get<List<BasketItem>>(ShoppingCartSessionVariable) != default)
            {

                shoppingCartList = _contextAccessor.HttpContext.Session.Get<List<BasketItem>>(ShoppingCartSessionVariable);

                if (shoppingCartList.Where(i => i.Product.Id == id).Any())
                {
                    shoppingCartList.Where(i => i.Product.Id == id).Select(x =>
                    {
                        x.Count += quanlity;
                        return x;
                    }).ToList();

                }
                else
                {
                    shoppingCartList.Add(new BasketItem
                    {

                        Count = quanlity,
                        Product = _unitOfWork.ProductRepository.GetEntityById(id)

                    });
                }

            }
            else
            {
                shoppingCartList = new List<BasketItem>
                {
                    new BasketItem
                    {
                        Count = quanlity,
                        Product = _unitOfWork.ProductRepository.GetEntityById(id)
                    }
                };
            }

            _contextAccessor.HttpContext.Session.Set<List<BasketItem>>(ShoppingCartSessionVariable, shoppingCartList);

        }*/
            List<BasketItem> shoppingCartList = _contextAccessor.HttpContext.Session.Get<List<BasketItem>>(ShoppingCartSessionVariable) ?? new List<BasketItem>(); //Neu Khong Co Thi Tao Moi

            var existingItem = shoppingCartList.FirstOrDefault(i => i.Vaccine.Id == id);

            if (existingItem != null)
            {
                existingItem.Count += quantity;

            }
            else
            {
                shoppingCartList.Add(new BasketItem
                {
                    Count = quantity,
                    Vaccine = _unitOfWork.Vaccine.GetEntityById(id)
                });
            }
            _contextAccessor.HttpContext.Session.Set<List<BasketItem>>(ShoppingCartSessionVariable, shoppingCartList);
        }

        public void RemoveItem(int id)
        {
            if (_contextAccessor.HttpContext.Session.Get<List<BasketItem>>(ShoppingCartSessionVariable) != default)
            {
                List<BasketItem> shoppingCartList = _contextAccessor.HttpContext.Session.Get<List<BasketItem>>(ShoppingCartSessionVariable);

                shoppingCartList.RemoveAll(i => i.Vaccine.Id == id);

                _contextAccessor.HttpContext.Session.Set<List<BasketItem>>(ShoppingCartSessionVariable, shoppingCartList);
            }


        }
        
        public void ClearBasket()
        {
            _contextAccessor.HttpContext.Session.Remove(ShoppingCartSessionVariable);
        }
    }
}
