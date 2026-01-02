using System;
using System.Collections.Generic;
using System.Linq;
using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.DAL
{
    public class StationaryRepository : IDisposable
    {
        private InventoryDbContext _context;

        public StationaryRepository()
        {
            _context = new InventoryDbContext();
        }

        public StationaryRepository(InventoryDbContext context)
        {
            _context = context;
        }

        // Get all stationaries
        public List<Stationary> GetAllStationaries()
        {
            return _context.Stationaries
                .OrderBy(s => s.LocationCode)
                .ToList();
        }

        // Get active stationaries
        public List<Stationary> GetActiveStationaries()
        {
            return _context.Stationaries
                .Where(s => s.IsActive)
                .OrderBy(s => s.LocationCode)
                .ToList();
        }

        // Get stationary by code (primary key)
        public Stationary GetStationaryByCode(string locationCode)
        {
            return _context.Stationaries.Find(locationCode);
        }

        // Create stationary
        public bool CreateStationary(Stationary stationary)
        {
            try
            {
                _context.Stationaries.Add(stationary);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Update stationary
        public bool UpdateStationary(Stationary stationary)
        {
            try
            {
                var existingStationary = _context.Stationaries.Find(stationary.LocationCode);
                if (existingStationary == null)
                {
                    return false;
                }

                // Update only the properties that can be changed
                existingStationary.Description = stationary.Description;
                existingStationary.IsActive = stationary.IsActive;

                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Delete stationary (soft delete)
        public bool DeleteStationary(string locationCode)
        {
            try
            {
                var stationary = _context.Stationaries.Find(locationCode);
                if (stationary != null)
                {
                    stationary.IsActive = false;
                    _context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        // Activate stationary
        public bool ActivateStationary(string locationCode)
        {
            try
            {
                var stationary = _context.Stationaries.Find(locationCode);
                if (stationary != null)
                {
                    stationary.IsActive = true;
                    _context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        // Check if location code exists
        public bool LocationCodeExists(string locationCode)
        {
            return _context.Stationaries.Any(s => s.LocationCode == locationCode);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
