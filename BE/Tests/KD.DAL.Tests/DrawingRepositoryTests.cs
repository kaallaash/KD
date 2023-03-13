using KD.DAL.Context;
using KD.DAL.Entities;
using KD.DAL.Tests.Entities;
using KD.DAL.Interfaces.Repositories;
using KD.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using Xunit;
using static KD.DAL.Tests.Entities.TestDrawingEntity;

namespace KD.DAL.Tests;

public class DrawingRepositoryTests : IDisposable
{
    private readonly IDrawingRepository<DrawingEntity> _drawingRepository;
    private readonly DatabaseContext _context;

    public DrawingRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<DatabaseContext>()
            .UseInMemoryDatabase(databaseName: "test_drawing_dal_db")
            .Options;

        _context = new DatabaseContext(options);
        _drawingRepository = new DrawingRepository(_context);
    }

    public async void Dispose() => await _context.Database.EnsureDeletedAsync();

    [Theory]
    [MemberData(nameof(GetValidDrawingEntities), MemberType = typeof(TestDrawingEntity))]
    public async Task GetById_ValidId_ReturnsDrawingEntity(DrawingEntity drawingEntity)
    {
        await AddAsync(_context, drawingEntity);

        var createdDrawingEntity = await _context.Drawings
            .FirstOrDefaultAsync(e => e.Title == drawingEntity.Title && e.Price == drawingEntity.Price);

        createdDrawingEntity.ShouldNotBeNull();

        var actualDrawing = await _drawingRepository.GetById(createdDrawingEntity.Id, default);

        actualDrawing.ShouldNotBeNull();
        actualDrawing.Id.ShouldBe(createdDrawingEntity.Id);
    }

    [Theory]
    [MemberData(nameof(GetValidDrawingEntities), MemberType = typeof(TestDrawingEntity))]
    public async Task GetById_InvalidId_ReturnsNull(DrawingEntity drawingEntity)
    {
        await AddAsync(_context, drawingEntity);

        var actualDrawing = await _drawingRepository.GetById(drawingEntity.Id + 1, default);
        actualDrawing.ShouldBeNull();
    }
    
    [Fact]
    public async Task GetAll_ReturnsDrawingEntities()
    {
        await AddAsync(_context, GetValidDrawingEntitiesWithId());
        var drawingsCount = _context.Drawings.Count();

        var actualDrawings = await _drawingRepository.GetAll(default);

        actualDrawings.Count().ShouldBe(drawingsCount);
    }

    [Theory]
    [MemberData(nameof(GetValidDrawingEntities), MemberType = typeof(TestDrawingEntity))]
    public async Task Create_ValidDrawingEntity_EntityIsCreated(
        DrawingEntity expectedValidDrawing)
    {
        var actualDrawing = await _drawingRepository.Create(expectedValidDrawing, default);

        actualDrawing.ShouldNotBeNull();
        actualDrawing.Title.ShouldBe(expectedValidDrawing.Title);
        actualDrawing.Price.ShouldBe(expectedValidDrawing.Price);
    }

    [Theory]
    [MemberData(nameof(GetValidDrawingEntities), MemberType = typeof(TestDrawingEntity))]
    public async Task Update_ValidDrawingEntity_EntityIsUpdated(
        DrawingEntity drawingEntity)
    {
        await AddAsync(_context, drawingEntity);

        var updatedDrawingEntity = drawingEntity;
        updatedDrawingEntity.Title += "+Title";

        var actualDrawing = await _drawingRepository.Update(updatedDrawingEntity, default);

        actualDrawing.ShouldNotBeNull();
        actualDrawing.Title.ShouldBe(updatedDrawingEntity.Title);
    }

    [Theory]
    [MemberData(nameof(GetValidDrawingEntities), MemberType = typeof(TestDrawingEntity))]
    public async Task Update_InvalidDrawingEntity_ThrowsException(
        DrawingEntity drawingEntity)
    {
        await AddAsync(_context, drawingEntity);

        var updatedDrawingEntity = drawingEntity;
        updatedDrawingEntity.Id += 1;

        await Should.ThrowAsync<InvalidOperationException>
            (async () => await _drawingRepository.Update(updatedDrawingEntity, default));
    }

    [Theory]
    [MemberData(nameof(GetValidDrawingEntities), MemberType = typeof(TestDrawingEntity))]
    public async Task Delete_ValidId_EntityIsDeleted(DrawingEntity drawingEntity)
    {
        await AddAsync(_context, drawingEntity);

        await Should.NotThrowAsync(
            async () => await _drawingRepository.Delete(drawingEntity, default));

        var deletedDrawing = await _context.Drawings.FirstOrDefaultAsync(e => e.Id == drawingEntity.Id);
        deletedDrawing.ShouldBeNull();
    }

    [Theory]
    [MemberData(nameof(GetValidDrawingEntities), MemberType = typeof(TestDrawingEntity))]
    public async Task Delete_InvalidId_ThrowsException(DrawingEntity drawingEntity)
    {
        await Should.ThrowAsync<DbUpdateConcurrencyException>(
            async () => await _drawingRepository.Delete(drawingEntity, default));
    }

    private static async Task AddAsync(DatabaseContext context, DrawingEntity drawingEntity)
    {
        await context.Drawings.AddAsync(drawingEntity, default);
        await context.SaveChangesAsync();
    }

    private static async Task AddAsync(DatabaseContext context, IEnumerable<DrawingEntity> drawingEntities)
    {
        await context.Drawings.AddRangeAsync(drawingEntities, default);
        await context.SaveChangesAsync();
    }
}