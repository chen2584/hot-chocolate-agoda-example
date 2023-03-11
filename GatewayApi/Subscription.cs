using GatewayApi;
using HotChocolate;

[ExtendObjectType("Subscription")]
public class Subscription
{
    [Subscribe]
    [Topic("AddTag")]
    public Tag TagAdded([EventMessage] Tag tag) => tag;
}