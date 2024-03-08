using FluentAssertions;

using GuidRecycler.Interfaces;

namespace GuidRecycler.Tests;

public abstract class BaseGuidRecyclerTests<T>(Func<T> sutFactory)
    where T : class, IGuidRecycler
{
    private readonly Func<T> _sutFactory = sutFactory;

    [Fact]
    public void GetGuid_Returns_FiFo()
    {
        // Given
        var sut = _sutFactory();
        var guid = Guid.NewGuid();
        sut.Recycle(guid);

        // When
        var output = sut.GetGuid();

        // Then
        output.Should().Be(guid);
    }

    [Fact]
    public void Empty_Recycler_Returns_Valid_Guid()
    {
        // Given
        var sut = _sutFactory();

        // When
        var output = sut.GetGuid();

        // Then
        output.Should().NotBeEmpty();
    }
}