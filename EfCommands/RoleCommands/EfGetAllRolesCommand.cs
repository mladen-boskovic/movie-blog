using Application.Commands.RoleCommands;
using Application.DTO;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCommands.RoleCommands
{
    public class EfGetAllRolesCommand : BaseEfCommand, IGetAllRolesCommand
    {
        public EfGetAllRolesCommand(MovieBlogContext context) : base(context)
        {
        }

        public IEnumerable<RoleDto> Execute()
        {
            return Context.Roles.Where(r => r.IsDeleted == false).Select(r => new RoleDto
            {
                Id = r.Id,
                Name = r.Name
            });
        }
    }
}
