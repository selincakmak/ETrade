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
    public class OrderLineManager : IOrderLineService
    {

        private IOrderLineDal _orderLineDal;

        public OrderLineManager(IOrderLineDal orderLineDal)
        {
            _orderLineDal = orderLineDal;
        }
        public IResult Add(OrderLine orderLine)
        {
            _orderLineDal.Add(orderLine);
            return new SuccessResult(Messages.ProductDeleted);
        }
    }
}
