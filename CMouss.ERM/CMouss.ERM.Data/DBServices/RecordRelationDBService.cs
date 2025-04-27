using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMouss.ERM.Data.DBModels;
using Microsoft.EntityFrameworkCore;

namespace CMouss.ERM.Data.DBServices
{
    public class RecordRelationDBService
    {
        private readonly ERMDBContext _context;

        public RecordRelationDBService(ERMDBContext context)
        {
            _context = context;
        }

        public async Task<List<RecordRelation>> GetAllAsync()
        {
            var recordRelations = await _context.RecordRelations
                .ToListAsync();
            return recordRelations;
        }

        public async Task<RecordRelation> GetByIdAsync(int id)
        {
            RecordRelation response = new();
            var recordRelation = await _context.RecordRelations
                .FirstOrDefaultAsync(x => x.Id == id);
            if (recordRelation != null)
            {
                response = recordRelation;
            }
            return response;
        }

        public async Task<List<RecordRelation>> GetByEntityRelationIdAsync(int entityRelationId)
        {
            var recordRelations = await _context.RecordRelations
                .Where(x => x.EntityRelationId == entityRelationId)
                .ToListAsync();
            return recordRelations;
        }

        public async Task<List<RecordRelation>> GetByLeftRecordIdAsync(int LeftRecordId)
        {
            var recordRelations = await _context.RecordRelations
                .Where(x => x.LeftRecordId == LeftRecordId)
                .ToListAsync();
            return recordRelations;
        }

        public async Task<List<RecordRelation>> GetByRightRecordIdAsync(int RightRecordId)
        {
            var recordRelations = await _context.RecordRelations
                .Where(x => x.RightRecordId == RightRecordId)
                .ToListAsync();
            return recordRelations;
        }

        public async Task<RecordRelation> GetByRelationAndRecordsAsync(int entityRelationId, int LeftRecordId, int RightRecordId)
        {
            RecordRelation response = new();
            var recordRelation = await _context.RecordRelations
                .FirstOrDefaultAsync(x =>
                    x.EntityRelationId == entityRelationId &&
                    x.LeftRecordId == LeftRecordId &&
                    x.RightRecordId == RightRecordId);
            if (recordRelation != null)
            {
                response = recordRelation;
            }
            return response;
        }

        public async Task<RecordRelation> AddAsync(int entityRelationId, int LeftRecordId, int RightRecordId, string userId)
        {
            // Check if entity relation exists
            var entityRelation = await _context.EntityRelations
                .FirstOrDefaultAsync(x => x.Id == entityRelationId);
            if (entityRelation == null)
            {
                throw new Exception("Entity Relation not found");
            }

            // Check if source record exists
            var sourceRecord = await _context.Records
                .FirstOrDefaultAsync(x => x.Id == LeftRecordId);
            if (sourceRecord == null)
            {
                throw new Exception("Source Record not found");
            }

            // Check if destination record exists
            var destinationRecord = await _context.Records
                .FirstOrDefaultAsync(x => x.Id == RightRecordId);
            if (destinationRecord == null)
            {
                throw new Exception("Destination Record not found");
            }

            // Check if relation already exists
            var existingRelation = await _context.RecordRelations
                .FirstOrDefaultAsync(x =>
                    x.EntityRelationId == entityRelationId &&
                    x.LeftRecordId == LeftRecordId &&
                    x.RightRecordId == RightRecordId);
            if (existingRelation != null)
            {
                throw new Exception("Record Relation already exists");
            }

            RecordRelation newRelation = new()
            {
                EntityRelationId = entityRelationId,
                LeftRecordId = LeftRecordId,
                RightRecordId = RightRecordId,
                CreateUserId = userId,
                CreateDateTime = DateTime.UtcNow
            };

            await _context.RecordRelations.AddAsync(newRelation);
            await _context.SaveChangesAsync();
            return newRelation;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var recordRelation = await _context.RecordRelations
                .FirstOrDefaultAsync(x => x.Id == id);
            if (recordRelation == null)
            {
                throw new Exception("Record Relation not found");
            }
            
            _context.RecordRelations.Remove(recordRelation);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteByRelationAndRecordsAsync(int entityRelationId, int LeftRecordId, int RightRecordId)
        {
            var recordRelation = await _context.RecordRelations
                .FirstOrDefaultAsync(x =>
                    x.EntityRelationId == entityRelationId &&
                    x.LeftRecordId == LeftRecordId &&
                    x.RightRecordId == RightRecordId);
            if (recordRelation == null)
            {
                throw new Exception("Record Relation not found");
            }
            
            _context.RecordRelations.Remove(recordRelation);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteByEntityRelationIdAsync(int entityRelationId)
        {
            var recordRelations = await _context.RecordRelations
                .Where(x => x.EntityRelationId == entityRelationId)
                .ToListAsync();
            
            if (!recordRelations.Any())
            {
                return false;
            }
            
            _context.RecordRelations.RemoveRange(recordRelations);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteByLeftRecordIdAsync(int LeftRecordId)
        {
            var recordRelations = await _context.RecordRelations
                .Where(x => x.LeftRecordId == LeftRecordId)
                .ToListAsync();
            
            if (!recordRelations.Any())
            {
                return false;
            }
            
            _context.RecordRelations.RemoveRange(recordRelations);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteByRightRecordIdAsync(int RightRecordId)
        {
            var recordRelations = await _context.RecordRelations
                .Where(x => x.RightRecordId == RightRecordId)
                .ToListAsync();
            
            if (!recordRelations.Any())
            {
                return false;
            }
            
            _context.RecordRelations.RemoveRange(recordRelations);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteByRecordIdAsync(int recordId)
        {
            var recordRelations = await _context.RecordRelations
                .Where(x => x.LeftRecordId == recordId || x.RightRecordId == recordId)
                .ToListAsync();
            
            if (!recordRelations.Any())
            {
                return false;
            }
            
            _context.RecordRelations.RemoveRange(recordRelations);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
