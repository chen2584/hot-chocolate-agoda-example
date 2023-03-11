using GatewayApi;
using HotChocolate;
using StackExchange.Redis;

const string AllowedOrigin = "allowedOrigin";

var builder = WebApplication.CreateBuilder(args);
// rest service
var serviceSection = builder.Configuration.GetSection("Services");
var tagsApiEndpoint = serviceSection.GetValue<string>("TagsApi:endpoint");
builder.Services.AddHttpClient<ITagsApiClient, TagsApiClient>(client => client.BaseAddress = new Uri(tagsApiEndpoint));

(string endpoint, string password) redisConfiguration =
(
  serviceSection.GetValue<string>("Redis:endpoint"),
  serviceSection.GetValue<string>("Redis:password")
);
var likesapiEndpoint = serviceSection.GetValue<string>("LikesApi:endpoint");
builder.Services.AddHttpClient("LikesGqlClient", client
    => client.BaseAddress = new Uri(likesapiEndpoint));
builder.Services
    .AddGraphQLServer()
    .AddTypeExtension<Query>()
    .AddTypeExtension<Mutation>()
    .AddTypeExtension<Subscription>()
    .AddRedisSubscriptions(_ =>
        ConnectionMultiplexer.Connect(new ConfigurationOptions
        {
            EndPoints = { redisConfiguration.endpoint },
            Password = redisConfiguration.password
        }))
    .AddRemoteSchema("LikesGqlClient", ignoreRootTypes: false);