﻿using Application.DTO;
using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands.RoleCommands
{
    public interface IGetAllRolesCommand : IGetAllCommand<IEnumerable<RoleDto>>
    {
    }
}
