using CQRS.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS.AppAPI.Commands
{
    public class UpdateData
    {
        public class Command : IRequest<int>
        {
            public int Id { get; set; }
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
                var emp = _db.Employees.Where(a => a.Id == request.Id).FirstOrDefault();

                if(emp == null)
                {
                    return default;
                }
                else
                {
                    emp.Name = request.Name;
                    emp.Email = request.Email;
                    emp.Phone = request.Phone;

                    await _db.SaveChangesAsync();
                    return emp.Id;
                }
            }
        }
    }
}
