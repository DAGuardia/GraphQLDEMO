using FirebaseAdmin;
using FirebaseAdminAuthentication.DependencyInjection.Extensions;
using FirebaseAdminAuthentication.DependencyInjection.Models;
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
    .AddTypeExtension<CourseQuery>()
    .AddType<Instructor>()
    .AddType<Course>()    
    .AddMutationType<Mutation>()
    .AddSubscriptionType<Subscription>()
    .AddInMemorySubscriptions()
    .AddFiltering()
    .AddSorting()
    .AddProjections()
    .AddAuthorization();

builder.Services.AddSingleton(FirebaseApp.Create());
builder.Services.AddFirebaseAuthentication();
builder.Services.AddAuthorization(o => o.AddPolicy("IsAdmin", p => p.RequireClaim(FirebaseUserClaimType.EMAIL, "damianguardia@gmail.com")));

builder.Services.AddPooledDbContextFactory<CourseDbContext>(o => o.UseSqlite(builder.Configuration.GetConnectionString("Default")).LogTo(Console.WriteLine) );
builder.Services.AddScoped<CoursesRepository>();
builder.Services.AddScoped<InstructorsRepository>();
builder.Services.AddScoped<InstructorDataloader>();
builder.Services.AddScoped<UserDataloader>();
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    IDbContextFactory<CourseDbContext> dbcontextfactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<CourseDbContext>>();
    using (var dbcontext = dbcontextfactory.CreateDbContext())
    {
        dbcontext.Database.Migrate();
    }
}

app.UseRouting();
app.UseAuthentication();
app.UseWebSockets();
app.MapGraphQL();
app.Run();
