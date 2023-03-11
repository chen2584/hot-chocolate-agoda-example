using System.Collections;
using HotChocolate;
using System.Collections;
using GatewayApi;

[ExtendObjectType("Query")]
public class Query
{
    public async Task<ICollection<Tag>> GetTagsAsync(
        [Service] ITagsApiClient service,
        CancellationToken cancellationToken)
    {
        return await service.TagAllAsync(cancellationToken);
    }
}