using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Core
{

    public interface IProjectRepository
    {
        IEnumerable<Project> GetAll();

        Project Get(int id);

        Project Add(Project Project);

        void Remove(int id);

        bool Update(Project Project);

    }
}
