using EMSFRONTEND.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
/*builder.Services.AddHttpClient<EmpTaskListService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5293");
// Adjust to your API Base URL
});*/

builder.Services.AddHttpClient<LoginSignupService>(); // Register the service
builder.Services.AddHttpClient<TeamService>();
builder.Services.AddHttpClient<PerformanceService>();
builder.Services.AddHttpClient<EmpTaskListService>();
builder.Services.AddHttpClient<LeaveRequestService>();
builder.Services.AddHttpClient<DashboardService>();

// Add session support
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Set the session timeout as needed
    options.Cookie.HttpOnly = true; // Make the session cookie HTTP only
    options.Cookie.IsEssential = true; // Make the session cookie essential
});

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession(); // Add session middleware

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=Login}/{id?}"); // Set Auth controller's Login as default


app.Run();
