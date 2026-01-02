using System;
using System.Collections.Generic;
using System.Linq;
using InventoryManagementSystem.Helpers;
using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.BL
{
    public class StationaryService
    {
        public StationaryService()
        {
        }

        // Get all stationaries
        public List<Stationary> GetAllStationaries()
        {
            var dtos = ApiClient.Instance.Get<List<StationaryDto>>("stationaries");
            if (dtos == null) return new List<Stationary>();
            return dtos.Select(MapToEntity).ToList();
        }

        // Get active stationaries
        public List<Stationary> GetActiveStationaries()
        {
            var all = GetAllStationaries();
            return all.Where(s => s.IsActive).ToList();
        }

        // Get stationary by code
        public Stationary GetStationaryByCode(string locationCode)
        {
            if (string.IsNullOrWhiteSpace(locationCode)) return null;
            var dto = ApiClient.Instance.Get<StationaryDto>($"stationaries/{locationCode}");
            return dto != null ? MapToEntity(dto) : null;
        }

        // Create stationary
        public bool CreateStationary(string locationCode, string description)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(locationCode)) return false;

                var dto = new StationaryDto
                {
                    LocationCode = locationCode.Trim().ToUpper(),
                    Description = description?.Trim(),
                    IsActive = true,
                    CreatedDate = DateTime.Now
                };

                return ApiClient.Instance.Post("stationaries", dto);
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
                // To preserve IsActive status, ideally we get first or just send what we know.
                // The backend API update replaces fields.
                // Strategy: Get existing to check existence and preserve other fields if needed, 
                // but here input is just code and description.
                // However, the backend Update expects a full object or at least handling IsActive.
                // Let's fetch first to be safe and preserve IsActive.
                var existing = GetStationaryByCode(locationCode);
                if (existing == null) return false;

                var dto = new StationaryDto
                {
                    LocationCode = locationCode,
                    Description = description?.Trim(),
                    IsActive = existing.IsActive, // Preserve status
                    CreatedDate = existing.CreatedDate
                };

                return ApiClient.Instance.Put($"stationaries/{locationCode}", dto);
            }
            catch
            {
                return false;
            }
        }

        // Delete stationary (soft delete)
        public bool DeleteStationary(string locationCode)
        {
            // API Delete is Soft Delete implementation in backend: IsActive = false
            return ApiClient.Instance.Delete($"stationaries/{locationCode}");
        }

        // Activate stationary
        public bool ActivateStationary(string locationCode)
        {
            // API doesn't have dedicated Activate, use Update or manual Get-Set-Put
            var existing = GetStationaryByCode(locationCode);
            if (existing == null) return false;

            var dto = new StationaryDto
            {
                LocationCode = existing.LocationCode,
                Description = existing.Description,
                IsActive = true, // Activate
                CreatedDate = existing.CreatedDate
            };

            return ApiClient.Instance.Put($"stationaries/{locationCode}", dto);
        }

        // Check if location code exists
        public bool LocationCodeExists(string locationCode)
        {
            return GetStationaryByCode(locationCode) != null;
        }

        private Stationary MapToEntity(StationaryDto dto)
        {
            return new Stationary
            {
                LocationCode = dto.LocationCode,
                Description = dto.Description,
                IsActive = dto.IsActive,
                CreatedDate = dto.CreatedDate
            };
        }
    }
}
