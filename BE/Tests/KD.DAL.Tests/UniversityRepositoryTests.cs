using KD.DAL.Context;
using KD.DAL.Entities;
using KD.DAL.Tests.Entities;
using KD.DAL.Interfaces.Repositories;
using KD.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using Xunit;
using static KD.DAL.Tests.Entities.TestUniversityEntity;

namespace KD.DAL.Tests;

public class UniversityRepositoryTests : IDisposable
{
    private readonly IUniversityRepository<UniversityEntity> _universityRepository;
    private readonly DatabaseContext _context;

    public UniversityRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<DatabaseContext>()
            .UseInMemoryDatabase(databaseName: "test_university_dal_db")
            .Options;

        _context = new DatabaseContext(options);
        _universityRepository = new UniversityRepository(_context);
    }
    public async void Dispose() => await _context.Database.EnsureDeletedAsync();

    [Theory]
    [MemberData(nameof(GetValidUniversityEntities), MemberType = typeof(TestUniversityEntity))]
    public async Task GetById_ValidId_ReturnsUniversityEntity(UniversityEntity university)
    {
        await AddAsync(_context, university);

        var createdUniversityEntity = await _context.Universities
            .FirstOrDefaultAsync(s => s.Name == university.Name);

        createdUniversityEntity.ShouldNotBeNull();

        var actualUniversity = await _universityRepository.GetById(createdUniversityEntity.Id, default);

        actualUniversity.ShouldNotBeNull();
        actualUniversity.Id.ShouldBe(createdUniversityEntity.Id);
    }

    [Theory]
    [MemberData(nameof(GetValidUniversityEntities), MemberType = typeof(TestUniversityEntity))]
    public async Task GetById_InvalidId_ReturnsNull(UniversityEntity university)
    {
        await AddAsync(_context, university);

        var actualUniversity = await _universityRepository.GetById(university.Id + 1, default);
        actualUniversity.ShouldBeNull();
    }

    [Theory]
    [MemberData(nameof(GetValidUniversityEntities), MemberType = typeof(TestUniversityEntity))]
    public async Task GetByName_ValidName_ReturnsUniversityEntity(UniversityEntity university)
    {
        await AddAsync(_context, university);

        var createdUniversityEntity = await _context.Universities
            .FirstOrDefaultAsync(s => s.Name == university.Name);

        createdUniversityEntity.ShouldNotBeNull();
        
        var actualUniversity = await _universityRepository.GetByName(createdUniversityEntity.Name, default);

        actualUniversity.ShouldNotBeNull();
        actualUniversity.Id.ShouldBe(createdUniversityEntity.Id);
    }

    [Fact]
    public async Task GetAll_ReturnsUniversityEntities()
    {
        await AddAsync(_context, GetValidUniversityEntitiesWithId());
        var universitysCount = _context.Universities.Count();

        var actualUniversitys = await _universityRepository.GetAll(default);
        actualUniversitys.Count().ShouldBe(universitysCount);
    }

    [Theory]
    [MemberData(nameof(GetValidUniversityEntities), MemberType = typeof(TestUniversityEntity))]
    public async Task Create_ValidUniversityEntity_EntityIsCreated(
        UniversityEntity expectedValidUniversity)
    {
        var actualUniversity = await _universityRepository.Create(expectedValidUniversity, default);

        actualUniversity.ShouldNotBeNull();
        actualUniversity.Name.ShouldBe(expectedValidUniversity.Name);
    }

    [Theory]
    [MemberData(nameof(GetValidUniversityEntities), MemberType = typeof(TestUniversityEntity))]
    public async Task Update_ValidUniversityEntity_EntityIsUpdated(
        UniversityEntity university)
    {
        await AddAsync(_context, university);

        var updatedUniversityEntity = university;
        updatedUniversityEntity.Name += "name";

        var actualUniversity = await _universityRepository.Update(updatedUniversityEntity, default);

        actualUniversity.ShouldNotBeNull();
        actualUniversity.Name.ShouldBe(updatedUniversityEntity.Name);
    }

    [Theory]
    [MemberData(nameof(GetValidUniversityEntities), MemberType = typeof(TestUniversityEntity))]
    public async Task Update_InvalidUniversityEntity_ThrowsException(
        UniversityEntity university)
    {
        await AddAsync(_context, university);

        var updatedUniversityEntity = university;
        updatedUniversityEntity.Id += 1;

        await Should.ThrowAsync<InvalidOperationException>
            (async () => await _universityRepository.Update(updatedUniversityEntity, default));
    }

    [Theory]
    [MemberData(nameof(GetValidUniversityEntities), MemberType = typeof(TestUniversityEntity))]
    public async Task Delete_ValidId_EntityIsDeleted(UniversityEntity university)
    {
        await AddAsync(_context, university);

        await Should.NotThrowAsync(
            async () => await _universityRepository.Delete(university, default));

        var deletedUniversity = await _context.Universities.FirstOrDefaultAsync(s => s.Id == university.Id);
        deletedUniversity.ShouldBeNull();
    }

    [Theory]
    [MemberData(nameof(GetValidUniversityEntities), MemberType = typeof(TestUniversityEntity))]
    public async Task Delete_InvalidId_ThrowsException(UniversityEntity university)
    {
        await Should.ThrowAsync<DbUpdateConcurrencyException>(
            async () => await _universityRepository.Delete(university, default));
    }

    private static async Task AddAsync(DatabaseContext context, UniversityEntity universityEntity)
    {
        await context.Universities.AddAsync(universityEntity, default);
        await context.SaveChangesAsync();
    }

    private static async Task AddAsync(DatabaseContext context, IEnumerable<UniversityEntity> universityEntities)
    {
        await context.Universities.AddRangeAsync(universityEntities, default);
        await context.SaveChangesAsync();
    }
}