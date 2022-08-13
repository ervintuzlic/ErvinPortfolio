using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErvinPortfolio.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IProjectRepository Project { get; }

        void Save();
    }
}
