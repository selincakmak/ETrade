using Business.Abstract;
using Business.Constant;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class OrderManager :IOrderService
    {
        private IOrderDal _orderDal;
        private IOrderLineDal _orderLineDal;
        public OrderManager(IOrderDal orderDal , IOrderLineDal orderLineDal)
        {
            _orderDal = orderDal;
            _orderLineDal = orderLineDal;
        }
             
    
        public IResult Add(Order order)
        {
            _orderDal.Add(order);
            foreach (var item in order.OrderLines)
            {
                item.OrderId = order.OrderId;
                _orderLineDal.Add(item);
            }

          

            return new SuccessResult(Messages.ProductDeleted);
        }

       
    }
}
