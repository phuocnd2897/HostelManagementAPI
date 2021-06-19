using System;
using System.Collections.Generic;
using System.Text;

namespace HM.Context
{
    public interface IUnitOfWork
    {
        HMContext dbContext { get; }
    }
}
