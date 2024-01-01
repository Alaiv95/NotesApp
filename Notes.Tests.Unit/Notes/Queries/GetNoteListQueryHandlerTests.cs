using Notes.Application.Notes.Queries.GetNoteList;
using Notes.Tests.Unit.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Tests.Unit.Notes.Queries;

public class GetNoteListQueryHandlerTests : QueryTestFixture
{
    [Test]
    public async Task GetNoteListQueryHandler_Success()
    {
        // Arrange
        var handler = new GetNoteListQueryHandler(Mapper, Context);

        // Act
        NoteListVm result = await handler.Handle(new GetNoteListQuery
        {
            UserId = NotesContextFactory.UserAId
        }, CancellationToken.None);

        // Assert
        Assert.True(result.GetType() == typeof(NoteListVm));
        Assert.True(result.Notes.Count == 2);
    }
}
