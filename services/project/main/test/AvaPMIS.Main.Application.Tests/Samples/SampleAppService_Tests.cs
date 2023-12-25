using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace AvaPMIS.Main.Samples;

public class SampleMain_Tests : MainApplicationTestBase
{
    private readonly ISampleMain _sampleMain;

    public SampleMain_Tests()
    {
        _sampleMain = GetRequiredService<ISampleMain>();
    }

    [Fact]
    public async Task GetAsync()
    {
        var result = await _sampleMain.GetAsync();
        result.Value.ShouldBe(42);
    }

    [Fact]
    public async Task GetAuthorizedAsync()
    {
        var result = await _sampleMain.GetAuthorizedAsync();
        result.Value.ShouldBe(42);
    }
}
