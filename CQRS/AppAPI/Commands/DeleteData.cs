using CQRS.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS.AppAPI.Commands
{
    public class DeleteData
    {
        public class Command : IRequest
        {
            public int Id { get; set; }
        }

        public class CommandHandler : IRequestHandler<Command, Unit>
        {
            private readonly EmployeeContext _db;
            public CommandHandler(EmployeeContext db) => _db = db;

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var emp = await _db.Employees.FindAsync(request.Id);
                if (emp == null) return Unit.Value;

                _db.Employees.Remove(emp);
                await _db.SaveChangesAsync(cancellationToken);

                return Unit.Value; 
            }
        }
    }
}
