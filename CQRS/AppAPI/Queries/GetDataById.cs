using CQRS.Data;
using CQRS.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS.AppAPI.Queries
{
    public class GetDataById
    {
        public class Query : IRequest<Employee>
        {
            public int Id { get; set; }
        }
        public class QueryHandler : IRequestHandler<Query, Employee>
        {
            private readonly EmployeeContext _db;

            public QueryHandler(EmployeeContext db) => _db = db;
                
            public async Task<Employee> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _db.Employees.FindAsync(request.Id);
            }
        }
    }
}
