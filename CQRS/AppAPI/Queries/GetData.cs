using CQRS.Data;
using CQRS.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS.AppAPI.Queries
{
    public class GetData
    {
        public class Query : IRequest<IEnumerable<Employee>> { };

        public class QueryHandler : IRequestHandler<Query, IEnumerable<Employee>>
        {
            private readonly EmployeeContext _db;

            public QueryHandler(EmployeeContext db) => _db = db;

            public async Task<IEnumerable<Employee>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _db.Employees.ToListAsync(cancellationToken);
            }
        }
    }
}
