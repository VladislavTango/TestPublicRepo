﻿using Lab5TestTask.Data;
using Lab5TestTask.Enums;
using Lab5TestTask.Models;
using Lab5TestTask.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Lab5TestTask.Services.Implementations;

/// <summary>
/// UserService implementation.
/// Implement methods here.
/// </summary>
public class UserService : IUserService
{
    private readonly ApplicationDbContext _dbContext;

    public UserService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<User> GetUserAsync()
    {
        return await _dbContext.Users
            .OrderBy(x => x.Sessions.Count)
            .LastAsync();
    }

    public async Task<List<User>> GetUsersAsync()
    {
        return await _dbContext.Users
         .Where(x => x.Sessions
             .Any(s => s.DeviceType == DeviceType.Mobile)
         )
         .Select(x => new User
         {
             Id = x.Id,
             Email = x.Email,
             Status = x.Status,
             Sessions = null
         })
         .ToListAsync();
    }
}
