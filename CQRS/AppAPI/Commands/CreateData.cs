using CQRS.Data;
using CQRS.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS.AppAPI.Commands
{
    public class CreateData
    {
        public class Command : IRequest<int>
        {
            public string Name { get; set; }
            public string Email { get; set; }
            public string Phone { get; set; }
        }

        public class CommandHandler : IRequestHandler<Command, int>
        {
            private readonly EmployeeContext _db;

            public CommandHandler(EmployeeContext db) => _db = db;

            public async Task<int> Handle(Command request, CancellationToken cancellationToken)
            {
                var entity = new Employee
                {
                    Name = request.Name,
                    Email = request.Email,
                    Phone = request.Phone
                };

                await _db.Employees.AddAsync(entity, cancellationToken);
                await _db.SaveChangesAsync(cancellationToken);

                return entity.Id;
            }

        }
    }
}
