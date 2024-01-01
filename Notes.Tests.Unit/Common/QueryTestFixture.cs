using AutoMapper;
using Notes.Application.Common.Mappings;
using Notes.Application.Interfaces;
using Notes.Persistence;

namespace Notes.Tests.Unit.Common;

public abstract class QueryTestFixture : IDisposable
{
    public NotesDbContext Context;
    public IMapper Mapper;

    public QueryTestFixture() 
    {
        Context = NotesContextFactory.Create();
        var configBuilder = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new AssemblyMappingProfile(
                typeof(INotesDbContext).Assembly));
        });

        Mapper = configBuilder.CreateMapper();
    }
    
    public void Dispose()
    {
        NotesContextFactory.Destroy(Context);
    }
}
