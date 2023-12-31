﻿using KonnAPI.Data;
using KonnAPI.Interfaces;
using KonnAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace KonnAPI.Repositories;

public class CategoryRepository : ICategoryRepository {
    private readonly DataContext _context;

    public CategoryRepository(DataContext context) {
        _context = context;
    }

    public async Task<IEnumerable<Category>> GetAllCategories() {
        return await _context.Categories.OrderByDescending(c => c.CreatedAt).ToListAsync();
    }

    public async Task<IEnumerable<Category>> GetWorkspaceCategories(int id) {
        return await _context.Categories.Where(c => c.WorkspaceId == id).OrderByDescending(c => c.CreatedAt).ToListAsync();
    }
}
