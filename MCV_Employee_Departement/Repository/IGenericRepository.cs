using MCV_Employee_Department.API.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MCV_Employee_Department.API.Repository
{
        public interface IDataRepository<TEntity>
        {
            Task<IEnumerable<TEntity>> GetAll(GetParam param);
            Task<IEnumerable<TEntity>> GetAll();
            Task<GetEmployeeResponse> GetAllEmpToUse(GetParam param); /*for employee*/
            Task<GetDepartmentResponse> GetAllDepToUse(GetParam param); /*for department*/
             TEntity GetById(long id);
            void Add(TEntity entity);
            void Update(TEntity dbEntity, TEntity entity);
            bool Delete(long id);
        }
}
