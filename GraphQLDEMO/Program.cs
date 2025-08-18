using GraphQLDEMO.Dataloaders;
using GraphQLDEMO.Schema.Courses.Mutation;
using GraphQLDEMO.Schema.Courses.Query;
using GraphQLDEMO.Schema.Courses.Subscription;
using GraphQLDEMO.services;
using GraphQLDEMO.services.Courses;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>()
    .AddSubscriptionType<Subscription>()
    .AddInMemorySubscriptions()
    .AddFiltering()
    .AddSorting()
    .AddProjections();

builder.Services.AddPooledDbContextFactory<CourseDbContext>(o => o.UseSqlite(builder.Configuration.GetConnectionString("Default")).LogTo(Console.WriteLine) );
builder.Services.AddScoped<CoursesRepository>();
builder.Services.AddScoped<InstructorsRepository>();
builder.Services.AddScoped<InstructorDataloader>();
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    IDbContextFactory<CourseDbContext> dbcontextfactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<CourseDbContext>>();
    using (var dbcontext = dbcontextfactory.CreateDbContext())
    {
        dbcontext.Database.Migrate();
    }
}

app.MapGraphQL();
app.UseWebSockets();
app.Run();
