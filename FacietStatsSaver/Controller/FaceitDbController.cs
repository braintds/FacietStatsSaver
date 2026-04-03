using System;
using System.Collections.Generic;
using System.Text;
using FacietStatsSaver.Entity;
using Microsoft.EntityFrameworkCore;

namespace FacietStatsSaver.Controller
{
    public partial class FaceitDbController : DbContext
    {
        private readonly ApplicationDbContext _context;

        public FaceitDbController(ApplicationDbContext context) => _context = context;

        public FaceitDbController() => Database.EnsureCreated();
    }
}
