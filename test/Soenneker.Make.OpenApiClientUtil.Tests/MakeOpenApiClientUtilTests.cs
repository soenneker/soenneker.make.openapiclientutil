using Soenneker.Make.OpenApiClientUtil.Abstract;
using Soenneker.Tests.HostedUnit;

namespace Soenneker.Make.OpenApiClientUtil.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public sealed class MakeOpenApiClientUtilTests : HostedUnitTest
{
    private readonly IMakeOpenApiClientUtil _openapiclientutil;

    public MakeOpenApiClientUtilTests(Host host) : base(host)
    {
        _openapiclientutil = Resolve<IMakeOpenApiClientUtil>(true);
    }

    [Test]
    public void Default()
    {

    }
}
