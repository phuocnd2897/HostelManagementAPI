using HM.Data.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace HM.Service.Service
{
    public interface IDashboardService
    {
        IEnumerable<int> CustomerByMonth();
    }
    public class DashboardService : IDashboardService
    {
        private ICustomerRepository _customerRepository;
        public DashboardService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public IEnumerable<int> CustomerByMonth()
        {
            int Jan = 0; 
            int Feb = 0; 
            int Mar = 0; 
            int Apr = 0; 
            int May = 0; 
            int June = 0; 
            int July = 0; 
            int Aug = 0; 
            int Sep = 0; 
            int Oct = 0; 
            int Nov = 0; 
            int Dec = 0;
            var customers = this._customerRepository.GetAll();
            foreach (var item in customers)
            {
                if (item.DateIn.Month == 1)
                {
                    Jan++;
                }
                else if (item.DateIn.Month == 2)
                {
                    Feb++;
                }
                else if (item.DateIn.Month == 3)
                {
                    Mar++;
                }
                else if (item.DateIn.Month == 4)
                {
                    Apr++;
                }
                else if (item.DateIn.Month == 5)
                {
                    May++;
                }
                else if (item.DateIn.Month == 6)
                {
                    June++;
                }
                else if (item.DateIn.Month == 7)
                {
                    July++;
                }
                else if (item.DateIn.Month == 8)
                {
                    Aug++;
                }
                else if (item.DateIn.Month == 9)
                {
                    Sep++;
                }
                else if (item.DateIn.Month == 10)
                {
                    Oct++;
                }
                else if (item.DateIn.Month == 11)
                {
                    Nov++;
                }
                else if (item.DateIn.Month == 12)
                {
                    Dec++;
                }
            }
            int[] result = new int[12] { Jan, Feb, Mar, Apr, May, June, July, Aug, Sep, Oct, Nov, Dec };
            return result;
        }
    }
}
