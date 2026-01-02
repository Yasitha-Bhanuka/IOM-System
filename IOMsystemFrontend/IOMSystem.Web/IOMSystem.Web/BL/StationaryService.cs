using System;
using System.Collections.Generic;
using InventoryManagementSystem.DAL;
using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.BL
{
    public class StationaryService
    {
        private StationaryRepository _stationaryRepository;

        public StationaryService()
        {
            _stationaryRepository = new StationaryRepository();
        }

        // Get all stationaries
        public List<Stationary> GetAllStationaries()
        {
            return _stationaryRepository.GetAllStationaries();
        }

        // Get active stationaries
        public List<Stationary> GetActiveStationaries()
        {
            return _stationaryRepository.GetActiveStationaries();
        }

        // Get stationary by code
        public Stationary GetStationaryByCode(string locationCode)
        {
            return _stationaryRepository.GetStationaryByCode(locationCode);
        }

        // Create stationary
        public bool CreateStationary(string locationCode, string description)
        {
            try
            {
                // Validate location code
                if (string.IsNullOrWhiteSpace(locationCode))
                {
                    return false;
                }

                // Check if code already exists
                if (_stationaryRepository.LocationCodeExists(locationCode))
                {
                    return false;
                }

                var stationary = new Stationary
                {
                    LocationCode = locationCode.Trim().ToUpper(),
                    Description = description?.Trim(),
                    IsActive = true,
                    CreatedDate = DateTime.Now
                };

                return _stationaryRepository.CreateStationary(stationary);
            }
            catch
            {
                return false;
            }
        }

        // Update stationary
        public bool UpdateStationary(string locationCode, string description)
        {
            try
            {
                var stationary = _stationaryRepository.GetStationaryByCode(locationCode);
                if (stationary == null)
                {
                    return false;
                }

                stationary.Description = description?.Trim();

                return _stationaryRepository.UpdateStationary(stationary);
            }
            catch
            {
                return false;
            }
        }

        // Delete stationary (soft delete)
        public bool DeleteStationary(string locationCode)
        {
            return _stationaryRepository.DeleteStationary(locationCode);
        }

        // Activate stationary
        public bool ActivateStationary(string locationCode)
        {
            return _stationaryRepository.ActivateStationary(locationCode);
        }

        // Check if location code exists
        public bool LocationCodeExists(string locationCode)
        {
            return _stationaryRepository.LocationCodeExists(locationCode);
        }
    }
}
